using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace bCurses.Helpers
{
    public static class ExtensionMethods
    {
        public static bool Remove<T>(this Collection<T> source, Func<T, bool> predicate)
        {
            var toRemove = source.Where(predicate).ToList();
            bool removedAll = true;
            foreach (T element in toRemove)
                removedAll = removedAll && source.Remove(element);
            return removedAll;
        }
    }
}
