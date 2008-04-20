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



        SpriteBatch spriteBatch;
        SpriteFont mBitmapFont;

        GraphicsDeviceManager graphics;
        ContentManager content;


        Background bg_level_1;
        Background bg_level_2;

        Background intro;
        
        
        Frog frog;

        Score score;

        public const float SPEED = 0.05f;

        public int level = 1;


        //EnnemyShip ship;

        Car car;
        Car car_2;
        Car car_3;
        Car car_4;
        Car car_5;

        Bus bus;
        Bus bus_2;
        Bus bus_3;

        Wood wood;

  


        Gateway gateway;
        Gateway gateway_2;
        Gateway gateway_3;
        Gateway gateway_4;
        Gateway gateway_5;

        LineManager lineManager = new LineManager();




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


            

                bg_level_1 = new Background(this, graphics,"Content/fond");
                bg_level_2 = new Background(this, graphics, "Content/bg_level_2");

                intro = new Background(this, graphics, "Content/intro");


                frog = new Frog(this, graphics);
                //ship = new EnnemyShip(this, graphics, new Vector2(100, 20));
               
            
                car = new Car(this, graphics, new Vector2(1200, 410));
                car_2 = new Car(this, graphics, new Vector2(1000, 420));
                car_3 = new Car(this, graphics, new Vector2(700, 410));
                car_4 = new Car(this, graphics, new Vector2(800, 530));
                car_5 = new Car(this, graphics, new Vector2(610, 420));

                wood = new Wood(this, graphics, new Vector2(110, 220));

         
  

               

 
                 this.Components.Add(car);
                 this.Components.Add(car_2);
                 this.Components.Add(car_3);
                 this.Components.Add(car_4);
                 this.Components.Add(car_5);

                 this.Components.Add(wood);



                gateway = new Gateway(this, graphics, new Vector2(200, 20));
                gateway_2 = new Gateway(this, graphics, new Vector2(400, 20));
                gateway_3 = new Gateway(this, graphics, new Vector2(600, 20));
                gateway_4 = new Gateway(this, graphics, new Vector2(800, 20));
                gateway_5 = new Gateway(this, graphics, new Vector2(1000, 20));

                //this.Components.Add(ship);


      
                   

//                     this.Components.Add(car);
                    

  //                  this.Components.Add(bus);








                    
                    this.Components.Add(frog);
            




   
                bus = new Bus(this, graphics, new Vector2(900, 530));
                this.Components.Add(bus);

                FPSManager fps = new FPSManager(this, graphics.GraphicsDevice);

                Score score = new Score(this, graphics.GraphicsDevice);
                this.Components.Add(score);

            this.Components.Add(fps);


                this.Components.Add(gateway);
                this.Components.Add(gateway_2);
                this.Components.Add(gateway_3);
                this.Components.Add(gateway_4);
                this.Components.Add(gateway_5);

                this.Components.Add(bg_level_1);
                this.Components.Add(bg_level_2);
                this.Components.Add(intro);
           



            base.Initialize();

            Sound.Play(Sounds.Music);
        }



        void NewGame()
        {


            
            gameState = GameState.Level1;
           
            

          //  player = new Player();
        }

        void Level_2()
        {
            gameState = GameState.Level2;
        }

        void GameOver()
        {
            gameState = GameState.GameOver;
        }


        void Diplayed()
        {
           

            if (gameState == GameState.Level1)
            {
                intro.Visible = false;
                bg_level_1.Visible = true;

                gateway.Visible = true;
                gateway_2.Visible = true;
                gateway_3.Visible = true;
                gateway_4.Visible = true;
                gateway_5.Visible = true;

                car.Visible = true;
                car_2.Visible = true;
                car_3.Visible = true;
                car_4.Visible = true;
                car_5.Visible = true;

                bus.Visible = true;
            }

            if (gameState == GameState.Level2)
            {
                bg_level_1.Visible = false;
                bg_level_2.Visible = true;

                wood.Visible = true;
            }
           

            frog.Visible = true;
        }

        void Hidden()
        {

            bg_level_1.Visible = false;
            bg_level_2.Visible = false;

            gateway.Visible = false;
            gateway_2.Visible = false;
            gateway_3.Visible = false;
            gateway_4.Visible = false;
            gateway_5.Visible = false;

            
            bus.Visible = false;

            car.Visible = false;
            car_2.Visible = false;
            car_3.Visible = false;
            car_4.Visible = false;
            car_5.Visible = false;

            wood.Visible = false;

          

            frog.Visible = false;
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

            if (keyboardState.IsKeyDown(Keys.Space) || gameState != GameState.TitleScreen) //When you press on start, the game run
            {
        
                    NewGame(); //New game
                
            }


            if (keyboardState.IsKeyDown(Keys.A)) //When you press on start, the game run
            {
                score.ScoreValue = score.ScoreValue + 500;
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


           if (frog.mLocation.X >= gateway.mLocation.X - 50 && frog.mLocation.X <= gateway.mLocation.X + 50
      && frog.mLocation.Y >= gateway.mLocation.Y - 50 && frog.mLocation.Y <= gateway.mLocation.Y + 50

             )
           {
               Level_2();
              
           }





           if (gameState == GameState.Level1) //When you press on start, the game run
           {
               bus.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
               
               car.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
               car_2.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
               car_3.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
               car_4.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
               car_5.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

               wood.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

               if (bus.mLocation.X == frog.mLocation.X)
               {
                   Exit();
               }
           
           }

      

            base.Update(gameTime);
        }

      //  
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


                Hidden();

                spriteBatch.Begin();
                spriteBatch.DrawString(mBitmapFont, "Press on space to start the game", new Vector2(250.0f, 700.0f), Color.BurlyWood);
                spriteBatch.End();

            }

            if (gameState == GameState.Level1)
            {

                Diplayed();

                spriteBatch.Begin();
                spriteBatch.DrawString(mBitmapFont, "Level " + level.ToString(), new Vector2(150.0f, 700.0f), Color.BurlyWood);
                spriteBatch.End();

            }

            if (gameState == GameState.Level2)
            {
                Diplayed();
            }



            if (gameState == GameState.GameOver)
            {
                graphics.GraphicsDevice.Clear(Color.Black);

                Hidden();

                spriteBatch.Begin();
                spriteBatch.DrawString(mBitmapFont, "Game Over", new Vector2(480.0f, 300.0f), Color.MediumSlateBlue);
                spriteBatch.End();
            }


            


            base.Draw(gameTime);
        }
    }
}
