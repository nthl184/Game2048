using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Game2048
{
    public partial class FrmMain : Form
    {
        private int tag = 5; // khoang cach giua cac o
        private Random rand = new Random();
        private int score = 0;
        private Label[,] cardLabel = new Label[4,4];
        int[,] cards = new int[4, 4];
        bool _isDown;
        private Point began, ended;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //init label
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cardLabel[i, j] = new Label();
                    cardLabel[i, j].Location = new Point(tag + i * (100 + tag), tag + j * (100 + tag));
                    cardLabel[i, j].Size = new Size(100, 100);
                    cardLabel[i, j].TabIndex = i * 4 + j;
                    cardLabel[i, j].Name = String.Format("lb%d%d", i, j);
                    cardLabel[i, j].BackColor = Color.FromName("ActiveBorder");
                    cardLabel[i, j].Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    cardLabel[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    this.Controls.Add(cardLabel[i, j]);
                }
            }

            // su kien game
            initCards();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // dien gia tri ra cac label
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (cards[i, j] == 0)
                        cardLabel[i, j].Text = "";
                    else
                        cardLabel[i, j].Text = cards[i, j].ToString();
                    setCardColor(i, j);
                }
            }
            lbScore.Text = score.ToString();
        }
        /// <summary>
        /// tao card ngau nhien
        /// </summary>
        bool creatRandomCard()
        {
            bool isDo = false;
            List<int> test = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                if (cards[i / 4, i % 4] == 0)
                {
                    test.Add(i);
                    isDo = true;
                }
            }
            if (test.Count > 0)
            {
                int set = test[rand.Next(0, test.Count - 1)];
                while (cards[set / 4, set % 4] != 0 && test.Count > 1)
                {
                    test.Remove(set);
                    set = test[rand.Next(0, test.Count - 1)];
                }
                cards[set / 4, set % 4] = rand.Next(1, 100) > 90 ? 4 : 2;
                score += cards[set / 4, set % 4];
            }
            return isDo;
        }
        /// <summary>
        /// doUp, doDown, doRight,doLeft
        /// </summary>
        /// <returns></returns>
        #region Su kien Move
        bool doUp()
        {
            bool isDo = false;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int y1 = y + 1; y1 < 4; y1++)
                    {
                        if (cards[x, y1] > 0)
                        {
                            if (cards[x, y] == 0)
                            {
                                cards[x, y] = cards[x, y1];
                                cards[x, y1] = 0;
                                y--;
                                isDo = true;
                            }
                            else if (cards[x, y] == cards[x, y1])
                            {
                                cards[x, y] *= 2;
                                cards[x, y1] = 0;
                                isDo = true;
                            }
                            break;
                        }
                    }
                }
            }
            if(isDo)
                creatRandomCard();
            return isDo;
        }
        
        bool doDown()
        {
            bool isDo = false;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 3; y >= 1; y--)
                {
                    for (int y1 = y - 1; y1 >= 0; y1--)
                    {
                        if (cards[x, y1] > 0)
                        {
                            if (cards[x, y] == 0)
                            {
                                cards[x, y] = cards[x, y1];
                                cards[x, y1] = 0;
                                y++;
                                isDo = true;
                            }
                            else if (cards[x, y] == cards[x, y1])
                            {
                                cards[x, y] *= 2;
                                cards[x, y1] = 0;
                                isDo = true;
                            }
                            break;
                        }
                    }
                }
            }
            if (isDo)
                creatRandomCard();
            return isDo;
        }

        bool doRight()
        {
            bool isDo = false;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 3; x >= 1; x--)
                {
                    for (int x1 = x - 1; x1 >= 0; x1--)
                    {
                        if (cards[x1, y] > 0)
                        {
                            if (cards[x, y] == 0)
                            {
                                cards[x, y] = cards[x1, y];
                                cards[x1, y] = 0;
                                x++;
                                isDo = true;
                            }
                            else if (cards[x, y] == cards[x1, y])
                            {
                                cards[x, y] *= 2;
                                cards[x1, y] = 0;
                                isDo = true;
                            }
                            break;
                        }
                    }
                }
            }
            if (isDo)
                creatRandomCard();
            return isDo;
        }

        bool doLeft()
        {
            bool isDo = false;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    for (int x1 = x + 1; x1 < 4; x1++)
                    {
                        if (cards[x1, y] > 0)
                        {
                            if (cards[x, y] == 0)
                            {
                                cards[x, y] = cards[x1, y];
                                cards[x1, y] = 0;
                                x--;
                                isDo = true;
                            }
                            else if (cards[x, y] == cards[x1, y])
                            {
                                cards[x, y] *= 2;
                                cards[x1, y] = 0;
                                isDo = true;
                            }
                            break;
                        }
                    }
                }
            }
            if (isDo)
                creatRandomCard();
            return isDo;
        }
        #endregion


        /// <summary>
        /// dung phim dieu huong tren ban phim
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
			if (e.KeyData == Keys.Up)
			{
				lblKey.Text = "LÊN";
				doUp();
			}

			if (e.KeyData == Keys.Down)
			{
				lblKey.Text = "XUỐNG";
				doDown();
			}

			if (e.KeyData == Keys.Right)
			{
				lblKey.Text = "PHÀI";
				doRight();
			}

			if (e.KeyData == Keys.Left)
			{
				lblKey.Text = "TRÁI";
				doLeft();
			}
                
            this.Refresh();
            
            // xu li game over
            if (CheckGameOver())
            {
                DialogResult dia = MessageBox.Show("Điểm của bạn: " + score.ToString() + "\n" + "Bạn có muốn chơi lại không?",
                    "Laptrinhvb.net", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dia == DialogResult.No)
                    Application.Exit();
                else
                {
                    initCards();
                }
            }
        }

        bool CheckGameOver()
        {

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if ( cards[x, y] == 0 || 
                        (y < 3 && cards[x,y] == cards[x,y+1]) || 
                        (x < 3 && cards[x, y] == cards[x + 1, y]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        void initCards()
        {
            score = 0;
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                    cards[x, y] = 0;
            creatRandomCard();
            creatRandomCard();
            this.Refresh();
        }

        void setCardColor(int x, int y)
        {
            switch (cards[x, y])
            {
                case 0: cardLabel[x, y].BackColor = Color.FromArgb(220,220,220); break;
                case 2: cardLabel[x, y].BackColor = Color.FromArgb(255, 192, 192); break;
                case 4: cardLabel[x, y].BackColor = Color.FromArgb(255, 128, 128); break;
                case 8: cardLabel[x, y].BackColor = Color.FromArgb(255, 224, 192); break;
                case 16: cardLabel[x, y].BackColor = Color.FromArgb(255, 192, 128); break;
                case 32: cardLabel[x, y].BackColor = Color.FromArgb(255, 255, 192); break;
                case 64: cardLabel[x, y].BackColor = Color.FromArgb(255, 255, 128); break;
                case 128: cardLabel[x, y].BackColor = Color.FromArgb(192, 255, 192); break;
                case 256: cardLabel[x, y].BackColor = Color.FromArgb(128, 255, 128); break;
                case 512: cardLabel[x, y].BackColor = Color.FromArgb(192, 255, 255); break;
                case 1024: cardLabel[x, y].BackColor = Color.FromArgb(128, 255, 255); break;
                case 2048: cardLabel[x, y].BackColor = Color.FromArgb(192, 192, 255); break;
                case 4096: cardLabel[x, y].BackColor = Color.FromArgb(128, 128, 255); break;
                case 8192: cardLabel[x, y].BackColor = Color.FromArgb(255, 192, 255); break;
            }
        }

       

    }
}
