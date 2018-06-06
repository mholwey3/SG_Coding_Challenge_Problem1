using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace SGCodingChallengeProblem1 {
	class PopulationStatisticsReportGenerator {

		List<Person> people;

		// Maps year to numPeopleAlive
		Dictionary<int, int> totalPeopleAlivePerYear;

		public PopulationStatisticsReportGenerator() {
			people = new List<Person>();
			totalPeopleAlivePerYear = new Dictionary<int, int>();
		}

		public void Run() {
			XmlNodeList people = GetDataset();
			DetermineTotalPeopleAlivePerYear(1900, 2000, people);

			foreach (KeyValuePair<int, int> entry in totalPeopleAlivePerYear) {
				Console.WriteLine("{0}: {1} people alive.", entry.Key, entry.Value);
			}

			GenerateResultsChart();
		}

		public XmlNodeList GetDataset() {
			XmlDocument dataDoc = new XmlDocument();
			dataDoc.Load(@"dataset.xml");
			return dataDoc.DocumentElement.ChildNodes;
		}

		/// <summary>
		/// Determines the total number of people that are alive for each year.
		/// </summary>
		public void DetermineTotalPeopleAlivePerYear(int startYear, int endYear, XmlNodeList people) {
			for(int i = startYear; i <= endYear; i++) {
				int numAlive = 0;
				foreach (XmlNode node in people) {
					int birthYear = Int32.Parse(node.SelectSingleNode("BirthYear").InnerText);
					int deathYear = Int32.Parse(node.SelectSingleNode("DeathYear").InnerText);
					Person p = new Person(birthYear, deathYear);
					if (p.WasAliveDuringYear(i)) {
						numAlive++;
					}
				}
				totalPeopleAlivePerYear.Add(i, numAlive);
			}
		}

		public void GenerateDatasetDataTable() {

		}

		public void GenerateResultsChart() {
			Chart chart = new Chart();

			chart.Size = new Size(900, 400);
			chart.Legends.Add(new Legend() { Name = "Legend" });
			chart.Legends[0].Docking = Docking.Bottom;
			ChartArea chartArea = new ChartArea() { Name = "ChartArea" };
			chartArea.AxisX.MajorGrid.LineWidth = 0;
			chartArea.AxisY.MajorGrid.LineWidth = 0;
			chartArea.BackColor = System.Drawing.Color.FromName("White");
			chart.ChartAreas.Add(chartArea);
			chart.Palette = ChartColorPalette.BrightPastel;
			string series = string.Empty;
			series = "Number of People Alive Per Year";
			chart.Series.Add(series);
			chart.Series[series].ChartType = SeriesChartType.Column;
			foreach(KeyValuePair<int, int> entry in totalPeopleAlivePerYear) {
				DataPoint dataPoint = new DataPoint() {
					AxisLabel = "series",
					XValue = entry.Key,
					YValues = new double[] { entry.Value }
				};
				chart.Series[series].Points.Add(dataPoint);
			}
			chart.Invalidate();
			chart.SaveImage(@"resultsChart.png", ChartImageFormat.Png);
			Console.WriteLine("Generated Chart: resultsChart.png");
		}

	}
}
