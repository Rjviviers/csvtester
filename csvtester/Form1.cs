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
                using (var reader = new StreamReader(filePathCsv))
                using (var csv = new CsvReader(reader))
                {
                    using (var dr = new CsvDataReader(csv))
                    {
                        var dt = new DataTable();
                        dt.Load(dr);
                        dataGridView1.DataSource = dt;

                    }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var dataS = new DataTable();
           // dataS = dataGridView1.DataSource;
            Console.WriteLine(dataS.ToString()); 
            var failed = richTextBox1.Lines;
            var temp = "";
            var newdata = new string[failed.Length];
            for (int i = 0; i < failed.Length; i++)
            {
                temp = failed[i].Replace("SKU ", "");
                newdata[i] = temp;
                //MessageBox.Show(temp,"works");
                
            }
            
              
        }
    }
}
