using System;
using System.Drawing;
using System.Windows.Forms;

namespace SGCodingChallengeProblem1 {
	class DatasetGenerator {

		int startYear;
		int endYear;
		int datasetSize;
		DataGridView dataGridView;

		public DatasetGenerator(int startYear, int endYear, int datasetSize) {
			this.startYear = startYear;
			this.endYear = endYear;
			this.datasetSize = datasetSize;
			dataGridView = new DataGridView();
		}

		/// <summary>
		/// Runs the generator
		/// </summary>
		public void Run() {
			SetupDataGridView();
			GenerateRandomDataset();
			SaveDataGridViewAsImage();
		}

		/// <summary>
		/// Generates random data for the PersonDataTable and writes this data to an XML file
		/// </summary>
		/// <param name="startYear">Min num for generating random birth year</param>
		/// <param name="endYear">Max num for generating random birth and death years</param>
		/// <param name="datasetSize">The number of rows of data to be created in the table</param>
		public void GenerateRandomDataset() {
			Data.PersonDataTable pdt = new Data.PersonDataTable();

			Random rand = new Random();
			for (int i = 0; i < datasetSize; i++) {
				Data.PersonRow row = pdt.NewPersonRow();
				int birthYear = rand.Next(startYear, endYear);
				int deathYear = rand.Next(birthYear, endYear);
				row["Id"] = i + 1;
				row["BirthYear"] = birthYear;
				row["DeathYear"] = deathYear;
				pdt.Rows.Add(row);

				string[] dataPoint = { row["Id"].ToString(), row["BirthYear"].ToString(), row["DeathYear"].ToString() };
				dataGridView.Rows.Add(dataPoint);
			}

			pdt.WriteXml(@"..\..\dataset.xml");
			Console.WriteLine("Generated Dataset: dataset.xml");
		}

		/// <summary>
		/// Initializes the structure and aesthetics of the data grid view
		/// </summary>
		public void SetupDataGridView() {
			dataGridView.ColumnCount = 3;

			dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView.Font, FontStyle.Bold);

			dataGridView.Name = "dataGridView";
			dataGridView.Location = new Point(8, 8);
			dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
			dataGridView.GridColor = Color.Black;
			dataGridView.RowHeadersVisible = false;

			dataGridView.Columns[0].Name = "ID";
			dataGridView.Columns[1].Name = "Birth Year";
			dataGridView.Columns[2].Name = "Death Year";

			dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridView.MultiSelect = false;
			dataGridView.Dock = DockStyle.Fill;
		}

		/// <summary>
		/// Saves the dataGridView object as a png image
		/// </summary>
		public void SaveDataGridViewAsImage() {
			int height = dataGridView.Height;
			dataGridView.Height = dataGridView.RowCount * dataGridView.RowTemplate.Height;

			Bitmap bitmap = new Bitmap(dataGridView.Width, dataGridView.Height);
			dataGridView.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView.Width, dataGridView.Height));

			dataGridView.Height = height;

			bitmap.Save(@"..\..\images\dataset.png");
			Console.WriteLine("Generated Dataset Image: dataset.png");
		}
	}
}
