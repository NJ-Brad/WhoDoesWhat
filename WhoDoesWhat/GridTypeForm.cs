namespace WhoDoesWhat
{
    public partial class GridTypeForm : Form
    {
        public GridTypeForm()
        {
            InitializeComponent();
            GridTypeDescriptions.Add("RACI", "Responsible, Accountable, Consulted, Informed");
            GridTypeDescriptions.Add("RASCI", "Responsible, Accountable, Supportive, Consulted, Informed");
            GridTypeDescriptions.Add("RACI-VS", "Responsible, Accountable, Consulted, Informed, Verifies, Signs Off");
            GridTypeDescriptions.Add("RACIO", "Responsible, Accountable, Consulted, Informed, Omitted");
            GridTypeDescriptions.Add("DRASCI", "Directs, Responsible, Accountable, Supportive, Consulted, Informed");
            GridTypeDescriptions.Add("DACI", "Directs, Accountable, Consulted, Informed");
            GridTypeDescriptions.Add("RATSI", "Responsible, Accountable, Task, Supportive, Informed");
            GridTypeDescriptions.Add("RAPID", "Recommends, Agree, Input, Decide, Perform");

            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
        }

        private void ComboBox2_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                //label1.Text = comboBox2.Items[comboBox2.SelectedIndex].ToString();
                label1.Text = GridTypeDescriptions[comboBox2.Items[comboBox2.SelectedIndex].ToString()];
            }
            //throw new NotImplementedException();
        }

        public string SelectedGridType { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
                DialogResult = DialogResult.None;
                return;
            }

            SelectedGridType = comboBox2.Items[comboBox2.SelectedIndex].ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        Dictionary<string, string> GridTypeDescriptions = new Dictionary<string, string>();
    }
}
