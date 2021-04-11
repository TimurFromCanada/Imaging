using System.Collections.Generic;
using System;
namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        static double GetBrightThreshold(double[,] original, double threshold)
        {
            var size = original.GetLength(0);
            var size2 = original.GetLength(1);
            var whiteNumber = Convert.ToInt32(Math.Floor(threshold * size * size2));
            var list = new List<double>();

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size2; j++)
                {
                    list.Add(original[i, j]);
                }
            }
                
            list.Sort();

            if (whiteNumber == 0)
            {
                return list[size * size2 - 1] + 1;
            }
                
            return list[size * size2 - whiteNumber];
        }

        public static double[,] ThresholdFilter(double[,] original, double threshold)
        {
            var brightThreshold = GetBrightThreshold(original, threshold);
            var size = original.GetLength(0);
            var size2 = original.GetLength(1);
            var result = new double[size, size2];

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size2; j++)
                {
                    if (original[i, j] >= brightThreshold)
                    {
                        result[i, j] = 1;
                    }
                }
            }
                
            return result;
        }
    }
}