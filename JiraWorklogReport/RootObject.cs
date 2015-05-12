using System;
using Newtonsoft.Json;

namespace JiraWorklogReport {
	public class RootObject
	{
		[JsonProperty(PropertyName = "worklog")]
		public Worklog[] Worklogs;

		[JsonProperty(PropertyName = "startDate")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime Started { get; set; }

		[JsonProperty(PropertyName = "endDate")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime Ended { get; set; }
	}
}