using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Frogger.Effects
{

    /// <summary>
    /// Emittes fire particles for the ship
    /// </summary>
    class ParticlesEmitter : DrawableGameComponent
    {

        #region Variables

        private Random mRandom;

        private Vector2 mLocation;

        private Particle[] mParticles;
        private Texture2D mTexture;

        private GraphicsDeviceManager mGraphics;
        private ContentManager mContent;

        private SpriteBatch mSpriteBatch;


        #endregion

        #region Properties

        /// <summary>
        /// Get or set the location of the emitter
        /// </summary>
        public Vector2 Location
        {
            get { return mLocation; }
            set { mLocation = value; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Represents a particles emitter
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphics"></param>
        /// <param name="location"></param>
        public ParticlesEmitter(Game game, GraphicsDeviceManager graphics, Vector2 location)
            : base(game)
        {
            this.mGraphics = graphics;
            this.mContent = new ContentManager(game.Services);
            this.mLocation = location;
            
            this.mParticles = new Particle[25];

            base.DrawOrder = 1;

            mRandom = new Random();

            for (int i = 0; i < mParticles.Length; i++)
            {
                mParticles[i] = new Particle();
                InitializeParticle(ref mParticles[i]);
            }
        }

        /// <summary>
        /// initialize a particle
        /// </summary>
        /// <param name="particle"></param>
        private void InitializeParticle(ref Particle particle)
        {
            particle.Direction = new Vector2((float)mRandom.NextDouble() - 0.5f, (float)mRandom.NextDouble());
            particle.Location = new Vector2(mLocation.X + ((float)mRandom.NextDouble() - 0.5f) * 10,
                                            mLocation.Y + ((float)mRandom.NextDouble() - 0.5f) * 10);

            particle.LifeTime = (float)mRandom.NextDouble() * 900;
        }

        /// <summary>
        /// Load the graphics content
        /// </summary>
        /// <param name="loadAllContent"></param>
        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                this.mSpriteBatch = new SpriteBatch(mGraphics.GraphicsDevice);
                mTexture = mContent.Load<Texture2D>("Content/fireball");
            }
        }

        #endregion

        #region Update / Draw

        /// <summary>
        /// Update the emitter
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < mParticles.Length; i++)
            {
                mParticles[i].Location += mParticles[i].Direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 10;
                mParticles[i].LifeTime -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (mParticles[i].LifeTime <= 0)
                {
                    InitializeParticle(ref mParticles[i]);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            mSpriteBatch.Begin(SpriteBlendMode.Additive);

            foreach (Particle particle in mParticles)
            {
                mSpriteBatch.Draw(mTexture, new Rectangle((int)particle.Location.X,
                                                          (int)particle.Location.Y,
                                                         16, 16), Color.White);
            }

            mSpriteBatch.End();
        }

        #endregion

    }
}
