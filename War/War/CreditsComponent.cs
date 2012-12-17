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
        Texture2D creditsImage;
        SpriteBatch batch;
        SpriteBatch buttonBatch;
        Button menuButton;
        MouseState mouse;
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
            menuButton = new Button(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - 100, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 200);
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

                mouse = Mouse.GetState();
                menuButton.changeCurrentFrame(mouse.X,mouse.Y);
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (menuButton.isCollided(mouse.X, mouse.Y))
                    {
                        War.CurrentState = War.GameState.Intro;
                    }

                }
            }
            catch (Exception e)
            {
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            buttonBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            float scale = Global.calculate16x9();
            batch.Draw(creditsImage, Vector2.Zero, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            buttonBatch.Draw(menuButton.getButtonTexture(), menuButton.getButtonPosition(),menuButton.getCurrentFrame(), Color.White,0,Vector2.Zero,1,SpriteEffects.None,0);
            batch.End();
            buttonBatch.End();
            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            batch = new SpriteBatch(Game.GraphicsDevice);
            buttonBatch = new SpriteBatch(Game.GraphicsDevice);
            creditsImage = Game.Content.Load<Texture2D>("creditsState");
            menuButton.setButtonTexture(Game.Content.Load<Texture2D>("menuButton"));
            base.LoadContent();
        }
    }
}
