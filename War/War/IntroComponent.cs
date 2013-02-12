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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class IntroComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D warMap;
        Texture2D warLogo;
        Vector2 warLogoPosition;
        SpriteBatch mapBatch;
        SpriteBatch logoBatch;
        List<Button> buttons;
        Boolean firstTimeIntro;
        Rectangle logoRectangle;
        SoundEffect swordSound;
        MouseState mouseStateCurrent,mouseStatePrevious;
        public IntroComponent(Game game)
            : base(game)
        {    
            buttons = new List<Button>();
            
            firstTimeIntro = true;
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            
            buttons.Add(new Button(800 / 2 - 50 ,600 / 10 + 100,2));
            buttons.Add(new Button(800 / 2 - 50, 600 / 10 + 150,2));
            buttons.Add(new Button(800 / 2 - 50, 600 / 10 + 200,2));
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (firstTimeIntro)
            {
                warLogoPosition.Y += 2;
            }
            if (warLogoPosition.Y >= 0 && firstTimeIntro)
            {
                firstTimeIntro = false;
                SoundEffectInstance swordSoundInstance = swordSound.CreateInstance();
                swordSoundInstance.Volume = .2f;
                swordSoundInstance.Play();
                logoRectangle.X = warLogo.Width / 2;
            }
            try
            {
                
                mouseStateCurrent = Mouse.GetState();
                for (int i = 0; i < buttons.Count(); i++)
                {
                    buttons[i].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                }
                if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
                {
                    if (buttons[2].isCollided(mouseStateCurrent.X,mouseStateCurrent.Y))
                    {        
                        War.CurrentState = War.GameState.Credits;                 
                    }
                    if (buttons[1].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                    {
                        War.CurrentState = War.GameState.Instructions;
                    }
                    if (buttons[0].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                    {
                        War.CurrentState = War.GameState.GameSet;
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
            mapBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix);
            logoBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix);
            mapBatch.Draw(warMap, Vector2.Zero, null, Color.White, 0, Vector2.Zero,1, SpriteEffects.None, 0);           
            logoBatch.Draw(warLogo,warLogoPosition, logoRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            for (int i = 0; i < buttons.Count; i++)
            {
                logoBatch.Draw(buttons[i].getButtonTexture(), buttons[i].getButtonPosition(),buttons[i].getCurrentFrame(), Color.White,0,Vector2.Zero,1,SpriteEffects.None,0);
                logoBatch.Draw(buttons[i].getButtonTexture(), buttons[i].getButtonPosition(),buttons[i].getCurrentFrame(), Color.White,0,Vector2.Zero,1,SpriteEffects.None,0);
                
            }
            
            
          //  batch.DrawString(font, "ALIEN RAID", new Vector2(150, 120), Color.Yellow);
          //  batch.DrawString(_smallFont, "Press ENTER to play", new Vector2(280, 250), Color.Cyan);
            mapBatch.End();
            logoBatch.End();

            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {         
            mapBatch = new SpriteBatch(Game.GraphicsDevice);
            logoBatch = new SpriteBatch(Game.GraphicsDevice);
            warMap = Game.Content.Load<Texture2D>("WarMapWindowGrey");
            warLogo = Game.Content.Load<Texture2D>("WarLogo");
            swordSound = Game.Content.Load<SoundEffect>("swordSound");
            Texture2D tex = Game.Content.Load<Texture2D>("startButton");
            buttons[0].setButtonTexture(Game.Content.Load<Texture2D>("startButton"));
            buttons[1].setButtonTexture(Game.Content.Load<Texture2D>("instructionsButton"));
            buttons[2].setButtonTexture(Game.Content.Load<Texture2D>("creditsButton"));
            warLogoPosition = new Vector2(800 / 2 - warLogo.Width/4, -warLogo.Height - 20);
            logoRectangle = new Rectangle(0, 0, warLogo.Width/2, warLogo.Height);
            
          //  font = Game.Content.Load<SpriteFont>("Font/stats");
            base.LoadContent();
        }
    }
}
