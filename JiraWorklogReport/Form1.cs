using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace JiraWorklogReport {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();

			TimeEntries = GetTimeEntries(GetDataFileName());

			DataGridView_TimeEntries.AutoGenerateColumns = false;
			DataGridView_TimeEntries.DataSource = TimeEntries;
			DataGridView_TimeEntries.CellValueChanged += DataGridView_TimeEntriesOnCellValueChanged;
		}
		
		public int CurrentRowIndex { get; set; }
		public BindingSource TimeEntries { get; set; }

		private void DataGridView_TimeEntriesOnCellValueChanged(object sender, DataGridViewCellEventArgs cellEventArgs) {
			if (cellEventArgs.ColumnIndex == 1) {
				string description = DataGridView_TimeEntries[cellEventArgs.ColumnIndex, cellEventArgs.RowIndex].Value.ToString();
				TimeEntry timeEntry = GetTimeEntry(cellEventArgs.RowIndex);
				timeEntry.Description = description;
			}

			UpdateTimeEntries();
		}

		private string GetDataFileName() {
			return "C:\\1_Development\\Projects\\JiraWorklogReport\\TimeEntries\\" + DateTime.UtcNow.Date.ToString("yyyy_MM_dd") + ".txt";
		}

		private BindingSource GetTimeEntries(string dataFile) {
			BindingSource bindingSource = new BindingSource();
			//Check if file exists
			if (!File.Exists(dataFile)) {
				File.Create(dataFile);

				bindingSource.DataSource = new BindingList<TimeEntry>();
			} else {
				bindingSource.DataSource = JsonConvert.DeserializeObject<BindingList<TimeEntry>>(File.ReadAllText(dataFile));
			}
			return bindingSource;
		}

		private void Button_CreateReport_Click(object sender, EventArgs e) {
			JiraConnector jiraConnector = new JiraConnector();
			JiraWorklogReport jiraWorklogReport = new JiraWorklogReport();

			jiraWorklogReport.WriteReport(jiraConnector.GetTimeEntries(DateTimePicker_StartDate.Value, DateTimePicker_EndDate.Value));
		}

		private void Button_SaveToJira_Click(object sender, EventArgs e) {
			JiraConnector jiraConnector = new JiraConnector();
			TimeEntries = GetTimeEntries(GetDataFileName());
			
			foreach (TimeEntry timeEntry in TimeEntries) {
				jiraConnector.InsertWorkLogEntry(ConvertToJiraTimeEntry(timeEntry));
			}
		}

		private JiraTimeEntry ConvertToJiraTimeEntry(TimeEntry timeEntry) {
			return new JiraTimeEntry {IssueKey = timeEntry.IssueKey, StartedUTC = timeEntry.Started.ToUniversalTime(), TimeSpent = timeEntry.Duration};
		}
		
		private void GridView_TimeEntries_Click(object sender, DataGridViewCellEventArgs e) {
			if (e.ColumnIndex != 0 && e.ColumnIndex != 5) { //0 is Start/Stop button, 5 is Delete button
				return;
			}

			TimeEntry timeEntry = GetTimeEntry(e.RowIndex);
			if (timeEntry == null ) {
				return;
			}

			if (e.ColumnIndex == 0 && !string.IsNullOrEmpty(timeEntry.Description)) {
				CurrentRowIndex = e.RowIndex;
				if (timeEntry.StartOrStopTimer()) UpdateTimeEntries();
			}

			if (e.ColumnIndex == 5) {
				TimeEntries.List.Remove(timeEntry);
				UpdateTimeEntries();
			}
		}

		private void UpdateTimeEntries() {
			File.WriteAllText(GetDataFileName(), JsonConvert.SerializeObject(TimeEntries.List));
		}

		private TimeEntry GetTimeEntry(int rowIndex) {
			if (DataGridView_TimeEntries.Rows.Count == 0) {
				return null;
			}
			return (TimeEntry) DataGridView_TimeEntries.Rows[rowIndex].DataBoundItem;
		}

		private void Button_AddEntry_Click(object sender, EventArgs e) {
			TimeEntry timeEntry = GetTimeEntry(DataGridView_TimeEntries.Rows.Count - 1);
			if (timeEntry != null) {
				timeEntry.StartOrStopTimer();
			}
			
			TimeEntries.Add(new TimeEntry());
		}
	}
}