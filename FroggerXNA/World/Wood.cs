using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frogger.World
{
    class Wood : WorldEntity
    {
        #region Enum

        enum AIState
        {
            GoRight,
            GoLeft,
        }

        #endregion

        #region Constants

        private const float SPEED = 0.1f;

        #endregion

        #region Variables


        public Vector2 mLocation;

        #endregion

        #region Properties


        public override Vector2 Location
        {
            get { return mLocation; }
        }


        public override Vector2 Size
        {
            get { return new Vector2(120, 15); }
        }
        #endregion

        #region Initialization


        public Wood(Game game, GraphicsDeviceManager graphics, Vector2 location)
            : base(game, graphics, "content/wood", true)
        {
            this.mLocation = location;



        }

        #endregion


        public override void Update(GameTime gameTime)
        {
            if (mLocation.X <= 10)
            {
                this.mLocation.X = GraphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
            }

            base.Update(gameTime);
        }

    }


}