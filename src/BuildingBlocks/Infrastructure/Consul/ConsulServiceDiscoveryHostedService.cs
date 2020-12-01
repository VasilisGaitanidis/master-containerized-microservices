using System;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Consul
{
    public class ConsulServiceDiscoveryHostedService : IHostedService
    {
        private readonly IConsulClient _consulClient;
        private readonly ConsulOptions _consulOptions;
        private readonly ILogger<ConsulServiceDiscoveryHostedService> _logger;

        private string _registrationId;

        public ConsulServiceDiscoveryHostedService(IConsulClient consulClient, ConsulOptions consulOptions, ILogger<ConsulServiceDiscoveryHostedService> logger)
        {
            _consulClient = consulClient ?? throw new ArgumentNullException(nameof(consulClient));
            _consulOptions = consulOptions ?? throw new ArgumentNullException(nameof(consulOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _registrationId = $"{_consulOptions.ServiceName}-{Guid.NewGuid()}";

            var registration = new AgentServiceRegistration
            {
                ID = _registrationId,
                Name = _consulOptions.ServiceName,
                Address = _consulOptions.ServiceAddress.Host,
                Port = _consulOptions.ServiceAddress.Port,
                Tags = _consulOptions.Tags
            };

            if (!_consulOptions.DisableAgentCheck)
            {
                var secondsAfterServiceDeregistration = _consulOptions.ServiceDeregistrationSeconds ?? 60;
                var intervalSeconds = _consulOptions.IntervalSeconds ?? 30;

                registration.Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(secondsAfterServiceDeregistration),
                    Interval = TimeSpan.FromSeconds(intervalSeconds),
                    HTTP = new Uri(_consulOptions.ServiceAddress, "healthchecks").OriginalString
                };
            }

            _logger.LogInformation("Registering service with registration Id {RegistrationId} with Consul", _registrationId);

            await _consulClient.Agent.ServiceDeregister(registration.ID, cancellationToken);
            await _consulClient.Agent.ServiceRegister(registration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("De-registering service with registration Id {RegistrationId} from Consul", _registrationId);

            await _consulClient.Agent.ServiceDeregister(_registrationId, cancellationToken);
        }
    }
}