﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        bool xPlayerTurn = true;
        int turnCount = 0;
        int pictureCounter = 1;
        PictureBox pic;

        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeCell();
        }

        private void InitializeGrid()
        {
            Grid.BackColor = Color.LightCoral;
            Grid.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
        }

        private void RestartGame()
        {
            InitializeCell();
            turnCount = 0;
        }
        private void InitializeCell()
        {
            string labelName;
            for (int i=1; i<=9; i++)
            {
                labelName = "pictureBox" + i;
                PictureBox picture;                                 
                picture = (PictureBox)Grid.Controls[labelName];
                picture.Tag = String.Empty;
                picture.Image = null;
                picture.BackColor = Color.Transparent;              
            }
        }

        private void Player_Click(object sender, EventArgs e)
        {
            PictureBox picture = (PictureBox)sender;

            if (picture.Tag != string.Empty)
            {
                return;
            }

            if (xPlayerTurn)
            {
                picture.Tag = "X";
                pic = picture;
                timer1.Start();
            }
            else
            {
                picture.Tag = "O";
                pic = picture;
                timer1.Start();
            }
            turnCount++;
            PlaySound("click_sound");
            CheckForWin();
            CheckForDraw();
            xPlayerTurn = !xPlayerTurn;
        }

        private void CheckForWin()
        {
            if (
                    (pictureBox1.Tag == pictureBox2.Tag && pictureBox2.Tag == pictureBox3.Tag && pictureBox2.Tag != string.Empty) ||
                    (pictureBox4.Tag == pictureBox5.Tag && pictureBox5.Tag == pictureBox6.Tag && pictureBox4.Tag != string.Empty) ||
                    (pictureBox7.Tag == pictureBox8.Tag && pictureBox8.Tag == pictureBox9.Tag && pictureBox7.Tag != string.Empty) ||
                    (pictureBox1.Tag == pictureBox4.Tag && pictureBox4.Tag == pictureBox7.Tag && pictureBox1.Tag != string.Empty) ||
                    (pictureBox2.Tag == pictureBox5.Tag && pictureBox5.Tag == pictureBox8.Tag && pictureBox2.Tag != string.Empty) ||
                    (pictureBox3.Tag == pictureBox6.Tag && pictureBox6.Tag == pictureBox9.Tag && pictureBox3.Tag != string.Empty) ||
                    (pictureBox1.Tag == pictureBox5.Tag && pictureBox5.Tag == pictureBox9.Tag && pictureBox1.Tag != string.Empty) ||
                    (pictureBox3.Tag == pictureBox5.Tag && pictureBox5.Tag == pictureBox7.Tag && pictureBox3.Tag != string.Empty)
                )
            {
                GameOver();
            }
        }

        private void WinnerCellsChangeColor()
        {
            if (pictureBox1.Tag == pictureBox2.Tag && pictureBox1.Tag == pictureBox3.Tag && pictureBox1.Tag != "")
            {
                ChangeCellColors(pictureBox1, pictureBox2, pictureBox3, Color.Purple);
            }
            else if (pictureBox4.Tag == pictureBox5.Tag && pictureBox4.Tag == pictureBox6.Tag && pictureBox4.Tag != "")
            {
                ChangeCellColors(pictureBox4, pictureBox5, pictureBox6, Color.Purple);
            }
            else if (pictureBox7.Tag == pictureBox8.Tag && pictureBox7.Tag == pictureBox9.Tag && pictureBox7.Tag != "")
            {
                ChangeCellColors(pictureBox7, pictureBox8, pictureBox9, Color.Purple);
            }
            else if (pictureBox1.Tag == pictureBox4.Tag && pictureBox1.Tag == pictureBox7.Tag && pictureBox1.Tag != "")
            {
                ChangeCellColors(pictureBox1, pictureBox4, pictureBox7, Color.Purple);
            }
            else if (pictureBox2.Tag == pictureBox5.Tag && pictureBox2.Tag == pictureBox8.Tag && pictureBox2.Tag != "")
            {
                ChangeCellColors(pictureBox2, pictureBox5, pictureBox8, Color.Purple);
            }
            else if (pictureBox3.Tag == pictureBox6.Tag && pictureBox3.Tag == pictureBox9.Tag && pictureBox3.Tag != "")
            {
                ChangeCellColors(pictureBox3, pictureBox6, pictureBox9, Color.Purple);
            }
            else if (pictureBox1.Tag == pictureBox5.Tag && pictureBox1.Tag == pictureBox9.Tag && pictureBox1.Tag != "")
            {
                ChangeCellColors(pictureBox1, pictureBox5, pictureBox9, Color.Purple);
            }
            else if (pictureBox3.Tag == pictureBox5.Tag && pictureBox3.Tag == pictureBox7.Tag && pictureBox3.Tag != "")
            {
                ChangeCellColors(pictureBox3, pictureBox5, pictureBox7, Color.Purple);
            }

        }
        private void ChangeCellColors(PictureBox firstLabel, PictureBox secondLabel, PictureBox thirdLabel, Color color)
        {
            firstLabel.BackColor = color;
            secondLabel.BackColor = color;
            thirdLabel.BackColor = color;
        }

        private void PlaySound(string soundName)
        {
            System.IO.Stream str = (System.IO.Stream)Properties.Resources.ResourceManager.GetObject(soundName);
            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            snd.Play();
        }
        private void GameOver()
        {
            string winner;
            if (xPlayerTurn)
            {
                winner = "X";
            }
            else
            {
                winner = "O";
            }
            WinnerCellsChangeColor();
           MessageBox.Show(winner + " wins!");
            RestartGame();
        }

        private void CheckForDraw()
        {
            if (turnCount == 9)
            {
                MessageBox.Show("Draw!");
                RestartGame();
            }
        }

        private void Animate()
        {
            string turn;
            string pictureName;
            
            turn = pic.Tag.ToString();
            turn = turn.ToLower();

            pictureName = turn + "_frame_0" + pictureCounter.ToString("00");
            pic.Image = (Image)Properties.Resources.ResourceManager.GetObject(pictureName);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureCounter += 1;
            if (pictureCounter > 20)
            {
                pictureCounter = 1;
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Animate();
        }
    }
}
