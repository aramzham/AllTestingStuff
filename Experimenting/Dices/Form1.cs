using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dices
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static int count = 0;
        private static int youTotal = 0;
        private static int oppTotal = 0;

        private void Roll_Click(object sender, EventArgs e)
        {
            #region random number & picture
            var rnd = new Random();
            var number = rnd.Next(1, 7);
            switch (number)
            {
                case 1:
                    pictureBox1.ImageLocation = @"C:\Users\HP\Desktop\1.png";
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 2:
                    pictureBox1.ImageLocation = @"C:\Users\HP\Desktop\2.png";
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 3:
                    pictureBox1.ImageLocation = @"C:\Users\HP\Desktop\3.png";
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 4:
                    pictureBox1.ImageLocation = @"C:\Users\HP\Desktop\4.png";
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 5:
                    pictureBox1.ImageLocation = @"C:\Users\HP\Desktop\5.png";
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 6:
                    pictureBox1.ImageLocation = @"C:\Users\HP\Desktop\6.png";
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
            }
            #endregion
            if (count % 2 == 0)
            {
                your_score.Text = opponent_score.Text = string.Empty;
                Roll.Text = "Your turn.\nRoll";
                your_score.Text = number.ToString();
            }
            else
            {
                Roll.Text = "Opponent rolls";
                opponent_score.Text = number.ToString();
            }
            count++;
            #region score
            if (your_score.Text != string.Empty && opponent_score.Text != string.Empty &&
                int.Parse(your_score.Text) > int.Parse(opponent_score.Text))
            {
                yourTotal.Text = (++youTotal).ToString();
                opponentTotal.Text = oppTotal.ToString();
            }
            if (your_score.Text != string.Empty && opponent_score.Text != string.Empty &&
                int.Parse(your_score.Text) < int.Parse(opponent_score.Text))
            {
                yourTotal.Text = youTotal.ToString();
                opponentTotal.Text = (++oppTotal).ToString();
            }
            #endregion
        }
    }
}
