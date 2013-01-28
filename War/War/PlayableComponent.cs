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
        List<Token> tokens;
        
        
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
            buttons.Add(new Button(10, 530, 1));
            buttons.Add(new Button(60, 495, 2));
            buttons.Add(new Button(60, 545, 2));
            buttons.Add(new Button(160, 545, 2));
            tokens = new List<Token>();
           

            MaquinaDeRegras.sortearTerritorios();
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
          /*  tokens = new List<Token>();
            List<Jogador> jogadores = Tabuleiro.jogadores;
            for (int i = 0; i < jogadores.Count; i++)
            {
                List<Territorio> territorios = jogadores[i].getTerritorios();
                for (int j = 0; j < territorios.Count; j++)
                {
                    Color cor = new Color();
                    switch (jogadores[i].getCor())
                    {
                        case Global.AMARELO:
                            cor = Color.Yellow;
                            break;
                        case Global.AZUL:
                            cor = Color.Blue;
                            break;
                        case Global.BRANCO:
                            cor = Color.White;
                            break;
                        case Global.PRETO:
                            cor = Color.Black;
                            break;
                        case Global.VERDE:
                            cor = Color.Green;
                            break;
                        case Global.VERMELHO:
                            cor = Color.Red;
                            break;
                        default:
                            break;
                    }
                    tokens.Add(new Token(territorios[j].getPosX(), territorios[j].getPosY(), 3, cor));
                }
            }*/


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
            for (int i = 0; i < tokens.Count; i++)
            {
                Color cor = new Color();
                
                buttonBatch.Draw(tokens[i].getTokenTexture(), tokens[i].getTokenPosition(), tokens[i].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }




            mapBatch.End();
            buttonBatch.End();
            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            mapBatch = new SpriteBatch(Game.GraphicsDevice);
            buttonBatch = new SpriteBatch(Game.GraphicsDevice);
            warMap = Game.Content.Load<Texture2D>("warMapWindow");
            buttons[0].setButtonTexture(Game.Content.Load<Texture2D>("getCardButton"));
            buttons[1].setButtonTexture(Game.Content.Load<Texture2D>("attackButton"));
            buttons[2].setButtonTexture(Game.Content.Load<Texture2D>("realocateButton"));
            buttons[3].setButtonTexture(Game.Content.Load<Texture2D>("endTurnButton"));

            for (int i = 0; i < tokens.Count; i++)
            {
                tokens[i].setTokenTexture(Game.Content.Load<Texture2D>("peon"));
            }

            base.LoadContent();
        }
    }
}
