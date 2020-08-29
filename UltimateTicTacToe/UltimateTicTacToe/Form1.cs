using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UltimateTicTacToe.View;
using UltimateTicTacToe.Game;
using System.Threading;

namespace UltimateTicTacToe
{
    public partial class Form1 : Form
    {
        Game.Game game = null;
        Form2 form2;
        int humanPlayer;

        Thread gameThread;
        public Color cellBackground { get; set; }
        public Form1(string name, int player,int depth, Image avatar, Form2 form)
        {
            InitializeComponent();
            DoubleBuffered = true;
            roundPictureBox2.Image = avatar;
            form2 = form;

           
            game = new Game.Game(player,depth);
            SetModels();
            SetSubscribers();
          
            humanPlayer = player;

            DrawPlayerSigns(humanPlayer);

            if(!game.IsPlayerTurn())
            {
                int moveIndeks = game.minmax.GetTurn();
                game.PlayMove(game.currPossibleMoves[moveIndeks]);
                CheckWin();

            }
            labelName.Text = name;

            gameThread = new Thread(() =>
            {
                game.minmax.GetTurn();
                CheckWin();
            });
            if (form2.NightMode)
            {
                DarkMode();

            }
            else
            {
                LightMode();
            }
        }

        private async void CellClicked(object sender, EventArgs e)
        {
            CellView clickedCell = (CellView)sender;
            if (!clickedCell.model.resolved && game.MovePossible(clickedCell.model) && game.IsPlayerTurn())
            {
                resetBackColor(cellBackground);
                game.PlayMove(clickedCell.model);
                roundPictureBox4.Show();
                roundPictureBox1.Hide();

                resetBackColor(cellBackground);
                CheckWin();

                if (!game.IsPlayerTurn())
                {
                    UseWaitCursor = true;
                    label1.Show();
                    int moveIndeks = await Task.Run(() => game.minmax.GetTurn());
                    game.PlayMove(game.currPossibleMoves[moveIndeks]);
                    label1.Hide();
                    UseWaitCursor = false;  
                    roundPictureBox4.Hide();
                    roundPictureBox1.Show();

                    CheckWin();
                }  
            }
        }

        private void CheckWin()
        {
            if (game.gameBoard.resolved)
            {
                if (game.gameBoard.cellValue == game.humanPlayer)
                {
                    MessageBox.Show("You won!!!");
                    game.playerWinCount++;
                    labelScorePlayer.Text = game.playerWinCount.ToString();
                    resetBackColor(cellBackground);

                    game.Reset();
                    
                    game.activePlayer = 1;
                    game.humanPlayer = -game.humanPlayer;
                    DrawPlayerSigns(game.humanPlayer);
                    if (!game.IsPlayerTurn())
                    {
                        int moveIndeks = game.minmax.GetTurn();
                        game.PlayMove(game.currPossibleMoves[moveIndeks]);
                        CheckWin();
                    }
                }

                else
                {
                    MessageBox.Show("Computer won!!!");
                    game.computerWinCount++;
                    labelScoreComputer.Text = game.computerWinCount.ToString();
                    resetBackColor(cellBackground);

                    game.Reset();
                  
                    game.activePlayer = 1;
                    game.humanPlayer = -game.humanPlayer;
                    DrawPlayerSigns(game.humanPlayer);
                   
                }

                if (!game.IsPlayerTurn())
                {
                    int moveIndeks = game.minmax.GetTurn();
                    game.PlayMove(game.currPossibleMoves[moveIndeks]);
                    CheckWin();
                }
            }
            else if (game.currPossibleMoves.Count == 0)
            {
                MessageBox.Show("Draw!!!");

                game.Reset();
                
                game.activePlayer = 1;
                game.humanPlayer = -game.humanPlayer;
                DrawPlayerSigns(game.humanPlayer);
                if (!game.IsPlayerTurn())
                {
                    int moveIndeks = game.minmax.GetTurn();
                    game.PlayMove(game.currPossibleMoves[moveIndeks]);
                    CheckWin();
                }
            }
            ColorResolved();
        }
        private void DrawPlayerSigns(int humanPlayer)
        {
            if (humanPlayer == 1)
            {

                cellView82.DrawCross();
                cellView83.DrawCircle();
                roundPictureBox1.BackColor = Color.FromArgb(29, 131, 212);
                roundPictureBox4.BackColor = Color.FromArgb(212, 60, 50);
                roundPictureBox4.Hide();
                roundPictureBox1.Show();
            }
            else
            {

                cellView82.DrawCircle();
                cellView83.DrawCross();
                roundPictureBox4.BackColor = Color.FromArgb(29, 131, 212);
                roundPictureBox1.BackColor = Color.FromArgb(212, 60, 50);
                roundPictureBox4.Show();
                roundPictureBox1.Hide();
            }

        }

