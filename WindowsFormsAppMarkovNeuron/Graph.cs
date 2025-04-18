using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsAppMarkovNeuron
{
    public partial class Graph : Form
    {
        private double[] data;
        public Graph(double[] data)
        {
            InitializeComponent();
            this.data = data;
        }

        private void Graph_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.Title = "Эпоха";
            chart1.ChartAreas[0].AxisY.Title = "Ошибка";
            chart1.ChartAreas[0].AxisY.Maximum = 1;
            for (int i = 0; i < data.Length; i++)
            {
                chart1.Series[0].Points.AddXY(i + 1, data[i]);
            }
        }
    }
}
