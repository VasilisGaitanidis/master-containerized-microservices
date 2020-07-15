using System;

namespace Domain.Core.Models
{
    /// <summary>
    /// Super type for all Identity types with generic Id.
    /// </summary>
    /// <typeparam name="TId">The generic identifier.</typeparam>
    public interface IIdentity<out TId>
    {
        /// <summary>
        /// The generic identifier.
        /// </summary>
        TId Id { get; }
    }

    /// <inheritdoc cref="IIdentity{TId}" />
    public abstract class Identity<TId> : IEquatable<Identity<TId>>, IIdentity<TId>
    {
        /// <summary>
        /// Initializes an Identity class.
        /// </summary>
        /// <param name="id">The generic identifier.</param>
        protected Identity(TId id)
        {
            Id = id;
        }

        /// <inheritdoc />
        public TId Id { get; }

        /// <inheritdoc />
        public bool Equals(Identity<TId> other)
        {
            if (ReferenceEquals(this, other))
                return true;

            return !ReferenceEquals(null, other) && Id.Equals(other.Id);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as Identity<TId>);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() ^ 93) + Id.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        public static bool operator ==(Identity<TId> left, Identity<TId> right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Identity<TId> left, Identity<TId> right)
        {
            return !(left == right);
        }
    }
}