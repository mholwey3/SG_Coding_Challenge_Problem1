using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCodingChallengeProblem1 {
	class DatasetGenerator {

		/// <summary>
		/// Generates random data for the PersonDataTable and writes this data to an XML file
		/// </summary>
		/// <param name="numDataEntries">The number of rows of data to be created in the table</param>
		public void GenerateRandomDataset(int numDataEntries) {
			Data.PersonDataTable pdt = new Data.PersonDataTable();

			Random rand = new Random();
			for (int i = 0; i < numDataEntries; i++) {
				Data.PersonRow row = pdt.NewPersonRow();
				int birthYear = rand.Next(1900, 2001);
				int deathYear = rand.Next(birthYear, 2001);
				row["BirthYear"] = birthYear;
				row["DeathYear"] = deathYear;
				pdt.Rows.Add(row);
			}

			pdt.WriteXml(@"dataset.xml");
		}

	}
}
