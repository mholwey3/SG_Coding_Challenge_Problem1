using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace SGCodingChallengeProblem1 {
	class PopulationStatisticsReportGenerator {
		private int startYear;
		private int endYear;

		private Chart chart;

		public PopulationStatisticsReportGenerator(int startYear, int endYear) {
			this.startYear = startYear;
			this.endYear = endYear;
			chart = new Chart();
		}

		public void Run() {
			SetupChart();
			XmlNodeList people = GetDataset();
			List<int> bestYears = FindYearsWithMostPeopleAlive(startYear, endYear, people);

			Console.WriteLine(bestYears.Count == 1 ? "The year with the most people alive is: " : "The years with the most people alive are: ");
			foreach(int year in bestYears) {
				Console.WriteLine(year);
			}

			DrawAndSaveChart();
		}

		/// <summary>
		/// Gets the Xml nodes that make up the dataset
		/// </summary>
		/// <returns>The Xml nodes</returns>
		public XmlNodeList GetDataset() {
			XmlDocument dataDoc = new XmlDocument();
			dataDoc.Load(@"dataset.xml");
			return dataDoc.DocumentElement.ChildNodes;
		}

		/// <summary>
		/// Finds the year(s) with the most people alive and adds a data point to the chart for each year
		/// </summary>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="people"></param>
		/// <returns>The year(s) with the most people alive</returns>
		public List<int> FindYearsWithMostPeopleAlive(int startYear, int endYear, XmlNodeList people) {
			List<int> bestYears = new List<int>();
			int numPeopleAliveDuringBestYear = 0;
			for (int year = startYear; year < endYear; year++) {
				int numAlive = 0;
				foreach (XmlNode person in people) {
					int birthYear = Int32.Parse(person.SelectSingleNode("BirthYear").InnerText);
					int deathYear = Int32.Parse(person.SelectSingleNode("DeathYear").InnerText);
					Person p = new Person(birthYear, deathYear);
					if (p.WasAliveDuringYear(year)) {
						numAlive++;
					}
				}

				if (numAlive == numPeopleAliveDuringBestYear) {
					bestYears.Add(year);
					numPeopleAliveDuringBestYear = numAlive;
				} else if (numAlive > numPeopleAliveDuringBestYear) {
					bestYears.Clear();
					bestYears.Add(year);
					numPeopleAliveDuringBestYear = numAlive;
				}
				AddDataPointToChart(year, numAlive);
			}

			return bestYears;
		}

		public void SetupChart() {
			chart = new Chart();
			chart.Size = new Size(2048, 512);
			chart.Legends.Add(new Legend() { Name = "Legend" });
			chart.Legends[0].Docking = Docking.Bottom;
			chart.Palette = ChartColorPalette.BrightPastel;

			ChartArea chartArea = new ChartArea() { Name = "ChartArea" };
			chartArea.AxisX.MajorGrid.LineWidth = 0;
			chartArea.AxisY.MajorGrid.LineWidth = 0;
			chartArea.AxisX.Title = "Year";
			chartArea.AxisY.Title = "Number of People Alive";
			chartArea.BackColor = System.Drawing.Color.FromName("White");
			chart.ChartAreas.Add(chartArea);
			chart.ChartAreas["ChartArea"].AxisX.Interval = 1;

			string series = string.Empty;
			series = "Count";
			chart.Series.Add(series);
			chart.Series[series].ChartType = SeriesChartType.Column;
		}

		public void AddDataPointToChart(int year, int numAlive) {
			DataPoint dataPoint = new DataPoint();
			dataPoint.AxisLabel = year.ToString();
			dataPoint.Label = numAlive.ToString();
			dataPoint.XValue = year;
			dataPoint.YValues = new double[] { numAlive };
			chart.Series[0].Points.Add(dataPoint);
		}

		public void DrawAndSaveChart() {
			chart.Invalidate();
			chart.SaveImage(@"totalPeopleAlivePerYear.png", ChartImageFormat.Png);
			Console.WriteLine("Generated Chart: totalPeopleAlivePerYear.png");
		}

	}
}
