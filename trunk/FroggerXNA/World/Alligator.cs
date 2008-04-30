using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Frogger.Utils;

namespace Frogger.World
{
    class Alligator : WorldEntity
    {
        #region Enum

        enum AIState
        {
            GoRight,
            GoLeft,
        }

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

        public override void Update(GameTime gameTime)
        {

            if (mLocation.X <= 10)
            {
                this.mLocation.X = GraphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
            }

            base.Update(gameTime);
        }

        #endregion
    }


}