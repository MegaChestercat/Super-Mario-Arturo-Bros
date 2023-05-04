using System.CodeDom.Compiler;
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
        SolidBrush sepia = new SolidBrush(Color.FromArgb(219, 190, 153));
        SolidBrush smoke = new SolidBrush(Color.FromArgb(132, 147, 156));

        int divs = 3;
        public int nTileWidth = 20;
        public int nTileHeight = 20;
        int nLevelWidth, nLevelHeight;
        Sprites coin;
        Sprites coin2;
        SoundPlayer soundPlayer;
        public int score = 0;
        bool isP1 = true;
        Thread thread, threadStop;
        public Graphics g;


        public Map(Size size)
        {
            coin = new Sprites(new Size(134, 134), new Size(nTileWidth, nTileHeight), new Point(), Resource1.croped, Resource1.croped);
            coin2 = new Sprites(new Size(35, 33), new Size(nTileWidth, nTileHeight), new Point(), Resource1.coin, Resource1.coin);
            map = "";
            map += "............................................................................................z.......................................z............................................GGGGGGGGGGGGGG.................................................................";
            map += ".........................e........................................z............................................................................BQQQ3oB...........................GUoooooooooo................................................C..................";
            map += "......SSSSS....................................e...........................C...............................................e................E..BBBBBBB.............e.............GGGGGGGGGGGGGG........................CCCC.............C..CC............C......";
            map += ".....SZZZZZS..............................................................CCC.......e.....CCCCCCCCCCCCCCCCCCCC...........................A.............................................................................CCC.................C........C...........";
            map += ".....SZZZZZS............................................e................CCCCC...............CCC................................................................z..................z.............E..............................................................";
            map += ".....SZZZZZS......................e.....................................CCCCCC................CCC.................................e.......B.....................................................................................................................";
            map += "......SSSSS......................................z........................C.................................................................B...B..........................z...................CC.CC.CC.........................................................";
            map += "......................e................................................z...................................................z.......................B...............e...............CC.........CCCCCCCCCC.........................CCC.C..........CCC.C...........";
            map += "........................................e....................................................k......1......2.........................................B............................CCC....A....CCCCCCCCC.............................CC...........CC.C.........H.";
            map += ".....................................................C.............z.................IIIIIIIIIIIIIIIIIIIIIIIIIIIIIII............z......ooo.....QQQ..........z..................................CC.CC.C.................z.....................................H..";
            map += ".................z.................................CCC.....o................z........IIIIIIIIIIIooo33oIIIIIIIIIIIIIII...................ooo..B.....B...................BQ.QB....B..........................................................................HH...";
            map += ".................................................CCCCCCCCCCCCC........................ioooooo3oooo333oooQQQ..................................B$$$$$B..............k....3......2.........................................................................HHH.....";
            map += "......................CCC.................................CC.........................IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII.................BBBBBBBBBBBBBBB........PPPPPPPPPPPPPPPPPPP.......................................................................HH.....";
            map += ".....................CCCCC............z..................CCCC.........z..............IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII................B.........................P.................................z...................z...................................HH.....";
            map += "....................CCCCCCC...........................CCCCCCCCCC.......................IIIIIIIIIIIIIIIIIIIIIIII........................ooo.....................P........................................................................................WW......";
            map += "..............................z...................................................................................................BB....3o3....................P......o..................3.....................................................TTTTTTTTTTTTTT...";
            map += "...................................3...o..o.............................X................3ooo3oo...................z....................ooo..............BBB...P.....oo...............BBBBB..................................................TTTTTTTTTTTTTTTT...";
            map += ".......................Q..........oN......B...........................XXXXX........2..o3ooo3ooo3oo3.k..........................B......333......................P..........Q.................................................................TTTTTTTTTTTTTTTTTT..";
            map += "..............3..o...............oNM..BBB......Q..................o.....X........EEEEEEEEEEEEEEEEEEEE.......................BB..........ooo............x.......P...............B................B.........................o.................WWWWWWWWWWWWWWWWWW..";
            map += "..............BBBQBB...B.........NMM..........................g......3......1.............................QQ............BB.............ooo...P....ffffBB.......P.............B...................B.....................B..3..B..............WWWWWWWWWWWWWWWWWW..";
            map += "................................NMMM...........Q.............AAAAAAAAAAAAAAAAQAAAAAAAA...............................B.................333...P!!!!!!!!!!!!!!!!!P$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$B............QQQQQQ......B.................YWWWWVVWWWWVVWWWWW..";
            map += ".....m......k.............k....NMMMM...1.............k.....1............................k......................k...........2...2...1....ooo..P!!!!!!!!!!!!!!!!!P$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$B.....k...1.....3..........................LWWWWWWWWWWWWWWWWW..";

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
            //Size sizeSky = new Size(bmp.Width, bmp.Height*3);
            //Size vizSky = new Size(bmp.Width*4, bmp.Height *2);
            
            //BG = new Sprites(sizeSky, vizSky, new Point(-(int)cameraPos.X, (int)cameraPos.Y), Resource1.BG, Resource1.BG);
            //BG.Display(g);
            char c;
            float stepX, stepY;
            for (int x = -1; x < nVisibleTilesX + 2; x++)
            {
                for (int y = -1; y < nVisibleTilesY + 2; y++)
                {
                    c = GetTile(x + (int)fOffsetX, y + (int)fOffsetY);
                    stepX = x * nTileWidth - fTileOffsetX;
                    stepY = y * nTileHeight - fTileOffsetY;
                    g.FillRectangle(sepia, stepX, stepY, nTileWidth, nTileHeight);
                    switch (c)
                    {
                        case '.':
                            g.FillRectangle(sepia, stepX, stepY, nTileWidth, nTileHeight);
                            break;
                        case '!':
                            g.DrawImage(Resource1.chapopote, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case '$':
                            g.DrawImage(Resource1.cloro, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'f':
                            g.DrawImage(Resource1.stonebrick, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'U':
                            g.DrawImage(Resource1.star, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'm':
                            g.DrawImage(Resource1.michael, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case '1':
                            g.DrawImage(Resource1.zombie1, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case '2':
                            g.DrawImage(Resource1.zombie2, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'g':
                            g.DrawImage(Resource1.Gokubig, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'k':
                            g.DrawImage(Resource1.koopa, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'i':
                            g.DrawImage(Resource1.koopa2, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'x':
                            g.DrawImage(Resource1.axolotl, stepX, stepY, nTileWidth + 1, nTileHeight+10);
                            break;
                        case 'z':
                            g.DrawImage(Resource1.zubat, stepX, stepY, nTileWidth + 1, nTileHeight+10);
                            break;
                        case 'e':
                            g.DrawImage(Resource1.eye, stepX, stepY, nTileWidth + 1, nTileHeight + 10);
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
                            coin.posX = stepX;
                            coin.posY = stepY;
                            coin.MoveSlow(2);
                            coin.Display(g);
                            if (coin.counter > 50)
                            {
                                SetTile(x + (int)fOffsetX, y + (int)fOffsetY, '.');
                                coin.counter = 0;
                            }
                            break;
                        case 'B':
                            g.DrawImage(Resource1.stonebrick, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case '?':
                            g.FillRectangle(Brushes.Yellow, stepX, stepY, nTileWidth, nTileHeight);
                            break;
                        case 'o':
                            coin.posX = stepX;
                            coin.posY = stepY;
                            coin.MoveSlow(1);
                            coin.Display(g);
                            if (coin.counter > 10)
                            {
                                coin.size = coin.temp;
                                coin.counter = 0;
                            }
                            break;
                        case '3':
                            coin2.posX = stepX;
                            coin2.posY = stepY;
                            coin2.MoveSlow(7);
                            coin2.Display(g);
                            break;
                        case 'Q':
                            g.DrawImage(Resource1.rainbow, stepX, stepY, nTileWidth+1, nTileHeight);
                            break;
                        case 'C':
                            g.FillRectangle(Brushes.White, stepX, stepY, nTileWidth, nTileHeight);
                            break;
                        case 'D':
                            g.FillRectangle(Brushes.White, stepX, stepY, nTileWidth, nTileHeight);
                            break;
                        case 'V':
                            g.DrawImage(Resource1.glass, stepX, stepY, nTileWidth, nTileHeight);
                            break;
                        case 'W':
                            g.DrawImage(Resource1.brick, stepX, stepY, nTileWidth+1, nTileHeight);
                            break;
                        case 'T':
                            g.DrawImage(Resource1.planks, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'H':
                            g.FillRectangle(smoke, stepX, stepY, nTileWidth, nTileHeight);
                            break;
                        case 'S':
                            g.FillRectangle(Brushes.Yellow, stepX, stepY, nTileWidth, nTileHeight);
                            break;
                        case 'Z':
                            g.FillRectangle(Brushes.Orange, stepX, stepY, nTileWidth, nTileHeight);
                            break;
                        case 'G':
                            g.DrawImage(Resource1.obsidian, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'I':
                            g.DrawImage(Resource1.ice, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'L':
                            g.DrawImage(Resource1.lower, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'Y':
                            g.DrawImage(Resource1.upper, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'M':
                            g.DrawImage(Resource1.mushroom1, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'N':
                            g.DrawImage(Resource1.mushroom2, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'P':
                            g.DrawImage(Resource1.purpur, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'E':
                            g.DrawImage(Resource1.prismarine, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'A':
                            g.DrawImage(Resource1.quartz, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        case 'X':
                            g.DrawImage(Resource1.stonebrick_mossy, stepX, stepY, nTileWidth + 1, nTileHeight);
                            break;
                        default:
                            g.DrawImage(Resource1.grass, stepX, stepY, nTileWidth+1, nTileHeight);
                            break;
                    }
                    //g.DrawRectangle(Pens.Gray, stepX, stepY, nTileWidth, nTileHeight);
                }
            }
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