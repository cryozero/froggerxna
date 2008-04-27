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
using System.Xml;

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
        GameOver,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        GameWin
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

        SpriteFont ScoreFont; //Score font


        SpriteFont sSpriteFont;
        Cue music = null;
        SoundBank soundBank;
        

        public int ScoreValue = 500; //Score value
        public int Level = 0; //Level
        public int lives = 3; //Number of lives
        public int Revision = 38; //Subversion revision

        //Backgrounds

        Background bg_intro;
        Background bg_level_1;
        Background bg_level_2;
        Background bg_level_3;
        Background bg_level_4;
        Background bg_level_5;

        //int level;
        
        Frog frog;
        Frog redfrog;
        Frog bluefrog;

       // Score score;

        //Objects

        List<Gateway> listGateway = new List<Gateway>();

        List<Car> listCar=new List<Car>();
        List<Bus> listBus = new List<Bus>();
        List<Wood> listWood = new List<Wood>();
        List<Alligator> listAlligator = new List<Alligator>();
        List<Turtle> listTurtle = new List<Turtle>();
        List<Taxi> listTaxi = new List<Taxi>();  


         SpriteBatch mBatch;
        Texture2D mHealthBar;
        int mCurrentHealth = 100;

                /// <summary>
        /// Constructor.
        /// </summary>

        SpriteFont mSpriteFont;

        private SpriteBatch mSpriteBatch;


        private ContentManager mContent;


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
                
                //All the Backgrounds

                bg_intro = new Background(this, graphics, "Content/bg_intro");
                bg_level_1 = new Background(this, graphics,"Content/bg_level_1");
                bg_level_2 = new Background(this, graphics, "Content/bg_level_2");
                bg_level_3 = new Background(this, graphics, "Content/bg_level_3");
                bg_level_4 = new Background(this, graphics, "Content/bg_level_4");
                bg_level_5 = new Background(this, graphics, "Content/bg_level_5");

                //The unique green Frog !

                frog = new Frog(this, graphics,"Content/frog","C");
                this.Components.Add(frog);

                //The red Frog (Mutliplayer)

                redfrog = new Frog(this, graphics, "Content/redfrog","L");
                this.Components.Add(redfrog);

                //The blue Frog (Mutliplayer)

                bluefrog = new Frog(this, graphics, "Content/bluefrog", "R");
                this.Components.Add(bluefrog);

                //Gateways
                listGateway.Add(new Gateway(this, graphics, new Vector2(200, 20)));
                listGateway.Add(new Gateway(this, graphics, new Vector2(400, 20)));
                listGateway.Add(new Gateway(this, graphics, new Vector2(600, 20)));
                listGateway.Add(new Gateway(this, graphics, new Vector2(800, 20)));
                listGateway.Add(new Gateway(this, graphics, new Vector2(1000, 20)));

                listGateway.ForEach(delegate(Gateway g) { this.Components.Add(g); });

                //Cars
                listCar.Add(new Car(this, graphics, new Vector2(120, 410)));
                listCar.Add(new Car(this, graphics, new Vector2(1000, 420)));
                listCar.Add(new Car(this, graphics, new Vector2(700, 410)));
                listCar.Add(new Car(this, graphics, new Vector2(800, 440)));
                listCar.Add(new Car(this, graphics, new Vector2(610, 420)));

                listCar.ForEach(delegate(Car c) { this.Components.Add(c); });

                //Buses
                listBus.Add(new Bus(this, graphics, new Vector2(900, 480)));
                listBus.Add(new Bus(this, graphics, new Vector2(300, 410)));
                listBus.Add(new Bus(this, graphics, new Vector2(100, 470)));

                listBus.ForEach(delegate(Bus b) { this.Components.Add(b); });

                //Woods
                listWood.Add(new Wood(this, graphics, new Vector2(110, 220)));
                listWood.Add(new Wood(this, graphics, new Vector2(650, 280)));
                listWood.Add(new Wood(this, graphics, new Vector2(310, 320)));
                listWood.Add(new Wood(this, graphics, new Vector2(510, 300)));
                listWood.Add(new Wood(this, graphics, new Vector2(910, 270)));

                listWood.ForEach(delegate(Wood w) { this.Components.Add(w); });

                //Alligators
                listAlligator.Add(new Alligator(this, graphics, new Vector2(790, 290)));
                listAlligator.Add(new Alligator(this, graphics, new Vector2(990, 200)));

                listAlligator.ForEach(delegate(Alligator a) { this.Components.Add(a); });


                //Turtles
                listTurtle.Add(new Turtle(this, graphics, new Vector2(780, 220)));

                listTurtle.ForEach(delegate(Turtle a) { this.Components.Add(a); });

                //Taxis
                listTaxi.Add(new Taxi(this, graphics, new Vector2(700, 220)));

                listTaxi.ForEach(delegate(Taxi t) { this.Components.Add(t); });

               // Info info = new Info(this, graphics.GraphicsDevice, ScoreValue.ToString());
                //this.Components.Add(info);


                this.mContent = new ContentManager(this.Services);
                this.mSpriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            


                //this.max = max;

               FPSManager fps = new FPSManager(this, graphics.GraphicsDevice);



            //    Score score = new Score(this, graphics.GraphicsDevice, ScoreValue);
              //this.Components.Add(score);

               this.Components.Add(fps);


                //Backgrounds

                this.Components.Add(bg_intro);
                this.Components.Add(bg_level_1);
                this.Components.Add(bg_level_2);
                this.Components.Add(bg_level_3);
                this.Components.Add(bg_level_4);
                this.Components.Add(bg_level_5);

                //Initialize

                base.Initialize();
        }

        //
        // Create a NewGame (Level 1)
        //

        void NewGame()
        {
            gameState = GameState.Level1; //Game state is level1
            Level = 1; //Level 1

            frog.Enabled = true;
            frog.Visible = true;
        }

        //
        // Level 2
        //


        void Level_2()
        {
            gameState = GameState.Level2;
            Level = 2;

            frog.mLocation.X=620;
            frog.mLocation.Y = 670;

        }

        //
        // Level 3
        //


        void Level_3()
        {
            gameState = GameState.Level3;
            Level = 3;
            
            frog.mLocation.X = 620;
            frog.mLocation.Y = 670;
        }

        //
        // Level 4
        //


        void Level_4()
        {
            gameState = GameState.Level4;
            frog.mLocation.X = 620;
            frog.mLocation.Y = 670;
            Level = 4;

            bg_level_1.Visible = false;
            bg_level_2.Visible = false;
            bg_level_3.Visible = false;
            bg_level_4.Visible = true;


        }


        //
        // Level 5
        //


        void Level_5()
        {
            gameState = GameState.Level5;
            Level = 5;

            frog.mLocation.X = 620;
            frog.mLocation.Y = 670;
        }

        //
        // GameOver
        //

        void GameOver()
        {
            gameState = GameState.GameOver;
        }

        //
        // You Win
        //

        void YouWin()
        {
            gameState = GameState.GameWin;
        }

        //
        //This function diplayed Entity
        //

        void Diplayed()
        {

            if (gameState == GameState.Level1)
            {
                bg_intro.Visible = false;
                bg_level_1.Visible = true;

                listGateway.ForEach(delegate(Gateway g) { g.Visible = true; });
                listCar.ForEach(delegate(Car c) { c.Visible=true; });
                listBus.ForEach(delegate(Bus b) { b.Visible = true; });
                listWood.ForEach(delegate(Wood w) { w.Visible = true; });
            }

            if (gameState == GameState.Level2)
            {
                bg_level_1.Visible = false;
                bg_level_2.Visible = true;

                listAlligator.ForEach(delegate(Alligator a) { a.Visible = true; });
            }

            if (gameState == GameState.Level3)
            {
                bg_level_1.Visible = false;
                bg_level_2.Visible = false;
                bg_level_3.Visible = true;

                listTurtle.ForEach(delegate(Turtle a) { a.Visible = true; });

            }

            if (gameState == GameState.Level4)
            {
                bg_level_1.Visible = false;
                bg_level_2.Visible = false;
                bg_level_3.Visible = false;
                bg_level_4.Visible = true;
                bg_level_5.Visible = false;

                listTaxi.ForEach(delegate(Taxi t) { t.Visible = true; });
            }


            if (gameState == GameState.Level5)
            {
                bg_level_1.Visible = false;
                bg_level_2.Visible = false;
                bg_level_3.Visible = false;
                bg_level_4.Visible = false;

                bg_level_5.Visible = true;

            }


           
        }

        void Hidden()
        {

            bg_level_1.Visible = false;
            bg_level_2.Visible = false;
            bg_level_3.Visible = false;
            bg_level_4.Visible = false;
            bg_level_5.Visible = false;

            listGateway.ForEach(delegate(Gateway g) { g.Visible = false; });
            listCar.ForEach(delegate(Car c) { c.Visible = false; });
            listBus.ForEach(delegate(Bus b) { b.Visible = false; });
            listWood.ForEach(delegate(Wood w) { w.Visible = false; });
            listAlligator.ForEach(delegate(Alligator a) { a.Visible = false; });
            listTurtle.ForEach(delegate(Turtle a) { a.Visible = false; });
            listTaxi.ForEach(delegate(Taxi t) { t.Visible = false; });


            frog.Enabled= false;
            frog.Visible = false;

            redfrog.Enabled = false;
            redfrog.Visible = false;

            bluefrog.Enabled = false;
            bluefrog.Visible = false;

        }

        void Multiplayer()
    {
        frog.Enabled = false;
        frog.Visible = false;

        redfrog.Enabled = true;
        redfrog.Visible = true;

        bluefrog.Enabled = true;
        bluefrog.Visible = true;

        gameState = GameState.Level1;

    }

        void Cheat()
        {
            if (Level == 1) { Level_2(); } //Next level is 2
            if (Level == 2) { Level_3(); } //Next level is 3
            if (Level == 3) { Level_4(); } //Next level is 4
            if (Level == 4) { Level_5(); } //Next level is 5

            ScoreValue = ScoreValue + 5000; //Bonus during the cheat (most than 5000)
        }


        void Collision(WorldEntity enemy)
        {
            if(frog.mLocation.X>=enemy.Location.X-50 && frog.mLocation.X <= enemy.Location.X+50
         && frog.mLocation.Y >= enemy.Location.Y-50 && frog.mLocation.Y <= enemy.Location.Y + 50
               )
           {
               if (lives == 0)
               {
                   GameOver();
               }
               else
               {
                   Sound.Play(Sounds.Collision);
                   lives = lives - 1;
                   frog.mLocation.X = 620;
                   frog.mLocation.Y = 670;
               }
                }
            
            }



        void SaveGame()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            XmlElement xmlelem;
            XmlText xmltext;
            xmldoc.AppendChild(xmlnode);

            xmlelem = xmldoc.CreateElement("", "GameStatus", "");
            xmltext = xmldoc.CreateTextNode("");
            xmlelem.AppendChild(xmltext);
            xmldoc.AppendChild(xmlelem);


            //Level

            xmlelem = xmldoc.CreateElement("", "Level", "");
            xmltext = xmldoc.CreateTextNode(gameState.ToString());
            xmlelem.AppendChild(xmltext);
            xmldoc.ChildNodes.Item(1).AppendChild(xmlelem);

            //Score

            xmlelem = xmldoc.CreateElement("", "Score", "");
            xmltext = xmldoc.CreateTextNode(ScoreValue.ToString());
            xmlelem.AppendChild(xmltext);
            xmldoc.ChildNodes.Item(1).AppendChild(xmlelem);


            xmldoc.Save("save.xml");

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

                this.mSpriteFont = mContent.Load<SpriteFont>("Content/SpriteFont");

                this.ScoreFont = mContent.Load<SpriteFont>("Fonts/ScoreFont");

                Sound.Play(Sounds.Music);

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


 

