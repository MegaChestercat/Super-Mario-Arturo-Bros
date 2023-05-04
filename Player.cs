using System.Security.Policy;

namespace SuperMarioArturoBros
{
    public class Player
    {
        Sprites mainSprite;

        public float fPlayerPosX = 1.0f;
        public float fPlayerPosY = 1.0f;

        private float fPlayerVelX = 0.0f;
        private float fPlayerVelY = 0.0f;
        Random ran;

        public bool finish, coin1, coin2, axolotl, goku, koopa, zombie1, zombie2, michael, cloro, chapopote, zubat, eye, star = false;

        public Sprites MainSprite
        {
            get { return mainSprite; }
        }

        public float FPlayerVelX
        {
            get { return fPlayerVelX; }
            set { fPlayerVelX = value; }
        }

        public float FPlayerVelY
        {
            get { return fPlayerVelY; }
            set { fPlayerVelY = value; }
        }

        public Player()
        {
            mainSprite = new Sprites(new Size(15, 21), new Size(15, 21), new Point(), Resource1.mario_RIGHT, Resource1.MARIO_LEFT);
            //mainSprite = new Sprites(new Size(58, 70), new Size(20, 25), new Point(), Resource1.LinkR, Resource1.LinkLFix1);
            ran = new Random();
        }

        public void Right(float fElapsedTime)
        {
            FPlayerVelX += (bPlayerOnGround ? 25.0f : 15.0f) * fElapsedTime;
            if (bPlayerOnGround)
                mainSprite.MoveRight();
        }

        public void Left(float fElapsedTime)
        {
            FPlayerVelX += (bPlayerOnGround ? -25.0f : -15.0f) * fElapsedTime;
            if (bPlayerOnGround)
                mainSprite.MoveLeft();
        }

        public void Frame(int x)
        {
            mainSprite.Frame(x);
        }
        public void Stop()
        {
            mainSprite.Frame(0);
        }

        public bool bPlayerOnGround = false;

        public void Update(float fElapsedTime, Map map)
        {
            //Gravity
            fPlayerVelY += 20.0f * fElapsedTime;//---------------

            // Drag
            if (bPlayerOnGround)
            {
                fPlayerVelX += -3.0f * fPlayerVelX * fElapsedTime;
                if (Math.Abs(fPlayerVelX) < 0.01f)
                    fPlayerVelX = 0.0f;
            }

            // Clamp velocities
            if (fPlayerVelX > 10.0f)
                fPlayerVelX = 10.0f;

            if (fPlayerVelX < -10.0f)
                fPlayerVelX = -10.0f;

            if (fPlayerVelY > 100.0f)
                fPlayerVelY = 100.0f;

            if (fPlayerVelY < -100.0f)
                fPlayerVelY = -100.0f;

            float fNewPlayerPosX = fPlayerPosX + fPlayerVelX * fElapsedTime;
            float fNewPlayerPosY = fPlayerPosY + fPlayerVelY * fElapsedTime;

            CheckPicks(map, fNewPlayerPosX, fNewPlayerPosY, 'o', '.');
            CheckPicks(map, fNewPlayerPosX, fNewPlayerPosY, '3', '.');
            int temp = ran.Next(1,5);
            if(temp == 2) CheckPicks(map, fNewPlayerPosX, fNewPlayerPosY, 'Q', 'o');
            if (temp == 3) CheckPicks(map, fNewPlayerPosX, fNewPlayerPosY, 'Q', '3');
            if (temp == 4) CheckPicks(map, fNewPlayerPosX, fNewPlayerPosY, 'Q', 'U');
            CheckPicks(map, fNewPlayerPosX, fNewPlayerPosY, 'f', '.');
            CheckPick(map, fNewPlayerPosX, fNewPlayerPosY, 'L');
            CheckPick(map, fNewPlayerPosX, fNewPlayerPosY, 'Y');
            CheckInteractions(map, fNewPlayerPosX, fNewPlayerPosY, 'x');
            CheckInteractions(map, fNewPlayerPosX, fNewPlayerPosY, 'm');
            CheckInteractions(map, fNewPlayerPosX, fNewPlayerPosY, 'g');
            CheckInteractions(map, fNewPlayerPosX, fNewPlayerPosY, '$');
            CheckInteractions(map, fNewPlayerPosX, fNewPlayerPosY, '!');
            CheckInteractions(map, fNewPlayerPosX, fNewPlayerPosY, 'z');
            CheckInteractions(map, fNewPlayerPosX, fNewPlayerPosY, 'e');
            CheckInteractions(map, fNewPlayerPosX, fNewPlayerPosY, 'i');
            CheckInteractions(map, fNewPlayerPosX, fNewPlayerPosY, 'k');
            CheckInteractions(map, fNewPlayerPosX, fNewPlayerPosY, '1');
            CheckInteractions(map, fNewPlayerPosX, fNewPlayerPosY, '2');
            CheckInteractions(map, fNewPlayerPosX, fNewPlayerPosY, 'U');
            CheckPicks(map, fNewPlayerPosX, fNewPlayerPosY, 'U', '.');


            // COLLISION
            if (fPlayerVelX <= 0)//left
            {
                if ((map.GetTile((int)(fNewPlayerPosX + 0.0f), (int)(fPlayerPosY + 0.0f)) != '.') || (map.GetTile((int)(fNewPlayerPosX + 0.0f), (int)(fPlayerPosY + 0.9f)) != '.'))
                {
                    if (fPlayerVelX != 0)
                        fNewPlayerPosX = (int)fNewPlayerPosX + 1;
                    fPlayerVelX = 0;
                }
            }
            else//right
            {
                if ((map.GetTile((int)(fNewPlayerPosX + 1.0f), (int)(fPlayerPosY + 0.0f)) != '.') || (map.GetTile((int)(fNewPlayerPosX + 1.0f), (int)(fPlayerPosY + 0.9f)) != '.'))
                {
                    if (fPlayerVelX != 0)
                        fNewPlayerPosX = (int)fNewPlayerPosX;

                    fPlayerVelX = 0;
                }
            }

            //bPlayerOnGround = false;
            if (fPlayerVelY <= 0)// up
            {
                if ((map.GetTile((int)(fNewPlayerPosX + 0.0f), (int)(fNewPlayerPosY + 0.0f)) != '.') || (map.GetTile((int)(fNewPlayerPosX + 0.9f), (int)(fNewPlayerPosY + 0.0f)) != '.'))
                {
                    fNewPlayerPosY = (int)fNewPlayerPosY + 1;
                    fPlayerVelY = 0;
                }
            }
            else
            {
                if ((map.GetTile((int)(fNewPlayerPosX + 0.0f), (int)(fNewPlayerPosY + 1.0f)) != '.') || (map.GetTile((int)(fNewPlayerPosX + 0.9f), (int)(fNewPlayerPosY + 1f)) != '.'))
                {
                    fNewPlayerPosY = (int)fNewPlayerPosY;
                    fPlayerVelY = 0;
                    if (!bPlayerOnGround)
                        Frame(0);

                    bPlayerOnGround = true;
                }
            }

            fPlayerPosX = fNewPlayerPosX;
            fPlayerPosY = fNewPlayerPosY;

            mainSprite.Display(map.g);
        }

