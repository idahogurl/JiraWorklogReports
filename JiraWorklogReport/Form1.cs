using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using JiraWorklogReport.Properties;
using Newtonsoft.Json;

namespace JiraWorklogReport {
	public partial class Form_Main : Form {
		public Form_Main() {
			InitializeComponent();

			TimeEntries = GetTimeEntries(GetDataFileName(DateTimePicker_TimeEntriesDate.Value));

			DataGridView_TimeEntries.AutoGenerateColumns = false;
			DataGridView_TimeEntries.DataSource = TimeEntries;
			DataGridView_TimeEntries.CellContentClick += DataGridView_TimeEntriesOnCellContentClick;
			DataGridView_TimeEntries.CellValueChanged += DataGridView_TimeEntriesOnCellValueChanged;

			Button_StartStop.Text = GetButtonStartStopText();
		}

		private int LastGridViewRowIndex => DataGridView_TimeEntries.Rows.GetLastRow(DataGridViewElementStates.None);
		public int CurrentRowIndex { get; set; }
		public BindingSource TimeEntries { get; set; }

		private string GetButtonStartStopText() {
			TimeEntry timeEntry = GetTimeEntry(LastGridViewRowIndex);
			if (timeEntry != null && timeEntry.Started == DateTime.MinValue) {
				return Resources.Start;
			}
			if (timeEntry != null && timeEntry.Ended == DateTime.MinValue) {
				return Resources.Stop;
			}
			return Resources.Start;
		}

		private void DataGridView_TimeEntriesOnCellContentClick(object sender, DataGridViewCellEventArgs eventArgs) {
			string columnName = DataGridView_TimeEntries.Columns[eventArgs.ColumnIndex].Name;
			if (columnName == "Delete") {
				TimeEntry timeEntry = GetTimeEntry(eventArgs.RowIndex);
				if (timeEntry == null) {
					return;
				}
				TimeEntries.Remove(timeEntry);
				WriteDataFile();
				Button_StartStop.Text = GetButtonStartStopText();
			}
		}

		private void DataGridView_TimeEntriesOnCellValueChanged(object sender, DataGridViewCellEventArgs cellEventArgs) {
			if (DataGridView_TimeEntries.Columns[cellEventArgs.ColumnIndex].Name  == "Description") {
				
				TimeEntry timeEntry = GetTimeEntry(cellEventArgs.RowIndex);
				timeEntry.Description = DataGridView_TimeEntries.CurrentCell.Value.ToString();
				WriteDataFile();
			}

			if (DataGridView_TimeEntries.Columns[cellEventArgs.ColumnIndex].Name  == "Started") {
				
				TimeEntry timeEntry = GetTimeEntry(cellEventArgs.RowIndex);

				if (DataGridView_TimeEntries.CurrentCell.Value == null) {
					timeEntry.StartedString = null;
					timeEntry.EndedString = null;
					Button_StartStop.Text = GetButtonStartStopText();
				} else {
					timeEntry.StartedString = DataGridView_TimeEntries.CurrentCell.Value.ToString();
				}

				//Currently the Started cell is in edit mode. Need to make Duration in edit mode to modify its value programmatically.
				BeginEdit("Duration");
				timeEntry.Duration = TimeEntry.GetDurationTimeSpan(timeEntry.Started, timeEntry.Ended);
				EndEdit();
			}

			if (DataGridView_TimeEntries.Columns[cellEventArgs.ColumnIndex].Name  == "Ended") {
				TimeEntry timeEntry = GetTimeEntry(cellEventArgs.RowIndex);
				if (DataGridView_TimeEntries.CurrentCell.Value == null) {
					timeEntry.EndedString = null;
					Button_StartStop.Text = GetButtonStartStopText();
				} else {
					timeEntry.EndedString = DataGridView_TimeEntries.CurrentCell.Value.ToString();
				}

				//Currently the Started cell is in edit mode. Need to make Duration in edit mode to modify its value programmatically.
				BeginEdit("Duration");
				timeEntry.Duration = TimeEntry.GetDurationTimeSpan(timeEntry.Started, timeEntry.Ended);
				EndEdit();
			}
			
		}

		private void BeginEdit(string columnName) {
			DataGridView_TimeEntries.CurrentCell = DataGridView_TimeEntries.Rows[LastGridViewRowIndex].Cells[columnName];
			DataGridView_TimeEntries.BeginEdit(true);
		}

		private void EndEdit() {
			DataGridView_TimeEntries.EndEdit();
			WriteDataFile();

			TimeEntries.ResetBindings(false);
		}

		private string GetDataFileName(DateTime dateTime) {
			return "C:\\1_Development\\Projects\\JiraWorklogReport\\TimeEntries\\" + dateTime.ToString("yyyy_MM_dd") + ".txt";
		}

		private BindingSource GetTimeEntries(string dataFile) {
			BindingSource bindingSource = new BindingSource();
			//Check if file exists
			if (!File.Exists(dataFile)) {
				using (FileStream fs = File.Create(dataFile)) {
					fs.Close();
				}

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
			TimeEntries.DataSource = GetTimeEntries(GetDataFileName(DateTimePicker_TimeEntriesDate.Value));

			foreach (TimeEntry timeEntry in TimeEntries) {
				if (timeEntry.IssueKey == null) {
					MessageBox.Show("Skipped");
				} else {
					jiraConnector.InsertWorkLogEntry(ConvertToJiraTimeEntry(timeEntry));
				}
				}
			}

		private JiraTimeEntry ConvertToJiraTimeEntry(TimeEntry timeEntry) {
			return new JiraTimeEntry {
				IssueKey = timeEntry.IssueKey,
				StartedUTC = timeEntry.Started.ToUniversalTime(),
				TimeSpent = (int) timeEntry.Duration.TotalSeconds
			};
		}

		private void WriteDataFile() {
			File.WriteAllText(GetDataFileName(DateTimePicker_TimeEntriesDate.Value), JsonConvert.SerializeObject(TimeEntries.List));
		}

		private TimeEntry GetTimeEntry(int rowIndex) {
			if (DataGridView_TimeEntries.Rows.Count == 0) {
				return null;
			}
			return (TimeEntry) DataGridView_TimeEntries.Rows[rowIndex].DataBoundItem;
		}

		private void Button_AddEntry_Click(object sender, EventArgs e) {
			TimeEntry timeEntry = GetTimeEntry(LastGridViewRowIndex);

			//Stop the previous time entry
			if (timeEntry != null) {
				timeEntry.Stop();
			}

			TimeEntries.Add(new TimeEntry());
			WriteDataFile();
		}

		private void Button_StartStop_Click(object sender, EventArgs e) {
			//Place the Started cell into Edit Mode
			BeginEdit("Started");
			
			TimeEntry timeEntry = GetTimeEntry(LastGridViewRowIndex);
			if (timeEntry.Started == DateTime.MinValue) {
				timeEntry.Started = DateTimePicker_TimeEntriesDate.Value;
				Button_StartStop.Text = Resources.Stop;
			} else {
				timeEntry.Stop();
				Button_StartStop.Text = Resources.Start;
			}
			EndEdit();
		}

		private void DateTimePicker_TimeEntriesDate_ValueChanged(object sender, EventArgs e) {
			TimeEntries.DataSource = GetTimeEntries(GetDataFileName(DateTimePicker_TimeEntriesDate.Value));
			TimeEntries.ResetBindings(false);
			Button_StartStop.Text = GetButtonStartStopText();
		}
	}
}