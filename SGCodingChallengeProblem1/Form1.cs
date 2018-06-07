using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGCodingChallengeProblem1 {
	public partial class Form1 : Form {

		private int datasetSize = 1000;
		private int startYear = 1900;
		private int endYear = 2001; // endYear is exclusive

		public Form1() {
			InitializeComponent();
		}

		private void generateDataset_Click(object sender, EventArgs e) {
			DatasetGenerator datasetGenerator = new DatasetGenerator();
			datasetGenerator.GenerateRandomDataset(startYear, endYear, datasetSize);
		}

		private void generatePopulationStatisticsReport_Click(object sender, EventArgs e) {
			PopulationStatisticsReportGenerator reportGenerator = new PopulationStatisticsReportGenerator(startYear, endYear);
			reportGenerator.Run();
		}
	}
}
