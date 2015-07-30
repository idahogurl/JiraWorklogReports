using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JiraWorklogReport.Annotations;

namespace JiraWorklogReport {
	public class TimeEntry : INotifyPropertyChanged {
		private string _Description;
		private TimeSpan _Duration;
		private DateTime _Ended;
		private DateTime _Started;

		public DateTime Started {
			get { return _Started; }
			set {
				if (value == _Started) {
					return;
				}
				_Started = value;
				OnPropertyChanged();
			}
		}

		public DateTime Ended {
			get { return _Ended; }
			set {
				if (value == _Ended) {
					return;
				}
				_Ended = value;
				OnPropertyChanged();
			}
		}

		public string StartedString {
			get {
				if (Started == DateTime.MinValue) {
					return null;
				}
				return Started.ToShortDateString() + " " + Started.ToShortTimeString();
			}

			set {
				if (string.IsNullOrWhiteSpace(value)) {
					Started = DateTime.MinValue;
					Ended = DateTime.MinValue;
					Duration = new TimeSpan();
				} else {
					Started = DateTime.Parse(value);
				}
			}
		}

		public string EndedString {
			get {
				if (Ended == DateTime.MinValue) {
					return null;
				}
				return Ended.ToShortDateString() + " " + Ended.ToShortTimeString();
			}
			set {
				if (string.IsNullOrWhiteSpace(value)) {
					Ended = DateTime.MinValue;
					Duration = new TimeSpan();
				} else {
					Ended = DateTime.Parse(value);
					Duration = GetDurationTimeSpan(Started, Ended);
				}
			}
		}

		public string Description {
			get { return _Description; }
			set {
				if (value == _Description) {
					return;
				}
				_Description = value;
				OnPropertyChanged();
			}
		}

		public string DurationDisplay {
			get {
				return JiraTimeEntry.GetTimeSpentDisplay(Duration);
			}
		}

		public TimeSpan Duration {
			get { return _Duration; }
			set {
				if (value == Duration) {
					return;
				}
				_Duration = value;
				OnPropertyChanged();
				_Duration = value;
			}
		}

		public string IssueKey {
			get {
				if (Description != null && Description.Contains(":")) {
					return Description.Split(':')[0];
				}
				return null;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void Stop(DateTime stopDateTime) {
			Ended = stopDateTime;
			Duration = GetDurationTimeSpan(Started, Ended);
		}

	    public void Start() {
	        
	    }

		public static TimeSpan GetDurationTimeSpan(DateTime startDateTime, DateTime endDateTime) {
			if (startDateTime == DateTime.MinValue || endDateTime == DateTime.MinValue) {
				return new TimeSpan();
			}
			return endDateTime.Subtract(startDateTime);
		}
	}
}