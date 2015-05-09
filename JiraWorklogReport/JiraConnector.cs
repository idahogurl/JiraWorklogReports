using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using Newtonsoft.Json;

namespace TogglToJiraSync {
	public class JiraConnector {
		private const string UserName = "rvest";
		private const string Password = "Re&99ba1-2/15";
		private readonly JiraWorklogReport _JiraWorklogReport = new JiraWorklogReport();

		public void GetWorkLogs(DateTime fromDate, DateTime toDate) {
			RestClient client = new RestClient(UserName, Password);

			const string dateFormat = "dd/MMM/yy";

			client.EndPoint =
				string.Format(
					"https://jira.navexglobal.com/rest/timesheet-gadget/1.0/raw-timesheet.json?targetUser=rvest&startDate={0}&endDate={1}",
					fromDate.ToString(dateFormat), toDate.ToString(dateFormat));
			client.UserName = "rvest";
			client.Password = "Re&99ba1-2/15";

			string json = client.MakeRequest("");

			WriteReport(JsonConvert.DeserializeObject<RootObject>(json).Worklogs);
		}

		private void WriteReport(Worklog[] worklogs) {

			DateTime lastStartDateTime = DateTime.MinValue;

			StreamWriter streamWriter = new StreamWriter("C:\\1_Development\\worklogAdp.html");
			using (HtmlWriter htmlWriter = new HtmlWriter(streamWriter)) {
				htmlWriter.WriteFullBeginTag("html");
				WriteHeader(htmlWriter);
				_JiraWorklogReport.WriteBodyTag(htmlWriter);
				List<TimeEntry> timeEntries = GetTimeEntries(worklogs);
				foreach (TimeEntry timeEntry in timeEntries) {
					DateTime startedLocal = timeEntry.StartedLocal;
					if (lastStartDateTime.Date != startedLocal.Date) {

						if (lastStartDateTime != DateTime.MinValue) {
							//if the values are the same then we haven't started a table
							htmlWriter.WriteEndTag("table");
							htmlWriter.WriteBreak();
						}
						//write new table
						_JiraWorklogReport.WriteBeginTableTag(htmlWriter);

						_JiraWorklogReport.WriteRowHtml(startedLocal, htmlWriter);

					}

					//Write the issue name, duration, start time and end time

					_JiraWorklogReport.WriteRowHtml(timeEntry, htmlWriter);

					lastStartDateTime = startedLocal; //hold onto the date, so the new table is only written when a new date is encounterd
				}

				htmlWriter.WriteEndTag("table");
				htmlWriter.WriteEndTag("html");
				htmlWriter.Flush();
			}
		}

		private static void WriteHeader(HtmlWriter htmlWriter) {
			htmlWriter.WriteFullBeginTag("head");
			htmlWriter.WriteFullBeginTag("style");
			htmlWriter.Write("td { padding:20px; margin:16px; }");
			htmlWriter.WriteEndTag("style");
			htmlWriter.WriteEndTag("head");
		}

		private List<TimeEntry> GetTimeEntries(Worklog[] worklogs) {
			List<TimeEntry> entries = new List<TimeEntry>();
			foreach (Worklog worklog in worklogs) {
				foreach (TimeEntry timeEntry in worklog.Entries) {
					timeEntry.IssueKey = worklog.Key;
					timeEntry.KeyWithDescription = worklog.Key + ": " + worklog.Summary;
					entries.Add(timeEntry);
				}
			}
			return entries.OrderBy(e => e.StartedUTC).ToList();
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
}