using System.Diagnostics;
using System.Text;

namespace WhoDoesWhat
{
    public partial class Form1 : Form
    {
        private string fileName = string.Empty;
        WDWDoc document = new WDWDoc();
        FileManager fileManager = new FileManager();
        BrowserHelper browserHelper = new BrowserHelper();
        List<CheckBox> responsibilityButtons = new();

        public Form1()
        {
            InitializeComponent();
            fileManager.Filter = "Who Does What Files (*.wdw)|*.wdw|All files (*.*)|*.*";

            responsibilityButtons.Add(checkBox1);
            responsibilityButtons.Add(checkBox2);
            responsibilityButtons.Add(checkBox3);
            responsibilityButtons.Add(checkBox4);
            responsibilityButtons.Add(checkBox5);
            responsibilityButtons.Add(checkBox6);

            checkBox1.CheckedChanged += CheckBox_CheckedChanged;
            checkBox2.CheckedChanged += CheckBox_CheckedChanged;
            checkBox3.CheckedChanged += CheckBox_CheckedChanged;
            checkBox4.CheckedChanged += CheckBox_CheckedChanged;
            checkBox5.CheckedChanged += CheckBox_CheckedChanged;
            checkBox6.CheckedChanged += CheckBox_CheckedChanged;
        }

        private void CheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (programaticSetting)
            {
                return;
            }
            List<string> checkedItems = new List<string>();
            foreach (CheckBox cb in responsibilityButtons)
            {
                if (cb.Checked)
                {
                    checkedItems.Add(cb.Text);
                }
            }
            UpdateItem(checkedItems);

            //    // item does not get checked until after this call
            //    List<string> checkedItems = new List<string>();
            //    foreach (object item in checkedListBox1.CheckedItems)
            //    {
            //        checkedItems.Add(item.ToString());
            //    }
            //    // add
            //    if ((e.CurrentValue == CheckState.Unchecked) && (e.NewValue == CheckState.Checked))
            //    {
            //        checkedItems.Add(checkedListBox1.Items[e.Index].ToString());
            //    }
            //    // remove
            //    if ((e.CurrentValue == CheckState.Checked) && (e.NewValue == CheckState.Unchecked))
            //    {
            //        //checkedItems.Add(checkedListBox1.Items[e.Index].ToString());
            //        checkedItems.Remove(checkedListBox1.Items[e.Index].ToString());
            //    }
            //    UpdateItem(checkedItems);
        }

