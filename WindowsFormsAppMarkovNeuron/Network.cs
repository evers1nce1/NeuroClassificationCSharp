using System;
using System.Linq;

namespace WindowsFormsAppMarkovNeuron
{
    class Network
    {
        private RBLayer layer;
        private Neuron[] outputNeurons;

        public Network(int inputSize, int hiddenSize, int outputSize, double[][] input, int seed = 1)
        {
            layer = new RBLayer(inputSize, hiddenSize, input);
            outputNeurons = new Neuron[outputSize];

            for (int i = 0; i < outputSize; i++)
            {
                outputNeurons[i] = new Neuron(
                    hiddenSize,
                    x => FunctionsList.logSigmoid(x),       
                    x => FunctionsList.logSigmoidDer(x),                  
                    seed + i
                );
            }
        }

        public double[] Activate(double[] input)
        {
            double[] rbOut = layer.Activate(input);
            return outputNeurons.Select(n => n.Activate(rbOut)).ToArray();
        }

        public double[] Train(double[][] inputs, double[][] targets, int epochs, double rate)
        {
            double[] errors = new double[epochs];

            for (int ep = 0; ep < epochs; ep++)
            {
                double totalError = 0;

                double[][] rbOuts = inputs.Select(inp => layer.Activate(inp)).ToArray();

                for (int k = 0; k < outputNeurons.Length; k++)
                {
                    double[] neuronTargets = targets.Select(t => t[k]).ToArray();
                    outputNeurons[k].Train(rbOuts, neuronTargets, 1, rate);
                }

                for (int i = 0; i < inputs.Length; i++)
                {
                    double[] outputs = Activate(inputs[i]);
                    for (int k = 0; k < outputs.Length; k++)
                    {
                        double diff = targets[i][k] - outputs[k];
                        totalError += diff * diff;
                    }
                }

                errors[ep] = totalError / inputs.Length;
            }


            return errors;
        }
    }
}
