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

namespace UltimateTicTacToe
{
    public partial class Form2 : Form
    {
        int player;
        int difficulty;
        string[] lokacije= new string[50];
        public Form2()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            comboBox1.SelectedIndex = 0;
            PopunaLokacija();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBoxName.TextLength.Equals(0))
            {
                MessageBox.Show("Please enter your name");
              
            }
            else
            {   
                if(comboBox1.SelectedIndex.Equals(2))
                {
                    MessageBox.Show("The best thing you can hope is draw !!");
                }
                Form1 form1 = new Form1(textBoxName.Text,player,difficulty,roundPictureBox2.Image,this);

                this.Hide();
                form1.ShowDialog();
                textBoxName.Clear();
                if (!NightMode)
                {
                    LightMode();
                }
                else
                {
                    DarkMode();
                }
                this.Show();
           
            }

           
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                player = 1;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked == true)
            {
                player = -1;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex.Equals(0))
            {
                difficulty = 4;
            }
            if (comboBox1.SelectedIndex.Equals(1))
            {
                difficulty = 6;
            }
            if (comboBox1.SelectedIndex.Equals(2))
            {
                difficulty = 8;
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Hide();
            HowToPlay hp = new HowToPlay(NightMode);
            hp.ShowDialog();
            Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Authors:\nMarko Vrančić\nBojan Borovčanin\nMilan Kovačević\nOgnjen Lubarda\n");
        }

        ToolTip tool = new ToolTip();
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            tool.Show("Authors", (PictureBox)sender, 3000);
        }

        private void pictureBox8_MouseHover(object sender, EventArgs e)
        {
            tool.Show("How to play", (PictureBox)sender, 3000);
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            tool.Show("Close", (PictureBox)sender, 3000);
        }
        int lokacija = 0;
        private void PopunaLokacija()
        {
           for(int i=0;i<50;i++)
            {
                lokacije[i] = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "/Resources/avatars/"+(i+1)+".png";
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {    
            if (lokacija == lokacije.Length - 1)
                lokacija = 0;
            else lokacija++;
            roundPictureBox2.Image = Image.FromFile(lokacije[lokacija]);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
          
            if (lokacija <= 0)
                lokacija = lokacije.Length-1;
            else lokacija--;
            roundPictureBox2.Image = Image.FromFile(lokacije[lokacija]);
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            tool.Show("Show previous picture", (PictureBox)sender, 3000);
        }

       private bool nightMode = false;
        public bool NightMode { get; set; }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if(!NightMode)
            {
                DarkMode();
                pictureBox4.Image = Properties.Resources.Asset_10;
                NightMode = true;
            }
            else
            {
                LightMode();
                pictureBox4.Image = Properties.Resources.Asset_12;
                NightMode = false;
            }
        }
        private void DarkMode()
        {
            BackColor = Color.FromArgb(32, 32, 32);
            pictureBox8.Image = Properties.Resources.Asset_7;
            foreach (Control x in Controls)
            {
                if (x is Label || x is Button || x is RadioButton)
                    x.ForeColor = Color.White;
            }
        }
        private void LightMode()
        {
            BackColor = Color.FromArgb(255, 255, 255);
            pictureBox8.Image = Properties.Resources.Asset_1;
            foreach (Control x in Controls)
            {
                if (x is Label || x is Button || x is RadioButton)
                    x.ForeColor = Color.Black;
            }
        }

      
    }
}
