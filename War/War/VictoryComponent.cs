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
    public class VictoryComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D warMap;
        Texture2D victoryLogo;
        Texture2D objective;
        Vector2 victoryLogoPosition;
        SpriteBatch spriteBatch;
        SpriteBatch batch;
        MouseState mouseStateCurrent,mouseStatePrevious;
        public static Jogador victorPlayer { get; set; }
        public VictoryComponent(Game game)
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
                //public const int BRANCO = 0;
                //public const int PRETO = 1;
                //public const int VERMELHO = 2;
                //public const int VERDE = 3;
                //public const int AZUL = 4;
                //public const int AMARELO = 5;
                switch (victorPlayer.getCor())
                {
                    case 0:
                        victoryLogo = Game.Content.Load<Texture2D>("whiteVictorious");
                        
                        break;
                    case 1:
                        victoryLogo = Game.Content.Load<Texture2D>("blackVictorious");
                        break;
                    case 2:
                        victoryLogo = Game.Content.Load<Texture2D>("redVictorious");
                        break;
                    case 3:
                        victoryLogo = Game.Content.Load<Texture2D>("greenVictorious");
                        break;
                    case 4:
                        victoryLogo = Game.Content.Load<Texture2D>("blueVictorious");
                        break;
                    case 5:
                        victoryLogo = Game.Content.Load<Texture2D>("yellowVictorious");
                        break;
                    default:

                        break;
                }
                objective = Game.Content.Load<Texture2D>("Cartas/objetivos/" + victorPlayer.getObjetivo().getIdentificador().ToString());
        
            }
            catch (Exception e)
            {
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix);
            
            batch.Draw(warMap, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(objective, new Vector2(280, 200), null, Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0);
            spriteBatch.Draw(victoryLogo, victoryLogoPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            
            
            batch.End();
            spriteBatch.End();
            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            batch = new SpriteBatch(Game.GraphicsDevice);
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            //creditsLogo = Game.Content.Load<Texture2D>("creditsLogo");
            warMap = Game.Content.Load<Texture2D>("warMapWindowGrey");
            victoryLogo = Game.Content.Load<Texture2D>("redVictorious");
            objective = Game.Content.Load<Texture2D>("Cartas/objetivos/1");
            victoryLogoPosition = new Vector2(Global.WIDTH / 2 - victoryLogo.Width / 2, 80);
           // menuButton.setButtonTexture(Game.Content.Load<Texture2D>("menuButton"));
            //creditsLogoPosition = new Vector2(Global.WIDTH / 2 - creditsLogo.Width / 2, 0);
            base.LoadContent();
        }
    }
}
