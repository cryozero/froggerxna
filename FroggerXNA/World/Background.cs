using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Frogger.World
{
    /// <summary>
    /// Represents the background of the game
    /// </summary>
    class Background : WorldEntity
    {
        #region Constants

        public int BACKGROUND_HEIGHT = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        public  int BACKGROUND_WIDTH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        
        #endregion

        #region Properties

        /// <summary>
        /// Location of the background
        /// </summary>
        public override Vector2 Location
        {
            get { return new Vector2(); }
        }

        /// <summary>
        /// Size of the background
        /// </summary>
        public override Vector2 Size
        {
            get { return new Vector2(BACKGROUND_WIDTH, BACKGROUND_HEIGHT); }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a background
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphics"></param>
        public Background(Game game, GraphicsDeviceManager graphics)
            : base(game, graphics, "Content/fond", false)
        {

        }

        #endregion

    }
}
