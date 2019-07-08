using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csvtester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SaveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        public DataTable dt = new DataTable();
        public string[] newdata = null;
        public string[] line = null; 
        private void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse csv Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "csv",
                Filter = "csv files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                string filePathCsv = textBox1.Text;

                using (StreamReader reader = new StreamReader(filePathCsv))
                using (CsvReader csv = new CsvReader(reader))
                {
                    
                    //while (csv.Read())
                    //{
                    //    line.Append(csv.GetField("Item number"));
                    //}
                    using (var dr = new CsvDataReader(csv))
                    {
                        dt.Columns.Add("Item number",typeof(string));
                        dt.Load(dr);
                        dataGridView1.DataSource = dt;
                    }
                    richTextBox1.Lines = line;
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var failed = richTextBox1.Lines;
            var temp = "";
            newdata = new string[failed.Length];
            for (int i = 0; i < failed.Length; i++)
            {
                temp = failed[i].Replace(
                    "SKU ",
                    "");
                newdata[i] = temp;
            }
            //richTextBox1.Text = dataGridView1.Rows.GetFirstRow();
            //foreach (var item in )
            //{
            //    richTextBox1.Text = item.ToString();
            //}
            //foreach (var item in newdata)
            //{
            //    if (item == dataGridView1.Columns.)
            //    {

            //    }
            //}
        }
    }
}
