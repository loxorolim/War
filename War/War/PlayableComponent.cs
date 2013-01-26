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
    public class PlayableComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D warMap;
        SpriteBatch mapBatch;
        SpriteBatch tokenBatch;
        SpriteBatch buttonBatch;
        SpriteBatch logoBatch;

        List<Button> buttons;
        
        
        public PlayableComponent(Game game)
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
            //Botoes pegar carta, atacar, realocar, finalizar
            buttons.Add(new Button(400,300, 1));
           // buttons.Add(new Button(Global.WIDTH / 2 - 50, Global.HEIGHT / 10 + 100, 5));
          //  buttons.Add(new Button(Global.WIDTH / 2 - 50, Global.HEIGHT / 10 + 100, 5));
         //   buttons.Add(new Button(Global.WIDTH / 2 - 50, Global.HEIGHT / 10 + 100, 5));
           
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
           



            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            mapBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            buttonBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
      
            mapBatch.Draw(warMap, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            for(int i = 0; i< buttons.Count;i++)
            {
                buttonBatch.Draw(buttons[i].getButtonTexture(), buttons[i].getButtonPosition(), buttons[i].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }




            mapBatch.End();
            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            mapBatch = new SpriteBatch(Game.GraphicsDevice);
            buttonBatch = new SpriteBatch(Game.GraphicsDevice);
            warMap = Game.Content.Load<Texture2D>("warMapWindow");
            buttons[0].setButtonTexture(Game.Content.Load<Texture2D>("getCardButton"));
            base.LoadContent();
        }
    }
}
