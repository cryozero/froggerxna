using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Frogger.Utils
{

    class FPSManager : DrawableGameComponent
    {
        #region Variables

        SpriteFont mSpriteFont;
        private ContentManager mContent;
        private SpriteBatch mSpriteBatch;

        private int mFrameCount;
        private int mFrameRate;
        TimeSpan mElapsedTime;

        #endregion

        #region Initialization

        public FPSManager(Game game , GraphicsDevice graphicDevice)
            : base(game)
        {
            this.mContent = new ContentManager(game.Services);
            this.mSpriteBatch = new SpriteBatch(graphicDevice);
           
        }


        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                this.mSpriteFont = mContent.Load<SpriteFont>("Content/SpriteFont");
            }
        }

        #endregion

        #region Draw / Update

        public override void Draw(GameTime gameTime)
        {
         
            this.mSpriteBatch.Begin();
            this.mSpriteBatch.DrawString(mSpriteFont,
                                         String.Format("FPS : {0}", mFrameRate),
                                         new Vector2(50, 50), Color.WhiteSmoke);

            this.mSpriteBatch.End();

            mFrameCount++;

        }


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
