using System;

namespace WindowsFormsAppMarkovNeuron
{
    public class Neuron
    {
        private double[] weights;
        private double bias;
        private Random random;
        private Func<double, double> activationFunc;
        private Func<double, double> activationFuncDer;

        public Neuron(int inputSize, Func<double, double> activationFunc, Func<double, double> activationFuncDer, int seed = 1)
        {
            random = new Random(seed);
            weights = new double[inputSize];
            this.activationFunc = activationFunc;
            this.activationFuncDer = activationFuncDer;

            for (int i = 0; i < inputSize; i++)
                weights[i] = random.NextDouble() - 0.5;

            bias = random.NextDouble() - 0.5;
        }

        public double Activate(double[] inputs)
        {
            double sum = bias;
            for (int i = 0; i < inputs.Length; i++)
                sum += inputs[i] * weights[i];

            return activationFunc(sum);
        }

        public void Train(double[][] input, double[] expectedOut, int epochCount, double learningRate)
        {
            for (int epoch = 0; epoch < epochCount; epoch++)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    double output = Activate(input[i]);
                    double error = expectedOut[i] - output;
                    for (int j = 0; j < weights.Length; j++)
                        weights[j] += learningRate * error * activationFuncDer(output) * input[i][j];

                    bias += learningRate * error * activationFuncDer(output);
                }
            }
        }
    }
}
