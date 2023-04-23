﻿namespace SuperMarioArtyutoBros
{
    public class Map
    {
        public string map;
        public Bitmap BMP;
        //Bitmap wood = new Bitmap("wood.jpg");
        //Bitmap wood02 = new Bitmap("wood02.jpg");

        public Map()
        {
            map = "";
            map += "...#................................................00000000000000000000000...............................................0000000000....ooo....00000000000000000.................00000000000000.....0000000000000000000000000...................................";
            map += "...#................................................00000000000000000000000...............................................0000000000....ooo....00000000000000000.................00000000000000.....0000000000000000000000000...................................";
            map += "...#................................................00000000000000000000000...............................................0000000000....ooo....00000000000000000.................00000000000000.....0000000000000000000000000...................................";
            map += "...#................................................####################..................####################..........................ooo.....................................................................................................................";
            map += "...############################################################################################################################################################################################################################################################.";
            map += "...##...................................................................................................................................ooo.....................................................................................................................";
            map += "...#................###................#.#..............................................................................................ooo.....................................................................................................................";
            map += "...#.***********************************************************************************************************************************************************************************************************************************************************";
            map += "...#...................##############################################################0000000000000000000000000000000000000000000000######################################################********...*****************************...............................";
            map += "...#.................................................###.............................00000000000000000000000000000000...................ooo..............................................................//////.................................................";
            map += "...#....................############.#............###................................00000000000000000000000000000000**.................ooo.....................................................................................................................";
            map += "...#.......................................................*******...................00000000000000000000000000000000..000000000000000000000000000000000000.........................11100000000000000000000000000000000000000000000000000000000.................";
            map += "...#....................#.############......###......................................000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000..............................";
            map += "...#.................................................................................00000000000000000000000000000000...........*************************************************..............555..............................................................";
            map += "...#....................................................................................000000000000000000000000........................ooo.......................................................5555..........................................................";
            map += "...#....................................................................................................................................ooo............................................................66.......................................................";
            map += "...#...........................................................................................................................*****....ooo.....................................................................................................................";
            map += "...#..............................................................***.....................................................-****.........ooo.....................................................................................................................";
            map += "...#................................................................................0000000000000000000000000000000005555555555.........ooo.....................................................................................................................";
            map += "...#....................................................................................................................................ooo.....................................................................................................................";
            map += "...#...........................................................444444444444444444444444.................................................ooo.....................................................................................................................";
            map += "...#....................................................................................................................................ooo.....................................................................................................................";

            GenerateIMG();
        }

        private void GenerateIMG()
        {
            int w, h, u;
            char v;
            Graphics gImg;

            // map properties
            w = 256;// MAP width
            h = 22; // MAP height
            u = 25; // IMAGE unit

            BMP = new Bitmap(w * u, h * u);
            gImg = Graphics.FromImage(BMP);

            // this.Text = bmp.Width.ToString();

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    v = map[(y * w) + x];
                    if (v == '.')
                        gImg.FillRectangle(Brushes.Tomato, x * u, y * u, u, u);
                    if (v == '*')
                        //gImg.DrawImage(wood02, x * u, y * u, u, u);
                    if (v == '#')
                        //gImg.DrawImage(wood, x * u, y * u, u, u);
                }
            }
        }
    }
}