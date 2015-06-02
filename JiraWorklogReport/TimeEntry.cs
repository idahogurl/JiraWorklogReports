using System;
using System.Globalization;

namespace JiraWorklogReport {
	public class TimeEntry {
		public DateTime Started { get; set; }
		public string StartedString { get { return Started.ToString(new CultureInfo("en-US")); }
			set {
				if (!string.IsNullOrEmpty(value)) {
					Started = DateTime.Parse(value);
				}
			}
		}

		public string EndedString {
			get {
				if (Ended == DateTime.MinValue) {
					return null;
				}
				return Ended.ToString(new CultureInfo("en-US"));
			}
			set {
				if (!string.IsNullOrEmpty(value)) {
					Ended = DateTime.Parse(value);
				}
			}
		}

		public DateTime Ended { get; set; }
		public string Description { get; set; }
		public string DurationDisplay { get; set; }
		public int Duration { get; set; }

		public string IssueKey {
			get {
				if (string.IsNullOrEmpty(Description)) {
					return null;
				}
				return Description.Split(':')[0];
			}
		}

		private void Start(DateTime startedDateTime) {
			Started = startedDateTime;
		}

		private void Stop() {
			Ended = DateTime.Now;
			SetDuration();
		}

		public bool StartOrStop(DateTime startedDateTime) {
			if (Started == DateTime.MinValue) {
				Start(startedDateTime);
				return true;
			}

			if (Duration == 0) {
				Stop();
				return true;
			}
			return false;
		}

		public void SetDuration() {
			TimeSpan timeSpan = Ended.Subtract(Started);
			Duration = (int) timeSpan.TotalSeconds;
			DurationDisplay = JiraTimeEntry.GetTimeSpentDisplay(timeSpan);
		}
	}
}