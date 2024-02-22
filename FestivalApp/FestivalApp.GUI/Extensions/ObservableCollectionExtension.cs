using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace FestivalApp.GUI.Extensions
{
    public static class ObservableCollectionExtension
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
