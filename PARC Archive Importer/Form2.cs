using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PARC_Archive_Importer
{
    public partial class Form2 : Form
    {
        public class CheckboxListItem
        {

            public string Text { get; set; }
            public string Tag { get; set; }

            public override string ToString()
            {
                return Text;
            }
            public int GetTag()
            {
                return int.Parse(Tag);
            }
        }
        public void ShowingArray(string[] array, string namefile)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (!string.IsNullOrEmpty(array[i]))
                {
                    CheckboxListItem cb1 = new CheckboxListItem();
                    cb1.Tag = i.ToString();
                    cb1.Text = array[i] + "\\" + namefile;
                    checkedListBox1.Items.Add(cb1);

                }

            }

        }
        public Form2()
        {
            InitializeComponent();
            button1.DialogResult = DialogResult.OK;

        }

        public List<int> IDofarch = new List<int>();

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (CheckboxListItem item in checkedListBox1.CheckedItems)
            {
                IDofarch.Add(item.GetTag());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++) checkedListBox1.SetItemChecked(i, false);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++) checkedListBox1.SetItemChecked(i, true);
        }
    }
}
