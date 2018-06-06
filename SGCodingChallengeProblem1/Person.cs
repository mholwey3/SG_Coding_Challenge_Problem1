using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCodingChallengeProblem1 {
	class Person {

		private int birthYear;
		private int deathYear;

		public Person(int birthYear, int deathYear) {
			this.birthYear = birthYear;
			this.deathYear = deathYear;
		}

		public int BirthYear {
			get {
				return birthYear;
			}

			set {
				birthYear = value;
			}
		}

		public int DeathYear {
			get {
				return deathYear;
			}

			set {
				deathYear = value;
			}
		}

		public bool WasAliveDuringYear(int year) {
			return birthYear <= year && deathYear >= year;
		}

	}
}
