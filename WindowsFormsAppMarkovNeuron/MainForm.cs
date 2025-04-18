using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppMarkovNeuron
{
    public partial class MainForm : Form
    {
        Network network;
        double[] maxes;
        public MainForm()
        {
            

            InitializeComponent();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            string[] inputLines = richTextBox1.Lines;
            string[] outputLines = richTextBox2.Lines;

            if (inputLines.Length != outputLines.Length)
            {
                MessageBox.Show("Количество строк входных и выходных данных не совпадает");
                return;
            }

            double[][] inputs = inputLines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Split(',')
                    .Select(s => double.TryParse(s.Trim(), out double val) ? val : 0)
                    .ToArray())
                .ToArray();

            double[][] expectedOut = outputLines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Split(',')
                    .Select(s => double.TryParse(s.Trim(), out double val) ? val : 0)
                    .ToArray())
                .ToArray();

            (maxes, inputs) = Utils.NormalizeDataset(inputs);
            if (network == null)
            {
                int inputSize = inputs.FirstOrDefault()?.Length ?? 1;
                int outputSize = expectedOut.FirstOrDefault()?.Length ?? 1;
                network = new Network(inputSize, 20, outputSize, inputs);
            }

            double[] err = network.Train(inputs, expectedOut, 5000, 0.05);
            new Graph(err).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (network == null)
            {
                MessageBox.Show("Сеть не инициализирована");
                return;
            }

            double[] inputs = textBox1.Text.Split(',')
                .Select(s => double.TryParse(s.Trim(), out double val) ? val : 0)
                .ToArray();
            inputs = Utils.NormalizeInput(inputs, maxes);
            double[] output = network.Activate(inputs);

            label5.Text = string.Join(", ", output.Select(x => Math.Round(x, 2)));

        }
    }
}
