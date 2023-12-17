namespace todolist_console.Utils
{
    public class Sort
    {
        public static void QuickSort<T, TKey>(List<T> collection, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            QuickSortRecursive(collection, keySelector, 0, collection.Count - 1);
        }

        private static void QuickSortRecursive<T, TKey>(List<T> collection, Func<T, TKey> keySelector, int left, int right) where TKey : IComparable<TKey>
        {
            if (left < right)
            {
                int pivotIndex = Partition(collection, keySelector, left, right);

                QuickSortRecursive(collection, keySelector, left, pivotIndex - 1);
                QuickSortRecursive(collection, keySelector, pivotIndex + 1, right);
            }
        }

        private static int Partition<T, TKey>(List<T> collection, Func<T, TKey> keySelector, int left, int right) where TKey : IComparable<TKey>
        {
            TKey pivotValue = keySelector(collection[right]);
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                TKey currentKey = keySelector(collection[j]);
                if (currentKey.CompareTo(pivotValue) <= 0)
                {
                    i++;
                    Swap(collection, i, j);
                }
            }

            Swap(collection, i + 1, right);
            return i + 1;
        }

        private static void Swap<T>(List<T> collection, int i, int j)
        {
            T temp = collection[i];
            collection[i] = collection[j];
            collection[j] = temp;
        }

    }
}
