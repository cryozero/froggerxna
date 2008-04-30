using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Frogger.Utils;

namespace Frogger.World
{
    class Frog : WorldEntity
    {
        #region Constants

        const float SPEED = 0.6f; //Frog speed
        String position;

        #endregion

        #region Variables

        public Vector2 mLocation;

        double TimeLastSound= 0;



        #endregion

        #region Properties

        public override Vector2 Location
        {
            get { return mLocation; }
        }

        public override Vector2 Size
        {
            get { return new Vector2(64, 64); }
        }

        #endregion

        #region Initialization

   
        public Frog(Game game, GraphicsDeviceManager graphics,String image,String position)
            : base(game, graphics, image, true)
        {

            if (position == "C") //Central frog (green)
            {
                this.mLocation = new Vector2(620, 670);
            }

            if (position == "L") //Left frog
            {
                this.mLocation = new Vector2(520, 670);
            }

            if (position == "R") //Right frog
            {
                this.mLocation = new Vector2(720, 670);
            }


            this.position=position; //Actual position

        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {

            //Central frog and Right frog , normal keyboard

            if (this.position == "C" || this.position == "R")
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    mLocation.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    if (mLocation.X > 0)
                        mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

                }

                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if (mLocation.Y > 0)
                        mLocation.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    mLocation.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

                }

            }


            //Left frog, keyboard QSDZ

            if (this.position == "L")
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                   
                    mLocation.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    if (mLocation.X > 0)
                        mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

                }

                if (Keyboard.GetState().IsKeyDown(Keys.Z))
                {
                    if (mLocation.Y > 0)
                        mLocation.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    mLocation.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

                }

            }




                        double timeElapsed = gameTime.TotalGameTime.TotalMilliseconds - TimeLastSound;

                        if (timeElapsed > 1000 && Keyboard.GetState().IsKeyDown(Keys.Up) )
                         {
                             Sound.Play(Sounds.Frog);
                             TimeLastSound = gameTime.TotalGameTime.TotalMilliseconds;
                         }


            base.Update(gameTime);
        }

        #endregion
    }
}
