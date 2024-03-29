﻿using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace csvtester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //richTextBox2.Hide();
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
                InitialDirectory = @"C:\",
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
                    using (var dr = new CsvDataReader(csv))
                    {
                        dt.Load(dr);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            String[] values = File.ReadAllText(textBox1.Text).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Dictionary<string, string> dListCsvData = new Dictionary<string, string>();
            StringBuilder csvcontent = new StringBuilder();
            var header = values[0];
            csvcontent.AppendLine(header);
            //csvcontent.Append(new[] { Environment.NewLine });
            foreach (var item in values)
            {
                var csvIdentifier = item.Split(',');
                var count = csvIdentifier.Count();
                string csvContext = "";
                for (int i = 1; i < count; i++)
                {
                    csvContext += csvIdentifier[i] + ",";
                }
                try
                {
                    if (csvIdentifier[0] != "")
                {
                    if (csvIdentifier[0] != null)
                    {
                        if (dListCsvData.ContainsKey(csvIdentifier[0]) == false)
                        {
                            dListCsvData.Add(csvIdentifier[0], csvContext);
                        }
                    }
                }
                else
                {
                    dListCsvData.Add("ignore" + r.Next(1, 99999999).ToString(), csvContext);
                }

                }
                catch (ArgumentException exc)
                {
                var title = "failed";
                MessageBoxButtons buttons = MessageBoxButtons.AbortRetryIgnore;
                DialogResult result = MessageBox.Show(exc.ToString(), title, buttons, MessageBoxIcon.Warning);
                }

            }
            int c = 0;
            foreach (var item in dListCsvData)
            {
                

                var failed = richTextBox1.Lines;
                var temp = "";
                
                newdata = new string[failed.Length];
                for (int i = 0; i < failed.Length; i++)
                {//add if statment with .contains skus

                    temp = failed[i].Replace(
                        "SKU ",
                        "");

                    if (temp.Contains(','))
                    {
                        var temp2 = temp.Split(',');
                        newdata[i] = temp2[0];

                    }
                    else if (temp.Contains('\t'))
                    {
                        var temp2 = temp.Split('\t');
                        newdata[i] = temp2[0];
                    }
                    else if (temp.Contains(' '))
                    {
                        
                        var temp2 = temp.Split(' ');
                        newdata[i] = temp2[0];
                    }  
                    else
                    {
                        newdata[i] = temp;
                    }
                }
                IEnumerable<string> uniqueItems = newdata.Distinct<string>();
                foreach(var newdItem in uniqueItems)
                {

                    if (item.Key == newdItem)
                    {
                        csvcontent.AppendLine(item.ToString().Trim(']', '['));
                        c++;
                        //newdata[i] = null;
                    }
                }

            }
            try
            {
                using (SaveFileDialog saveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = @"C:\",
                    Title = "Save csv Files",
                    CheckFileExists = false,
                    CheckPathExists = true,
                    DefaultExt = "csv",
                    Filter = "csv files (*.csv)|*.csv",
                    FilterIndex = 2,
                    RestoreDirectory = true
                })
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        textBox2.Text = saveFileDialog1.FileName;
                    }
                }

                File.AppendAllText(textBox2.Text, csvcontent.ToString());
                string message = "file was gesave na " + textBox2.Text;
                string title = "Success";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                string message = "ek kan nie hier save nie try op n ander plek save";
                string title = "Unauthorized Access To File Path";
                MessageBoxButtons buttons = MessageBoxButtons.AbortRetryIgnore;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);

            }
            catch (ArgumentException)
            {
                string message = "niks was select nie";
                string title = "Path was not specified";
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);

            }

        }
    }
}
