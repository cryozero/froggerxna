using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Frogger.Utils;

namespace Frogger.World
{

    class Car : WorldEntity
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
            get { return new Vector2(64, 40); }
        }
        #endregion

        #region Initialization


        public Car(Game game, GraphicsDeviceManager graphics, Vector2 location)
            : base(game, graphics, "content/car", true)
        {
            Sound.Initialize();
            this.mLocation = location;
            
 

        }

        #endregion


        public override void Update(GameTime gameTime)
        {
        
            if (mLocation.X <= 10)
            {
                this.mLocation.X = GraphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
                Sound.Play(Sounds.Car);
            }

            base.Update(gameTime);
        }

    }


}