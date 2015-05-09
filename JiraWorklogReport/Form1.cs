using System;
using System.Windows.Forms;
using TogglToJiraSync;

namespace JiraWorklogReport {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void Button_GetEntries_Click(object sender, EventArgs e) {
			JiraConnector jiraConnector = new JiraConnector();
			//TogglConnector connector = new TogglConnector();

			jiraConnector.GetWorkLogs(DateTimePicker_StartDate.Value, DateTimePicker_EndDate.Value);
		}

		private void Button_SaveToJira_Click(object sender, EventArgs e) {
			//Write to txt file the logs
		}
	}
}