        private void ShowDocument()
        {
            comboBox2.SelectedIndex = comboBox2.FindStringExact(document.GridType);

            listBox1.Items.Clear();
            foreach (string str in document.Tasks)
            {
                listBox1.Items.Add(str);
            }

            listBox2.Items.Clear();
            foreach (string str in document.People_Roles)
            {
                listBox2.Items.Add(str);
            }
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            await browserHelper.Attach(webView21);

            //browserHelper.ShowUrl("https://www.cnn.com");

            //webView21.CoreWebView2.NavigateToString("<html><body><img src=\"notebook://Image+One.jpg\"></body></html>");
            //webView21.CoreWebView2.NavigateToString("<html><body>" +
            //    "<h1>Hello Brad</h1>" +
            //    "</body></html>");

            browserHelper.ShowHtml("<html><body>" +
                "<h1>Hello Brad</h1>" +
                "</body></html>");

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                fileName = args[1];
                document = fileManager.Open<WDWDoc>(fileName);
                ShowDocument();
            }
            //else
            //{
            //    if (!NewDocument())
            //    {
            //        Close();
            //    }
            //}
        }

        private bool NewDocument()
        {
            bool keepGoing = false;
            GridTypeForm gtf = new();
            if (gtf.ShowDialog() == DialogResult.OK)
            {
                comboBox2.SelectedIndex = comboBox2.FindStringExact(gtf.SelectedGridType);
                keepGoing = true;
            }

            return keepGoing;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://www.interfacing.com/what-is-rasci-raci";
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://expertprogrammanagement.com/2018/04/rapid-decision-making-model/";
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://www.linkedin.com/pulse/rapid-raci-matrices-same-heres-how-use-them-francisco-sagastume-he3if/";
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Insert:
                    string newTask = InputBox.ShowDialog("Name", "Add Task");
                    if (!string.IsNullOrEmpty(newTask))
                    {
                        int pos = listBox1.Items.Add(newTask);
                        document.Tasks.Add(newTask);
                        listBox1.SelectedIndex = pos;
                    }
                    break;
            }
        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Insert:
                    string newRolePerson = InputBox.ShowDialog("Name", "Add Role / Person");
                    if (!string.IsNullOrEmpty(newRolePerson))
                    {
                        int pos = listBox2.Items.Add(newRolePerson);
                        document.People_Roles.Add(newRolePerson);
                        listBox2.SelectedIndex = pos;
                    }
                    break;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
            ShowResponsibilities();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textBox2.Text = listBox2.Items[listBox2.SelectedIndex].ToString();
            ShowResponsibilities();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
                return;

            string selModel = comboBox2.Items[comboBox2.SelectedIndex].ToString();

            //checkedListBox1.Items.Clear();
            foreach (CheckBox cb in responsibilityButtons)
            {
                cb.Enabled = false;
                cb.Visible = false;
                cb.Text = string.Empty;
                cb.Checked = false;
            }

            List<ComboItem> items = ComboItems.RACI;

            switch (comboBox2.Items[comboBox2.SelectedIndex].ToString())
            {
                case "RACI":
                    items = ComboItems.RACI;
                    break;
                case "RASCI":
                    items = ComboItems.RASCI;
                    break;
                case "RACI-VS":
                    items = ComboItems.RACI_VS;
                    break;
                case "RACIO":
                    items = ComboItems.RACIO;
                    break;
                case "DRASCI":
                    items = ComboItems.DRASCI;
                    break;
                case "DACI":
                    items = ComboItems.DACI;
                    break;
                case "RATSI":
                    items = ComboItems.RATSI;
                    break;
                case "RAPID":
                    items = ComboItems.RAPID;
                    break;
            }

            int respNum = 0;

            foreach (ComboItem item in items)
            {
                //checkedListBox1.Items.Add(item);
                CheckBox cb = responsibilityButtons[respNum];
                cb.Enabled = false;
                cb.Visible = true;
                cb.Text = item.Name;
                cb.Checked = false;

                respNum++;
            }
            ShowResponsibilities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileManager.Save(document);
        }

        bool programaticSetting = false;
        //private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        //{
        //    if (programaticSetting)
        //    {
        //        return;
        //    }
        //    // item does not get checked until after this call
        //    List<string> checkedItems = new List<string>();
        //    foreach (object item in checkedListBox1.CheckedItems)
        //    {
        //        checkedItems.Add(item.ToString());
        //    }
        //    // add
        //    if ((e.CurrentValue == CheckState.Unchecked) && (e.NewValue == CheckState.Checked))
        //    {
        //        checkedItems.Add(checkedListBox1.Items[e.Index].ToString());
        //    }
        //    // remove
        //    if ((e.CurrentValue == CheckState.Checked) && (e.NewValue == CheckState.Unchecked))
        //    {
        //        //checkedItems.Add(checkedListBox1.Items[e.Index].ToString());
        //        checkedItems.Remove(checkedListBox1.Items[e.Index].ToString());
        //    }
        //    UpdateItem(checkedItems);
        //}

        private void UpdateItem(List<string> responsibilities)
        {
            string task = GetTaskValue();
            string party = GetPartyValue();

            if ((string.IsNullOrEmpty(task)) || (string.IsNullOrEmpty(party)))
            {
                return;
            }
            WDWItem item = document.Items.Where(s => ((s.Task == task) && (s.Person_Role == party)))
                .FirstOrDefault();
            if (item != null)
            {
                if (responsibilities.Count > 0)
                {
                    item.Responsibilities.Clear();
                    item.Responsibilities.AddRange(responsibilities);
                }
                else
                {
                    document.Items.Remove(item);
                }
            }
            else
            {
                document.Items.Add(new WDWItem()
                {
                    Task = task,
                    Person_Role = party,
                    Responsibilities = responsibilities
                });
            }
        }

        private string GetTaskValue()
        {
            string rtnVal = string.Empty;

            if (listBox1.SelectedIndex != -1)
            {
                rtnVal = listBox1.Items[listBox1.SelectedIndex].ToString();
            }

            return rtnVal;
        }

        private string GetPartyValue()
        {
            string rtnVal = string.Empty;

            if (listBox2.SelectedIndex != -1)
            {
                rtnVal = listBox2.Items[listBox2.SelectedIndex].ToString();
            }

            return rtnVal;
        }

        private void ShowResponsibilities()
        {
            string task = GetTaskValue();
            string party = GetPartyValue();

            bool enabled = ((comboBox2.SelectedIndex != -1) &&
                (!string.IsNullOrEmpty(task)) &&
                (!string.IsNullOrEmpty(party)));

            programaticSetting = true;

            foreach (CheckBox cb in responsibilityButtons)
            {
                cb.Enabled = enabled && (!string.IsNullOrEmpty(cb.Text));
                //cb.Enabled = false;
                //cb.Visible = false;
                //cb.Text = string.Empty;
                cb.Checked = false;
            }

            WDWItem item = document.Items.Where(s => ((s.Task == task) && (s.Person_Role == party)))
                .FirstOrDefault();
            if (item != null)
            {
                int buttonNumber = 0;
                foreach (string responsibility in item.Responsibilities)
                {
                    foreach (CheckBox cb in responsibilityButtons)
                    {
                        if (cb.Text.Equals(responsibility, StringComparison.OrdinalIgnoreCase))
                            cb.Checked = true;
                    }
                    //int idx = checkedListBox1.FindStringExact(responsibility);
                    //if (idx != -1)
                    //{
                    //    checkedListBox1.SetItemChecked(idx, true);
                    //}
                }
            }

            programaticSetting = false;
        }

        //private void ShowResponsibilities_OG()
        //{
        //    string task = GetTaskValue();
        //    string party = GetPartyValue();

        //    checkedListBox1.Enabled = ((comboBox2.SelectedIndex != -1) &&
        //        (!string.IsNullOrEmpty(task)) &&
        //        (!string.IsNullOrEmpty(party)));

        //    programaticSetting = true;

        //    for (int itemNum = 0; itemNum < checkedListBox1.Items.Count; itemNum++)
        //    {
        //        checkedListBox1.SetItemChecked(itemNum, false);
        //    }

        //    WDWItem item = document.Items.Where(s => ((s.Task == task) && (s.Person_Role == party)))
        //        .FirstOrDefault();
        //    if (item != null)
        //    {
        //        foreach (string responsibility in item.Responsibilities)
        //        {
        //            int idx = checkedListBox1.FindStringExact(responsibility);
        //            if (idx != -1)
        //            {
        //                checkedListBox1.SetItemChecked(idx, true);
        //            }
        //        }
        //    }

        //    programaticSetting = false;
        //}


        private void button2_Click(object sender, EventArgs e)
        {
            string newTask = InputBox.ShowDialog("Name   :", "Add Task");
            if (!string.IsNullOrEmpty(newTask))
            {
                int pos = listBox1.Items.Add(newTask);
                document.Tasks.Add(newTask);
                listBox1.SelectedIndex = pos;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string newRolePerson = InputBox.ShowDialog("Name    :", "Add Role / Person");
            if (!string.IsNullOrEmpty(newRolePerson))
            {
                int pos = listBox2.Items.Add(newRolePerson);
                document.People_Roles.Add(newRolePerson);
                listBox2.SelectedIndex = pos;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            string sel = listBox1.Items[listBox1.SelectedIndex].ToString();
            document.Tasks.Remove(sel);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == -1)
                return;

            string sel = listBox2.Items[listBox2.SelectedIndex].ToString();
            document.People_Roles.Remove(sel);
            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NewDocument())
            {
                document = new WDWDoc();
                document.GridType = comboBox2.Items[comboBox2.SelectedIndex].ToString();
                ShowDocument();
                ShowResponsibilities();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WDWDoc opened = fileManager.Open<WDWDoc>();
            if (document != null)
            {
                document = opened;
                ShowDocument();
                ShowResponsibilities();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileManager.Save(document);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileManager.SaveAs(document);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AddHtmlTop();
            sb.StartTable();
            sb.AddTableHeader(BumpColumns(document.People_Roles));
            foreach (string task in document.Tasks)
            {
                sb.AddHeaderedTableRow(BuildTaskRow(task));
            }
            sb.EndTable();
            sb.AddHtmlBottom();

            browserHelper.ShowHtml(sb.ToString());

            //browserHelper.ShowHtml("<html><body>" +
            //    "<h1>Hello Brad</h1>" +
            //    "</body></html>");
        }

        // this will leave the first column empty (useful for the header row)
        private List<string> BumpColumns(List<string> columns)
        {
            List<string> result = new List<string>();
            result.Add(string.Empty);
            result.InsertRange(1, columns);
            return result;
        }

        private List<string> BuildTaskRow(string taskName)
        {
            List<string> result = new List<string>();
            result.Add(taskName);

            foreach (string party in document.People_Roles)
            {
                StringBuilder respString = new();
                WDWItem item = document.Items.Where(s => ((s.Task == taskName) && (s.Person_Role == party)))
                    .FirstOrDefault();
                if (item != null)
                {
                    foreach (string responsibility in item.Responsibilities)
                    {
                        if (respString.Length > 0)
                        {
                            respString.Append(", ");
                        }
                        respString.Append(responsibility);
                    }
                }
                result.Add(respString.ToString());
            }

            return result;
        }

    }
}
