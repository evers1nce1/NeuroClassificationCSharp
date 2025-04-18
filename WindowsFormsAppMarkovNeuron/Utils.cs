using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppMarkovNeuron
{
    static class Utils
    {
        public static (double[] maxValues, double[][] normalizedData) NormalizeDataset(double[][] data)
        {

            int featuresCount = data[0].Length;
            double[] max = new double[featuresCount];

            for (int j = 0; j < featuresCount; j++)
                max[j] = double.MinValue;

            foreach (var row in data)
            {
                for (int j = 0; j < featuresCount; j++)
                {
                    max[j] = Math.Max(max[j], Math.Abs(row[j]));
                }
            }

            double[][] normalized = data.Select(row =>
                row.Select((val, j) => max[j] != 0 ? val / max[j] : 0).ToArray()
            ).ToArray();

            return (max, normalized);
        }
        public static double EuclideanDistance(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
                sum += Math.Pow(a[i] - b[i], 2);
            return Math.Sqrt(sum);
        }

        public static double[] NormalizeInput(double[] input, double[] maxValues)
        {

            return input.Select((val, j) => maxValues[j] != 0 ? val / maxValues[j] : 0).ToArray();
        }

    }
}
