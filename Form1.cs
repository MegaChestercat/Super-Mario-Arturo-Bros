using System.Drawing.Drawing2D;
using System.Media;

namespace SuperMarioArturoBros
{
    public partial class Form1 : Form
    {

        Map map;
        Player player;
        int score;
        bool right, left;
        int timeLeft;
        bool start = false;

        SoundPlayer sPlayer;
        Thread thread, thread2;

        float fCameraPosX = 0.0f;
        float fCameraPosY = 0.0f;

        float fElapsedTime;


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
            score = 0;
            player = new Player();
            fElapsedTime = 0.05f;
            //Size sizeSky = new Size(PCT_CANVAS.Width / 2, PCT_CANVAS.Height);
            //Size vizSky = new Size(PCT_CANVAS.Width * 2, PCT_CANVAS.Height);
            //BG = new Sprites(sizeSky, vizSky, new Point(), 5, Resource1.BG);

            //Size sizeBack = new Size(PCT_CANVAS.Width / 2, PCT_CANVAS.Height / 2);
            //Size vizBack = new Size(PCT_CANVAS.Width, PCT_CANVAS.Height);
            //back = new Sprites(sizeBack, vizBack, new Point(), 15, map.BMP);

            //Point posMario = new Point((PCT_CANVAS.Width / 2) - 29, 300);
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
            e.Graphics.DrawString("MARIO \n  " + score, new Font("Arcade Normal", 17), Brushes.White, 50, 20);
            e.Graphics.DrawString("TIME \n " + timeLeft, new Font("Arcade Normal", 17), Brushes.White, 900, 20);
            e.Graphics.DrawString("LEVEL \n  1", new Font("Arcade Normal", 17), Brushes.White, 480, 20);
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
                    player.FPlayerVelY = -6.0f;
                    player.bPlayerOnGround = false;
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
                        player.FPlayerVelY = -15;
                        player.Frame(2);
                        player.bPlayerOnGround = false;
                    }
                    break;
            }
        }
    }
}