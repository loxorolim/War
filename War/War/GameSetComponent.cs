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
    class GameSetComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SpriteBatch logoBatch;
        Texture2D warMap;
        Texture2D gameSetLogo;
        Vector2 gameSetPosition;
        List<Button> buttons;
        MouseState mouseStateCurrent,mouseStatePrevious;
        int activeButton = 0;
        public GameSetComponent(Game game)
            : base(game)
        {
            buttons = new List<Button>();
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            buttons.Add(new Button(Global.WIDTH / 2 - 100, Global.HEIGHT / 10 + 100,5));
            buttons.Add(new Button(Global.WIDTH / 2 - 100, Global.HEIGHT / 10 + 200,5));
            buttons.Add(new Button(Global.WIDTH / 2 - 100, Global.HEIGHT / 10 + 300,5));
            buttons.Add(new Button(Global.WIDTH / 2 - 100, Global.HEIGHT / 10 + 400,5));
            buttons.Add(new Button(Global.WIDTH / 2 - 100, Global.HEIGHT / 10 + 500,5));
            buttons.Add(new Button(Global.WIDTH / 2 - 100, Global.HEIGHT / 10 + 600,5));
            //setas
            buttons.Add(new Button(Global.WIDTH / 2 - 200, Global.HEIGHT / 10 + 100,2));
            buttons.Add(new Button(Global.WIDTH / 2 + 100, Global.HEIGHT / 10 + 100,2));

            buttons.Add(new Button(Global.WIDTH / 2 - 200, Global.HEIGHT / 10 + 200,2));
            buttons.Add(new Button(Global.WIDTH / 2 + 100, Global.HEIGHT / 10 + 200,2));

            buttons.Add(new Button(Global.WIDTH / 2 - 200, Global.HEIGHT / 10 + 300,2));
            buttons.Add(new Button(Global.WIDTH / 2 + 100, Global.HEIGHT / 10 + 300,2));

            buttons.Add(new Button(Global.WIDTH / 2 - 200, Global.HEIGHT / 10 + 400,2));
            buttons.Add(new Button(Global.WIDTH / 2 + 100, Global.HEIGHT / 10 + 400,2));

            buttons.Add(new Button(Global.WIDTH / 2 - 200, Global.HEIGHT / 10 + 500,2));
            buttons.Add(new Button(Global.WIDTH / 2 + 100, Global.HEIGHT / 10 + 500,2));

            buttons.Add(new Button(Global.WIDTH / 2 - 200, Global.HEIGHT / 10 + 600,2));
            buttons.Add(new Button(Global.WIDTH / 2 + 100, Global.HEIGHT / 10 + 600,2));

            //menu e play
            buttons.Add(new Button(Global.WIDTH / 2 - 400, Global.HEIGHT / 10 + 800, 2));
            buttons.Add(new Button(Global.WIDTH / 2 + 200, Global.HEIGHT / 10 + 800, 2));

            //cores
            buttons.Add(new Button(Global.WIDTH / 2 +300, Global.HEIGHT / 10 + 100, 1));
            buttons.Add(new Button(Global.WIDTH / 2 +300, Global.HEIGHT / 10 + 200, 1));
            buttons.Add(new Button(Global.WIDTH / 2 +300, Global.HEIGHT / 10 + 300, 1));
            buttons.Add(new Button(Global.WIDTH / 2 +300, Global.HEIGHT / 10 + 400, 1));
            buttons.Add(new Button(Global.WIDTH / 2 +300, Global.HEIGHT / 10 + 500, 1));
            buttons.Add(new Button(Global.WIDTH / 2 +300, Global.HEIGHT / 10 + 600, 1));
            
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            try
            {

                mouseStateCurrent = Mouse.GetState();
                for (int i = 6; i < 20; i++)
                {
                    buttons[i].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                }
                if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
                {
                    if (buttons[6].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))                  
                        buttons[0].setPreviousFrame();                  
                    if (buttons[7].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        buttons[0].setNextFrame();

                    if (buttons[8].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        buttons[1].setPreviousFrame();
                    if (buttons[9].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        buttons[1].setNextFrame();

                    if (buttons[10].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        buttons[2].setPreviousFrame();
                    if (buttons[11].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        buttons[2].setNextFrame();

                    if (buttons[12].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        buttons[3].setPreviousFrame();
                    if (buttons[13].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        buttons[3].setNextFrame();

                    if (buttons[14].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        buttons[4].setPreviousFrame();
                    if (buttons[15].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        buttons[4].setNextFrame();

                    if (buttons[16].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        buttons[5].setPreviousFrame();
                    if (buttons[17].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        buttons[6].setNextFrame();

                    if(buttons[18].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        War.CurrentState = War.GameState.Intro;
                    if (buttons[19].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                    {

                    }


                    
                    
                    
                }
                mouseStatePrevious = mouseStateCurrent;
            }
            catch (Exception e)
            {
            }
 
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            logoBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            spriteBatch.Draw(warMap, Vector2.Zero, null, Color.White, 0, Vector2.Zero, Global.SCALE, SpriteEffects.None, 0);
            logoBatch.Draw(gameSetLogo, gameSetPosition, null, Color.White, 0, Vector2.Zero, Global.SCALE, SpriteEffects.None, 0);
            for (int i = 0; i < buttons.Count; i++)
            {
                    logoBatch.Draw(buttons[i].getButtonTexture(), buttons[i].getButtonPosition(), buttons[i].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            
            spriteBatch.End();
            logoBatch.End();

            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            logoBatch = new SpriteBatch(Game.GraphicsDevice);
            warMap = Game.Content.Load<Texture2D>("WarMapWindowGrey");
            gameSetLogo = Game.Content.Load<Texture2D>("gameSetLogo");
            gameSetPosition = new Vector2(Global.WIDTH / 2 - gameSetLogo.Width / 2, 0);
            buttons[0].setButtonTexture(Game.Content.Load<Texture2D>("optionButtons"));
            buttons[1].setButtonTexture(Game.Content.Load<Texture2D>("optionButtons"));
            buttons[2].setButtonTexture(Game.Content.Load<Texture2D>("optionButtons"));
            buttons[3].setButtonTexture(Game.Content.Load<Texture2D>("optionButtons"));
            buttons[4].setButtonTexture(Game.Content.Load<Texture2D>("optionButtons"));
            buttons[5].setButtonTexture(Game.Content.Load<Texture2D>("optionButtons"));
            //botoes da seta!
            buttons[6].setButtonTexture(Game.Content.Load<Texture2D>("leftButton"));
            buttons[7].setButtonTexture(Game.Content.Load<Texture2D>("rightButton"));

            buttons[8].setButtonTexture(Game.Content.Load<Texture2D>("leftButton"));
            buttons[9].setButtonTexture(Game.Content.Load<Texture2D>("rightButton"));

            buttons[10].setButtonTexture(Game.Content.Load<Texture2D>("leftButton"));
            buttons[11].setButtonTexture(Game.Content.Load<Texture2D>("rightButton"));

            buttons[12].setButtonTexture(Game.Content.Load<Texture2D>("leftButton"));
            buttons[13].setButtonTexture(Game.Content.Load<Texture2D>("rightButton"));

            buttons[14].setButtonTexture(Game.Content.Load<Texture2D>("leftButton"));
            buttons[15].setButtonTexture(Game.Content.Load<Texture2D>("rightButton"));

            buttons[16].setButtonTexture(Game.Content.Load<Texture2D>("leftButton"));
            buttons[17].setButtonTexture(Game.Content.Load<Texture2D>("rightButton"));
            // botao menu e play
            buttons[18].setButtonTexture(Game.Content.Load<Texture2D>("menuButton"));
            buttons[19].setButtonTexture(Game.Content.Load<Texture2D>("playButton"));
            //cores
            buttons[20].setButtonTexture(Game.Content.Load<Texture2D>("white"));
            buttons[21].setButtonTexture(Game.Content.Load<Texture2D>("black"));
            buttons[22].setButtonTexture(Game.Content.Load<Texture2D>("red"));
            buttons[23].setButtonTexture(Game.Content.Load<Texture2D>("green"));
            buttons[24].setButtonTexture(Game.Content.Load<Texture2D>("blue"));
            buttons[25].setButtonTexture(Game.Content.Load<Texture2D>("yellow"));

            base.LoadContent();
        }
       
    }
    
}
