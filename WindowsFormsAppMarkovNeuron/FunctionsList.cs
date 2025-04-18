using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppMarkovNeuron
{
    static class FunctionsList
    {
        public static Func<double, double> logSigmoid = x => 1.0 / (1.0 + Math.Exp(-x));
        public static Func<double, double> logSigmoidDer = output => output * (1 - output);
        public static Func<double, double> tanSigmoid = x => Math.Tanh(x);
        public static Func<double, double> tanSigmoidDer = output => 1 - Math.Pow(output, 2); 
        public static Func<double, double> linearFunc = x => x;
        public static Func<double, double> linearFuncDer = x => 1;

        public static double Gauss(double[] input, double[] center, double width)
        {
            double distance = 0;
            for (int i = 0; i < input.Length; i++)
            {
                distance += Math.Pow(input[i] - center[i], 2);
            }
            return Math.Exp(-distance / (2 * Math.Pow(width, 2)));
        }
    }
}
