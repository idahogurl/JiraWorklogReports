using System.Windows.Forms;

namespace JiraWorklogReport {
	partial class Form_Main {
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
			this.TabControl_TimeEntries = new System.Windows.Forms.TabControl();
			this.TabPage_TimeEntries = new System.Windows.Forms.TabPage();
			this.Label_DateFilter = new System.Windows.Forms.Label();
			this.Button_StartStop = new System.Windows.Forms.Button();
			this.DateTimePicker_TimeEntriesDate = new System.Windows.Forms.DateTimePicker();
			this.Button_AddEntry = new System.Windows.Forms.Button();
			this.Button_SaveToJira = new System.Windows.Forms.Button();
			this.DataGridView_TimeEntries = new System.Windows.Forms.DataGridView();
			this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Started = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Ended = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
			this.TabPage_Report = new System.Windows.Forms.TabPage();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.DateTimePicker_StartDate = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.DateTimePicker_EndDate = new System.Windows.Forms.DateTimePicker();
			this.Button_CreateReport = new System.Windows.Forms.Button();
			this.TabControl_TimeEntries.SuspendLayout();
			this.TabPage_TimeEntries.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DataGridView_TimeEntries)).BeginInit();
			this.TabPage_Report.SuspendLayout();
			this.SuspendLayout();
			// 
			// TabControl_TimeEntries
			// 
			this.TabControl_TimeEntries.Controls.Add(this.TabPage_TimeEntries);
			this.TabControl_TimeEntries.Controls.Add(this.TabPage_Report);
			this.TabControl_TimeEntries.Location = new System.Drawing.Point(19, 12);
			this.TabControl_TimeEntries.Name = "TabControl_TimeEntries";
			this.TabControl_TimeEntries.SelectedIndex = 0;
			this.TabControl_TimeEntries.Size = new System.Drawing.Size(693, 337);
			this.TabControl_TimeEntries.TabIndex = 12;
			// 
			// TabPage_TimeEntries
			// 
			this.TabPage_TimeEntries.Controls.Add(this.Label_DateFilter);
			this.TabPage_TimeEntries.Controls.Add(this.Button_StartStop);
			this.TabPage_TimeEntries.Controls.Add(this.DateTimePicker_TimeEntriesDate);
			this.TabPage_TimeEntries.Controls.Add(this.Button_AddEntry);
			this.TabPage_TimeEntries.Controls.Add(this.Button_SaveToJira);
			this.TabPage_TimeEntries.Controls.Add(this.DataGridView_TimeEntries);
			this.TabPage_TimeEntries.Location = new System.Drawing.Point(4, 25);
			this.TabPage_TimeEntries.Name = "TabPage_TimeEntries";
			this.TabPage_TimeEntries.Padding = new System.Windows.Forms.Padding(3);
			this.TabPage_TimeEntries.Size = new System.Drawing.Size(685, 308);
			this.TabPage_TimeEntries.TabIndex = 0;
			this.TabPage_TimeEntries.Text = "Time Entries";
			this.TabPage_TimeEntries.UseVisualStyleBackColor = true;
			// 
			// Label_DateFilter
			// 
			this.Label_DateFilter.AutoSize = true;
			this.Label_DateFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Label_DateFilter.Location = new System.Drawing.Point(18, 11);
			this.Label_DateFilter.Name = "Label_DateFilter";
			this.Label_DateFilter.Size = new System.Drawing.Size(154, 16);
			this.Label_DateFilter.TabIndex = 22;
			this.Label_DateFilter.Text = "View Time Entries for";
			// 
			// Button_StartStop
			// 
			this.Button_StartStop.Location = new System.Drawing.Point(102, 42);
			this.Button_StartStop.Name = "Button_StartStop";
			this.Button_StartStop.Size = new System.Drawing.Size(75, 23);
			this.Button_StartStop.TabIndex = 21;
			this.Button_StartStop.Text = "Start";
			this.Button_StartStop.UseVisualStyleBackColor = true;
			this.Button_StartStop.Click += new System.EventHandler(this.Button_StartStop_Click);
			// 
			// DateTimePicker_TimeEntriesDate
			// 
			this.DateTimePicker_TimeEntriesDate.Location = new System.Drawing.Point(178, 6);
			this.DateTimePicker_TimeEntriesDate.Name = "DateTimePicker_TimeEntriesDate";
			this.DateTimePicker_TimeEntriesDate.Size = new System.Drawing.Size(238, 22);
			this.DateTimePicker_TimeEntriesDate.TabIndex = 20;
			this.DateTimePicker_TimeEntriesDate.ValueChanged += new System.EventHandler(this.DateTimePicker_TimeEntriesDate_ValueChanged);
			// 
			// Button_AddEntry
			// 
			this.Button_AddEntry.Location = new System.Drawing.Point(21, 42);
			this.Button_AddEntry.Name = "Button_AddEntry";
			this.Button_AddEntry.Size = new System.Drawing.Size(75, 23);
			this.Button_AddEntry.TabIndex = 19;
			this.Button_AddEntry.Text = "Add New";
			this.Button_AddEntry.UseVisualStyleBackColor = true;
			this.Button_AddEntry.Click += new System.EventHandler(this.Button_AddEntry_Click);
			// 
			// Button_SaveToJira
			// 
			this.Button_SaveToJira.Location = new System.Drawing.Point(21, 265);
			this.Button_SaveToJira.Name = "Button_SaveToJira";
			this.Button_SaveToJira.Size = new System.Drawing.Size(113, 23);
			this.Button_SaveToJira.TabIndex = 18;
			this.Button_SaveToJira.Text = "Save to Jira";
			this.Button_SaveToJira.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.Button_SaveToJira.UseVisualStyleBackColor = true;
			this.Button_SaveToJira.Click += new System.EventHandler(this.Button_SaveToJira_Click);
			// 
			// DataGridView_TimeEntries
			// 
			this.DataGridView_TimeEntries.AllowUserToAddRows = false;
			this.DataGridView_TimeEntries.AllowUserToDeleteRows = false;
			this.DataGridView_TimeEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridView_TimeEntries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Description,
            this.Started,
            this.Ended,
            this.Duration,
            this.Delete});
			this.DataGridView_TimeEntries.Location = new System.Drawing.Point(21, 71);
			this.DataGridView_TimeEntries.Name = "DataGridView_TimeEntries";
			this.DataGridView_TimeEntries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DataGridView_TimeEntries.Size = new System.Drawing.Size(619, 178);
			this.DataGridView_TimeEntries.TabIndex = 17;
			// 
			// Description
			// 
			this.Description.DataPropertyName = "Description";
			this.Description.HeaderText = "Description";
			this.Description.Name = "Description";
			this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Description.Width = 300;
			// 
			// Started
			// 
			this.Started.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Started.DataPropertyName = "StartedString";
			this.Started.HeaderText = "Started";
			this.Started.Name = "Started";
			this.Started.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Started.Width = 57;
			// 
			// Ended
			// 
			this.Ended.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Ended.DataPropertyName = "EndedString";
			this.Ended.HeaderText = "Ended";
			this.Ended.Name = "Ended";
			this.Ended.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Ended.Width = 54;
			// 
			// Duration
			// 
			this.Duration.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Duration.DataPropertyName = "DurationDisplay";
			this.Duration.HeaderText = "Duration";
			this.Duration.Name = "Duration";
			this.Duration.ReadOnly = true;
			this.Duration.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Duration.Width = 64;
			// 
			// Delete
			// 
			this.Delete.HeaderText = "";
			this.Delete.Name = "Delete";
			this.Delete.Text = "Delete";
			this.Delete.UseColumnTextForButtonValue = true;
			// 
			// TabPage_Report
			// 
			this.TabPage_Report.Controls.Add(this.label3);
			this.TabPage_Report.Controls.Add(this.label1);
			this.TabPage_Report.Controls.Add(this.DateTimePicker_StartDate);
			this.TabPage_Report.Controls.Add(this.label2);
			this.TabPage_Report.Controls.Add(this.DateTimePicker_EndDate);
			this.TabPage_Report.Controls.Add(this.Button_CreateReport);
			this.TabPage_Report.Location = new System.Drawing.Point(4, 25);
			this.TabPage_Report.Name = "TabPage_Report";
			this.TabPage_Report.Padding = new System.Windows.Forms.Padding(3);
			this.TabPage_Report.Size = new System.Drawing.Size(685, 308);
			this.TabPage_Report.TabIndex = 1;
			this.TabPage_Report.Text = "Time Entries Report";
			this.TabPage_Report.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(27, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(208, 16);
			this.label3.TabIndex = 12;
			this.label3.Text = "Date Range of Jira Worklogs";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(27, 51);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 16);
			this.label1.TabIndex = 10;
			this.label1.Text = "Start";
			// 
			// DateTimePicker_StartDate
			// 
			this.DateTimePicker_StartDate.Location = new System.Drawing.Point(70, 46);
			this.DateTimePicker_StartDate.Margin = new System.Windows.Forms.Padding(4);
			this.DateTimePicker_StartDate.Name = "DateTimePicker_StartDate";
			this.DateTimePicker_StartDate.Size = new System.Drawing.Size(265, 22);
			this.DateTimePicker_StartDate.TabIndex = 8;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(27, 79);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 16);
			this.label2.TabIndex = 11;
			this.label2.Text = "End";
			// 
			// DateTimePicker_EndDate
			// 
			this.DateTimePicker_EndDate.Location = new System.Drawing.Point(70, 79);
			this.DateTimePicker_EndDate.Margin = new System.Windows.Forms.Padding(4);
			this.DateTimePicker_EndDate.Name = "DateTimePicker_EndDate";
			this.DateTimePicker_EndDate.Size = new System.Drawing.Size(265, 22);
			this.DateTimePicker_EndDate.TabIndex = 9;
			// 
			// Button_CreateReport
			// 
			this.Button_CreateReport.Location = new System.Drawing.Point(79, 119);
			this.Button_CreateReport.Margin = new System.Windows.Forms.Padding(4);
			this.Button_CreateReport.Name = "Button_CreateReport";
			this.Button_CreateReport.Size = new System.Drawing.Size(127, 28);
			this.Button_CreateReport.TabIndex = 7;
			this.Button_CreateReport.Text = "Create Report";
			this.Button_CreateReport.UseVisualStyleBackColor = true;
			this.Button_CreateReport.Click += new System.EventHandler(this.Button_CreateReport_Click);
			// 
			// Form_Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(728, 381);
			this.Controls.Add(this.TabControl_TimeEntries);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form_Main";
			this.Text = "Time Clock";
			this.TabControl_TimeEntries.ResumeLayout(false);
			this.TabPage_TimeEntries.ResumeLayout(false);
			this.TabPage_TimeEntries.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DataGridView_TimeEntries)).EndInit();
			this.TabPage_Report.ResumeLayout(false);
			this.TabPage_Report.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private TabControl TabControl_TimeEntries;
		private TabPage TabPage_TimeEntries;
		private Button Button_StartStop;
		private DateTimePicker DateTimePicker_TimeEntriesDate;
		private Button Button_AddEntry;
		private Button Button_SaveToJira;
		private DataGridView DataGridView_TimeEntries;
		private DataGridViewTextBoxColumn Description;
		private DataGridViewTextBoxColumn Started;
		private DataGridViewTextBoxColumn Ended;
		private DataGridViewTextBoxColumn Duration;
		private DataGridViewButtonColumn Delete;
		private TabPage TabPage_Report;
		private Label label1;
		private DateTimePicker DateTimePicker_StartDate;
		private Label label2;
		private DateTimePicker DateTimePicker_EndDate;
		private Button Button_CreateReport;
		private Label Label_DateFilter;
		private Label label3;
	}
}

