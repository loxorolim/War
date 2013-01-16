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
    public class CreditsComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D warMap;
        Texture2D creditsLogo;
        Vector2 creditsLogoPosition;
        SpriteBatch batch;
        SpriteBatch spriteBatch;
        Button menuButton;
        MouseState mouseStateCurrent,mouseStatePrevious;
        public CreditsComponent(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            menuButton = new Button(Global.WIDTH/ 2 - 100, Global.HEIGHT - 150,2);
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
                menuButton.changeCurrentFrame(mouseStateCurrent.X,mouseStateCurrent.Y);
                if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
                {
                    if (menuButton.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                    {
                        War.CurrentState = War.GameState.Intro;
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
            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            batch.Draw(warMap, Vector2.Zero, null, Color.White, 0, Vector2.Zero, Global.SCALE, SpriteEffects.None, 0);
            spriteBatch.Draw(menuButton.getButtonTexture(), menuButton.getButtonPosition(),menuButton.getCurrentFrame(), Color.White,0,Vector2.Zero,1,SpriteEffects.None,0);
            spriteBatch.Draw(creditsLogo, creditsLogoPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            batch.End();
            spriteBatch.End();
            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            batch = new SpriteBatch(Game.GraphicsDevice);
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            creditsLogo = Game.Content.Load<Texture2D>("creditsLogo");
            warMap = Game.Content.Load<Texture2D>("warMapWindowGrey");
            menuButton.setButtonTexture(Game.Content.Load<Texture2D>("menuButton"));
            creditsLogoPosition = new Vector2(Global.WIDTH / 2 - creditsLogo.Width / 2, 0);
            base.LoadContent();
        }
    }
}
