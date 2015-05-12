using Newtonsoft.Json;

namespace JiraWorklogReport {
	public class Worklog
	{
		[JsonProperty(PropertyName = "key")]
		public string Key { get; set; }

		[JsonProperty(PropertyName = "summary")]
		public string Summary { get; set; }

		[JsonProperty(PropertyName = "entries")]
		public JiraTimeEntry[] Entries { get; set; }
	}
}