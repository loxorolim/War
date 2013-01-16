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
    public class InstructionsComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch insBatch;
        SpriteBatch buttonBatch;
        Texture2D currentRule;
        int currentRuleNumber;
        int numberOfRules;
        List<Button> buttons;
        MouseState mouseStateCurrent,mouseStatePrevious;
        public InstructionsComponent(Game game)
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
            // TODO: Add your initialization code here
            buttons.Add(new Button(Global.WIDTH / 2 - 100, Global.HEIGHT - 150,2));
            buttons.Add(new Button(100, Global.HEIGHT - 150,2));
            buttons.Add(new Button(Global.WIDTH - 300, Global.HEIGHT - 150,2));
            currentRuleNumber = 0;
            numberOfRules = 1;
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
                for (int i = 0; i < buttons.Count(); i++)
                {
                    buttons[i].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                }
                if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
                {
                    if (buttons[0].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                    {
                        War.CurrentState = War.GameState.Intro;
                        currentRuleNumber = 0;
                    }
                    if (buttons[1].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && currentRuleNumber > 0 )
                    {
                        currentRuleNumber--;
                    }
                    if (buttons[2].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && currentRuleNumber < numberOfRules)
                    {
                        currentRuleNumber++;
                    }
                   
                }
                mouseStatePrevious = mouseStateCurrent;
            }
            catch (Exception e)
            {
            }
            switch (currentRuleNumber)
            {
                case 0:
                    {
                        currentRule = Game.Content.Load<Texture2D>("rules1Window");
                        break;
                    }
                case 1:
                    {
                        currentRule = Game.Content.Load<Texture2D>("rules2Window");
                        break;
                    }
                default:
                    {
                        break;
                    }

            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            insBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            buttonBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            insBatch.Draw(currentRule, Vector2.Zero, null, Color.White, 0, Vector2.Zero, Global.SCALE, SpriteEffects.None, 0);

            buttonBatch.Draw(buttons[0].getButtonTexture(), buttons[0].getButtonPosition(), buttons[0].getCurrentFrame(), Color.White, 0, Vector2.Zero, Global.SCALE, SpriteEffects.None, 0);
            if(currentRuleNumber > 0)
                buttonBatch.Draw(buttons[1].getButtonTexture(), buttons[1].getButtonPosition(), buttons[1].getCurrentFrame(), Color.White, 0, Vector2.Zero, Global.SCALE, SpriteEffects.None, 0);
            if(currentRuleNumber < numberOfRules)
                buttonBatch.Draw(buttons[2].getButtonTexture(), buttons[2].getButtonPosition(), buttons[2].getCurrentFrame(), Color.White, 0, Vector2.Zero, Global.SCALE, SpriteEffects.None, 0);
            insBatch.End();
            buttonBatch.End();

            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            insBatch = new SpriteBatch(Game.GraphicsDevice);
            buttonBatch = new SpriteBatch(Game.GraphicsDevice);
            currentRule = Game.Content.Load<Texture2D>("rules1Window");
            buttons[0].setButtonTexture(Game.Content.Load<Texture2D>("menuButton"));
            buttons[1].setButtonTexture(Game.Content.Load<Texture2D>("backButton"));
            buttons[2].setButtonTexture(Game.Content.Load<Texture2D>("nextButton"));

            base.LoadContent();
        }
       
    }
}
