﻿namespace SuperMarioArturoBros
{
    public class Sprites
    {
        int increment;
        public RectangleF size, display, temp;
        Bitmap imgDisplay, imgL, imgR;
        public int counter;

        public float posX
        {
            set { display.X = value; }
        }

        public float posY
        {
            set { display.Y = value; }
        }

        public Bitmap ImgDisplay
        {
            get { return imgDisplay; }
            set { imgDisplay = value; }
        }

        public Sprites(Size original, Size display, Point starting, Bitmap right, Bitmap left)
        {
            counter = 0;
            this.increment = original.Width;
            this.display = new RectangleF(starting.X, starting.Y, display.Width, display.Height);
            this.size = new RectangleF(0, 0, original.Width, original.Height);
            this.temp = this.size;
            this.imgR = right;
            this.imgL = left;
            this.imgDisplay = right;
        }

        public void Frame(int x)
        {
            size.X = (x * size.Width) % imgDisplay.Width;
        }

        public void MoveLeft()
        {
            imgDisplay = imgL;
            size.X = (increment + size.X) % imgDisplay.Width;
        }

        public void MoveRight()
        {
            imgDisplay = imgR;
            size.X = (increment + size.X) % imgDisplay.Width;
        }
        public void MoveSlow(int value)
        {
            if (counter % value == 0)
                size.X = (increment + size.X) % imgDisplay.Width;

            counter++;
        }

        public void Display(Graphics g)//
        {
            g.DrawImage(imgDisplay, display, size, GraphicsUnit.Pixel);
        }
    }
}