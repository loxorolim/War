using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace War
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class War : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch tokenBatch;
        Texture2D warMap;
        Token token = new Token(400, 400, Color.Black); //Testando commit pelo Git Extensions
        Token token2 = new Token(345, 800, Color.Blue);
        Vector2 warMapPosition = Vector2.Zero;
        IntroComponent IntroComponent;
        CreditsComponent CreditsComponent;
        Song mainMusic;
        Boolean startMainMusic;
        public static GameState CurrentState { get; set; }
        public War()
        {
            
            graphics = new GraphicsDeviceManager(this);
            IntroComponent = new IntroComponent(this);
            CreditsComponent = new CreditsComponent(this);
            Components.Add(IntroComponent);
            Components.Add(CreditsComponent);
            CurrentState = GameState.Intro;
            
           // GotoState();
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            this.graphics.IsFullScreen = true;
            this.IsMouseVisible = true;
            startMainMusic = true;
            
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
              
          //  IntroComponent.Enabled = IntroComponent.Visible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tokenBatch = new SpriteBatch(GraphicsDevice);
            warMap = Content.Load<Texture2D>("WarMap16x9");
            token.setTexture(Content.Load<Texture2D>(token.getImgFile()));
            token2.setTexture(Content.Load<Texture2D>(token.getImgFile()));
            mainMusic = Content.Load<Song>("Medieval Music");
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GotoState();
            KeyboardState keyboard = Keyboard.GetState();
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if(keyboard.IsKeyDown(Keys.Escape))
                this.Exit();
            if (keyboard.IsKeyDown(Keys.Down))
                token.setUpdatePosition(3, 0);
            // TODO: Add your update logic here

            if (startMainMusic)
            {
                MediaPlayer.Play(mainMusic);
                startMainMusic = false;
                if (MediaPlayer.IsMuted)
                {
                    startMainMusic = true;
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Desenha o mapa

            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
    //        float scale = calculateScale16x9();
     //       spriteBatch.Draw(warMap, warMapPosition, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);

           // spriteBatch.Draw(token.getTexture(), new Vector2(Mouse.GetState().X,Mouse.GetState().Y), null, token.getColor(), 0, Vector2.Zero, 1, SpriteEffects.None, 0);
           //   spriteBatch.Draw(circle, new Vector2(400,1000), null, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
          //  spriteBatch.Draw(token2.getTexture(), token2.getPosition(), null, token2.getColor(), 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            
          //  spriteBatch.End();
      

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        protected float calculateScale16x9()
        {
            float scale = 1.0f;
            float x = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            scale = x / 3360;
            return scale;
        }
        public enum GameState
        {
            Intro, InPlay, GameOver, Credits, Instructions
        }
        public GameState State { get; private set; }

        public void GotoState()
        {
            switch (CurrentState)
            {
                case GameState.Intro:
                    IntroComponent.Enabled = IntroComponent.Visible = true;
                    CreditsComponent.Enabled = CreditsComponent.Visible = false;
                    
                 //   AliensComponent.Enabled = AliensComponent.Visible = false;
                //    PlayerComponent.Enabled = PlayerComponent.Visible = false;
                //    GameOverComponent.Enabled = GameOverComponent.Visible = false;
                    break;

                case GameState.InPlay:
                    IntroComponent.Enabled = IntroComponent.Visible = false;
               //     AliensComponent.Enabled = AliensComponent.Visible = true;
               //     PlayerComponent.Enabled = PlayerComponent.Visible = true;
              //      GameOverComponent.Enabled = GameOverComponent.Visible = false;
                    break;

                case GameState.GameOver:
                      IntroComponent.Enabled = IntroComponent.Visible = false;
               //     AliensComponent.Enabled = AliensComponent.Visible = false;
              //      PlayerComponent.Enabled = PlayerComponent.Visible = false;
             //      GameOverComponent.Enabled = GameOverComponent.Visible = true;
                    break;

                case GameState.Credits:
                    IntroComponent.Enabled = IntroComponent.Visible = false;
                    CreditsComponent.Enabled = CreditsComponent.Visible = true;
                    //     AliensComponent.Enabled = AliensComponent.Visible = false;
                    //      PlayerComponent.Enabled = PlayerComponent.Visible = false;
                    //      GameOverComponent.Enabled = GameOverComponent.Visible = true;
                    break;
                case GameState.Instructions:
                    IntroComponent.Enabled = IntroComponent.Visible = false;
                    CreditsComponent.Enabled = CreditsComponent.Visible = false;

                    //   AliensComponent.Enabled = AliensComponent.Visible = false;
                    //    PlayerComponent.Enabled = PlayerComponent.Visible = false;
                    //    GameOverComponent.Enabled = GameOverComponent.Visible = false;
                    break;
            }
          
        }
        
    }
}
