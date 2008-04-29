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



    /// Differents states (include levels)

    #region enum

    enum GameState
    {
        TitleScreen,
        GameOver,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        GameWin,
        Bonus
    };

    #endregion


    //Class FroggerXNA

    public class FroggerXNA : Microsoft.Xna.Framework.Game
    {
        GameState gameState = GameState.TitleScreen;
        GraphicsDeviceManager graphics;
        ContentManager content;
        SpriteBatch spriteBatch;
        
        
        SpriteFont mBitmapFont;

        SpriteFont ScoreFont; //Score font


        SpriteFont sSpriteFont;


        double time = 0;

        int BestScore;


        public double ScoreValue = 500; //Score value
        public double Level = 0; //Level
        public double lives = 3; //Number of lives
        public int Revision = 38; //Subversion revision

        //Backgrounds

        Background bg_intro;
        Background bg_level_1;
        Background bg_level_2;
        Background bg_level_3;
        Background bg_level_4;
        Background bg_level_5;
        Background bg_bonus;

        //int level;
        
        Frog frog; //Green Frog
        Frog redfrog; //Red Frog
        Frog bluefrog; //Blue Frog

       // Score score;

        //Objects

        List<Gateway> listGateway = new List<Gateway>();
        List<Car> listCar=new List<Car>();
        List<Bus> listBus = new List<Bus>();
        List<Wood> listWood = new List<Wood>();
        List<Alligator> listAlligator = new List<Alligator>();
        List<Turtle> listTurtle = new List<Turtle>();
        List<Taxi> listTaxi = new List<Taxi>();
        List<Bicycle> listBicycle = new List<Bicycle>();
        List<Nenuphar> listNenuphar = new List<Nenuphar>();
        List<Cow> listCow = new List<Cow>();

         SpriteBatch mBatch;
      
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
                bg_bonus = new Background(this, graphics, "Content/bg_bonus");

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
                listTurtle.Add(new Turtle(this, graphics, new Vector2(780, 230)));
                listTurtle.Add(new Turtle(this, graphics, new Vector2(30, 210)));
                listTurtle.Add(new Turtle(this, graphics, new Vector2(710, 210)));

                listTurtle.ForEach(delegate(Turtle a) { this.Components.Add(a); });

                //Taxis
                listTaxi.Add(new Taxi(this, graphics, new Vector2(10, 420)));
                listTaxi.Add(new Taxi(this, graphics, new Vector2(900, 425)));
                listTaxi.Add(new Taxi(this, graphics, new Vector2(30, 450)));

                listTaxi.ForEach(delegate(Taxi t) { this.Components.Add(t); });


                //Bicycle
                listBicycle.Add(new Bicycle(this, graphics, new Vector2(110, 460)));

                listBicycle.ForEach(delegate(Bicycle b) { this.Components.Add(b); });

                //Nenuphar
                listNenuphar.Add(new Nenuphar(this, graphics, new Vector2(110, 460)));

                listNenuphar.ForEach(delegate(Nenuphar n) { this.Components.Add(n); });

                //Cow
                listCow.Add(new Cow(this, graphics, new Vector2(100, 460)));
                listCow.Add(new Cow(this, graphics, new Vector2(130, 360)));
                listCow.Add(new Cow(this, graphics, new Vector2(500, 440)));
                listCow.Add(new Cow(this, graphics, new Vector2(600, 420)));
                listCow.Add(new Cow(this, graphics, new Vector2(900, 960)));
                listCow.Add(new Cow(this, graphics, new Vector2(600, 650)));
                listCow.Add(new Cow(this, graphics, new Vector2(200, 490)));
                listCow.Add(new Cow(this, graphics, new Vector2(150, 120)));
                listCow.Add(new Cow(this, graphics, new Vector2(710, 460)));
                listCow.Add(new Cow(this, graphics, new Vector2(190, 960)));
                listCow.Add(new Cow(this, graphics, new Vector2(830, 260)));
                listCow.Add(new Cow(this, graphics, new Vector2(500, 450)));
                listCow.Add(new Cow(this, graphics, new Vector2(130, 360)));
                listCow.Add(new Cow(this, graphics, new Vector2(560, 470)));
                listCow.Add(new Cow(this, graphics, new Vector2(800, 480)));
                listCow.Add(new Cow(this, graphics, new Vector2(990, 990)));
                listCow.Add(new Cow(this, graphics, new Vector2(620, 620)));
                listCow.Add(new Cow(this, graphics, new Vector2(400, 440)));
                listCow.Add(new Cow(this, graphics, new Vector2(550, 520)));
                listCow.Add(new Cow(this, graphics, new Vector2(710, 760)));
                listCow.Add(new Cow(this, graphics, new Vector2(197, 970)));
                listCow.Add(new Cow(this, graphics, new Vector2(330, 240)));

                listCow.ForEach(delegate(Cow w) { this.Components.Add(w); });

                //Frames per second

                FPSManager fps = new FPSManager(this, graphics.GraphicsDevice);
                this.Components.Add(fps);


                this.mContent = new ContentManager(this.Services);
                this.mSpriteBatch = new SpriteBatch(graphics.GraphicsDevice);
                  
                //Backgrounds

                this.Components.Add(bg_intro);
                this.Components.Add(bg_level_1);
                this.Components.Add(bg_level_2);
                this.Components.Add(bg_level_3);
                this.Components.Add(bg_level_4);
                this.Components.Add(bg_level_5);
                this.Components.Add(bg_bonus);

                //Initialize

                base.Initialize();
            }

            #region functions

            //Initial location of the green frog

        void SingleFrogLocation()
        {

            frog.mLocation.X = 620; //Position X
            frog.mLocation.Y = 670; // Position Y 
        }

        //Initial location of the red frog

        void MultiplayerRedFrogLocation()
        {
            redfrog.mLocation.X = 520; //Position X
            redfrog.mLocation.Y = 670; // Position Y 
        }

        //Initial location of the blue frog

        void MultiplayerBlueFrogLocation()
        {
            bluefrog.mLocation.X = 720; //Position X
            bluefrog.mLocation.Y = 670; // Position Y 
        }

        // Create a NewGame (Level 1, "The Wood")

        void NewGame()
        {
            frog.Enabled = true; //Frog can move
            frog.Visible = true; //Frog is visible
            this.time = 0; //Time is 0
            gameState = GameState.Level1; //Game state is level1
            Level = 1; //Level 1
            SingleFrogLocation(); //Initial location of the green frog
            ScoreValue = 500; //Initial score is 500
            lives = 3; //You have 3 lives
        }

        // Level 2 ("Dali")

        void Level_2()
        {
            this.time = 0; //Time is 0
            gameState = GameState.Level2; //Game state is level2
            Level = 2; //Level 2
            SingleFrogLocation();
            MultiplayerBlueFrogLocation();
            MultiplayerRedFrogLocation();
        }

        // Level 3 ("London")

        void Level_3() 
        {
            BestScoreLoad();
            this.time = 0; //Time is 0
            gameState = GameState.Level3;
            Level = 3;
            SingleFrogLocation();
            MultiplayerBlueFrogLocation();
            MultiplayerRedFrogLocation();
        }

        // Level 4 ("Space")

        void Level_4()
        {
            this.time = 0; //Time is 0
            gameState = GameState.Level4;
            Level = 4;
            SingleFrogLocation();
            MultiplayerBlueFrogLocation();
            MultiplayerRedFrogLocation();
        }

        // Level 5 ("Mario")

        void Level_5()
        {
            this.time = 0; //Time is 0
            gameState = GameState.Level5;
            Level = 5;
            SingleFrogLocation();
            MultiplayerBlueFrogLocation();
            MultiplayerRedFrogLocation();
        }


        void bonus()
        {
            Hidden();
            
            this.time = 0; //Time is 0
            gameState = GameState.Bonus;
            bg_bonus.Enabled = true;
            SingleFrogLocation();
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


        void LoadGame()
        {
            //Level_5();
            bg_intro.Visible = false;
            NewGame();
            //Level_2();

            XmlDocument myXmlDocument = new XmlDocument();

            try
            {
                myXmlDocument.Load("save.xml");


                XmlNode node;
                node = myXmlDocument.DocumentElement;

               

                foreach (XmlNode node1 in node.ChildNodes)
                {
                    if (node1.Name == "Level")
                    {
                        Level = Int32.Parse(node1.InnerText.ToString());
                    }

                    if (node1.Name == "Score")
                    {
                        ScoreValue = Int32.Parse(node1.InnerText.ToString());
                    }

                    if (node1.Name == "Live")
                    {
                        lives = Int32.Parse(node1.InnerText.ToString());
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            

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
                bg_intro.Visible = false;
                bg_level_1.Visible = false;
                bg_level_2.Visible = true;

                listGateway.ForEach(delegate(Gateway g) { g.Visible = true; });
                listCar.ForEach(delegate(Car c) { c.Visible = true; });
                listBus.ForEach(delegate(Bus b) { b.Visible = true; });
                listWood.ForEach(delegate(Wood w) { w.Visible = true; });
                listAlligator.ForEach(delegate(Alligator a) { a.Visible = true; });
            }

            if (gameState == GameState.Level3)
            {
                bg_level_1.Visible = false;
                bg_level_2.Visible = false;
                bg_level_3.Visible = true;

                listGateway.ForEach(delegate(Gateway g) { g.Visible = true; });
                listCar.ForEach(delegate(Car c) { c.Visible = true; });
                listBus.ForEach(delegate(Bus b) { b.Visible = true; });
                listWood.ForEach(delegate(Wood w) { w.Visible = true; });
                listAlligator.ForEach(delegate(Alligator a) { a.Visible = true; });
                listTurtle.ForEach(delegate(Turtle a) { a.Visible = true; });

            }

            if (gameState == GameState.Level4)
            {
                bg_level_1.Visible = false;
                bg_level_2.Visible = false;
                bg_level_3.Visible = false;
                bg_level_4.Visible = true;
                bg_level_5.Visible = false;

                listGateway.ForEach(delegate(Gateway g) { g.Visible = true; });
                listCar.ForEach(delegate(Car c) { c.Visible = true; });
                listBus.ForEach(delegate(Bus b) { b.Visible = true; });
                listWood.ForEach(delegate(Wood w) { w.Visible = true; });
                listAlligator.ForEach(delegate(Alligator a) { a.Visible = true; });
                listTurtle.ForEach(delegate(Turtle a) { a.Visible = true; });
                listTaxi.ForEach(delegate(Taxi t) { t.Visible = true; });
                listBicycle.ForEach(delegate(Bicycle b) { b.Visible = true; });
            }


            if (gameState == GameState.Level5)
            {
                bg_level_1.Visible = false;
                bg_level_2.Visible = false;
                bg_level_3.Visible = false;
                bg_level_4.Visible = false;

                bg_level_5.Visible = true;

                listGateway.ForEach(delegate(Gateway g) { g.Visible = true; });
                listCar.ForEach(delegate(Car c) { c.Visible = true; });
                listBus.ForEach(delegate(Bus b) { b.Visible = true; });
                listWood.ForEach(delegate(Wood w) { w.Visible = true; });
                listAlligator.ForEach(delegate(Alligator a) { a.Visible = true; });
                listTurtle.ForEach(delegate(Turtle a) { a.Visible = true; });
                listTaxi.ForEach(delegate(Taxi t) { t.Visible = true; });
                listBicycle.ForEach(delegate(Bicycle b) { b.Visible = true; });
                listNenuphar.ForEach(delegate(Nenuphar n) { n.Visible = true; });


            }


           
        }

        void Hidden()
        {

            bg_level_1.Visible = false;
            bg_level_2.Visible = false;
            bg_level_3.Visible = false;
            bg_level_4.Visible = false;
            bg_level_5.Visible = false;
            bg_bonus.Visible = false;

            listGateway.ForEach(delegate(Gateway g) { g.Visible = false; });
            listCar.ForEach(delegate(Car c) { c.Visible = false; });
            listBus.ForEach(delegate(Bus b) { b.Visible = false; });
            listWood.ForEach(delegate(Wood w) { w.Visible = false; });
            listAlligator.ForEach(delegate(Alligator a) { a.Visible = false; });
            listTurtle.ForEach(delegate(Turtle a) { a.Visible = false; });
            listTaxi.ForEach(delegate(Taxi t) { t.Visible = false; });
            listBicycle.ForEach(delegate(Bicycle b) { b.Visible = false; });
            listNenuphar.ForEach(delegate(Nenuphar b) { b.Visible = false; });
            listCow.ForEach(delegate(Cow c) { c.Visible = false; });



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

        ScoreValue = 500;
        lives = 5;
        Level = 1;

    }




        void Collision(WorldEntity enemy)
        {
            if(
              
  
         (frog.mLocation.X>=enemy.Location.X-50 && frog.mLocation.X <= enemy.Location.X+50
         && frog.mLocation.Y >= enemy.Location.Y-50 && frog.mLocation.Y <= enemy.Location.Y + 50)     
          ||
          (redfrog.mLocation.X >= enemy.Location.X - 50 && redfrog.mLocation.X <= enemy.Location.X + 50
         && redfrog.mLocation.Y >= enemy.Location.Y - 50 && redfrog.mLocation.Y <= enemy.Location.Y + 50)
                          ||
          (bluefrog.mLocation.X >= enemy.Location.X - 50 && bluefrog.mLocation.X <= enemy.Location.X + 50
         && bluefrog.mLocation.Y >= enemy.Location.Y - 50 && bluefrog.mLocation.Y <= enemy.Location.Y + 50)   
              
                
                )
           {
               if (lives == 1)
               {
                   GameOver();
               }
               else
               {
                   Sound.Play(Sounds.Collision);
                   lives = lives - 1;
                   ScoreValue = ScoreValue - 100;

                   frog.mLocation.X = 620;
                   frog.mLocation.Y=570;

                   MultiplayerBlueFrogLocation();
                   MultiplayerRedFrogLocation();

               }
                }
            
            }


        void BestScoreLoad()
        {

            XmlDocument myXmlDocument = new XmlDocument();

            try
            {
                myXmlDocument.Load("bestscore.xml");


                XmlNode node;
                node = myXmlDocument.DocumentElement;



                foreach (XmlNode node1 in node.ChildNodes)
                {
                    if (node1.Name == "Score")
                    {
                        this.BestScore = Int32.Parse(node1.InnerText.ToString());
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }

        void BestScoreSave()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            XmlElement xmlelem;
            XmlText xmltext;
            xmldoc.AppendChild(xmlnode);

            xmlelem = xmldoc.CreateElement("", "BestScoreFile", "");
            xmltext = xmldoc.CreateTextNode("");
            xmlelem.AppendChild(xmltext);
            xmldoc.AppendChild(xmlelem);

            //Score

            xmlelem = xmldoc.CreateElement("", "Score", "");
            xmltext = xmldoc.CreateTextNode(ScoreValue.ToString());
            xmlelem.AppendChild(xmltext);
            xmldoc.ChildNodes.Item(1).AppendChild(xmlelem);

            xmldoc.Save("bestscore.xml");
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
            xmltext = xmldoc.CreateTextNode(Level.ToString());
            xmlelem.AppendChild(xmltext);
            xmldoc.ChildNodes.Item(1).AppendChild(xmlelem);

            //Score

            xmlelem = xmldoc.CreateElement("", "Score", "");
            xmltext = xmldoc.CreateTextNode(ScoreValue.ToString());
            xmlelem.AppendChild(xmltext);
            xmldoc.ChildNodes.Item(1).AppendChild(xmlelem);

            //Live

            xmlelem = xmldoc.CreateElement("", "Live", "");
            xmltext = xmldoc.CreateTextNode(lives.ToString());
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


            //double RealTime = gameTime.TotalGameTime.TotalSeconds;

            time = time + 1*0.02;

            //if(==1)
            //{
          //  time = this.time;
            if (time >= 2 || time <= 4)
            {
                BestScoreLoad();
            }
          
            if (ScoreValue > BestScore)
            {
                BestScoreSave();
            }

            
            //}





            //Keyboard state

            KeyboardState keyboardState = Keyboard.GetState();

            //If escape is pressed => Exit 

            if (keyboardState.IsKeyDown(Keys.Escape)){this.Exit();}



                        TimeSpan  mElapsedTime = gameTime.ElapsedGameTime;

                        if (mElapsedTime > TimeSpan.FromSeconds(5))
                        {

                            FPSManager fps = new FPSManager(this, graphics.GraphicsDevice);
                            this.Components.Add(fps);
                        }




             if (Level == 2 && keyboardState.IsKeyDown(Keys.L)) {Level_2();}
             if (Level == 3 && keyboardState.IsKeyDown(Keys.L)) { Level_3(); }
             if (Level == 4 && keyboardState.IsKeyDown(Keys.L)) { Level_4(); }
             if (Level == 5 && keyboardState.IsKeyDown(Keys.L)) { Level_5(); }


            //If space is pressed => NewGame  

            if (gameState == GameState.TitleScreen &&  keyboardState.IsKeyDown(Keys.S)) //When you press on start, the game run
            {
                NewGame();
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


            //Load Gale

            if (gameState == GameState.TitleScreen && keyboardState.IsKeyDown(Keys.L))
            {
                LoadGame();
            }

            #region cheats

            //One live bonus

            if (keyboardState.IsKeyDown(Keys.C) && keyboardState.IsKeyDown(Keys.Space))
            {
                lives = lives+1*0.1;//Combinaison C + Space = +1 live
            }

            //Increase the score

            if (keyboardState.IsKeyDown(Keys.C) && keyboardState.IsKeyDown(Keys.O))
            {
                ScoreValue = ScoreValue + 1000*0.1;
            }

            //Increase the time

            if (keyboardState.IsKeyDown(Keys.C) && keyboardState.IsKeyDown(Keys.T))
            {
                time = time - 10;
            }

            //Bonus level

            if (keyboardState.IsKeyDown(Keys.C) && keyboardState.IsKeyDown(Keys.B))
            {
                bonus();
            }


            #endregion



            //
            // All the collisions
            //
            listCar.ForEach(delegate(Car c) { Collision(c); }); //Cars
            listBus.ForEach(delegate(Bus b) { Collision(b); }); //Buses
            listWood.ForEach(delegate(Wood w) { Collision(w); }); //Woods
            listTaxi.ForEach(delegate(Taxi t) { Collision(t); }); //Taxis




            //After 10 seconds, the game is over

            if (time>=10){GameOver();}


            listGateway.ForEach(delegate(Gateway g) {


                if ((frog.mLocation.X >= g.mLocation.X - 50 && frog.mLocation.X <= g.mLocation.X + 50
       && frog.mLocation.Y >= g.mLocation.Y - 50 && frog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level1
              )
              ||
              (
              redfrog.mLocation.X >= g.mLocation.X - 50 && redfrog.mLocation.X <= g.mLocation.X + 50
       && redfrog.mLocation.Y >= g.mLocation.Y - 50 && redfrog.mLocation.Y <= g.mLocation.Y + 50
                &&
                             bluefrog.mLocation.X >= g.mLocation.X - 50 && bluefrog.mLocation.X <= g.mLocation.X + 50
       && bluefrog.mLocation.Y >= g.mLocation.Y - 50 && bluefrog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level1
              )
               )

                {
                    ScoreValue = ScoreValue + 750;
                    Level_2();

                }
            
            });


            listGateway.ForEach(delegate(Gateway g)
            {


                if ((frog.mLocation.X >= g.mLocation.X - 50 && frog.mLocation.X <= g.mLocation.X + 50
       && frog.mLocation.Y >= g.mLocation.Y - 50 && frog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level2
              )
              ||
              (
              redfrog.mLocation.X >= g.mLocation.X - 50 && redfrog.mLocation.X <= g.mLocation.X + 50
       && redfrog.mLocation.Y >= g.mLocation.Y - 50 && redfrog.mLocation.Y <= g.mLocation.Y + 50
                &&
                             bluefrog.mLocation.X >= g.mLocation.X - 50 && bluefrog.mLocation.X <= g.mLocation.X + 50
       && bluefrog.mLocation.Y >= g.mLocation.Y - 50 && bluefrog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level2
              )
               )
                {
                    ScoreValue = ScoreValue + 750;
                    Level_3();

                }

            });




            listGateway.ForEach(delegate(Gateway g)
            {


                if ((frog.mLocation.X >= g.mLocation.X - 50 && frog.mLocation.X <= g.mLocation.X + 50
       && frog.mLocation.Y >= g.mLocation.Y - 50 && frog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level3
              )
              ||
              (
              redfrog.mLocation.X >= g.mLocation.X - 50 && redfrog.mLocation.X <= g.mLocation.X + 50
       && redfrog.mLocation.Y >= g.mLocation.Y - 50 && redfrog.mLocation.Y <= g.mLocation.Y + 50
                &&
                             bluefrog.mLocation.X >= g.mLocation.X - 50 && bluefrog.mLocation.X <= g.mLocation.X + 50
       && bluefrog.mLocation.Y >= g.mLocation.Y - 50 && bluefrog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level3
              )
               )
                {
                    ScoreValue = ScoreValue + 750;
                    Level_4();

                }

            });



            listGateway.ForEach(delegate(Gateway g)
            {


                if ((frog.mLocation.X >= g.mLocation.X - 50 && frog.mLocation.X <= g.mLocation.X + 50
       && frog.mLocation.Y >= g.mLocation.Y - 50 && frog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level4
              )
              ||
              (
              redfrog.mLocation.X >= g.mLocation.X - 50 && redfrog.mLocation.X <= g.mLocation.X + 50
       && redfrog.mLocation.Y >= g.mLocation.Y - 50 && redfrog.mLocation.Y <= g.mLocation.Y + 50
                &&
                             bluefrog.mLocation.X >= g.mLocation.X - 50 && bluefrog.mLocation.X <= g.mLocation.X + 50
       && bluefrog.mLocation.Y >= g.mLocation.Y - 50 && bluefrog.mLocation.Y <= g.mLocation.Y + 50
                && gameState == GameState.Level4
              )
               )
                {
                    ScoreValue = ScoreValue + 750;
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
                    ScoreValue = ScoreValue + 750;
                    YouWin();

                }

            });

            if (gameState == GameState.Level1 || gameState == GameState.Level2 || gameState == GameState.Level3 || gameState == GameState.Level4 || gameState == GameState.Level5 || gameState == GameState.Bonus) //When you press on start, the game run
           {

               float SPEED = 0; //Declaration

               if (gameState == GameState.Level1) { SPEED = 0.3f;} //Level 1 Speed
               if (gameState == GameState.Level2) { SPEED = 0.4f; } //Level 2 Speed
               if (gameState == GameState.Level3) { SPEED = 0.5f; } //Level 3 Speed
               if (gameState == GameState.Level4) { SPEED = 0.6f; } //Level 4 Speed
               if (gameState == GameState.Level5) { SPEED = 0.7f; } //Level 5 Speed
               if (gameState == GameState.Bonus) { SPEED = 0.8f; } //Bonus Speed

               //
               // Objects movement and Speed
               //

               listCar.ForEach(delegate(Car c) { c.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
               listBus.ForEach(delegate(Bus b) { b.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
               listWood.ForEach(delegate(Wood w) { w.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
               listAlligator.ForEach(delegate(Alligator a) { a.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
               listTurtle.ForEach(delegate(Turtle a) { a.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
               listTaxi.ForEach(delegate(Taxi t) { t.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
               listBicycle.ForEach(delegate(Bicycle b) { b.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
               listCow.ForEach(delegate(Cow c) { c.mLocation.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * SPEED; });
  
           }


           if (gameState == GameState.GameOver && keyboardState.IsKeyDown(Keys.S)) { NewGame(); }



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
                BestScoreLoad();
                Hidden();


                spriteBatch.Begin();
                spriteBatch.DrawString(ScoreFont, "The Frogger by Maxime Rauer", new Vector2(20, 760), Color.White);
                spriteBatch.DrawString(ScoreFont, "Actual Bestscore is: "+BestScore, new Vector2(450, 760), Color.YellowGreen);
                spriteBatch.DrawString(ScoreFont, "Konami@1981", new Vector2(950, 760), Color.White);
                spriteBatch.DrawString(ScoreFont, "Revision: " + Revision.ToString(), new Vector2(1150, 760), Color.White);
                spriteBatch.End();
            
            }


            if (gameState == GameState.Level1 || gameState == GameState.Level2 || gameState == GameState.Level3 || gameState == GameState.Level4 || gameState == GameState.Level5)
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
                spriteBatch.DrawString(mBitmapFont, "Your score is "+ScoreValue.ToString(), new Vector2(400.0f, 400.0f), Color.White);
                spriteBatch.DrawString(ScoreFont, "Press escape to leave the game", new Vector2(470, 460), Color.DarkGoldenrod);
                spriteBatch.DrawString(ScoreFont, "Press on S to start a new game", new Vector2(470, 490), Color.RoyalBlue);
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

            if (gameState != GameState.GameOver && gameState != GameState.TitleScreen && gameState != GameState.GameWin)
            {

            double TimeLeft=10.0-Math.Round(time);

            spriteBatch.Begin();
            spriteBatch.DrawString(ScoreFont, "Score: " + ScoreValue.ToString(), new Vector2(20, 760), Color.White);
            spriteBatch.DrawString(ScoreFont, "Level: " + Math.Round(Level), new Vector2(140, 760), Color.White);
            spriteBatch.DrawString(ScoreFont, "Live(s) left: " + Math.Round(lives), new Vector2(250, 760), Color.White);
            spriteBatch.DrawString(ScoreFont, "Time left: " + TimeLeft.ToString()+"s", new Vector2(390, 760), Color.White);
            spriteBatch.DrawString(ScoreFont, "To save game, press P", new Vector2(650, 760), Color.Turquoise);
            spriteBatch.DrawString(ScoreFont, "Konami@1981", new Vector2(950, 760), Color.White);
            spriteBatch.DrawString(ScoreFont, "Revision: " + Revision.ToString(), new Vector2(1150, 760), Color.White);
            spriteBatch.End();
            }

            //Draw
            
            base.Draw(gameTime);
        }

        #endregion

    }
}
        #endregion