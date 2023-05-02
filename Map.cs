using System.Drawing.Drawing2D;
using System.Media;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace SuperMarioArturoBros
{
    public class Map
    {
        private string map;
        public Bitmap bmp;
        public Sprites BG;
        //Bitmap wood = new Bitmap("wood.jpg");
        //Bitmap wood02 = new Bitmap("wood02.jpg");

        int divs = 3;
        public int nTileWidth = 20;
        public int nTileHeight = 20;
        int nLevelWidth, nLevelHeight;
        Sprites coin;
        SoundPlayer soundPlayer;
        int score;
        bool isP1 = true;
        Thread thread, threadStop;
        public Graphics g;

        public Map(Size size)
        {
            map = "";
            map += "..........................................................................................................................0000000000.............................................00000000000000.....0000000000000000000000000..........*........................";
            map += "..........................................................................................................................0000000000...........00000000000000000.................00000000000000.....0000000000000000000000000...........*.......................";
            map += "..........................................................................................................................0000000000...........00000000000000000.................00000000000000.....0000000000000000000000000............*......................";
            map += "..........................................................................................####################..................................................................................................................................................";
            map += "..............................................................................................................................................#############################################################################################*###################.";
            map += "............................................................................................................................................................................................................................................*...................";
            map += ".............................................................................................................................................................................................................................................*..................";
            map += "...........................................................................................................................................*********************************************************************************************************************";
            map += ".........................................................................................................................................................................................********...*****************************..............*................";
            map += ".....................................................................................00000000000000000000000000000000...................ooo..............................................................//////.................................*......*........";
            map += ".....................................................................................0000000BBBBBBBBBBBBBBB0B00000000**.................ooo......................................................................................................*......*.......";
            map += ".................................................#############.......................0000000000BBBBBBBBBBB00000000000..000000000000000000000000000000000000.........................11100000000000000000000000000000000000000000000000000000000...*......*......";
            map += ".....................................................................................0000000BBBBBBBBBBBBBBBB0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000.................*......**....";
            map += ".....0...............................................................................00000000000000000000000000000000...........*************************************************..............555..................................................*.......*...";
            map += "........o....BBBBBBB........................BBBB.......##########.......................000000000000000000000000........................ooo.......................................................5555...............................................*..........";
            map += ".......................................##########................##.....................................................................ooo............................................................66.............................................*.........";
            map += "...............................................................................................................................*****....ooo............................................................................................................*........";
            map += "..................................................................***.....................................................-****.........ooo.............................................................................................................*.......";
            map += "..........................o...............k.......................o..............BBBBBBBBBBBBBB00000000000000000000005555555555.........ooo..............................................................................................................*......";
            map += "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBbbbbbbbbbbbbbb.............................................................................ooo...............................................................................................................*.....";
            map += "..............................................................BBBBBBBBBBBBBBBBBBB444444.................................................ooo................................................................................................................*....";
            map += "........................................................................................................................................ooo.................................................................................................................*...";

            nLevelWidth = 256;
            nLevelHeight = 22;

            bmp = new Bitmap(size.Width / divs, size.Height / divs);

            g = Graphics.FromImage(bmp);
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.SmoothingMode = SmoothingMode.HighSpeed;
        }

        public void Draw(PointF cameraPos, string message, Player player, int timeLeft)
        {
            // Draw Level based on the visible tiles on our picturebox (canvas)
            int nVisibleTilesX = bmp.Width / nTileWidth;
            int nVisibleTilesY = bmp.Height / nTileHeight;

            // Calculate Top-Leftmost visible tile
            float fOffsetX = cameraPos.X - (float)nVisibleTilesX / 2.0f;
            float fOffsetY = cameraPos.Y - (float)nVisibleTilesY / 2.0f;

            // Clamp camera to game boundaries
            if (fOffsetX < 0) fOffsetX = 0;
            if (fOffsetY < 0) fOffsetY = 0;
            if (fOffsetX > nLevelWidth - nVisibleTilesX) fOffsetX = nLevelWidth - nVisibleTilesX;
            if (fOffsetY > nLevelHeight - nVisibleTilesY) fOffsetY = nLevelHeight - nVisibleTilesY;

            float fTileOffsetX = (fOffsetX - (int)fOffsetX) * nTileWidth;
            float fTileOffsetY = (fOffsetY - (int)fOffsetY) * nTileHeight;
            GC.Collect();
            //Draw visible tile map//*--------------------DRAW------------------------------
            Size sizeSky = new Size(bmp.Width, bmp.Height*3);
            Size vizSky = new Size(bmp.Width*4, bmp.Height *2);
            BG = new Sprites(sizeSky, vizSky, new Point(-(int)cameraPos.X, (int)cameraPos.Y), Resource1.BG, Resource1.BG);
            BG.Display(g);
            char c;
            float stepX, stepY;
            for (int x = -1; x < nVisibleTilesX + 2; x++)
            {
                for (int y = -1; y < nVisibleTilesY + 2; y++)
                {
                    c = GetTile(x + (int)fOffsetX, y + (int)fOffsetY);
                    stepX = x * nTileWidth - fTileOffsetX;
                    stepY = y * nTileHeight - fTileOffsetY;
                    //g.FillRectangle(Brushes.Cyan, stepX, stepY, nTileWidth, nTileHeight);
                    switch (c)
                    {
                        case '.':
                            //g.FillRectangle(Brushes.Cyan, stepX, stepY, nTileWidth, nTileHeight);
                            break;
                        case '#':
                            g.FillRectangle(Brushes.Red, stepX, stepY, nTileWidth, nTileHeight);
                            g.FillRectangle(Brushes.Black, stepX, stepY, nTileWidth, nTileHeight);
                            g.FillRectangle(Brushes.DarkRed, stepX + 1, stepY + 1, nTileWidth - 2, nTileHeight - 2);
                            g.FillEllipse(Brushes.DarkRed, stepX, stepY, nTileWidth, nTileHeight);
                            g.FillEllipse(Brushes.DarkSlateGray, stepX, stepY, nTileWidth / 2, nTileHeight / 2);
                            g.DrawLine(Pens.Black, stepX + nTileHeight / 2, stepY + nTileHeight / 2, stepX + nTileHeight, stepY + nTileHeight - 3);
                            g.DrawLine(Pens.Maroon, stepX + nTileHeight / 2, 2 + stepY + nTileHeight / 2, 1 + stepX + nTileHeight, stepY + nTileHeight - 2);
                            g.DrawLine(Pens.Black, stepX + nTileHeight / 2, stepY, stepX + nTileHeight / 2, stepY + nTileHeight * 2 / 3);
                            g.DrawLine(Pens.Black, 1 + stepX + nTileHeight / 2, stepY + 1, 2 + stepX + nTileHeight / 2, 3 + stepY + nTileHeight * 2 / 3);
                            g.DrawLine(Pens.Maroon, 2 + stepX + nTileHeight / 2, stepY, 1 + stepX + nTileHeight / 2, stepY + nTileHeight * 2 / 3);
                            g.DrawLine(Pens.Black, stepX + nTileHeight / 2, stepY + nTileHeight * 2 / 3, stepX, stepY + nTileHeight / 3);
                            g.DrawRectangle(Pens.Black, stepX + nTileHeight / 2, stepY, nTileWidth, nTileHeight - 1);
                            g.DrawRectangle(Pens.Gray, stepX, stepY, nTileWidth, nTileHeight - 1);
                            break;
                        case 'a'://ejemplo para sustituir parte del mapa con otra animación
                            //coin.posX = stepX;
                            //coin.posY = stepY;
                            //coin.MoveSlow(2);
                            coin.Display(g);/*
                            if (coin.counter > 50)
                            {
                                SetTile(x + (int)fOffsetX, y + (int)fOffsetY, '.');
                                coin.counter = 0;
                            }*/
                            break;
                        case 'B':
                            g.FillRectangle(Brushes.Black, stepX, stepY, nTileWidth, nTileHeight);
                            g.FillPolygon(Brushes.Gray, new PointF[] { new PointF(stepX, stepY), new PointF(stepX + nTileWidth, stepY), new PointF(stepX, stepY + nTileHeight) });
                            g.FillRectangle(Brushes.DarkGray, stepX + nTileHeight / 4, stepY + nTileHeight / 4, nTileWidth / 2, nTileHeight / 2);
                            g.DrawLine(Pens.DarkGray, stepX, stepY, stepX + nTileWidth, stepY + nTileHeight);
                            break;
                        case '?':
                            g.FillRectangle(Brushes.Yellow, stepX, stepY, nTileWidth, nTileHeight);
                            break;
                        case 'o':
                            //coin.posX = stepX;
                            //coin.posY = stepY;
                            //coin.MoveSlow(7);
                            //coin.Display(g);
                            break;
                        case '*':
                            //g.DrawImage(Resource1.questionS, stepX, stepY, nTileWidth, nTileHeight);
                            break;
                        default:

                            g.FillRectangle(Brushes.Black, stepX, stepY, nTileWidth, nTileHeight);
                            g.FillRectangle(Brushes.DarkRed, stepX + 1, stepY + 1, nTileWidth - 2, nTileHeight - 2);
                            g.DrawLine(Pens.Black, stepX + nTileHeight / 2, stepY + nTileHeight / 2, stepX + nTileHeight, stepY + nTileHeight - 3);
                            g.DrawLine(Pens.Maroon, stepX + nTileHeight / 2, 2 + stepY + nTileHeight / 2, 1 + stepX + nTileHeight, stepY + nTileHeight - 2);
                            g.DrawLine(Pens.Black, stepX + nTileHeight / 2, stepY, stepX + nTileHeight / 2, stepY + nTileHeight * 2 / 3);
                            g.DrawLine(Pens.Black, 1 + stepX + nTileHeight / 2, stepY + 1, 2 + stepX + nTileHeight / 2, 3 + stepY + nTileHeight * 2 / 3);
                            g.DrawLine(Pens.Maroon, 2 + stepX + nTileHeight / 2, stepY, 1 + stepX + nTileHeight / 2, stepY + nTileHeight * 2 / 3);
                            g.DrawLine(Pens.Black, stepX + nTileHeight / 2, stepY + nTileHeight * 2 / 3, stepX, stepY + nTileHeight / 3);
                            //g.DrawRectangle(Pens.Black, stepX + nTileHeight / 2, stepY, nTileWidth, nTileHeight - 1);
                            g.DrawRectangle(Pens.Gray, stepX, stepY, nTileWidth, nTileHeight - 1);
                            //g.DrawImage(Resource1.TILER, stepX, stepY, nTileWidth, nTileHeight);//heavy
                            break;
                    }
                    //g.DrawRectangle(Pens.Gray, stepX, stepY, nTileWidth, nTileHeight);
                }
            }

            g.DrawString("MARIO \n  " + score, new Font("Arcade Normal", 10), Brushes.White, 5, 5);
            g.DrawString("TIME \n " + timeLeft, new Font("Arcade Normal", 10), Brushes.White, 280, 5);
            g.DrawString("LEVEL \n  1", new Font("Arcade Normal", 10), Brushes.White, 150, 5);
            player.MainSprite.posX = (player.fPlayerPosX - fOffsetX) * nTileWidth;
            player.MainSprite.posY = (player.fPlayerPosY - fOffsetY) * nTileHeight;
        }

        public void SetTile(float x, float y, char c)//changes the tile
        {
            if (x >= 0 && x < nLevelWidth && y >= 0 && y < nLevelHeight)
            {
                int index = (int)y * nLevelWidth + (int)x;
                map = map.Remove(index, 1).Insert(index, c.ToString());
                //Play();
                score += 100;

            }
        }

        public char GetTile(float x, float y)
        {
            if (x >= 0 && x < nLevelWidth && y >= 0 && y < nLevelHeight)
                return map[(int)y * nLevelWidth + (int)x];
            else
                return ' ';
        }
    }
}