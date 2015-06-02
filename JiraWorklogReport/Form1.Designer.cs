using System.Windows.Forms;

namespace JiraWorklogReport {
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
			this.Button_GetEntries = new System.Windows.Forms.Button();
			this.DateTimePicker_StartDate = new System.Windows.Forms.DateTimePicker();
			this.DateTimePicker_EndDate = new System.Windows.Forms.DateTimePicker();
			this.Button_SaveToJira = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.DateTimePicker_TimeEntriesDate = new System.Windows.Forms.DateTimePicker();
			this.button1 = new System.Windows.Forms.Button();
			this.Button_Save_To_Jira = new System.Windows.Forms.Button();
			this.DataGridView_TimeEntries = new System.Windows.Forms.DataGridView();
			this.Button_StartStop = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Started = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Ended = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Label_Focus = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DataGridView_TimeEntries)).BeginInit();
			this.SuspendLayout();
			// 
			// Button_GetEntries
			// 
			this.Button_GetEntries.Location = new System.Drawing.Point(150, 86);
			this.Button_GetEntries.Margin = new System.Windows.Forms.Padding(4);
			this.Button_GetEntries.Name = "Button_GetEntries";
			this.Button_GetEntries.Size = new System.Drawing.Size(127, 28);
			this.Button_GetEntries.TabIndex = 0;
			this.Button_GetEntries.Text = "Create Report";
			this.Button_GetEntries.UseVisualStyleBackColor = true;
			this.Button_GetEntries.Click += new System.EventHandler(this.Button_CreateReport_Click);
			// 
			// DateTimePicker_StartDate
			// 
			this.DateTimePicker_StartDate.Location = new System.Drawing.Point(87, 22);
			this.DateTimePicker_StartDate.Margin = new System.Windows.Forms.Padding(4);
			this.DateTimePicker_StartDate.Name = "DateTimePicker_StartDate";
			this.DateTimePicker_StartDate.Size = new System.Drawing.Size(265, 22);
			this.DateTimePicker_StartDate.TabIndex = 1;
			// 
			// DateTimePicker_EndDate
			// 
			this.DateTimePicker_EndDate.Location = new System.Drawing.Point(87, 54);
			this.DateTimePicker_EndDate.Margin = new System.Windows.Forms.Padding(4);
			this.DateTimePicker_EndDate.Name = "DateTimePicker_EndDate";
			this.DateTimePicker_EndDate.Size = new System.Drawing.Size(265, 22);
			this.DateTimePicker_EndDate.TabIndex = 2;
			// 
			// Button_SaveToJira
			// 
			this.Button_SaveToJira.Location = new System.Drawing.Point(19, 232);
			this.Button_SaveToJira.Margin = new System.Windows.Forms.Padding(4);
			this.Button_SaveToJira.Name = "Button_SaveToJira";
			this.Button_SaveToJira.Size = new System.Drawing.Size(100, 28);
			this.Button_SaveToJira.TabIndex = 4;
			this.Button_SaveToJira.Text = "Save to Jira";
			this.Button_SaveToJira.UseVisualStyleBackColor = true;
			this.Button_SaveToJira.Click += new System.EventHandler(this.Button_SaveToJira_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 26);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Start";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 54);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "End";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.DateTimePicker_StartDate);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.DateTimePicker_EndDate);
			this.groupBox1.Controls.Add(this.Button_GetEntries);
			this.groupBox1.Location = new System.Drawing.Point(20, 10);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox1.Size = new System.Drawing.Size(428, 120);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Report Date Range";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.Label_Focus);
			this.groupBox2.Controls.Add(this.DateTimePicker_TimeEntriesDate);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.Button_Save_To_Jira);
			this.groupBox2.Controls.Add(this.DataGridView_TimeEntries);
			this.groupBox2.Location = new System.Drawing.Point(20, 169);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(818, 268);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Time Entries";
			// 
			// DateTimePicker_TimeEntriesDate
			// 
			this.DateTimePicker_TimeEntriesDate.Location = new System.Drawing.Point(99, 19);
			this.DateTimePicker_TimeEntriesDate.Name = "DateTimePicker_TimeEntriesDate";
			this.DateTimePicker_TimeEntriesDate.Size = new System.Drawing.Size(238, 22);
			this.DateTimePicker_TimeEntriesDate.TabIndex = 14;
			this.DateTimePicker_TimeEntriesDate.ValueChanged += new System.EventHandler(this.DateTimePicker_TimeEntriesDate_ValueChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(7, 18);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 13;
			this.button1.Text = "Add New";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button_AddEntry_Click);
			// 
			// Button_Save_To_Jira
			// 
			this.Button_Save_To_Jira.Location = new System.Drawing.Point(7, 239);
			this.Button_Save_To_Jira.Name = "Button_Save_To_Jira";
			this.Button_Save_To_Jira.Size = new System.Drawing.Size(113, 23);
			this.Button_Save_To_Jira.TabIndex = 12;
			this.Button_Save_To_Jira.Text = "Save to Jira";
			this.Button_Save_To_Jira.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.Button_Save_To_Jira.UseVisualStyleBackColor = true;
			this.Button_Save_To_Jira.Click += new System.EventHandler(this.Button_SaveToJira_Click);
			// 
			// DataGridView_TimeEntries
			// 
			this.DataGridView_TimeEntries.AllowUserToAddRows = false;
			this.DataGridView_TimeEntries.AllowUserToDeleteRows = false;
			this.DataGridView_TimeEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridView_TimeEntries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Button_StartStop,
            this.Description,
            this.Started,
            this.Ended,
            this.Duration,
            this.Delete});
			this.DataGridView_TimeEntries.Location = new System.Drawing.Point(7, 47);
			this.DataGridView_TimeEntries.Name = "DataGridView_TimeEntries";
			this.DataGridView_TimeEntries.Size = new System.Drawing.Size(800, 178);
			this.DataGridView_TimeEntries.TabIndex = 11;
			this.DataGridView_TimeEntries.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridView_TimeEntries_Click);
			// 
			// Button_StartStop
			// 
			this.Button_StartStop.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Button_StartStop.HeaderText = "";
			this.Button_StartStop.Name = "Button_StartStop";
			this.Button_StartStop.Text = "Start/Stop";
			this.Button_StartStop.UseColumnTextForButtonValue = true;
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
			// Label_Focus
			// 
			this.Label_Focus.AutoSize = true;
			this.Label_Focus.Location = new System.Drawing.Point(344, 18);
			this.Label_Focus.Name = "Label_Focus";
			this.Label_Focus.Size = new System.Drawing.Size(0, 16);
			this.Label_Focus.TabIndex = 15;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(859, 492);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.Button_SaveToJira);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.Text = "Form1";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DataGridView_TimeEntries)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Button_GetEntries;
		private System.Windows.Forms.DateTimePicker DateTimePicker_StartDate;
		private System.Windows.Forms.DateTimePicker DateTimePicker_EndDate;
		private System.Windows.Forms.Button Button_SaveToJira;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DataGridView DataGridView_TimeEntries;
		private System.Windows.Forms.Button Button_Save_To_Jira;
		private System.Windows.Forms.Button button1;
		private DateTimePicker DateTimePicker_TimeEntriesDate;
		private DataGridViewButtonColumn Button_StartStop;
		private DataGridViewTextBoxColumn Description;
		private DataGridViewTextBoxColumn Started;
		private DataGridViewTextBoxColumn Ended;
		private DataGridViewTextBoxColumn Duration;
		private DataGridViewButtonColumn Delete;
		private Label Label_Focus;
	}
}

