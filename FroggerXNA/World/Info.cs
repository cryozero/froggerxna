using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Frogger.Utils;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Frogger.World
{

    /// <summary>
    /// Represents the ship of the player
    /// </summary>
    class Info:DrawableGameComponent
    {




        #region Variables

        /// <summary>
        /// Current AI state of this ship
        /// </summary>
 

        /// <summary>
        /// location of the ship
        /// </summary>
        public Vector2 mLocation;


        SpriteFont mSpriteFont;

        private SpriteBatch mSpriteBatch;

  
        private ContentManager mContent;


        private int mFrameCount;
        private int mFrameRate;
        TimeSpan mElapsedTime;
        String max;


        #endregion

        #region Properties



        #endregion

        #region Initialization


        public Info(Game game , GraphicsDevice graphicDevice,String max)
            : base(game)
        {
            this.mContent = new ContentManager(game.Services);
            this.mSpriteBatch = new SpriteBatch(graphicDevice);
            this.max = max;
        }



        #endregion

        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                this.mSpriteFont = mContent.Load<SpriteFont>("Content/SpriteFont");
            }
        }


        public override void Draw(GameTime gameTime)
        {

            this.mSpriteBatch.Begin();

           /* this.mSpriteBatch.DrawString(mSpriteFont,
                                         String.Format("To save the game, press P {0}", this.max),
                                         new Vector2(300, 750), Color.White);
            */


            this.mSpriteBatch.End();

            base.Draw(gameTime);

  

        }

        public override void Update(GameTime gameTime)
        {

            // Measure our framerate.
            mElapsedTime = gameTime.ElapsedGameTime;

            if (mElapsedTime > TimeSpan.FromSeconds(1))
            {
                this.max = max + 1;
            }

            base.Update(gameTime);
        }

    }


}