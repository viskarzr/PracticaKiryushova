using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaKiryushova
{
    internal class DataWork
    {

        public static int[] CreateMas(int a)
        {
            Random rnd = new Random();
            int[] result = new int[a];
            for (int i = 0; i < a; i++)
            {
                result[i] = rnd.Next(-20, 30);
            }
            return result;
        }

        public static int[] SwapMas(int[] a)
        {
           int[] masSecond = (int[])a.Clone();
            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 1; i < masSecond.Length; i++)
            {
                if (masSecond[i] < masSecond[minIndex]) minIndex = i;
                if (masSecond[i] > masSecond[maxIndex]) maxIndex = i;
            }
            int temp = masSecond[minIndex];
            masSecond[minIndex] = masSecond[maxIndex];
            masSecond[maxIndex] = temp;
            return masSecond;
        }
    }
}
