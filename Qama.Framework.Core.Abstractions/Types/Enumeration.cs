using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Qama.Framework.Core.Abstractions.Exceptions;

namespace Qama.Framework.Core.Abstractions.Types
{
    public abstract class Enumeration : IComparable
    {
        private readonly string _name;
        private readonly int _id;
        protected Enumeration(int id, string name) => (_id, _name) = (id, name);
        public sealed override string ToString() => _name;
        public override int GetHashCode() => _id.GetHashCode();

        private static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();
        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration otherValue))
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = _id.Equals(otherValue._id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => _id.CompareTo(((Enumeration)other)._id);

        public static T FromId<T>(int id) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, int>(id, "id", item => item._id == id);
            return matchingItem;
        }

        public static T FromName<T>(string name) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, string>(name, "name", item => item._name == name);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new InternalApplicationException(InternalApplicationExceptionDomain.DefaultApplicationException, message);
            }

            return matchingItem;
        }

    }
}
