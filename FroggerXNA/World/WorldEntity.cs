using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Frogger.World
{
    abstract class WorldEntity : DrawableGameComponent
    {
        #region Variables

        GraphicsDeviceManager graphics;
        ContentManager content;
        SpriteBatch mSpriteBatch;
        Texture2D mTexture;
        string mTextureName;
        bool mCheckCollisions;

        #endregion

        #region Properties

  
        protected GraphicsDeviceManager GraphicsDeviceManager
        {
            get { return graphics; }
        }

        public abstract Vector2 Location
        {
            get;
        }


        public abstract Vector2 Size
        {
            get;
        }

        private float mRotation;

        public float Rotation
        {
            get { return mRotation; }
            set { mRotation = value; }
        }


        #endregion

        #region Initialization


        public WorldEntity(Game game, GraphicsDeviceManager graphics, string textureName , bool checkCollisions)
            : base(game)
        {
            this.graphics = graphics;
            this.content = new ContentManager(game.Services);

            this.mTextureName = textureName;
            this.mCheckCollisions = checkCollisions; 

        }



        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                mSpriteBatch = new SpriteBatch(graphics.GraphicsDevice);
                mTexture = content.Load<Texture2D>(mTextureName);
            }
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            mSpriteBatch.Begin();
            
            mSpriteBatch.Draw(mTexture, new Rectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y), null, Color.White,mRotation, new Vector2(), SpriteEffects.None, 1f);

            mSpriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

        #region Dispose


        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
            {
                content.Unload();
            }
        }


        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }

        #endregion
    }
}
