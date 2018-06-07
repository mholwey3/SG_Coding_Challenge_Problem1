using System;

namespace SGCodingChallengeProblem1 {
	class DatasetGenerator {

		/// <summary>
		/// Generates random data for the PersonDataTable and writes this data to an XML file
		/// </summary>
		/// <param name="startYear">Min num for generating random birth year</param>
		/// <param name="endYear">Max num for generating random birth and death years</param>
		/// <param name="datasetSize">The number of rows of data to be created in the table</param>
		public void GenerateRandomDataset(int startYear, int endYear, int datasetSize) {
			Data.PersonDataTable pdt = new Data.PersonDataTable();

			Random rand = new Random();
			for (int i = 0; i < datasetSize; i++) {
				Data.PersonRow row = pdt.NewPersonRow();
				int birthYear = rand.Next(startYear, endYear);
				int deathYear = rand.Next(birthYear, endYear);
				row["BirthYear"] = birthYear;
				row["DeathYear"] = deathYear;
				pdt.Rows.Add(row);
			}

			pdt.WriteXml(@"dataset.xml");
			Console.WriteLine("Generated Dataset: dataset.xml");
		}
	}
}
