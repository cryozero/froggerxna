#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Frogger.World;
using Frogger.Utils;
#endregion

namespace FroggerXNA
{

    static class Program
    {

        /// The main entry point for the application.

        static void Main(string[] args)
        {
            using (FroggerXNA game = new FroggerXNA())
            {
                game.Run();
            }
        }
    }


    /// Different states for the game (include levels)

    enum GameState
    {
        TitleScreen,
        GameActive,
        GameOver,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5
    };



    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FroggerXNA : Microsoft.Xna.Framework.Game
    {
        #region variables

        GraphicsDeviceManager graphics;
        ContentManager content;
        Background background;
        PlayerShip player;




        List<Line> titleLineList = new List<Line>();
        Matrix titleMatrix;
        
        List<Line> authorLineList = new List<Line>();
        Matrix authorMatrix;
        
        List<Line> versionLineList = new List<Line>();
        Matrix versionMatrix;

        List<Line> konamiLineList = new List<Line>();
        Matrix konamiMatrix;

                /// <summary>
        /// Constructor.
        /// </summary>


        #endregion

        #region Initialization

        public FroggerXNA()
        {

            //Sound.Initialize();

            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);

           // graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(OnPreparingDeviceSettings);

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
        
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            background = new Background(this, graphics);
            player = new PlayerShip(this, graphics);
            FPSManager fps = new FPSManager(this, graphics.GraphicsDevice);

            this.Components.Add(fps);


            this.Components.Add(background);
            this.Components.Add(player);


            base.Initialize();
        }

        #endregion

        /// <summary>
        /// Load your graphics content.  If loadAllContent is true, you should
        /// load content from both ResourceManagementMode pools.  Otherwise, just
        /// load ResourceManagementMode.Manual content.
        /// </summary>
        /// <param name="loadAllContent">Which type of content to load.</param>
        protected override void LoadGraphicsContent(bool loadAllContent)
        {

            graphics.GraphicsDevice.RenderState.CullMode = CullMode.None;

            if (loadAllContent)
            {
                // TODO: Load any ResourceManagementMode.Automatic content
            }

            // TODO: Load any ResourceManagementMode.Manual content
        }


        /// <summary>
        /// Unload your graphics content.  If unloadAllContent is true, you should
        /// unload content from both ResourceManagementMode pools.  Otherwise, just
        /// unload ResourceManagementMode.Manual content.  Manual content will get
        /// Disposed by the GraphicsDevice during a Reset.
        /// </summary>
        /// <param name="unloadAllContent">Which type of content to unload.</param>
        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
            {
                // TODO: Unload any ResourceManagementMode.Automatic content
                content.Unload();
            }

            // TODO: Unload any ResourceManagementMode.Manual content
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
