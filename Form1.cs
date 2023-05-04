using Microsoft.VisualBasic.Devices;
using System.Drawing.Drawing2D;
using System.Media;

namespace SuperMarioArturoBros
{
    public partial class Form1 : Form
    {

        Map map;
        Player player;
        bool right, left;
        int timeLeft;
        bool start = false;

        SoundPlayer sPlayer, sPlayer2, sPlayer3, sPlayer4, sPlayer5, sPlayer6, sPlayer7, sPlayer8;
        Thread thread, thread2, thread3, thread4, thread5, thread6, thread7, thread8;
        bool leftMove;
        int lives = 3;

        float fCameraPosX = 0.0f;
        float fCameraPosY = 0.0f;

        float fElapsedTime;
        Random ran;


        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //Init();
        }

        private void Init()
        {
            timeLeft = 400;
            map = new Map(PCT_CANVAS.Size);
            PCT_CANVAS.Image = map.bmp;
            left = right = false;
            player = new Player();
            fElapsedTime = 0.05f;
            leftMove = true;
            ran = new Random();
            sPlayer = new SoundPlayer(Resource1.inicio);
            sPlayer2 = new SoundPlayer(Resource1.finish);
            sPlayer3 = new SoundPlayer(Resource1.time);
            sPlayer4 = new SoundPlayer(Resource1.money);
            sPlayer5 = new SoundPlayer(Resource1.jump1);
            sPlayer6 = new SoundPlayer(Resource1.jump2);
            sPlayer7 = new SoundPlayer(Resource1.die1);
            sPlayer8 = new SoundPlayer(Resource1.die2);
            Play();
        }
        private void Play()
        {
            thread = new Thread(PlayThread);
            thread.Start();
        }
        private void Play2()
        {
            thread2 = new Thread(PlayThread2);
            thread2.Start();
        }
        private void Play3()
        {
            thread3 = new Thread(PlayThread3);
            thread3.Start();
        }
        private void Play4()
        {
            thread4 = new Thread(PlayThread4);
            thread4.Start();
        }
        private void Play5()
        {
            thread5 = new Thread(PlayThread5);
            thread5.Start();
        }
        private void Play6()
        {
            thread6 = new Thread(PlayThread6);
            thread6.Start();
        }
        private void Play7()
        {
            thread7 = new Thread(PlayThread7);
            thread7.Start();
        }
        private void Play8()
        {
            thread8 = new Thread(PlayThread8);
            thread8.Start();
        }
        private void PlayThread()
        {
            sPlayer.Play();
        }
        private void PlayThread2()
        {
            sPlayer7.Play();
        }
        private void PlayThread3()
        {
            sPlayer8.Play();
        }
        private void PlayThread4()
        {
            sPlayer3.Play();
        }
        private void PlayThread5()
        {
            sPlayer2.Play();
        }
        private void PlayThread6()
        {
            sPlayer4.Play();
        }
        private void PlayThread7()
        {
            sPlayer5.Play();
        }
        private void PlayThread8()
        {
            sPlayer6.Play();
        }
        private void TIMER_Tick(object sender, EventArgs e)
        {
            UpdateEnv();
        }

        private void PCT_CANVAS_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;



