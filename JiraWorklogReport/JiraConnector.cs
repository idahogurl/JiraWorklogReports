using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JiraWorklogReport {
	public class JiraConnector {
		private JiraAuthUser _AuthUser;

		public JiraConnector() {
			const string configIniPath = "C:\\1_Development\\Projects\\JiraWorklogReport\\TimeEntries\\config.ini";
			if (!File.Exists(configIniPath)) {
				using (FileStream fs = File.Create(configIniPath)) {
					fs.Close();
				}
				
				throw new Exception("Missing configuration file");
			}
			_AuthUser = JsonConvert.DeserializeObject<JiraAuthUser>(File.ReadAllText(configIniPath));

			if (_AuthUser == null) {
				_AuthUser = new JiraAuthUser();
				try {
					File.WriteAllText(configIniPath, JsonConvert.SerializeObject(_AuthUser));
				} catch (Exception e) {
					MessageBox.Show(e.Message);
				}
			}

			if (string.IsNullOrEmpty(_AuthUser.Username) || string.IsNullOrEmpty(_AuthUser.Password)) { 
				throw new Exception("Empty configuration file");
			}
		}

		public List<JiraTimeEntry> GetTimeEntries(DateTime fromDate, DateTime toDate) {
			RestClient client = new RestClient(_AuthUser.Username, _AuthUser.Password);

			const string dateFormat = "dd/MMM/yy";

			client.EndPoint =
				$"{_AuthUser.JiraUrl}/rest/timesheet-gadget/1.0/raw-timesheet.json?" +
				$"targetUser={_AuthUser.Username}&startDate={fromDate.ToString(dateFormat)}&endDate={toDate.ToString(dateFormat)}";


			string json = client.MakeRequest("");

			if (json == null) { //a connection error happened
				return new List<JiraTimeEntry>();
			}
			return GetTimeEntries(JsonConvert.DeserializeObject<RootObject>(json).Worklogs);
		}

		private List<JiraTimeEntry> GetTimeEntries(Worklog[] worklogs) {
			List<JiraTimeEntry> entries = new List<JiraTimeEntry>();
			foreach (Worklog worklog in worklogs) {
				foreach (JiraTimeEntry timeEntry in worklog.Entries) {
					timeEntry.IssueKey = worklog.Key;
					timeEntry.KeyWithDescription = worklog.Key + ": " + worklog.Summary;
					entries.Add(timeEntry);
				}
			}
			return entries.OrderBy(e => e.StartedUTC).ToList();
		}

		public void InsertWorkLogEntry(JiraTimeEntry timeEntry) {
			RestClient client = new RestClient(_AuthUser.Username, _AuthUser.Password);
			client.EndPoint = $"{_AuthUser.JiraUrl}/rest/api/2/issue/{timeEntry.IssueKey}/worklog";

			string timeEntryDisplay = timeEntry.IssueKey + ": " + timeEntry.StartedLocal + " (" + timeEntry.TimeSpentDisplay + ")";

			if (IsSaved(client, timeEntry)) {
				MessageBox.Show(timeEntryDisplay, "Already Saved");
			} else {
				client.Method = HttpVerb.POST;
				client.PostData = JsonConvert.SerializeObject(
					new WorkLogEntry { timeSpent = timeEntry.TimeSpentDisplay, started = GetStartedDateTime(timeEntry.StartedLocal) });

				client.MakeRequest();

				MessageBox.Show(timeEntryDisplay, "Saved");
			}
		}

		private bool IsSaved(RestClient client, JiraTimeEntry timeEntry) {
			client.Method = HttpVerb.GET;
			string response = client.MakeRequest();

			JObject deserializeObject = (JObject)JsonConvert.DeserializeObject(response);
			foreach (JToken worklog in deserializeObject["worklogs"]) {
				JToken started = worklog["started"];
				JToken timeSpent = worklog["timeSpent"];
			
				if (started.ToObject<DateTime>().ToString("s") == timeEntry.StartedLocal.ToString("s") && timeEntry.TimeSpentDisplay == timeSpent.ToObject<string>()) {
					return true;
				}
			}
			return false;
		}

		private string GetStartedDateTime(DateTime started) {
			string timezone = started.ToString("zzz").Replace(":", "");
			return started.ToString("yyyy-MM-ddTHH:mm:ss.fff") + timezone;
		}

		public class HtmlWriter : HtmlTextWriter {
			public HtmlWriter(TextWriter writer) : base(writer) {}
			public HtmlWriter(TextWriter writer, string tabString) : base(writer, tabString) {}

			public void EndStyleTag(HtmlTextWriter htmlWriter) {
				htmlWriter.Write(DoubleQuoteChar);
				htmlWriter.Write(TagRightChar);
			}

			public void BeginStyleTag(HtmlTextWriter htmlWriter) {
				htmlWriter.Write(SpaceChar);
				htmlWriter.Write("style");
				htmlWriter.Write(EqualsDoubleQuoteString);
			}
		}
	}

	public class WorkLogEntry {
		public string timeSpent { get; set; }
		public string started { get; set; }
	}
}