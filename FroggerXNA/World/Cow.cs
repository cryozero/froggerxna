using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Frogger.Utils;

namespace Frogger.World
{

    class Cow : WorldEntity
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
            get { return new Vector2(50, 50); }
        }
        #endregion

        #region Initialization


        public Cow(Game game, GraphicsDeviceManager graphics, Vector2 location)
            : base(game, graphics, "content/Cow", true)
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
                Sound.Play(Sounds.Cow);
            }

            base.Update(gameTime);
        }

        #endregion
    }


}