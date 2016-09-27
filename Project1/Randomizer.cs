using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    static class Randomizer
    {
        /// <summary>
        /// Shuffles an array using the Knuth algorithm.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array to shuffle.</param>
        /// <param name="minIndex">The minimum index to start shuffling, inclusive.</param>
        /// <param name="maxIndex">The maximum index to end shuffling, inclusive.</param>
        public static void KnuthShuffle<T>(T[] array, int minIndex, int maxIndex)
        {
            System.Random random = new System.Random();
            for (int i = minIndex; i <= maxIndex; i++)
            {
                int j = random.Next(i, maxIndex + 1); // Don't select from the entire array on subsequent loops
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
    }
}
