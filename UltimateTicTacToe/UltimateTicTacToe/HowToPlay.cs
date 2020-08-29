using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateTicTacToe
{
    public partial class HowToPlay : Form
    {
       
        public HowToPlay(bool form2)
        {
            InitializeComponent();
            if (!form2)
            {
                LightMode();
            }
            else
            {
                DarkMode();
            }
        }
     


        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            ToolTip tool = new ToolTip();
            tool.Show("Close", (PictureBox)sender, 3000);
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LightMode()
        {
            BackColor = Color.FromArgb(255, 255, 255);
          
            foreach (Control x in Controls)
            {
                if (x is Label || x is Button || x is RadioButton)
                    x.ForeColor = Color.Black;
            }
            richTextBox1.BackColor = Color.White;
            richTextBox1.ForeColor = Color.Black;
        }
        private void DarkMode()
        {
            BackColor = Color.FromArgb(32, 32, 32);
            
            foreach (Control x in Controls)
            {
                if (x is Label)
                    x.ForeColor = Color.White;
            }
            richTextBox1.BackColor = Color.FromArgb(32, 32, 32);
            richTextBox1.ForeColor = Color.White;
        }
    }
}
