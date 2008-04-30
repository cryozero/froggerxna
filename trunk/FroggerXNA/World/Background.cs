using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Frogger.Utils;

namespace Frogger.World
{
    class Background : WorldEntity
    {
        #region Constants

        public int BACKGROUND_HEIGHT = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height-50;
        public  int BACKGROUND_WIDTH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        
        #endregion

        #region Properties

        public override Vector2 Location
        {
            get { return new Vector2(); }
        }

        public override Vector2 Size
        {
            get { return new Vector2(BACKGROUND_WIDTH, BACKGROUND_HEIGHT); }
        }

        #endregion

        #region Initialization

        public Background(Game game, GraphicsDeviceManager graphics,String image)
            : base(game, graphics, image, false)
        {
            this.DrawOrder = 0;
        }

        #endregion

    }
}
