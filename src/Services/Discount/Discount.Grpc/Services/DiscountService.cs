using System;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Application.UseCases.Commands.CreateCoupon;
using Discount.Application.UseCases.Commands.DeleteCoupon;
using Discount.Application.UseCases.Commands.UpdateCoupon;
using Discount.Application.UseCases.Queries.GetCouponByProductName;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Grpc.Services
{
    /// <summary>
    /// The discount gRPC service.
    /// </summary>
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="DiscountService"/>.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="mapper">The mapper.</param>
        public DiscountService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get discount gRPC method.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The server call context.</param>
        /// <returns>A <see cref="Task{CouponModel}"/>.</returns>
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var couponDto = await _mediator.Send(new GetCouponByProductNameQuery(request.ProductName));

            return _mapper.Map<CouponModel>(couponDto);
        }

        /// <summary>
        /// Create discount gRPC method.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The server call context.</param>
        /// <returns>A <see cref="Task{CouponModel}"/>.</returns>
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var couponDto = await _mediator.Send(new CreateCouponCommand(request.ProductName, request.Description, request.Amount));

            return _mapper.Map<CouponModel>(couponDto);
        }

        /// <summary>
        /// Update discount gRPC method.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The server call context.</param>
        /// <returns>A <see cref="Task{CouponModel}"/>.</returns>
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var couponDto = await _mediator.Send(new UpdateCouponCommand(request.ProductName, request.Description, request.Amount));

            return _mapper.Map<CouponModel>(couponDto);
        }

        /// <summary>
        /// Delete discount gRPC method.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The server call context.</param>
        /// <returns>A <see cref="Task{DeleteDiscountResponse}"/>.</returns>
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _mediator.Send(new DeleteCouponCommand(request.ProductName));

            return new DeleteDiscountResponse { Success = deleted };
        }
    }
}