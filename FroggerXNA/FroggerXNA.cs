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

        GameState gameState = GameState.TitleScreen;

        Matrix viewMatrix = Matrix.Identity;
        Matrix projMatrix;

        SpriteBatch spriteBatch;
        SpriteFont mBitmapFont;

        GraphicsDeviceManager graphics;
        ContentManager content;
        Background background;
        Frog frog;

        public const float SPEED = 0.05f;

        public int level = 1;


        //EnnemyShip ship;

        Car car;
        Car car2;
        Bus bus;

        LineManager lineManager = new LineManager();


        List<Line> titleLineList = new List<Line>();
        Matrix titleMatrix;
        
        List<Line> authorLineList = new List<Line>();
        Matrix authorMatrix;
        
        List<Line> versionLineList = new List<Line>();
        Matrix versionMatrix;

        List<Line> konamiLineList = new List<Line>();
        Matrix konamiMatrix;


        SpriteBatch mBatch;
        Texture2D mHealthBar;
        int mCurrentHealth = 100;

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

           

                background = new Background(this, graphics);
                frog = new Frog(this, graphics);
                //ship = new EnnemyShip(this, graphics, new Vector2(100, 20));
                car = new Car(this, graphics, new Vector2(1200, 400));
                car2 = new Car(this, graphics, new Vector2(1000, 420));
               // bus = new Bus(this, graphics, new Vector2(900, 530));

                List<Car> carlist = new List<Car>();

                car = new Car(this, graphics, new Vector2(1200, 400));
                carlist.Add(car);

                foreach (Car card in carlist)
                {
                    this.Components.Add(card);
                }

                

                //this.Components.Add(ship);


      
                   

