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

        double TimeLastDraw = 0;

        //public int level;

        public int m_level;

        public int level
        {
            // Determines the Y map coordinate.  Y=0 is the top-most tile on
            // the map.  The Y coordinate represents the Y value of the left-most
            // displayed map tile.
            get { return this.m_level; }
            set { this.m_level = value; }
        }


        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the Score
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphicDevice"></param>
        public Score(Game game, GraphicsDevice graphicDevice,int ScoreValue)
            : base(game)
        {
            this.mContent = new ContentManager(game.Services);
            this.mSpriteBatch = new SpriteBatch(graphicDevice);
            this.ScoreValue = ScoreValue;
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

            ScoreValue = ScoreValue + 100;

           
             
            this.mSpriteBatch.Begin();
            this.mSpriteBatch.DrawString(mSpriteFont,
                                         String.Format("Score : {0}", ScoreValue),
                                         new Vector2(120, 750), Color.White);
            this.mSpriteBatch.DrawString(mSpriteFont,
                                         String.Format("Time : {0}", gameTime.TotalGameTime.TotalSeconds),
                                         new Vector2(350, 750), Color.White);

           this.mSpriteBatch.DrawString(mSpriteFont,
                        String.Format("Level : {0}", m_level),
                         new Vector2(1150, 750), Color.White);



                this.mSpriteBatch.End();
       
           

        }



        public void ScoreUpdate(int ScoreValue)
        {
            this.ScoreValue = ScoreValue;
        }


        /// <summary>
        /// Update the framerate
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
           // ScoreValue = 0;

            

                Draw(gameTime);
            }
        


  
        #endregion

    }
}