        private void CheckPicks(Map map, float fNewPlayerPosX, float fNewPlayerPosY, char c, char c2)
        {
            // Check for pickups!
            if (map.GetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 0.0f) == c)
            {
                if(c == 'o')
                {
                    map.SetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 0.0f, c2);
                    coin1 = true;
                }
                else if(c == '3')
                {
                    map.SetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 0.0f, c2);
                    coin2 = true;
                }
                else
                {
                    map.SetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 0.0f, c2);
                }
            }

            if (map.GetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 1.0f) == c)
            {
                if (c == 'o')
                {
                    map.SetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 1.0f, c2);
                    coin1 = true;
                }
                else if (c == '3')
                {
                    map.SetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 1.0f, c2);
                    coin2 = true;
                }
                else
                {
                    map.SetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 1.0f, c2);
                }
            }

            if (map.GetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 0.0f) == c)
            {
                if (c == 'o')
                {
                    map.SetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 0.0f, c2);
                    coin1 = true;
                }
                else if (c == '3')
                {
                    map.SetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 0.0f, c2);
                    coin2 = true;
                }
                else
                {
                    map.SetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 0.0f, c2);
                }
            }

            if (map.GetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 1.0f) == c)
            {
                if (c == 'o')
                {
                    map.SetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 1.0f, c2);
                    coin1 = true;
                }
                else if (c == '3')
                {
                    map.SetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 1.0f, c2);
                    coin2 = true;
                }
                else
                {
                    map.SetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 1.0f, c2);
                }
            }
        }

        private void CheckPick(Map map, float fNewPlayerPosX, float fNewPlayerPosY, char c)
        {
            if (map.GetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 0.0f) == c)
            {
                finish = true;
            }
        }

        private void CheckInteractions(Map map, float fNewPlayerPosX, float fNewPlayerPosY, char c)
        {
            if (map.GetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 0.0f) == c)
            {
                if (c == 'x') axolotl = true;
                else if (c == '$') cloro = true;
                else if (c == '!') chapopote = true;
                else if (c == 'z') zubat = true;
                else if (c == 'e') eye = true;
                else if (c == 'U') star = true;
                else if (c == 'm') michael = true;
                else if (c == 'k') koopa = true;
                else if (c == 'i') koopa = true;
                else if (c == '1') zombie1 = true;
                else if (c == '2') zombie2 = true;

            }
            if (map.GetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 1.0f) == c)
            {
                if (c == '$') cloro = true;
                else if (c == '!') chapopote = true;
                else if (c == 'z') zubat = true;
                else if (c == 'e') eye = true;
                else if (c == 'U') star = true;
                else if (c == 'k') koopa = true;
                else if (c == 'i') koopa = true;
                else if (c == '1') zombie1 = true;
                else if (c == '2') zombie2 = true;
            }
            if (map.GetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 1.0f) == c)
            {
                if (c == 'z') zubat = true;
                else if (c == 'e') eye = true;
                else if (c == 'U') star = true;
                else if (c == 'k') koopa = true;
                else if (c == 'i') koopa = true;
                else if (c == '1') zombie1 = true;
                else if (c == '2') zombie2 = true;
            }
            if (map.GetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 0.0f) == c)
            {
                if (c == 'z') zubat = true;
                else if (c == 'e') eye = true;
                else if (c == 'U') star = true;
                else if (c == 'g') goku = true;
                else if (c == 'k') koopa = true;
                else if (c == 'i') koopa = true;
                else if (c == '1') zombie1 = true;
                else if (c == '2') zombie2 = true;
            }
        }

    }

}