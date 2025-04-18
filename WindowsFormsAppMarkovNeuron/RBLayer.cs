using System;
using System.Linq;

namespace WindowsFormsAppMarkovNeuron
{
    class RBLayer
    {
        private double[][] centers;
        private double[] widths;
        private Random random;

        public RBLayer(int inputSize, int hiddenSize, double[][] train)
        {
            random = new Random();
            centers = new double[hiddenSize][];
            widths = new double[hiddenSize];

            centers[0] = (double[])train[random.Next(train.Length)].Clone();
            for (int i = 1; i < hiddenSize; i++)
            {
                var farthest = train.OrderByDescending(
                    x => centers.Take(i).Min(c => Utils.EuclideanDistance(c, x))).First();
                centers[i] = (double[])farthest.Clone();
            }

            double totalDistance = 0;
            int pairCount = 0;

            for (int i = 0; i < hiddenSize; i++)
            {
                for (int j = i + 1; j < hiddenSize; j++)
                {
                    double dist = 0;
                    for (int k = 0; k < inputSize; k++)
                    {
                        dist += Math.Pow(centers[i][k] - centers[j][k], 2);
                    }
                    totalDistance += Math.Sqrt(dist);
                    pairCount++;
                }
            }

            double meanDistance = totalDistance / pairCount;

            //σ = d/√(2m)
            //d среднее расстояние, m число нейронов
            double sigma = meanDistance / Math.Sqrt(2 * hiddenSize);

            for (int i = 0; i < hiddenSize; i++)
            {
                widths[i] = sigma;
            }
        }

        public double[] Activate(double[] input)
        {
            double[] outputs = new double[centers.Length];
            for (int i = 0; i < centers.Length; i++)
                outputs[i] = FunctionsList.Gauss(input, centers[i], widths[i]);

            return outputs;
        }

    }
}
