using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaKiryushova
{
    public class ArrayProcessor
    {
        private int[] _data;
        private Random _random;

        public ArrayProcessor()
        {
            _random = new Random();
        }

        public void Generate(int size)
        {
            if (size <= 0) throw new ArgumentException("Размер должен быть больше 0");

            _data = new int[size];
            for (int i = 0; i < size; i++)
            {
                _data[i] = _random.Next(-20, 30); 
            }
        }

        public int[] GetData()
        {
            return _data != null ? (int[])_data.Clone() : new int[0];
        }

        public void SetData(int[] newData)
        {
            _data = newData;
        }

        public void SwapMinMax()
        {
            if (_data == null || _data.Length == 0) return;

            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 1; i < _data.Length; i++)
            {
                if (_data[i] < _data[minIndex]) minIndex = i;
                if (_data[i] > _data[maxIndex]) maxIndex = i;
            }

            int temp = _data[minIndex];
            _data[minIndex] = _data[maxIndex];
            _data[maxIndex] = temp;
        }
    }
}
