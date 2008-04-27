using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Frogger.Utils
{
    /// <summary>
    /// Used to watch the framerate
    /// </summary>
    class FPSManager : DrawableGameComponent
    {
        #region Variables

        SpriteFont mSpriteFont;
        private ContentManager mContent;
        private SpriteBatch mSpriteBatch;

        private int mFrameCount;
        private int mFrameRate;
        TimeSpan mElapsedTime;
        String max;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the FPSManager
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphicDevice"></param>
        public FPSManager(Game game , GraphicsDevice graphicDevice,String max)
            : base(game)
        {
            this.mContent = new ContentManager(game.Services);
            this.mSpriteBatch = new SpriteBatch(graphicDevice);
            this.max = max;
        }

        /// <summary>
        /// Load the sprite font
        /// </summary>
        /// <param name="loadAllContent"></param>
        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                this.mSpriteFont = mContent.Load<SpriteFont>("Content/SpriteFont");
            }
        }

        #endregion

        #region Draw / Update

        /// <summary>
        /// Draw the framerate
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
         
            this.mSpriteBatch.Begin();
            this.mSpriteBatch.DrawString(mSpriteFont,
                                         String.Format("FPS : {0}", mFrameRate),
                                         new Vector2(10, 750), Color.White);


            this.mSpriteBatch.DrawString(mSpriteFont,
                                         String.Format("To save the game, press P {0}",max),
                                         new Vector2(300, 750), Color.White);


            
            this.mSpriteBatch.End();

            mFrameCount++;

        }

        /// <summary>
        /// Update the framerate
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Measure our framerate.
            mElapsedTime += gameTime.ElapsedGameTime;

            if (mElapsedTime > TimeSpan.FromSeconds(1))
            {
                mElapsedTime -= TimeSpan.FromSeconds(1);
                mFrameRate = mFrameCount;
                mFrameCount = 0;
            }
        }

        #endregion

    }
}
