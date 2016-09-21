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
        /// Shuffles an array using the Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="rng">Randomly generated number.</param>
        /// <param name="array">The array to shuffle.</param>
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        /// <summary>
        /// Shuffles a list using the Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this Random rng, IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = list[n];
                list[n] = list[k];
                list[k] = temp;
            }
        }
    }
}
