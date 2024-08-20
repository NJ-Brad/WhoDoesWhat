namespace WhoDoesWhat
{
    public class ComboItem
    {
        public string Id { get; set; }
        public string Display { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Color Color { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ComboItems
    {
        static ComboItem Blank = new() { Id = "", Display = "", Name = "", Description = "Please make a selection" };
        static ComboItem R = new() { Id = "R", Display = "R", Name = "Responsible", Description = "Think of this person as the project owner" };
        static ComboItem A = new() { Id = "A", Display = "A", Name = "Accountable", Description = "This person has final control over a project task and the resources associated with it" };
        static ComboItem C = new() { Id = "C", Display = "C", Name = "Consulted", Description = "Those who are Consulted are there to help the Responsible person finish tasks with success" };
        static ComboItem I = new() { Id = "I", Display = "I", Name = "Informed", Description = "These are people who need to be kept in the loop during the project life-cycle" };
        static ComboItem Su = new() { Id = "Su", Display = "Su", Name = "Supportive", Description = "Supportive people are able to provide resources to the Responsible project team members" };
        static ComboItem V = new() { Id = "V", Display = "V", Name = "Verification", Description = "Will scrutinize the quality and correctness of a given specific task" };
        static ComboItem Si = new() { Id = "Si", Display = "Si", Name = "Sign-Off", Description = "Will provide the task completion verdict by approving it" };
        static ComboItem O = new() { Id = "O", Display = "O", Name = "Omitted", Description = "explicit mentioning of the roles excluded from the scope of a certain task" };
        static ComboItem D = new() { Id = "D", Display = "D", Name = "Driver", Description = "Source of (usually delegated) task" };
        static ComboItem T = new() { Id = "T", Display = "T", Name = "Task", Description = "Who will perform the task" };

        static ComboItem Rec = new() { Id = "Rec", Display = "Rec", Name = "Recommend", Description = "Recommends a course of action or present a series of options" };
        static ComboItem Agr = new() { Id = "Agr", Display = "Agr", Name = "Agree", Description = "The people who must agree with a recommendation before it can move forward" };
        static ComboItem Inp = new() { Id = "Inp", Display = "Inp", Name = "Input", Description = "The people who can (should) make input into the decision" };
        static ComboItem Dec = new() { Id = "Dec", Display = "Dec", Name = "Decide", Description = "This role is the decision maker and it should be a single person." };
        static ComboItem Per = new() { Id = "Per", Display = "Per", Name = "Perform", Description = "The people who will perform the decision" };


        public static List<ComboItem> RACI = new List<ComboItem> { R, A, C, I };
        public static List<ComboItem> RASCI = new List<ComboItem> { R, A, Su, C, I };
        public static List<ComboItem> RACI_VS = new List<ComboItem> { R, A, C, I, V, Si };
        public static List<ComboItem> RACIO = new List<ComboItem> { R, A, C, I, O };
        public static List<ComboItem> DRASCI = new List<ComboItem> { D, R, A, Su, C, I };
        public static List<ComboItem> DACI = new List<ComboItem> { D, A, C, I };
        public static List<ComboItem> RATSI = new List<ComboItem> { R, A, T, Su, I };
        public static List<ComboItem> RAPID = new List<ComboItem> { Rec, Agr, Inp, Dec, Per };

    }
}
