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


    class Player
    {
        public int score = 0;
        public int lives = 3;

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

        GameState gameState = GameState.GameActive;

        Matrix viewMatrix = Matrix.Identity;
        Matrix projMatrix;

  

        GraphicsDeviceManager graphics;
        ContentManager content;
        Background background;
        PlayerShip player;


        //EnnemyShip ship;

        Car car;

        LineManager lineManager = new LineManager();


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



        #region Initialization

        public FroggerXNA()
        {

            Sound.Initialize();

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

            if (gameState == GameState.GameActive)
            {

                background = new Background(this, graphics);
                player = new PlayerShip(this, graphics);
                //ship = new EnnemyShip(this, graphics, new Vector2(100, 20));
                car = new Car(this, graphics, new Vector2(1200, 400));


                FPSManager fps = new FPSManager(this, graphics.GraphicsDevice);

                //this.Components.Add(ship);
                this.Components.Add(car);
                this.Components.Add(fps);
                this.Components.Add(background);
                this.Components.Add(player);

            }

            Sound.Play(Sounds.Damage);

          

            base.Initialize();

            // Some static text
            StrokeFont.AddStringCentered("The Fr gger", titleLineList);
            titleMatrix = Matrix.CreateScale(0.01f) * Matrix.CreateTranslation(0, 0.2f, 0);

            StrokeFont.AddStringCentered("By Maxime Rauer", authorLineList);
            authorMatrix = Matrix.CreateScale(0.004f) * Matrix.CreateTranslation(0, -0.3f, 0);

            StrokeFont.AddStringCentered("Version 1.0\nApril 15, 2008", versionLineList);
            versionMatrix = Matrix.CreateScale(0.002f) * Matrix.CreateTranslation(0, -0.6f, 0);

            StrokeFont.AddStringCentered("Based on Konami'game@1984", konamiLineList);
            konamiMatrix = Matrix.CreateScale(0.003f) * Matrix.CreateTranslation(0, 0.7f, 0);
 
        }



        void NewGame()
        {
            gameState = GameState.GameActive;

          //  player = new Player();
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

            Create2DProjectionMatrix();

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

        public void Create2DProjectionMatrix()
        {
            // Projection matrix ignores Z and just squishes X or Y to balance the upcoming viewport stretch
            float projScaleX;
            float projScaleY;
            float width = graphics.GraphicsDevice.Viewport.Width;
            float height = graphics.GraphicsDevice.Viewport.Height;
            if (width > height)
            {
                // Wide window
                projScaleX = height / width;
                projScaleY = 1.0f;
            }
            else
            {
                // Tall window
                projScaleX = 1.0f;
                projScaleY = width / height;
            }
            projMatrix = Matrix.CreateScale(projScaleX, projScaleY, 0.0f);
            projMatrix.M43 = 0.5f;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Sound.Update();
            KeyboardState keyboardState = Keyboard.GetState(); //Keyboard state
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            //Exit 
            if (gamePadState.Buttons.Back == ButtonState.Pressed ||
                keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (keyboardState.IsKeyDown(Keys.Space)) //When you press on start, the game run
            {
                if (gameState != GameState.GameActive)
                    NewGame(); //New game
                
            }

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




            //Displayed before the game start

            if (gameState == GameState.TitleScreen)
            {

                lineManager.Draw(titleLineList, 1.5f, Color.BlanchedAlmond.ToVector4(), viewMatrix, projMatrix, 0, null, titleMatrix, 0.90f);
                lineManager.Draw(authorLineList, 1.5f, Color.CornflowerBlue.ToVector4(), viewMatrix, projMatrix, 0, null, authorMatrix, 0.80f);
                lineManager.Draw(versionLineList, 2.0f, new Vector4(0.4f, 0.4f, 0.4f, 1), viewMatrix, projMatrix, 0, null, versionMatrix, 0.60f);
                lineManager.Draw(konamiLineList, 1.0f, new Vector4(0.4f, 0.4f, 0.4f, 1), viewMatrix, projMatrix, 0, null, konamiMatrix, 0.30f);
                
            }

            base.Draw(gameTime);
        }
    }
}