/*
 * 
 * 
 * 
            if (music == null)
            {
                //music = soundBank.GetCue("Audio/Music");
                music.Play();
            }
            else if (music.IsPaused)
            {
                music.Resume();
            }*/

            //Keyboard state

            KeyboardState keyboardState = Keyboard.GetState();

            //If escape is pressed => Exit 

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }



                        TimeSpan  mElapsedTime = gameTime.ElapsedGameTime;

                        if (mElapsedTime > TimeSpan.FromSeconds(5))
                        {

                            FPSManager fps = new FPSManager(this, graphics.GraphicsDevice);
                            this.Components.Add(fps);
                        }





            //If space is pressed => NewGame  

            if (gameState == GameState.TitleScreen &&  keyboardState.IsKeyDown(Keys.S)) //When you press on start, the game run
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

            //Save the game

            if (keyboardState.IsKeyDown(Keys.P))
            {
                SaveGame();
            }

            //Mutliplayer

            if (gameState == GameState.TitleScreen && keyboardState.IsKeyDown(Keys.M))
            {
                Multiplayer();
            }


            //Cheat

            if (keyboardState.IsKeyDown(Keys.C) && keyboardState.IsKeyDown(Keys.H))
            {
                Cheat();
              
            }



            //Force the health to remain between 0 and 100
            mCurrentHealth = (int)MathHelper.Clamp(mCurrentHealth, 0, 100);
           

            //
            // All the collisions
            //
            listCar.ForEach(delegate(Car c) { Collision(c); }); //Cars
            listBus.ForEach(delegate(Bus b) { Collision(b); }); //Buses
            listWood.ForEach(delegate(Wood w) { Collision(w); }); //Woods
 

            //After 20 seconds, the game is over

            if (gameState == GameState.Level1 && gameTime.TotalGameTime.TotalSeconds>=20)
            {
                GameOver();
            }


            listGateway.ForEach(delegate(Gateway g) {


                if (frog.mLocation.X >= g.mLocation.X - 50 && frog.mLocation.X <= g.mLocation.X + 50
       && frog.mLocation.Y >= g.mLocation.Y - 50 && frog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level1
              )
                {
                    ScoreValue = ScoreValue + 750;
                    Level_2();

                }
            
            });


            listGateway.ForEach(delegate(Gateway g)
            {


                if (frog.mLocation.X >= g.mLocation.X - 50 && frog.mLocation.X <= g.mLocation.X + 50
       && frog.mLocation.Y >= g.mLocation.Y - 50 && frog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level2
              )
                {
                    Level_3();

                }

            });




            listGateway.ForEach(delegate(Gateway g)
            {


                if (frog.mLocation.X >= g.mLocation.X - 50 && frog.mLocation.X <= g.mLocation.X + 50
       && frog.mLocation.Y >= g.mLocation.Y - 50 && frog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level3
              )
                {
                    Level_4();

                }

            });



            listGateway.ForEach(delegate(Gateway g)
            {


                if (frog.mLocation.X >= g.mLocation.X - 50 && frog.mLocation.X <= g.mLocation.X + 50
       && frog.mLocation.Y >= g.mLocation.Y - 50 && frog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level4
              )
                {
                    Level_5();

                }

            });




            listGateway.ForEach(delegate(Gateway g)
{

                            if (frog.mLocation.X >= g.mLocation.X - 50 && frog.mLocation.X <= g.mLocation.X + 50
       && frog.mLocation.Y >= g.mLocation.Y - 50 && frog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level5
              )
                {
                    YouWin();

                }

            });

            if (gameState == GameState.Level1 || gameState == GameState.Level2 || gameState == GameState.Level3 || gameState == GameState.Level4 || gameState == GameState.Level5) //When you press on start, the game run
           {

               float SPEED = 0;

               if (gameState == GameState.Level1)
               {
                   SPEED = 0.3f;
               }

               if (gameState == GameState.Level2)
               {
                   SPEED = 0.4f;
               }

               if (gameState == GameState.Level3)
               {
                   SPEED = 0.5f;
               }

               if (gameState == GameState.Level4)
               {
                   SPEED = 0.6f;
               }

               if (gameState == GameState.Level5)
               {
                   SPEED = 0.7f;
               }
               
               //
               // Movement and Speed
               //

               listCar.ForEach(delegate(Car c) { c.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
               listBus.ForEach(delegate(Bus b) { b.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
               listWood.ForEach(delegate(Wood w) { w.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
               listAlligator.ForEach(delegate(Alligator a) { a.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
               listTurtle.ForEach(delegate(Turtle a) { a.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
               listTaxi.ForEach(delegate(Taxi t) { t.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
  
 
               //alligator.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED;
              

               this.spriteBatch.Begin();

               //this.spriteBatch.DrawString(sSpriteFont,
                 //           String.Format("Level : {0}", level),
                   //         new Vector2(1150, 750), Color.White);

               this.spriteBatch.End();

           }






            base.Update(gameTime);
        }

        #endregion

        #region Draw


        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);





            //Displayed before the game start

            if (gameState == GameState.TitleScreen)
            {
                Hidden();
            }

            if (gameState == GameState.Level1)
            {
                Diplayed();             
            }

            if (gameState == GameState.Level2)
            {              
                Diplayed();
            }


            if (gameState == GameState.Level3 || gameState == GameState.Level4 || gameState == GameState.Level5)
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
                spriteBatch.DrawString(mBitmapFont, "Your score is "+ScoreValue.ToString(), new Vector2(420.0f, 400.0f), Color.White);
                spriteBatch.End();
            }


            //
            // GameWin
            //

            if (gameState == GameState.GameWin)
            {
                graphics.GraphicsDevice.Clear(Color.Black);

                Hidden();
                frog.Enabled = false;


                spriteBatch.Begin();
                spriteBatch.DrawString(mBitmapFont, "You win !!", new Vector2(490.0f, 200.0f), Color.White);
                spriteBatch.DrawString(mBitmapFont, "Your score is " + ScoreValue.ToString(), new Vector2(420.0f, 400.0f), Color.White);
                spriteBatch.End();
            }


            //Display Score (ScoreValue) and Level (Level)

            double TimeLeft=20.0-gameTime.TotalGameTime.TotalSeconds;

            spriteBatch.Begin();
            spriteBatch.DrawString(ScoreFont, "Score: " + ScoreValue.ToString(), new Vector2(20, 760), Color.White);
            spriteBatch.DrawString(ScoreFont, "Level: " + Level.ToString(), new Vector2(170, 760), Color.White);
            spriteBatch.DrawString(ScoreFont, "Live(s): " + lives.ToString(), new Vector2(300, 760), Color.White);
            spriteBatch.DrawString(ScoreFont, "Time left: " + TimeLeft.ToString()+" s", new Vector2(450, 760), Color.White);
            spriteBatch.DrawString(ScoreFont, "Konami@1981", new Vector2(850, 760), Color.White);
            spriteBatch.DrawString(ScoreFont, "Revision: " + Revision.ToString(), new Vector2(1050, 760), Color.White);
            spriteBatch.End();
            
            //Draw
            
            base.Draw(gameTime);
        }

        #endregion

    }
}