            //BG.Display(e.Graphics);
            VerifyInteractions(e);
            if (player.finish)
            {
                TIMER.Enabled = false;
                COUNTDOWN.Enabled = false;
                Play5();
                MessageBox.Show("Congratulations, you finished the level.  \nTotal Points: " + map.score + "\n\nRemaining time: " + timeLeft, "Level completed");
                Application.Exit();
            }
            if (timeLeft >= 0)
            {
                e.Graphics.DrawString("MARIO \n  " + map.score, new Font("Arcade Normal", 17), Brushes.White, 50, 20);
                e.Graphics.DrawString("TIME \n " + timeLeft, new Font("Arcade Normal", 17), Brushes.White, 900, 20);
                e.Graphics.DrawString("LEVEL \n  1", new Font("Arcade Normal", 17), Brushes.White, 570, 20);
                e.Graphics.DrawString("LIVES \n  " + lives, new Font("Arcade Normal", 17), Brushes.White, 340, 20);
            }
            else
            {
                TIMER.Enabled = false;
                COUNTDOWN.Enabled = false;
                Play4();
                MessageBox.Show("Game Over. The time has run out.", "Game Over");
                Application.Exit();

            }

        }

        private void VerifyInteractions(PaintEventArgs e)
        {
            if (player.axolotl)
            {
                TIMER.Enabled = false;
                COUNTDOWN.Enabled = false;
                MessageBox.Show("Aaarrrp", "Axolotl");
                TIMER.Enabled = true;
                COUNTDOWN.Enabled = true;
                player.axolotl = false;
                player.fPlayerPosX = player.fPlayerPosX - 1;
                player.fPlayerPosY = player.fPlayerPosY - 1;
                map.score = map.score + 400;

            }
            if (player.goku)
            {
                TIMER.Enabled = false;
                COUNTDOWN.Enabled = false;
                MessageBox.Show("Hey Buddy, take this Senzu bean so you can became stronger", "Goku");
                TIMER.Enabled = true;
                COUNTDOWN.Enabled = true;
                player.goku = false;
                player.fPlayerPosX = player.fPlayerPosX + 1;
                lives = lives + 2;

            }
            if (player.michael)
            {
                int temp = ran.Next(1, 5);
                if (temp == 2)
                {
                    TIMER.Enabled = false;
                    COUNTDOWN.Enabled = false;
                    MessageBox.Show("Hee Hee", "Michael");
                    TIMER.Enabled = true;
                    COUNTDOWN.Enabled = true;
                    player.michael = false;
                    player.fPlayerPosX = player.fPlayerPosX - 1;
                    
                }
                else if(temp == 3)
                {
                    TIMER.Enabled = false;
                    COUNTDOWN.Enabled = false;
                    MessageBox.Show("I wish you good luck on your adventure.", "Michael");
                    TIMER.Enabled = true;
                    COUNTDOWN.Enabled = true;
                    player.michael = false;
                    player.fPlayerPosX = player.fPlayerPosX - 1;
                }
                else if (temp == 4)
                {
                    TIMER.Enabled = false;
                    COUNTDOWN.Enabled = false;
                    MessageBox.Show("Be careful where you step on", "Michael");
                    TIMER.Enabled = true;
                    COUNTDOWN.Enabled = true;
                    player.michael = false;
                    player.fPlayerPosX = player.fPlayerPosX - 1;
                }
            }
            else if (player.chapopote)
            {
                if (lives > 0)
                {
                    player.chapopote = false;
                    player.fPlayerPosX = player.fPlayerPosX - 1;
                    player.fPlayerPosY = player.fPlayerPosY - 2;
                    lives--;
                    Play2();
                }
                if (lives <= 0)
                {
                    TIMER.Enabled = false;
                    COUNTDOWN.Enabled = false;
                    Play3();
                    MessageBox.Show("Game Over. You run out of lives.", "Game Over");
                    Application.Exit();

                }
            }
            else if (player.cloro)
            {
                player.cloro = false;
                player.fPlayerPosX = player.fPlayerPosX - 1;
                player.fPlayerPosY = player.fPlayerPosY - 2;
                map.score = map.score - 230;
                Play2();
            }
            else if (player.zubat || player.eye)
            {
                if (lives > 0)
                {
                    if(player.zubat) player.zubat = false;
                    if (player.eye) player.eye = false;
                    player.fPlayerPosX = player.fPlayerPosX - 2;
                    player.fPlayerPosY = player.fPlayerPosY + 2;
                    lives--;
                    Play2();
                }
                if (lives <= 0)
                {
                    TIMER.Enabled = false;
                    COUNTDOWN.Enabled = false;
                    Play3();
                    MessageBox.Show("Game Over. You run out of lives.", "Game Over");
                    Application.Exit();
                }
            }
            else if (player.koopa || player.zombie1 || player.zombie2)
            {
                if (lives > 0)
                {
                    if(player.koopa) player.koopa = false;
                    if (player.zombie1) player.zombie1 = false;
                    if (player.zombie2) player.zombie2 = false;
                    player.fPlayerPosX = player.fPlayerPosX - 2;
                    player.fPlayerPosY = player.fPlayerPosY - 1;
                    lives--;
                    Play2();
                }
                if (lives <= 0)
                {
                    TIMER.Enabled = false;
                    COUNTDOWN.Enabled = false;
                    Play3();
                    MessageBox.Show("Game Over. You run out of lives.", "Game Over");
                    Application.Exit();
                }
            }
            else if (player.star)
            {
                player.star = false;
                map.score = map.score + 5999;
                Play6();
            }
            else if (player.coin1)
            {
                player.coin1 = false;
                map.score = map.score + 290;
            }
            else if (player.coin2)
            {
                player.coin2 = false;
                map.score = map.score + 100;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Left:
                    left = true;
                    break;
                case Keys.Right:
                    right = true;
                    break;
                case Keys.Up:
                    player.FPlayerVelY = -9.0f;
                    player.bPlayerOnGround = false;
                    int temp = ran.Next(1, 4);
                    if (temp == 2) Play7();
                    if (temp == 3) Play8();
                    break;
            }

            UpdateEnv();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                return;

            left = false;
            right = false;

            player.Stop();
        }

        private void UpdateEnv()
        {
            if (left)
            {
                player.Left(fElapsedTime);
            }
            if (right)
            {
                player.Right(fElapsedTime);
            }

            fCameraPosX = player.fPlayerPosX;
            fCameraPosY = player.fPlayerPosY;

            map.Draw(new PointF(fCameraPosX, fCameraPosY), player.fPlayerPosX.ToString(), player, timeLeft);
            player.Update(fElapsedTime, map);
            PCT_CANVAS.Invalidate();
        }

        private void COUNTDOWN_Tick(object sender, EventArgs e)
        {
            timeLeft--;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Space:
                    if (player.FPlayerVelY == 0)// sin brincar o cayendo
                    {
                        player.FPlayerVelY = -14;
                        player.Frame(2);
                        player.bPlayerOnGround = false;
                        int temp = ran.Next(1, 4);
                        if (temp == 2) Play7();
                        if (temp == 3) Play8();
                    }
                    break;
            }
        }
    }
}