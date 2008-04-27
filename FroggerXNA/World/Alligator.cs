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
    class Alligator : WorldEntity
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
        private const float SPEED = 0.05f;

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
            get { return new Vector2(80, 50); }
        }
        #endregion

        #region Initialization


        public Alligator(Game game, GraphicsDeviceManager graphics, Vector2 location)
            : base(game, graphics, "content/alligator", true)
        {
            this.mLocation = location;


        }

        #endregion

        #region Update

        /// <summary>
        /// Update the ennemy ship location and behavior
        /// </summary>
        /// <param name="gameTime"></param>
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

        #endregion
    }


}