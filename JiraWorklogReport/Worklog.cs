using Newtonsoft.Json;

namespace TogglToJiraSync {
	public class Worklog
	{
		[JsonProperty(PropertyName = "key")]
		public string Key { get; set; }

		[JsonProperty(PropertyName = "summary")]
		public string Summary { get; set; }

		[JsonProperty(PropertyName = "entries")]
		public TimeEntry[] Entries { get; set; }
	}
}