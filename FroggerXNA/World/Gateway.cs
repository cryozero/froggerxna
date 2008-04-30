using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frogger.World
{

    class Gateway : WorldEntity
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

        AIState mCurrentState;

        public Vector2 mLocation;

        #endregion

        #region Properties

        public override Vector2 Location
        {
            get { return mLocation; }
        }

        public override Vector2 Size
        {
            get { return new Vector2(100, 100); }
        }
        #endregion

        #region Initialization


        public Gateway(Game game, GraphicsDeviceManager graphics, Vector2 location)
            : base(game, graphics, "content/gateway", true)
        {
            this.mLocation = location;

            Random r = new Random();
            mCurrentState = (AIState)r.Next(0, 2);
        }

        #endregion


    }


}