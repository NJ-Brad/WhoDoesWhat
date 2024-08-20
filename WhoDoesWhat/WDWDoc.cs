namespace WhoDoesWhat
{
    public class WDWDoc
    {
        public string GridType { get; set; } = "RACI";
        public List<string> Tasks { get; set; } = new List<string>();
        public List<string> People_Roles { get; set; } = new List<string>();
        public List<WDWItem> Items { get; set; } = new List<WDWItem>();
    }

    public class WDWItem
    {
        public string Task { get; set; }
        public string Person_Role { get; set; }
        public List<string> Responsibilities { get; set; } = new List<string>();
    }
}
//         [YamlIgnore]
