using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Frogger.World
{
    /// <summary>
    /// Represents a world entity
    /// Each visible object in the screen must inherit from this class
    /// </summary>
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

        /// <summary>
        /// Get the GraphicsDeviceManager
        /// </summary>
        protected GraphicsDeviceManager GraphicsDeviceManager
        {
            get { return graphics; }
        }

        /// <summary>
        /// the location of the entity 
        /// </summary>
        public abstract Vector2 Location
        {
            get;
        }

        /// <summary>
        /// the size of the entity
        /// </summary>
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

        /// <summary>
        /// Represents a world entity
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphics"></param>
        /// <param name="textureName"></param>
        public WorldEntity(Game game, GraphicsDeviceManager graphics, string textureName , bool checkCollisions)
            : base(game)
        {
            this.graphics = graphics;
            this.content = new ContentManager(game.Services);

            this.mTextureName = textureName;
            this.mCheckCollisions = checkCollisions; 

           // if (checkCollisions == true)
             //   CollisionsManager.AddEntity(this);
        }


        /// <summary>
        /// Loads the graphics content
        /// </summary>
        /// <param name="loadAllContent"></param>
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

        /// <summary>
        /// Draw the worldentity
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            mSpriteBatch.Begin();
            
            mSpriteBatch.Draw(mTexture, new Rectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y), null, Color.White,mRotation, new Vector2(), SpriteEffects.None, 1f);

            mSpriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Unloads graphic contents
        /// </summary>
        /// <param name="unloadAllContent"></param>
        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
            {
                content.Unload();
            }
        }

        /// <summary>
        /// Dispose the world entity
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {/*
            if (disposing == true)
            {
                //if (mCheckCollisions == true)
                    //CollisionsManager.RemoveEntity(this);

                UnloadGraphicsContent(true);
            }*/

            base.Dispose(disposing);
        }

        #endregion
    }
}
