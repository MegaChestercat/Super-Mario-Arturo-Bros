namespace SuperMarioArturoBros
{
    public class Sprites
    {
        Rectangle size, display;
        Bitmap imgDisplay, imgL, imgR;
        int increment;
        Point pos;

        public int PosY
        {
            get { return display.Y; }
            set { display.Y = value; }
        }

        public int PosX
        {
            get { return size.X; }
            set { size.X = value; }
        }

        public Sprites(Size original, Size display, Point starting, int increment, Bitmap right)
        {
            pos = starting;
            this.increment = increment;
            this.display = new Rectangle(starting.X, starting.Y, display.Width, display.Height);
            this.size = new Rectangle(0, 0, original.Width, original.Height);
            this.imgR = right;
            this.imgDisplay = right;
        }

        public Sprites(Size original, Size display, Point starting, Bitmap right, Bitmap left)
        {
            pos = starting;
            this.increment = original.Width;
            this.display = new Rectangle(starting.X, starting.Y, display.Width, display.Height);
            this.size = new Rectangle(0, 0, original.Width, original.Height);
            this.imgR = right;
            this.imgL = left;
            this.imgDisplay = right;
        }

        public void Position(int x, int y)
        {
            size.X = x;
            size.Y = y;
        }

        public void Frame(int x)
        {
            size.X = (x * size.Width) % imgDisplay.Width;
        }

        public void MoveReverse()
        {
            imgDisplay = imgL;
            size.X = (increment + size.X) % imgDisplay.Width;
        }

        public void MoveRight()
        {
            imgDisplay = imgR;
            size.X = (increment + size.X) % imgDisplay.Width;
        }

        public void MoveLeft()
        {
            size.X -= increment;
        }

        public void Display(Graphics g)
        {
            if (display.Y < pos.Y)
                display.Y += 15;

            g.DrawImage(imgDisplay, display, size, GraphicsUnit.Pixel);
        }
    }
}