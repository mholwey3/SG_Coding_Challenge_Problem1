using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace SGCodingChallengeProblem1 {
	class PopulationStatisticsReportGenerator {

		private int startYear;
		private int endYear;
		List<YearStatistic> yearsWithMostPeopleAlive;
		private Chart chart;

		public PopulationStatisticsReportGenerator(int startYear, int endYear) {
			this.startYear = startYear;
			this.endYear = endYear;
			chart = new Chart();
		}

		/// <summary>
		/// Runs the generator
		/// </summary>
		public void Run() {
			SetupChart();

			XmlNodeList people = GetDataset();
			yearsWithMostPeopleAlive = FindYearsWithMostPeopleAlive(startYear, endYear, people);

			AddSummaryToChart();
			DrawAndSaveChart();
		}

		/// <summary>
		/// Gets the Xml nodes that make up the dataset
		/// </summary>
		/// <returns>The Xml nodes</returns>
		public XmlNodeList GetDataset() {
			XmlDocument dataDoc = new XmlDocument();
			dataDoc.Load(@"..\..\dataset.xml");
			return dataDoc.DocumentElement.ChildNodes;
		}

		/// <summary>
		/// Finds the year(s) with the most people alive and adds a data point to the chart for each year evaluated
		/// </summary>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="people"></param>
		/// <returns>The year(s) with the most people alive</returns>
		public List<YearStatistic> FindYearsWithMostPeopleAlive(int startYear, int endYear, XmlNodeList people) {
			List<YearStatistic> bestYears = new List<YearStatistic>();
			int numAliveDuringBestYears = 0;
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

				// If the year being evaluated has the same number of people alive as other best year(s), 
				// add it to the list to keep track of all years. If it has more people alive than other best
				// years, clear the list and start it over with the new best year.
				YearStatistic stat = new YearStatistic(year, numAlive);
				if (numAlive == numAliveDuringBestYears) {
					bestYears.Add(stat);
				} else if (numAlive > numAliveDuringBestYears) {
					bestYears.Clear();
					bestYears.Add(stat);
					numAliveDuringBestYears = numAlive;
				}
				AddDataPointToChart(stat);
			}

			return bestYears;
		}

		/// <summary>
		/// Initializes how the chart looks as well as its series
		/// </summary>
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
			chartArea.BackColor = Color.FromName("White");
			chart.ChartAreas.Add(chartArea);
			chart.ChartAreas["ChartArea"].AxisX.Interval = 1;

			string series = string.Empty;
			series = "Count";
			chart.Series.Add(series);
			chart.Series[series].ChartType = SeriesChartType.Column;
		}

		/// <summary>
		/// Adds a single data point to the chart
		/// </summary>
		/// <param name="year">X Value - Year being recorded</param>
		/// <param name="numAlive">Y Value - Number of people alive</param>
		public void AddDataPointToChart(YearStatistic stat) {
			DataPoint dataPoint = new DataPoint();
			dataPoint.AxisLabel = stat.Year.ToString();
			dataPoint.Label = stat.NumPeopleAlive.ToString();
			dataPoint.XValue = stat.Year;
			dataPoint.YValues = new double[] { stat.NumPeopleAlive };
			chart.Series[0].Points.Add(dataPoint);
		}

		/// <summary>
		/// Adds a summary with the algorithm results to the chart
		/// </summary>
		public void AddSummaryToChart() {
			int mostPeopleAlive = yearsWithMostPeopleAlive[0].NumPeopleAlive;
			string[] years = new string[yearsWithMostPeopleAlive.Count];
			for (int i = 0; i < yearsWithMostPeopleAlive.Count; i++) {
				years[i] = yearsWithMostPeopleAlive[i].Year.ToString();
			}
			string summary = string.Format("The year(s) with the most people alive ({0} people) is (are): {1}", mostPeopleAlive, string.Join(",", years));
			chart.Titles.Add("Title1");
			chart.Titles["Title1"].Text = summary;
			chart.Titles["Title1"].Docking = Docking.Bottom;
		}

		/// <summary>
		/// Draws the chart and saves it as a png image
		/// </summary>
		public void DrawAndSaveChart() {
			chart.Invalidate();
			chart.SaveImage(@"..\..\images\totalPeopleAlivePerYear.png", ChartImageFormat.Png);
			Console.WriteLine("Generated Chart: totalPeopleAlivePerYear.png");
		}

	}
}
