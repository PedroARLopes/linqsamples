using System;

namespace CarsProj
{
    public class CarStatistics
    {
        public int Max { get; set; }
        public int Min { get; set; }
        private int _total;
        private int _count;
        public double Average { get; set; }

        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }

        public CarStatistics Accumulate(Car car)
        {
            _count++;
            _total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);
            return this;
        }

        public CarStatistics Compute()
        {
            Average = _total / _count;
            return this;
        }
    }
}