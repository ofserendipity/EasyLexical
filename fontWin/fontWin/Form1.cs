using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fontWin
{
    public partial class Form1 : Form
    {
        string wordlistpath = string.Empty;
        List<string> wordlist = new List<string>();
        private Point mouseoff;//鼠标移动位置变量
        private bool leftflag;//鼠标是否为左键
        const int MaxFontSize = 70;
        const int MidFontSize = 50;
        const int MinFontSize = 30;
        private int FontSize = MaxFontSize;
        private Color FontColor= Color.Orchid;
        string newword = "Precipitous";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 3500; 
        }
        


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择txt单词本，一个单词占一行";
            openFileDialog.Filter = "文本文件（*.txt)|*.txt|所有文件（*.)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                wordlistpath = openFileDialog.FileName.ToString();

            }
            StreamReader sr = new StreamReader(wordlistpath, Encoding.Default);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                wordlist.Add(line);

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rd = new Random();
            int index = rd.Next(wordlist.Count());
            if(index>0) newword = wordlist[index];
            this.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (wordlistpath == string.Empty) MessageBox.Show("请先输入单词本");
            else 
            {
                timer1.Enabled = !timer1.Enabled;
                if (timer1.Enabled == true)
                {
                    button3.Text = "暂停";
                }
                else
                {
                    button3.Text = "播放";
                }                        
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush sldBrush = new SolidBrush(FontColor);
            Font font = new Font("楷体", FontSize, FontStyle.Bold);
            newword = newword.ToString();
            g.DrawString(newword, font, sldBrush, new Point(20, 20));
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                mouseoff = new Point(-e.X, -e.Y);
                leftflag = true;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(leftflag)
            {
                Point mouseset = Control.MousePosition;
                mouseset.Offset(mouseoff.X, mouseoff.Y);
                Location = mouseset;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if(leftflag)
            {
                leftflag = false;
            }
        }
        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           switch(listBox1.SelectedIndex)
            {
                case 0: this.Close();break;
                case 1: FontSize = MaxFontSize;this.Refresh(); break;
                case 2:FontSize = MidFontSize; this.Refresh(); break;
                case 3: FontSize = MinFontSize;this.Refresh();break;
                case 4: this.TopMost = !this.TopMost; break;
                case 5: FontColor = Color.Orchid;this.Refresh();break;
                case 6: FontColor = Color.Green;this.Refresh();break;
                case 7:FontColor = Color.Blue;this.Refresh();break;
                

            }

        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            listBox1.Visible = true;
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.Visible == true)
            {
                listBox1.Visible = false;
            }
        }
    }
}
