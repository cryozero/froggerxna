using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Frogger.Utils;

namespace Frogger.World
{

    /// <summary>
    /// Represents the ship of the player
    /// </summary>
    class Turtle : WorldEntity
    {
        #region Enum

        /// <summary>
        /// State of the AI
        /// </summary>
        enum AIState
        {
            GoRight,
            GoLeft,
        }

        #endregion

        #region Constants

        /// <summary>
        /// Fix the speed of the ennemy ship
        /// </summary>
        private const float SPEED = 0.1f;

        #endregion

        #region Variables


        public Vector2 mLocation;

        #endregion

        #region Properties

        /// <summary>
        /// Get the location
        /// </summary>
        public override Vector2 Location
        {
            get { return mLocation; }
        }

        /// <summary>
        /// Get the Size
        /// </summary>
        public override Vector2 Size
        {
            get { return new Vector2(64, 64); }
        }
        #endregion

        #region Initialization


        public Turtle(Game game, GraphicsDeviceManager graphics, Vector2 location)
            : base(game, graphics, "content/turtle", true)
        {
            Sound.Initialize();
            this.mLocation = location;



        }

        #endregion


        public override void Update(GameTime gameTime)
        {
            Sound.Update();
            // mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

            if (mLocation.X <= 10)
            {
                this.mLocation.X = GraphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
                Sound.Play(Sounds.Car);
            }

            base.Update(gameTime);
        }

    }


}