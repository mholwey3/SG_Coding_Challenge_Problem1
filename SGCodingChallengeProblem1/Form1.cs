using System;
using System.Windows.Forms;

namespace SGCodingChallengeProblem1 {
	public partial class Form1 : Form {

		private int datasetSize = 100;
		private int startYear = 1900;
		private int endYear = 2001; // endYear is exclusive

		public Form1() {
			InitializeComponent();
		}

		private void generateDataset_Click(object sender, EventArgs e) {
			DatasetGenerator datasetGenerator = new DatasetGenerator(startYear, endYear, datasetSize);
			datasetGenerator.Run();
		}

		private void generatePopulationStatisticsReport_Click(object sender, EventArgs e) {
			PopulationStatisticsReportGenerator reportGenerator = new PopulationStatisticsReportGenerator(startYear, endYear);
			reportGenerator.Run();
		}
	}
}
