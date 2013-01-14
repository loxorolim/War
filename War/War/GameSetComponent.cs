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
 
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            logoBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            spriteBatch.Draw(warMap, Vector2.Zero, null, Color.White, 0, Vector2.Zero, Global.SCALE, SpriteEffects.None, 0);
            logoBatch.Draw(gameSetLogo, gameSetPosition, null, Color.White, 0, Vector2.Zero, Global.SCALE, SpriteEffects.None, 0);          
            
            spriteBatch.End();
            logoBatch.End();

            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            logoBatch = new SpriteBatch(Game.GraphicsDevice);
            warMap = Game.Content.Load<Texture2D>("WarMap16x9Grey");
            gameSetLogo = Game.Content.Load<Texture2D>("gameSetLogo");
            gameSetPosition = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - gameSetLogo.Width / 2, 0);
            base.LoadContent();
        }
       
    }
    
}
