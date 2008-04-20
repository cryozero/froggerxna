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
        GraphicsDeviceManager graphics;
        ContentManager content;
        SpriteBatch spriteBatch;
        SpriteFont mBitmapFont;
        SpriteFont sSpriteFont;

        
        

        public int ScoreValue = 0;
        


         ContentManager sContent;
        SpriteBatch sSpriteBatch;

        //Backgrounds

        Background intro;
        Background bg_level_1;

        int level;
        
        Frog frog;

        Score score;

        //Cars

        Car car;
        Car car_2;
        Car car_3;
        Car car_4;
        Car car_5;

        //Buses

        Bus bus;
        Bus bus_2;
        Bus bus_3;

        //Woods

        Wood wood;

        //Gateways

        Gateway gateway;
        Gateway gateway_2;
        Gateway gateway_3;
        Gateway gateway_4;
        Gateway gateway_5;

        //
        //Level 2 && > Object
        //

        Background bg_level_2;


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


            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
        
        }

        protected override void Initialize()
        {
                //Backgrounds

                intro = new Background(this, graphics, "Content/intro");
                bg_level_1 = new Background(this, graphics,"Content/bg_level_1");
                bg_level_2 = new Background(this, graphics, "Content/bg_level_2");

                //The unique Frog !

                frog = new Frog(this, graphics);
                this.Components.Add(frog);                 
            
                //Cars

                car = new Car(this, graphics, new Vector2(120, 410));
                this.Components.Add(car);

                car_2 = new Car(this, graphics, new Vector2(1000, 420));
                this.Components.Add(car_2);
    
                car_3 = new Car(this, graphics, new Vector2(700, 410));
                this.Components.Add(car_3);    

                car_4 = new Car(this, graphics, new Vector2(800, 530));
                this.Components.Add(car_4);   

                car_5 = new Car(this, graphics, new Vector2(610, 420));
                this.Components.Add(car_5);

                //Buses

                bus = new Bus(this, graphics, new Vector2(900, 530));
                this.Components.Add(bus);

                bus_2 = new Bus(this, graphics, new Vector2(300, 410));
                this.Components.Add(bus_2);

                bus_3 = new Bus(this, graphics, new Vector2(300, 410));
                this.Components.Add(bus_3);

                //Woods

                wood = new Wood(this, graphics, new Vector2(110, 220));
                this.Components.Add(wood);
         

                gateway = new Gateway(this, graphics, new Vector2(200, 20));
                gateway_2 = new Gateway(this, graphics, new Vector2(400, 20));
                gateway_3 = new Gateway(this, graphics, new Vector2(600, 20));
                gateway_4 = new Gateway(this, graphics, new Vector2(800, 20));
                gateway_5 = new Gateway(this, graphics, new Vector2(1000, 20));

                
            




   
              

                FPSManager fps = new FPSManager(this, graphics.GraphicsDevice);

               

              Score score = new Score(this, graphics.GraphicsDevice,  ScoreValue);
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

        //
        // Create a NewGame
        //

        void NewGame()
        {
            System.Diagnostics.Stopwatch.StartNew();
            gameState = GameState.Level1;
        }

        void Level_2()
        {
            gameState = GameState.Level2;
        }

        void GameOver()
        {
            gameState = GameState.GameOver;
        }

        //
        //This function diplayed Entity
        //

        void Diplayed()
        {

            if (gameState == GameState.Level1)
            {
                intro.Visible = false;
                bg_level_1.Visible = true;

                frog.Enabled = true;


                //Cars

                car.Visible = true;
                car_2.Visible = true;
                car_3.Visible = true;
                car_4.Visible = true;
                car_5.Visible = true;

                //Buses

                bus.Visible = true;
                bus_2.Visible = true;
                bus_3.Visible = true;

                //Woods

                wood.Visible = true;

                gateway.Visible = true;
                gateway_2.Visible = true;
                gateway_3.Visible = true;
                gateway_4.Visible = true;
                gateway_5.Visible = true;



            }

            if (gameState == GameState.Level2)
            {
                bg_level_1.Visible = false;
                bg_level_2.Visible = true;

                
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
            bus_2.Visible = false;
            bus_3.Visible = false;

            car.Visible = false;
            car_2.Visible = false;
            car_3.Visible = false;
            car_4.Visible = false;
            car_5.Visible = false;

            wood.Visible = false;


            frog.Visible = false;

        }


        void Collision(WorldEntity enemy)
        {
            if(frog.mLocation.X>=enemy.Location.X-50 && frog.mLocation.X <= enemy.Location.X+50
         && frog.mLocation.Y >= enemy.Location.Y-50 && frog.mLocation.Y <= enemy.Location.Y + 50
               )
           {
               GameOver();
            }
            
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

                sSpriteFont = content.Load<SpriteFont>("Content/SpriteFont");

               

            }
        }



        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
            {  
                content.Unload();
            }
        }


        #region Update


        protected override void Update(GameTime gameTime)
        {
            //Sound.Update();

            //Keyboard state

            KeyboardState keyboardState = Keyboard.GetState();

            //If escape is pressed => Exit 

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            //If space is pressed => NewGame  

            if (gameState == GameState.TitleScreen &&  keyboardState.IsKeyDown(Keys.Space)) //When you press on start, the game run
            {
                NewGame();
            }

            //If the game is over, press on N to play again and space to exit
            
            if (gameState == GameState.GameOver && keyboardState.IsKeyDown(Keys.N))
            {
                NewGame();
            }

            if (gameState == GameState.GameOver && keyboardState.IsKeyDown(Keys.Space))
            {
                Exit();
            }





            //Force the health to remain between 0 and 100
            mCurrentHealth = (int)MathHelper.Clamp(mCurrentHealth, 0, 100);
           

            //
            // All the collisions
            //

            //Cars
            
            Collision(car);
            Collision(car_2);
            Collision(car_3);
            Collision(car_4);

            //Buses

            Collision(bus);
            Collision(bus_2);
            Collision(bus_3);

            //After 20 seconds, the game is over

            if (gameState == GameState.Level1 && gameTime.TotalGameTime.TotalSeconds>=20)
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
               float SPEED = 0.5f;

               car.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
               car_2.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
               car_3.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
               car_4.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
               car_5.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

               bus.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
               bus_2.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
               bus_3.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;

               wood.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;


               this.spriteBatch.Begin();

               this.spriteBatch.DrawString(sSpriteFont,
                            String.Format("Level : {0}", level),
                            new Vector2(1150, 750), Color.White);

               this.spriteBatch.End();

           }






            base.Update(gameTime);
        }

        #endregion

        #region Draw


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




            //
            // GameOver Draw
            //

            if (gameState == GameState.GameOver)
            {
                graphics.GraphicsDevice.Clear(Color.Black);

                Hidden();
                frog.Enabled = false;
                

                spriteBatch.Begin();
                spriteBatch.DrawString(mBitmapFont, "Game Over", new Vector2(470.0f, 200.0f), Color.White);
                spriteBatch.DrawString(mBitmapFont, "Your score is 0", new Vector2(420.0f, 400.0f), Color.White);
                spriteBatch.End();
            }


            


            base.Draw(gameTime);
        }

        #endregion

    }
}
