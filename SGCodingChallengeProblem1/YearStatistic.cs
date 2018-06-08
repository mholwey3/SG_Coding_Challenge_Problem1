namespace SGCodingChallengeProblem1 {
	class YearStatistic {

		private int year;
		private int numPeopleAlive;

		public YearStatistic(int year, int numPeopleAlive) {
			this.Year = year;
			this.NumPeopleAlive = numPeopleAlive;
		}

		public int Year {
			get {
				return year;
			}

			set {
				year = value;
			}
		}

		public int NumPeopleAlive {
			get {
				return numPeopleAlive;
			}

			set {
				numPeopleAlive = value;
			}
		}

	}
}
