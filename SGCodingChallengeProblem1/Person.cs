namespace SGCodingChallengeProblem1 {
	class Person {
		
		private int birthYear;
		private int deathYear;

		public Person(int birthYear, int deathYear) {
			this.birthYear = birthYear;
			this.deathYear = deathYear;
		}

		public bool WasAliveDuringYear(int year) {
			return birthYear <= year && deathYear >= year;
		}

	}
}
