using System;
using System.Web.UI;

namespace TogglToJiraSync {
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

		public void WriteRowHtml(TimeEntry timeEntry, JiraConnector.HtmlWriter htmlWriter) {
			const string dateFormat = "hh:mm tt";

			htmlWriter.WriteBeginTag("tr");
			htmlWriter.BeginStyleTag(htmlWriter);
			htmlWriter.WriteStyleAttribute("border-bottom", "1px solid #222222");
			htmlWriter.EndStyleTag(htmlWriter);

			htmlWriter.WriteBeginTag("td");
			htmlWriter.WriteAttribute("width", "300");
			WriteTextStyle(htmlWriter, "#222222", "14px");
			htmlWriter.Write(timeEntry.KeyWithDescription);
			htmlWriter.WriteEndTag("td");

			htmlWriter.WriteBeginTag("td");
			htmlWriter.WriteAttribute("width", "100");
			WriteTextStyle(htmlWriter, "#222222", "14px");
			htmlWriter.Write(timeEntry.TimeSpentDisplay);
			htmlWriter.WriteEndTag("td");

			htmlWriter.WriteBeginTag("td");
			WriteTextStyle(htmlWriter, "#888888", "11px");
			htmlWriter.Write(timeEntry.StartedLocal.ToString(dateFormat) + " - " + timeEntry.EndedLocal.ToString(dateFormat));
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
	}
}