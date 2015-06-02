using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace JiraWorklogReport {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();

			TimeEntries = GetTimeEntries(GetDataFileName(DateTime.Now.Date));

			DataGridView_TimeEntries.AutoGenerateColumns = false;
			DataGridView_TimeEntries.DataSource = TimeEntries;
			DataGridView_TimeEntries.CellEndEdit += DataGridView_TimeEntriesOnCellEndEdit;
			Label_Focus.GotFocus += Label_FocusOnGotFocus;
		}

		private void Label_FocusOnGotFocus(object sender, EventArgs eventArgs) {
			for (int rowIndex = 0; rowIndex < DataGridView_TimeEntries.Rows.Count; rowIndex++) {
				DataGridViewRow row = DataGridView_TimeEntries.Rows[rowIndex];
				TimeEntry timeEntry = GetTimeEntry(rowIndex);
				timeEntry.Description = row.Cells[1].Value.ToString();
				timeEntry.StartedString = row.Cells[2].Value.ToString();
				timeEntry.EndedString = row.Cells[3].Value.ToString();

				if (string.IsNullOrEmpty(timeEntry.EndedString)) {
					timeEntry.Duration = 0;
				} else {
					timeEntry.SetDuration();
				}
			}
			UpdateTimeEntries();
		}

		public int CurrentRowIndex { get; set; }
		public BindingSource TimeEntries { get; set; }

		private void DataGridView_TimeEntriesOnCellEndEdit(object sender, DataGridViewCellEventArgs cellEventArgs) {
			Label_Focus.Focus();
		}

		private string GetDataFileName(DateTime dateTime) {
			return "C:\\1_Development\\Projects\\JiraWorklogReport\\TimeEntries\\" + dateTime.ToString("yyyy_MM_dd") + ".txt";
		}

		private BindingSource GetTimeEntries(string dataFile) {
			BindingSource bindingSource = new BindingSource();
			//Check if file exists
			if (!File.Exists(dataFile)) {
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
			TimeEntries = GetTimeEntries(GetDataFileName(DateTimePicker_TimeEntriesDate.Value.Date));
			
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
			if (timeEntry == null) {
				return;
			}

			if (e.ColumnIndex == 0) {
				if (timeEntry.StartOrStop(DateTime.Now)) {
					UpdateTimeEntries();
				}
			}

			if (e.ColumnIndex == 5) {
				TimeEntries.List.Remove(timeEntry);
				DataGridView_TimeEntries.DataSource = TimeEntries;
				UpdateTimeEntries();
			}
		}

		private void UpdateTimeEntries() {
			File.WriteAllText(GetDataFileName(DateTimePicker_TimeEntriesDate.Value.Date), JsonConvert.SerializeObject(TimeEntries.List));
			DataGridView_TimeEntries.DataSource = TimeEntries;
		}

		private TimeEntry GetTimeEntry(int rowIndex) {
			if (DataGridView_TimeEntries.Rows.Count == 0) {
				return null;
			}
			return (TimeEntry) DataGridView_TimeEntries.Rows[rowIndex].DataBoundItem;
		}

		private void Button_AddEntry_Click(object sender, EventArgs e) {
			TimeEntry timeEntry = GetTimeEntry(DataGridView_TimeEntries.Rows.Count - 1);
			DateTime startedDateTime = DateTimePicker_TimeEntriesDate.Value;
			
			if (timeEntry != null) {
				timeEntry.StartOrStop(startedDateTime);
			}

			DateTime now = DateTime.Now;
			DateTime started = new DateTime(startedDateTime.Year, startedDateTime.Month, startedDateTime.Day, now.Hour, now.Minute, now.Second);
			TimeEntries.Add(new TimeEntry() { Started =  started} );

			UpdateTimeEntries();
		}

		private void DateTimePicker_TimeEntriesDate_ValueChanged(object sender, EventArgs e) {
			DataGridView_TimeEntries.DataSource = GetTimeEntries(GetDataFileName(DateTimePicker_TimeEntriesDate.Value.Date));
        }
	}
}