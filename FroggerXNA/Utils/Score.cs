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
    class Score : DrawableGameComponent
    {
        #region Variables

        SpriteFont mSpriteFont;
        private ContentManager mContent;
        private SpriteBatch mSpriteBatch;

        TimeSpan mElapsedTime;
        public int ScoreValue;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the Score
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphicDevice"></param>
        public Score(Game game, GraphicsDevice graphicDevice)
            : base(game)
        {
            this.mContent = new ContentManager(game.Services);
            this.mSpriteBatch = new SpriteBatch(graphicDevice);
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

        #region Draw

        /// <summary>
        /// Draw the framerate
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {

            this.mSpriteBatch.Begin();
            this.mSpriteBatch.DrawString(mSpriteFont,
                                         String.Format("Score : {0}", ScoreValue),
                                         new Vector2(120, 750), Color.White);
            this.mSpriteBatch.End();

           

        }

        /// <summary>
        /// Update the framerate
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Measure our framerate.
            mElapsedTime += gameTime.ElapsedGameTime;

            if (mElapsedTime > TimeSpan.FromSeconds(2))
            {
                Draw(gameTime);
            }
        }


  
        #endregion

    }
}