        private void ColorResolved()
        {
            
            for(int i=0;i<9;i++)
            {
                if(game.gameBoard.childCells[i].resolved)
                {
                    if (game.gameBoard.childCells[i].cellValue == 1)
                        for (int j=0;j<9;j++)
                         {
                            game.gameBoard.childCells[i].childCells[j].SetSubBackground(Color.PaleTurquoise);
                         }
                    else
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            game.gameBoard.childCells[i].childCells[j].SetSubBackground(Color.DarkSalmon);
                        }
                    }
                }
            }
        }

        private void SetModels()
        {
            cellView1.model  = game.gameBoard.childCells[0].childCells[0];
            cellView2.model  = game.gameBoard.childCells[0].childCells[1];
            cellView3.model  = game.gameBoard.childCells[0].childCells[2];
            cellView4.model  = game.gameBoard.childCells[1].childCells[0];
            cellView5.model  = game.gameBoard.childCells[1].childCells[1];
            cellView6.model  = game.gameBoard.childCells[1].childCells[2];
            cellView7.model  = game.gameBoard.childCells[2].childCells[0];
            cellView8.model  = game.gameBoard.childCells[2].childCells[1];
            cellView9.model  = game.gameBoard.childCells[2].childCells[2];
            cellView10.model = game.gameBoard.childCells[0].childCells[3];
            cellView11.model = game.gameBoard.childCells[0].childCells[4];
            cellView12.model = game.gameBoard.childCells[0].childCells[5];
            cellView13.model = game.gameBoard.childCells[1].childCells[3];
            cellView14.model = game.gameBoard.childCells[1].childCells[4];
            cellView15.model = game.gameBoard.childCells[1].childCells[5];
            cellView16.model = game.gameBoard.childCells[2].childCells[3];
            cellView17.model = game.gameBoard.childCells[2].childCells[4];
            cellView18.model = game.gameBoard.childCells[2].childCells[5];
            cellView19.model = game.gameBoard.childCells[0].childCells[6];
            cellView20.model = game.gameBoard.childCells[0].childCells[7];
            cellView21.model = game.gameBoard.childCells[0].childCells[8];
            cellView22.model = game.gameBoard.childCells[1].childCells[6];
            cellView23.model = game.gameBoard.childCells[1].childCells[7];
            cellView24.model = game.gameBoard.childCells[1].childCells[8];
            cellView25.model = game.gameBoard.childCells[2].childCells[6];
            cellView26.model = game.gameBoard.childCells[2].childCells[7];
            cellView27.model = game.gameBoard.childCells[2].childCells[8];
            cellView28.model = game.gameBoard.childCells[3].childCells[0];
            cellView29.model = game.gameBoard.childCells[3].childCells[1];
            cellView30.model = game.gameBoard.childCells[3].childCells[2];
            cellView31.model = game.gameBoard.childCells[4].childCells[0];
            cellView32.model = game.gameBoard.childCells[4].childCells[1];
            cellView33.model = game.gameBoard.childCells[4].childCells[2];
            cellView34.model = game.gameBoard.childCells[5].childCells[0];
            cellView35.model = game.gameBoard.childCells[5].childCells[1];
            cellView36.model = game.gameBoard.childCells[5].childCells[2];
            cellView37.model = game.gameBoard.childCells[3].childCells[3];
            cellView38.model = game.gameBoard.childCells[3].childCells[4];
            cellView39.model = game.gameBoard.childCells[3].childCells[5];
            cellView40.model = game.gameBoard.childCells[4].childCells[3];
            cellView41.model = game.gameBoard.childCells[4].childCells[4];
            cellView42.model = game.gameBoard.childCells[4].childCells[5];
            cellView43.model = game.gameBoard.childCells[5].childCells[3];
            cellView44.model = game.gameBoard.childCells[5].childCells[4];
            cellView45.model = game.gameBoard.childCells[5].childCells[5];
            cellView46.model = game.gameBoard.childCells[3].childCells[6];
            cellView47.model = game.gameBoard.childCells[3].childCells[7];
            cellView48.model = game.gameBoard.childCells[3].childCells[8];
            cellView49.model = game.gameBoard.childCells[4].childCells[6];
            cellView50.model = game.gameBoard.childCells[4].childCells[7];
            cellView51.model = game.gameBoard.childCells[4].childCells[8];
            cellView52.model = game.gameBoard.childCells[5].childCells[6];
            cellView53.model = game.gameBoard.childCells[5].childCells[7];
            cellView54.model = game.gameBoard.childCells[5].childCells[8];
            cellView55.model = game.gameBoard.childCells[6].childCells[0];
            cellView56.model = game.gameBoard.childCells[6].childCells[1];
            cellView57.model = game.gameBoard.childCells[6].childCells[2];
            cellView58.model = game.gameBoard.childCells[7].childCells[0];
            cellView59.model = game.gameBoard.childCells[7].childCells[1];
            cellView60.model = game.gameBoard.childCells[7].childCells[2];
            cellView61.model = game.gameBoard.childCells[8].childCells[0];
            cellView62.model = game.gameBoard.childCells[8].childCells[1];
            cellView63.model = game.gameBoard.childCells[8].childCells[2];
            cellView64.model = game.gameBoard.childCells[6].childCells[3];
            cellView65.model = game.gameBoard.childCells[6].childCells[4];
            cellView66.model = game.gameBoard.childCells[6].childCells[5];
            cellView67.model = game.gameBoard.childCells[7].childCells[3];
            cellView68.model = game.gameBoard.childCells[7].childCells[4];
            cellView69.model = game.gameBoard.childCells[7].childCells[5];
            cellView70.model = game.gameBoard.childCells[8].childCells[3];
            cellView71.model = game.gameBoard.childCells[8].childCells[4];
            cellView72.model = game.gameBoard.childCells[8].childCells[5];
            cellView73.model = game.gameBoard.childCells[6].childCells[6];
            cellView74.model = game.gameBoard.childCells[6].childCells[7];
            cellView75.model = game.gameBoard.childCells[6].childCells[8];
            cellView76.model = game.gameBoard.childCells[7].childCells[6];
            cellView77.model = game.gameBoard.childCells[7].childCells[7];
            cellView78.model = game.gameBoard.childCells[7].childCells[8];
            cellView79.model = game.gameBoard.childCells[8].childCells[6];
            cellView80.model = game.gameBoard.childCells[8].childCells[7];
            cellView81.model = game.gameBoard.childCells[8].childCells[8];
        }

        private void SetSubscribers()
        {
            game.gameBoard.childCells[0].childCells[0].AddSubscriber(cellView1);
            game.gameBoard.childCells[0].childCells[1].AddSubscriber(cellView2);
            game.gameBoard.childCells[0].childCells[2].AddSubscriber(cellView3);
            game.gameBoard.childCells[1].childCells[0].AddSubscriber(cellView4);
            game.gameBoard.childCells[1].childCells[1].AddSubscriber(cellView5);
            game.gameBoard.childCells[1].childCells[2].AddSubscriber(cellView6);
            game.gameBoard.childCells[2].childCells[0].AddSubscriber(cellView7);
            game.gameBoard.childCells[2].childCells[1].AddSubscriber(cellView8);
            game.gameBoard.childCells[2].childCells[2].AddSubscriber(cellView9);
            game.gameBoard.childCells[0].childCells[3].AddSubscriber(cellView10);
            game.gameBoard.childCells[0].childCells[4].AddSubscriber(cellView11);
            game.gameBoard.childCells[0].childCells[5].AddSubscriber(cellView12);
            game.gameBoard.childCells[1].childCells[3].AddSubscriber(cellView13);
            game.gameBoard.childCells[1].childCells[4].AddSubscriber(cellView14);
            game.gameBoard.childCells[1].childCells[5].AddSubscriber(cellView15);
            game.gameBoard.childCells[2].childCells[3].AddSubscriber(cellView16);
            game.gameBoard.childCells[2].childCells[4].AddSubscriber(cellView17);
            game.gameBoard.childCells[2].childCells[5].AddSubscriber(cellView18);
            game.gameBoard.childCells[0].childCells[6].AddSubscriber(cellView19);
            game.gameBoard.childCells[0].childCells[7].AddSubscriber(cellView20);
            game.gameBoard.childCells[0].childCells[8].AddSubscriber(cellView21);
            game.gameBoard.childCells[1].childCells[6].AddSubscriber(cellView22);
            game.gameBoard.childCells[1].childCells[7].AddSubscriber(cellView23);
            game.gameBoard.childCells[1].childCells[8].AddSubscriber(cellView24);
            game.gameBoard.childCells[2].childCells[6].AddSubscriber(cellView25);
            game.gameBoard.childCells[2].childCells[7].AddSubscriber(cellView26);
            game.gameBoard.childCells[2].childCells[8].AddSubscriber(cellView27);
            game.gameBoard.childCells[3].childCells[0].AddSubscriber(cellView28);
            game.gameBoard.childCells[3].childCells[1].AddSubscriber(cellView29);
            game.gameBoard.childCells[3].childCells[2].AddSubscriber(cellView30);
            game.gameBoard.childCells[4].childCells[0].AddSubscriber(cellView31);
            game.gameBoard.childCells[4].childCells[1].AddSubscriber(cellView32);
            game.gameBoard.childCells[4].childCells[2].AddSubscriber(cellView33);
            game.gameBoard.childCells[5].childCells[0].AddSubscriber(cellView34);
            game.gameBoard.childCells[5].childCells[1].AddSubscriber(cellView35);
            game.gameBoard.childCells[5].childCells[2].AddSubscriber(cellView36);
            game.gameBoard.childCells[3].childCells[3].AddSubscriber(cellView37);
            game.gameBoard.childCells[3].childCells[4].AddSubscriber(cellView38);
            game.gameBoard.childCells[3].childCells[5].AddSubscriber(cellView39);
            game.gameBoard.childCells[4].childCells[3].AddSubscriber(cellView40);
            game.gameBoard.childCells[4].childCells[4].AddSubscriber(cellView41);
            game.gameBoard.childCells[4].childCells[5].AddSubscriber(cellView42);
            game.gameBoard.childCells[5].childCells[3].AddSubscriber(cellView43);
            game.gameBoard.childCells[5].childCells[4].AddSubscriber(cellView44);
            game.gameBoard.childCells[5].childCells[5].AddSubscriber(cellView45);
            game.gameBoard.childCells[3].childCells[6].AddSubscriber(cellView46);
            game.gameBoard.childCells[3].childCells[7].AddSubscriber(cellView47);
            game.gameBoard.childCells[3].childCells[8].AddSubscriber(cellView48);
            game.gameBoard.childCells[4].childCells[6].AddSubscriber(cellView49);
            game.gameBoard.childCells[4].childCells[7].AddSubscriber(cellView50);
            game.gameBoard.childCells[4].childCells[8].AddSubscriber(cellView51);
            game.gameBoard.childCells[5].childCells[6].AddSubscriber(cellView52);
            game.gameBoard.childCells[5].childCells[7].AddSubscriber(cellView53);
            game.gameBoard.childCells[5].childCells[8].AddSubscriber(cellView54);
            game.gameBoard.childCells[6].childCells[0].AddSubscriber(cellView55);
            game.gameBoard.childCells[6].childCells[1].AddSubscriber(cellView56);
            game.gameBoard.childCells[6].childCells[2].AddSubscriber(cellView57);
            game.gameBoard.childCells[7].childCells[0].AddSubscriber(cellView58);
            game.gameBoard.childCells[7].childCells[1].AddSubscriber(cellView59);
            game.gameBoard.childCells[7].childCells[2].AddSubscriber(cellView60);
            game.gameBoard.childCells[8].childCells[0].AddSubscriber(cellView61);
            game.gameBoard.childCells[8].childCells[1].AddSubscriber(cellView62);
            game.gameBoard.childCells[8].childCells[2].AddSubscriber(cellView63);
            game.gameBoard.childCells[6].childCells[3].AddSubscriber(cellView64);
            game.gameBoard.childCells[6].childCells[4].AddSubscriber(cellView65);
            game.gameBoard.childCells[6].childCells[5].AddSubscriber(cellView66);
            game.gameBoard.childCells[7].childCells[3].AddSubscriber(cellView67);
            game.gameBoard.childCells[7].childCells[4].AddSubscriber(cellView68);
            game.gameBoard.childCells[7].childCells[5].AddSubscriber(cellView69);
            game.gameBoard.childCells[8].childCells[3].AddSubscriber(cellView70);
            game.gameBoard.childCells[8].childCells[4].AddSubscriber(cellView71);
            game.gameBoard.childCells[8].childCells[5].AddSubscriber(cellView72);
            game.gameBoard.childCells[6].childCells[6].AddSubscriber(cellView73);
            game.gameBoard.childCells[6].childCells[7].AddSubscriber(cellView74);
            game.gameBoard.childCells[6].childCells[8].AddSubscriber(cellView75);
            game.gameBoard.childCells[7].childCells[6].AddSubscriber(cellView76);
            game.gameBoard.childCells[7].childCells[7].AddSubscriber(cellView77);
            game.gameBoard.childCells[7].childCells[8].AddSubscriber(cellView78);
            game.gameBoard.childCells[8].childCells[6].AddSubscriber(cellView79);
            game.gameBoard.childCells[8].childCells[7].AddSubscriber(cellView80);
            game.gameBoard.childCells[8].childCells[8].AddSubscriber(cellView81);
        }

        private void resetBackColor(Color color)
        {
            cellView1.BackColor  = color;
            cellView2.BackColor  = color;
            cellView3.BackColor  = color;
            cellView4.BackColor  = color;
            cellView5.BackColor  = color;
            cellView6.BackColor  = color;
            cellView7.BackColor  = color;
            cellView8.BackColor  = color;
            cellView9.BackColor  = color;
            cellView10.BackColor = color;
            cellView11.BackColor = color;
            cellView12.BackColor = color;
            cellView13.BackColor = color;
            cellView14.BackColor = color;
            cellView15.BackColor = color;
            cellView16.BackColor = color;
            cellView17.BackColor = color;
            cellView18.BackColor = color;
            cellView19.BackColor = color;
            cellView20.BackColor = color;
            cellView21.BackColor = color;
            cellView22.BackColor = color;
            cellView23.BackColor = color;
            cellView24.BackColor = color;
            cellView25.BackColor = color;
            cellView26.BackColor = color;
            cellView27.BackColor = color;
            cellView28.BackColor = color;
            cellView29.BackColor = color;
            cellView30.BackColor = color;
            cellView31.BackColor = color;
            cellView32.BackColor = color;
            cellView33.BackColor = color;
            cellView34.BackColor = color;
            cellView35.BackColor = color;
            cellView36.BackColor = color;
            cellView37.BackColor = color;
            cellView38.BackColor = color;
            cellView39.BackColor = color;
            cellView40.BackColor = color;
            cellView41.BackColor = color;
            cellView42.BackColor = color;
            cellView43.BackColor = color;
            cellView44.BackColor = color;
            cellView45.BackColor = color;
            cellView46.BackColor = color;
            cellView47.BackColor = color;
            cellView48.BackColor = color;
            cellView49.BackColor = color;
            cellView50.BackColor = color;
            cellView51.BackColor = color;
            cellView52.BackColor = color;
            cellView53.BackColor = color;
            cellView54.BackColor = color;
            cellView55.BackColor = color;
            cellView56.BackColor = color;
            cellView57.BackColor = color;
            cellView58.BackColor = color;
            cellView59.BackColor = color;
            cellView60.BackColor = color;
            cellView61.BackColor = color;
            cellView62.BackColor = color;
            cellView63.BackColor = color;
            cellView64.BackColor = color;
            cellView65.BackColor = color;
            cellView66.BackColor = color;
            cellView67.BackColor = color;
            cellView68.BackColor = color;
            cellView69.BackColor = color;
            cellView70.BackColor = color;
            cellView71.BackColor = color;
            cellView72.BackColor = color;
            cellView73.BackColor = color;
            cellView74.BackColor = color;
            cellView75.BackColor = color;
            cellView76.BackColor = color;
            cellView77.BackColor = color;
            cellView78.BackColor = color;
            cellView79.BackColor = color;
            cellView80.BackColor = color;
            cellView81.BackColor = color;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Close();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Authors:\nMarko Vrančić\nBojan Borovčanin\nMilan Kovačević\nOgnjen Lubarda\n");
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Hide();
            HowToPlay hp = new HowToPlay(form2.NightMode);
            hp.ShowDialog();
            Show();
        }

        private void LightMode()
        {
            BackColor = Color.FromArgb(255, 255, 255);
            pictureBox8.Image = Properties.Resources.Asset_1;
            roundPictureBox2.BackColor = Color.White;
            roundPictureBox3.BackColor = Color.White;
            foreach (Control x in Controls)
            {
                if (x is Label || x is Button || x is RadioButton)
                    x.ForeColor = Color.Black;
            }


            cellBackground = Color.FromArgb(229, 229, 229);

            panel1.BackColor = Color.DarkGray;
            panel2.BackColor = Color.DarkGray;
            panel3.BackColor = Color.DarkGray;
            panel4.BackColor = Color.DarkGray;

            foreach(CellView cell in tableLayoutPanel1.Controls)
            {
                cell.setBackground(cellBackground);
            }

            foreach (Cell move in game.currPossibleMoves)
            {
                move.UpdateAllMoves();
            }

        }

        private void DarkMode()
        {
            BackColor = Color.FromArgb(32, 32, 32);
            pictureBox8.Image = Properties.Resources.Asset_7;

            roundPictureBox2.BackColor = Color.FromArgb(32, 32, 32);
            roundPictureBox3.BackColor = Color.FromArgb(32, 32, 32);

            foreach (Control x in Controls)
            {
                if (x is Label || x is Button || x is RadioButton)
                    x.ForeColor = Color.White;
            }

           
                cellBackground = Color.FromArgb(51, 51, 51);

            panel1.BackColor = Color.FromArgb(78, 78, 78);
            panel2.BackColor = Color.FromArgb(78, 78, 78);
            panel3.BackColor = Color.FromArgb(78, 78, 78);
            panel4.BackColor = Color.FromArgb(78, 78, 78);

            labelName.ForeColor = Color.White;

            foreach (CellView cell in tableLayoutPanel1.Controls)
            {
                cell.setBackground(cellBackground);
            }

            foreach (Cell move in game.currPossibleMoves)
            {
                move.UpdateAllMoves();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if(form2.NightMode)
            {
                LightMode();
                pictureBox4.Image = Properties.Resources.Asset_12;
                form2.NightMode = !form2.NightMode;
            }
            else
            {
                DarkMode();
                pictureBox4.Image = Properties.Resources.Asset_10;
                form2.NightMode = !form2.NightMode;
            }
        }
    }
}
