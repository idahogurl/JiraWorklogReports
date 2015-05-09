using System;
using Newtonsoft.Json;

namespace TogglToJiraSync {
	public class TimeEntry {
		[JsonProperty(PropertyName = "startDate")]
		[JsonConverter(typeof (UnixDateTimeConverter))]
		public DateTime StartedUTC { get; set; }

		[JsonProperty(PropertyName = "created")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Created { get; set; }

		[JsonProperty(PropertyName = "updated")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Updated { get; set; }

		[JsonProperty(PropertyName = "timeSpent")]
		public int TimeSpent { get; set; }

		[JsonProperty(PropertyName = "comment")]
		public string Comment { get; set; }

		[JsonProperty(PropertyName = "author")]
		public string Author { get; set; }

		[JsonProperty(PropertyName = "authorFullName")]
		public string AuthorFullName { get; set; }

		[JsonProperty(PropertyName = "updateAuthor")]
		public string UpdateAuthor { get; set; }

		[JsonProperty(PropertyName = "updateAuthorFullName")]
		public string UpdateAuthorFullName { get; set; }

		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }

		public DateTime EndedLocal => StartedLocal.AddSeconds(TimeSpent);

		public string KeyWithDescription { get; set; }

		public string IssueKey { get; set; }

		public DateTime StartedLocal => StartedUTC.ToLocalTime();

		public string TimeSpentDisplay {
			get {
				TimeSpan timeSpentTimeSpan = TimeSpan.FromSeconds(TimeSpent); ;
				if (timeSpentTimeSpan.Hours == 0) {
					return timeSpentTimeSpan.Minutes + "m";
				}
				return timeSpentTimeSpan.Hours + "h " + timeSpentTimeSpan.Minutes + "m";
			}
		}
	}
}