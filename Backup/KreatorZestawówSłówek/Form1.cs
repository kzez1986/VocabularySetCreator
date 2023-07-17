using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace KreatorZestawówSłówek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Add(textBox1.Text);
            listView2.Items.Add(textBox2.Text);
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Zestawy słówek (*.zst)|*.zst";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                WczytajPlik(openFileDialog1.FileName);
            }
        }

        private void WczytajPlik(string nazwaPliku)
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            StreamReader sr = new StreamReader(nazwaPliku);
            string linia;
            while (!sr.EndOfStream)
            {
                linia = sr.ReadLine();
                int indeks = linia.LastIndexOf('|');
                string[] podział = new string[2];
                podział[0] = linia.Substring(0, indeks);
                podział[1] = linia.Substring(indeks + 1, linia.Length - indeks - 1);
                listView1.Items.Add(podział[0]);
                listView2.Items.Add(podział[1]);
            }
            sr.Close();
        }

        private void ZapiszPlik(string nazwa_pliku, bool typ) //false - zestaw, true - tekst
        {
            StreamWriter sw = new StreamWriter(nazwa_pliku);
            string linia;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                linia = listView1.Items[i].ToString();
                if (typ == false)
                    linia = linia + "|";
                else
                    linia = linia + " - ";
                linia = linia + listView2.Items[i].ToString();
                sw.WriteLine(linia);
            }
            sw.Close();
        }

        private void listView2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
            if (lb.SelectedIndex != -1)
            {
                int indeks = lb.SelectedIndex;
                listView1.SelectedIndex = indeks;
                listView2.SelectedIndex = indeks;
                textBox1.Text = listView1.Items[listView1.SelectedIndex].ToString();
                textBox2.Text = listView2.Items[listView2.SelectedIndex].ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndex != -1)
            {
                int indeks = listView1.SelectedIndex;
                listView1.Items.RemoveAt(indeks);
                listView2.Items.RemoveAt(indeks);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndex != -1)
            {
                int indeks = listView1.SelectedIndex;
                listView1.Items.RemoveAt(indeks);
                listView2.Items.RemoveAt(indeks);
                listView1.Items.Insert(indeks, textBox1.Text);
                listView2.Items.Insert(indeks, textBox2.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Zestawy słówek (*.zst)|*.zst";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ZapiszPlik(saveFileDialog1.FileName, false);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Pliki tekstowe (*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ZapiszPlik(saveFileDialog1.FileName, true);
            }
        }
    }
}
