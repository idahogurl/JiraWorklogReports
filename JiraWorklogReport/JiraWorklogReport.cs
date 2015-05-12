using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

namespace JiraWorklogReport {
	public class JiraWorklogReport {
		public void WriteBodyTag(JiraConnector.HtmlWriter htmlWriter) {
			htmlWriter.WriteBeginTag("body");
			htmlWriter.BeginStyleTag(htmlWriter);
			htmlWriter.WriteStyleAttribute("background-color", "#F2F2F2");
			htmlWriter.WriteStyleAttribute("font-family", "'Open Sans', Arial, sans-serif'");
			htmlWriter.EndStyleTag(htmlWriter);
		}

		public void WriteBeginTableTag(JiraConnector.HtmlWriter htmlWriter) {
			htmlWriter.WriteBeginTag("table"); // Begin #1
			htmlWriter.BeginStyleTag(htmlWriter);
			htmlWriter.WriteStyleAttribute("width", "100%");
			htmlWriter.WriteStyleAttribute("border-collapse", "collapse");
			htmlWriter.EndStyleTag(htmlWriter);
		}

		public void WriteRowHtml(DateTime date, JiraConnector.HtmlWriter htmlWriter) {
			htmlWriter.WriteFullBeginTag("tr");

			htmlWriter.WriteBeginTag("td");
			htmlWriter.WriteAttribute("colspan", "3");
			WriteTextStyle(htmlWriter, "#222222", "21px");
			htmlWriter.Write(date.ToString("D"));
			htmlWriter.WriteEndTag("span");
			htmlWriter.WriteEndTag("td");
			htmlWriter.WriteEndTag("tr");
		}

		public void WriteRowHtml(JiraTimeEntry jiraTimeEntry, JiraConnector.HtmlWriter htmlWriter) {
			const string dateFormat = "hh:mm tt";

			htmlWriter.WriteBeginTag("tr");
			htmlWriter.BeginStyleTag(htmlWriter);
			htmlWriter.WriteStyleAttribute("border-bottom", "1px solid #222222");
			htmlWriter.EndStyleTag(htmlWriter);

			htmlWriter.WriteBeginTag("td");
			htmlWriter.WriteAttribute("width", "300");
			WriteTextStyle(htmlWriter, "#222222", "14px");
			htmlWriter.Write(jiraTimeEntry.KeyWithDescription);
			htmlWriter.WriteEndTag("td");

			htmlWriter.WriteBeginTag("td");
			htmlWriter.WriteAttribute("width", "100");
			WriteTextStyle(htmlWriter, "#222222", "14px");
			htmlWriter.Write(jiraTimeEntry.TimeSpentDisplay);
			htmlWriter.WriteEndTag("td");

			htmlWriter.WriteBeginTag("td");
			WriteTextStyle(htmlWriter, "#888888", "11px");
			htmlWriter.Write(jiraTimeEntry.StartedLocal.ToString(dateFormat) + " - " + jiraTimeEntry.EndedLocal.ToString(dateFormat));
			htmlWriter.WriteEndTag("td");

			htmlWriter.WriteEndTag("tr");
		}

		public void WriteTextStyle(HtmlTextWriter htmlWriter, string color, string fontSize) {
			htmlWriter.Write(HtmlTextWriter.SpaceChar);
			htmlWriter.Write("style");
			htmlWriter.Write(HtmlTextWriter.EqualsDoubleQuoteString);
			htmlWriter.WriteStyleAttribute("color", color);
			htmlWriter.WriteStyleAttribute("font", fontSize + " 'Open Sans', Arial, sans-serif'");
			htmlWriter.WriteStyleAttribute("padding", "5px");
			htmlWriter.Write(HtmlTextWriter.DoubleQuoteChar);
			htmlWriter.Write(HtmlTextWriter.TagRightChar);
		}

		public void WriteReport(List<JiraTimeEntry> timeEntries) {

			DateTime lastStartDateTime = DateTime.MinValue;

			StreamWriter streamWriter = new StreamWriter("C:\\1_Development\\worklogAdp.html");
			using (JiraConnector.HtmlWriter htmlWriter = new JiraConnector.HtmlWriter(streamWriter)) {
				htmlWriter.WriteFullBeginTag("html");
				WriteHeader(htmlWriter);
				WriteBodyTag(htmlWriter);

				foreach (JiraTimeEntry timeEntry in timeEntries) {
					DateTime startedLocal = timeEntry.StartedLocal;
					if (lastStartDateTime.Date != startedLocal.Date) {

						if (lastStartDateTime != DateTime.MinValue) {
							//if the values are the same then we haven't started a table
							htmlWriter.WriteEndTag("table");
							htmlWriter.WriteBreak();
						}
						//write new table
						WriteBeginTableTag(htmlWriter);

						WriteRowHtml(startedLocal, htmlWriter);

					}

					//Write the issue name, duration, start time and end time

					WriteRowHtml(timeEntry, htmlWriter);

					lastStartDateTime = startedLocal; //hold onto the date, so the new table is only written when a new date is encounterd
				}

				htmlWriter.WriteEndTag("table");
				htmlWriter.WriteEndTag("html");
				htmlWriter.Flush();
			}
		}

		private static void WriteHeader(JiraConnector.HtmlWriter htmlWriter) {
			htmlWriter.WriteFullBeginTag("head");
			htmlWriter.WriteFullBeginTag("style");
			htmlWriter.Write("td { padding:20px; margin:16px; }");
			htmlWriter.WriteEndTag("style");
			htmlWriter.WriteEndTag("head");
		}
	}
}