//                     this.Components.Add(car);
                    this.Components.Add(car2);

  //                  this.Components.Add(bus);









                    this.Components.Add(frog);
            Sound.Play(Sounds.Damage);




   
                bus = new Bus(this, graphics, new Vector2(900, 530));
                this.Components.Add(bus);

                FPSManager fps = new FPSManager(this, graphics.GraphicsDevice);
                this.Components.Add(fps);

                this.Components.Add(background);

           

            // Some static text
            StrokeFont.AddStringCentered("The Fr gger", titleLineList);
            titleMatrix = Matrix.CreateScale(0.01f) * Matrix.CreateTranslation(0, 0.2f, 0);
            
            StrokeFont.AddStringCentered("By Maxime Rauer", authorLineList);
            authorMatrix = Matrix.CreateScale(0.004f) * Matrix.CreateTranslation(0, -0.3f, 0);

            StrokeFont.AddStringCentered("Version 1.0\nApril 15, 2008", versionLineList);
            versionMatrix = Matrix.CreateScale(0.002f) * Matrix.CreateTranslation(0, -0.6f, 0);

            StrokeFont.AddStringCentered("Based on Konami'game@1984", konamiLineList);
            konamiMatrix = Matrix.CreateScale(0.003f) * Matrix.CreateTranslation(0, 0.7f, 0);


            base.Initialize();

        }



        void NewGame()
        {

            

            gameState = GameState.Level1;
           
            

          //  player = new Player();
        }


        void GameOver()
        {
            gameState = GameState.GameOver;
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

           

            if (loadAllContent)
            {
                graphics.GraphicsDevice.RenderState.CullMode = CullMode.None;
                //Initialize the Sprite batch
                mBatch = new SpriteBatch(this.graphics.GraphicsDevice);

                //Create the content manager to load the images
                ContentManager aLoader = new ContentManager(this.Services);

                //Load the HealthBar image from the disk into the Texture2D object
                mHealthBar = aLoader.Load<Texture2D>("content/HealthBar") as Texture2D;

                spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
                mBitmapFont = content.Load<SpriteFont>("fonts/space");
                
                // TODO: Load any ResourceManagementMode.Automatic content
            }

            //Create2DProjectionMatrix();

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


            if (keyboardState.IsKeyDown(Keys.A)) //When you press on start, the game run
            {
                gameState=GameState.Level2;

            }


            if (keyboardState.IsKeyDown(Keys.Up) == true)
            {
                mCurrentHealth += 1;
            }

            //If the Down Arrowis pressed, decrease the Health bar
            if (keyboardState.IsKeyDown(Keys.Down) == true)
            {
                mCurrentHealth -= 1;
            }

            //Force the health to remain between 0 and 100
            mCurrentHealth = (int)MathHelper.Clamp(mCurrentHealth, 0, 100);
           
            
           if(keyboardState.IsKeyDown(Keys.C)==true)
            {
                frog.mLocation.X = 100;
           }


           if (frog.mLocation.X >= bus.mLocation.X-50 && frog.mLocation.X <= bus.mLocation.X+50
               && frog.mLocation.Y >= bus.mLocation.Y-50 && frog.mLocation.Y <= bus.mLocation.Y + 50
               
               )
           {
               GameOver();
           }


           if (gameState == GameState.Level1) //When you press on start, the game run
           {
               bus.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
               car.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

               if (bus.mLocation.X == frog.mLocation.X)
               {
                   Exit();
               }
           
           }

      

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

                //  lineManager.Draw(titleLineList, 1.5f, Color.BlanchedAlmond.ToVector4(), viewMatrix, projMatrix, 0, null, titleMatrix, 0.90f);
                //lineManager.Draw(authorLineList, 1.5f, Color.CornflowerBlue.ToVector4(), viewMatrix, projMatrix, 0, null, authorMatrix, 0.80f);
                //lineManager.Draw(versionLineList, 2.0f, new Vector4(0.4f, 0.4f, 0.4f, 1), viewMatrix, projMatrix, 0, null, versionMatrix, 0.60f);
                //lineManager.Draw(konamiLineList, 1.0new Vector4(0.4f, 0.4f, 0.4f, 1), viewMatrix, projMatrix, 0, null, konamiMatrix, 0.30f);
                graphics.GraphicsDevice.Clear(Color.Black); 

                background.Visible = false;
                bus.Visible = false;
                car.Visible = false;
                car2.Visible = false;
                frog.Visible = false;

                spriteBatch.Begin();
                spriteBatch.DrawString(mBitmapFont, "Press on space to start the game", new Vector2(250.0f, 700.0f), Color.BurlyWood);
                spriteBatch.End();

            }

            if (gameState == GameState.Level1)
            {


                background.Visible = true;
                bus.Visible = true;
                
                car.Visible = true;
                car2.Visible = true;
                frog.Visible = true;

                spriteBatch.Begin();
                spriteBatch.DrawString(mBitmapFont, "Level " + level.ToString(), new Vector2(150.0f, 700.0f), Color.BurlyWood);
                spriteBatch.End();

            }

            if (gameState == GameState.GameOver)
            {
                graphics.GraphicsDevice.Clear(Color.Black); 

                background.Visible = false;
                spriteBatch.Begin();
                spriteBatch.DrawString(mBitmapFont, "Game Over", new Vector2(480.0f, 300.0f), Color.MediumSlateBlue);
                spriteBatch.End();
            }


            /*
            //TODO: Add your drawing code here
            mBatch.Begin();

            //Draw the negative space for the health bar
            mBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width / 2 - mHealthBar.Width / 2, 30, mHealthBar.Width, 44), new Rectangle(0, 45, mHealthBar.Width, 44), Color.Gray);

            //Draw the current health level based on the current Health
            mBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width / 2 - mHealthBar.Width / 2, 30, (int)(mHealthBar.Width * ((double)mCurrentHealth / 100)), 44), new Rectangle(0, 45, mHealthBar.Width, 44), Color.Red);

            //Draw the box around the health bar
            mBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width / 2 - mHealthBar.Width / 2, 30, mHealthBar.Width, 44), new Rectangle(0, 0, mHealthBar.Width, 44), Color.White);

            mBatch.End();
            */

            


            base.Draw(gameTime);
        }
    }
}
