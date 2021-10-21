using System.Collections.Generic;

namespace VertexFragment
{
    public static class CollectionUtils
    {
        /// <summary>
        /// Fills the provided array with the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Fill<T>(ref T[] array, T value)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = value;
            }
        }

        /// <summary>
        /// Fills the provided array with the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Fill<T>(ref T[,] array, T value)
        {
            for (int y = 0; y < array.GetLength(1); ++y)
            {
                for (int x = 0; x < array.GetLength(0); ++x)
                {
                    array[x, y] = value;
                }
            }
        }

        /// <summary>
        /// Fills the provided List with the specified value.
        /// The list will be cleared and that value will be inserted the specified number of times.<para/>
        /// 
        /// If count is not specified, then the current list size will be used (not capacity).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <param name="count"></param>
        public static void Fill<T>(List<T> list, T value, int count = -1)
        {
            count = (count == -1) ? list.Count : count;

            list.Clear();

            for (int i = 0; i < count; ++i)
            {
                list.Add(value);
            }
        }

        /// <summary>
        /// Fills the provided list with new instances of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        public static void FillNew<T>(List<T> list, int count = -1) where T : new()
        {
            count = (count == -1) ? list.Count : count;

            list.Clear();

            for (int i = 0; i < count; ++i)
            {
                list.Add(new T());
            }
        }

        /// <summary>
        /// Inserts the contents of the source array int othe destination array.
        /// The source array must fit completely within the destination.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        public static void Insert<T>(T[,] source, T[,] destination, int xOffset, int yOffset)
        {
            int width = source.GetLength(0);
            int height = source.GetLength(1);

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    destination[x + xOffset, y + yOffset] = source[x, y];
                }
            }
        }

        /// <summary>
        /// Extracts a subset of the given source array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static T[,] Extract<T>(T[,] source, int xOffset, int yOffset, int width, int height)
        {
            T[,] subset = new T[width, height];

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    subset[x, y] = source[x + xOffset, y + yOffset];
                }
            }

            return subset;
        }
    }
}
