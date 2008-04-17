using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frogger.World
{

    /// <summary>
    /// Represents the ship of the player
    /// </summary>
    class Car : WorldEntity
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
        private const float SPEED = 0.55f;

        #endregion

        #region Variables

        /// <summary>
        /// Current AI state of this ship
        /// </summary>
        AIState mCurrentState;

        /// <summary>
        /// location of the ship
        /// </summary>
        Vector2 mLocation;

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


        public Car(Game game, GraphicsDeviceManager graphics, Vector2 location)
            : base(game, graphics, "content/car", true)
        {
            this.mLocation = location;

            Random r = new Random();
            mCurrentState = (AIState)r.Next(0, 2);
        }

        #endregion

        #region Update

        /// <summary>
        /// Update the ennemy ship location and behavior
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

            base.Update(gameTime);
        }

        #endregion
    }


}