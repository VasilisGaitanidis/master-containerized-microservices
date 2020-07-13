﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Domain.Core
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public static bool operator ==(ValueObject obj1, ValueObject obj2)
        {
            if (ReferenceEquals(obj1, null) ^ ReferenceEquals(obj2, null))
            {
                return false;
            }
            return ReferenceEquals(obj1, null) || obj1.Equals(obj2);
        }

        public static bool operator !=(ValueObject obj1, ValueObject obj2)
        {
            return !(obj1 == obj2);
        }

        public bool Equals(ValueObject other)
        {
            return Equals(other as object);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            var other = (ValueObject)obj;
            using IEnumerator<object> thisValues = GetEqualityComponents().GetEnumerator();
            using IEnumerator<object> otherValues = other.GetEqualityComponents().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }
                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}