namespace SGCodingChallengeProblem1 {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.generateDataset = new System.Windows.Forms.Button();
			this.generatePopulationStatisticsReport = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// generateDataset
			// 
			this.generateDataset.Location = new System.Drawing.Point(130, 61);
			this.generateDataset.Name = "generateDataset";
			this.generateDataset.Size = new System.Drawing.Size(133, 35);
			this.generateDataset.TabIndex = 0;
			this.generateDataset.Text = "Generate New Dataset";
			this.generateDataset.UseVisualStyleBackColor = true;
			this.generateDataset.Click += new System.EventHandler(this.generateDataset_Click);
			// 
			// generatePopulationStatisticsReport
			// 
			this.generatePopulationStatisticsReport.Location = new System.Drawing.Point(91, 130);
			this.generatePopulationStatisticsReport.Name = "generatePopulationStatisticsReport";
			this.generatePopulationStatisticsReport.Size = new System.Drawing.Size(211, 37);
			this.generatePopulationStatisticsReport.TabIndex = 1;
			this.generatePopulationStatisticsReport.Text = "Generate Population Statistics Report";
			this.generatePopulationStatisticsReport.UseVisualStyleBackColor = true;
			this.generatePopulationStatisticsReport.Click += new System.EventHandler(this.generatePopulationStatisticsReport_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 250);
			this.Controls.Add(this.generatePopulationStatisticsReport);
			this.Controls.Add(this.generateDataset);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button generateDataset;
		private System.Windows.Forms.Button generatePopulationStatisticsReport;
	}
}

