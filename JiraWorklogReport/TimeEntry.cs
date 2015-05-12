using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JiraWorklogReport.Annotations;

namespace JiraWorklogReport {
	public class TimeEntry : INotifyPropertyChanged {
		private string _Description;
		private DateTime _Ended;
		private DateTime _Started;
		private Stopwatch _Stopwatch;
		private string _DurationDisplay;

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
			get { return _DurationDisplay; }
			set {
				if (value == _DurationDisplay) {
					return;
				}
				_DurationDisplay = value;
				OnPropertyChanged();
			}
		}

		public int Duration { get; set; }

		public string IssueKey => Description.Split(':')[0];

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void StartTimer() {
			_Stopwatch = new Stopwatch();
			_Stopwatch.Start();

			Started = DateTime.Now;
		}

		private void StopTimer() {
			Ended = DateTime.Now;
			if (_Stopwatch == null) {
				TimeSpan timeSpan = Ended.Subtract(Started);
				Duration = (int) timeSpan.TotalSeconds;
				DurationDisplay = JiraTimeEntry.GetTimeSpentDisplay(timeSpan);
			} else {
				_Stopwatch.Stop();
				Duration = (int) _Stopwatch.Elapsed.TotalSeconds; //Create an entry
				DurationDisplay = JiraTimeEntry.GetTimeSpentDisplay(_Stopwatch.Elapsed);

				_Stopwatch = null;
			}
		}

		public bool StartOrStopTimer() {
			if(Started == DateTime.MinValue) {
				StartTimer();
				return true;
			}

			if (Duration == 0) {
				StopTimer();
				return true;
			}
			return false;
		}
	}
}