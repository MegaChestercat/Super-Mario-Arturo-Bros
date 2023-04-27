using System.Drawing.Drawing2D;

namespace SuperMarioArturoBros
{
    public partial class Form1 : Form
    {

        Map map;
        private static Rectangle src, dst;
        private Sprites BG, back, mario;
        int score;
        bool right, left, jump;
        int timeLeft;


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
            map = new Map();
            right = jump = false;
            score = 0;
            Size sizeSky = new Size(PCT_CANVAS.Width / 2, PCT_CANVAS.Height);
            Size vizSky = new Size(PCT_CANVAS.Width * 2, PCT_CANVAS.Height);
            BG = new Sprites(sizeSky, vizSky, new Point(), 5, Resource1.BG);

            Size sizeBack = new Size(PCT_CANVAS.Width / 2, PCT_CANVAS.Height / 2);
            Size vizBack = new Size(PCT_CANVAS.Width, PCT_CANVAS.Height);
            back = new Sprites(sizeBack, vizBack, new Point(), 15, map.BMP);

            Point posMario = new Point((PCT_CANVAS.Width / 2) - 29, 300);
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
            if (right)
            {
                CANVAS_Action(1);
            }
            if (left)
            {
                CANVAS_Action(-1);
            }
            /*
            if (jump && mario.PosY > 250 && mario.PosY < 50)
            {
                mario.PosY -= 20;
            }*/
            else
                jump = false;
            //score = mario.PosY.ToString();
            PCT_CANVAS.Invalidate();
        }

        private void PCT_CANVAS_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;

            BG.Display(e.Graphics);
            e.Graphics.DrawString("MARIO \n  " + score, new Font("Arcade Normal", 17), Brushes.White, 50, 20);
            e.Graphics.DrawString("TIME \n " + timeLeft, new Font("Arcade Normal", 17), Brushes.White, 900, 20);
            e.Graphics.DrawString("LEVEL \n  1", new Font("Arcade Normal", 17), Brushes.White, 480, 20);
        }

        private void CANVAS_Action(int X)
        {
            if (X < 0)
            {

                BG.MoveLeft();
                back.MoveLeft();

            }
            if (X > 0)
            {

                BG.MoveRight();
                back.MoveRight();
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
                    AsignY();
                    break;
            }
        }
        private void AsignY()
        {
            //mario.PosY -= 60;
            jump = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //mario.Frame(0);
            if (e.KeyCode == Keys.Right)
                right = false;
            if (e.KeyCode == Keys.Left)
                left = false;
            if (e.KeyCode == Keys.Up)
                jump = false;//*/
        }

        private void COUNTDOWN_Tick(object sender, EventArgs e)
        {
            timeLeft--;
        }
    }
}