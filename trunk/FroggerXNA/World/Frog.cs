using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Frogger.Effects;

namespace Frogger.World
{
    /// <summary>
    /// Represents the ship of the player
    /// </summary>
    class Frog : WorldEntity
    {
        #region Constants

        /// <summary>
        /// Default speed of the ship
        /// </summary>
        const float SPEED = 0.2f;

        #endregion

        #region Variables

        /// <summary>
        /// location of the ship
        /// </summary>
        Vector2 mLocation;

        /// <summary>
        /// time when the player shot for the last time
        /// </summary>
        double mTimeLastShoot = 0;

        ParticlesEmitter mEngineEffect;

        #endregion

        #region Properties

        /// <summary>
        /// Location of the ship in the screen
        /// </summary>
        public override Vector2 Location
        {
            get { return mLocation; }
        }

        /// <summary>
        /// Size of the ship
        /// </summary>
        public override Vector2 Size
        {
            get { return new Vector2(64, 64); }
        }

        #endregion

        #region Initialization

   
        public Frog(Game game, GraphicsDeviceManager graphics)
            : base(game, graphics, "content/frog", true)
        {
            this.mLocation = new Vector2(550, 660);

            mEngineEffect = new ParticlesEmitter(game, graphics, new Vector2(this.Location.X + this.Size.X / 2f,
                                                                this.Location.Y + this.Size.Y));

           // game.Components.Add(mEngineEffect);
        }

        #endregion

        #region Update

        /// <summary>
        /// Update the player's location
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            mEngineEffect.Location = new Vector2(this.Location.X + this.Size.X / 2f, this.Location.Y + this.Size.Y);

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
               // if (mLocation.X < Background.BACKGROUND_WIDTH - Size.X)
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
               // if (mLocation.Y < Background.BACKGROUND_HEIGHT - Size.Y)
                    mLocation.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                double timeElapsed = gameTime.TotalGameTime.TotalMilliseconds - mTimeLastShoot;

                if (timeElapsed > 500)
                {
                    //PlayerShoot shoot = new PlayerShoot(Game, GraphicsDeviceManager, mLocation);
                    //Game.Components.Add(shoot);

                    mTimeLastShoot = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            base.Update(gameTime);
        }

        #endregion
    }
}
