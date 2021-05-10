using System.Collections.Generic;
using System.Linq;

namespace Qama.Framework.Core.Abstractions.DAL
{
    public static class ValueObjectExtensions
    {
        public static IList<T> UpdateFrom<T>(this IList<T> sourceList, IList<T> destinationList) where T : IValueObject
        {
            var addedItems = destinationList.Except(sourceList).ToList();
            var deletedItems = sourceList.Except(destinationList).ToList();
            deletedItems.ForEach(a => sourceList.Remove(a));
            addedItems.ForEach(sourceList.Add);
            return sourceList;
        }
    }
}
