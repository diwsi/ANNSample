namespace WindowsFormsApplication1
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Trainx1 = new System.Windows.Forms.Button();
            this.Trainx50 = new System.Windows.Forms.Button();
            this.Trainx500 = new System.Windows.Forms.Button();
            this.TestBtn = new System.Windows.Forms.Button();
            this.TrainCounterlbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 12);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Actual";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Red;
            series2.Legend = "Legend1";
            series2.Name = "ANN";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(1222, 300);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // Trainx1
            // 
            this.Trainx1.Location = new System.Drawing.Point(390, 342);
            this.Trainx1.Name = "Trainx1";
            this.Trainx1.Size = new System.Drawing.Size(112, 23);
            this.Trainx1.TabIndex = 5;
            this.Trainx1.Text = "Train X1";
            this.Trainx1.UseVisualStyleBackColor = true;
            this.Trainx1.Click += new System.EventHandler(this.Trainx1_Click);
            // 
            // Trainx50
            // 
            this.Trainx50.Location = new System.Drawing.Point(521, 342);
            this.Trainx50.Name = "Trainx50";
            this.Trainx50.Size = new System.Drawing.Size(112, 23);
            this.Trainx50.TabIndex = 6;
            this.Trainx50.Text = "Train X50";
            this.Trainx50.UseVisualStyleBackColor = true;
            this.Trainx50.Click += new System.EventHandler(this.Trainx50_Click);
            // 
            // Trainx500
            // 
            this.Trainx500.Location = new System.Drawing.Point(652, 342);
            this.Trainx500.Name = "Trainx500";
            this.Trainx500.Size = new System.Drawing.Size(112, 23);
            this.Trainx500.TabIndex = 7;
            this.Trainx500.Text = "Train X500";
            this.Trainx500.UseVisualStyleBackColor = true;
            this.Trainx500.Click += new System.EventHandler(this.Trainx500_Click);
            // 
            // TestBtn
            // 
            this.TestBtn.Location = new System.Drawing.Point(848, 342);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(112, 23);
            this.TestBtn.TabIndex = 8;
            this.TestBtn.Text = "Test";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // TrainCounterlbl
            // 
            this.TrainCounterlbl.AutoSize = true;
            this.TrainCounterlbl.Location = new System.Drawing.Point(569, 319);
            this.TrainCounterlbl.Name = "TrainCounterlbl";
            this.TrainCounterlbl.Size = new System.Drawing.Size(10, 13);
            this.TrainCounterlbl.TabIndex = 9;
            this.TrainCounterlbl.Text = " ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 387);
            this.Controls.Add(this.TrainCounterlbl);
            this.Controls.Add(this.TestBtn);
            this.Controls.Add(this.Trainx500);
            this.Controls.Add(this.Trainx50);
            this.Controls.Add(this.Trainx1);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button Trainx1;
        private System.Windows.Forms.Button Trainx50;
        private System.Windows.Forms.Button Trainx500;
        private System.Windows.Forms.Button TestBtn;
        private System.Windows.Forms.Label TrainCounterlbl;
    }
}

