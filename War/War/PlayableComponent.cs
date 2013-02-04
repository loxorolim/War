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
        Texture2D mapGuide;
        SpriteBatch mapBatch;
        SpriteBatch tokenBatch;
        SpriteBatch buttonBatch;
        SpriteBatch logoBatch;
        SpriteFont font;
        MouseState mouseStateCurrent,mouseStatePrevious;
        Boolean drawGuide = false;
        public static Boolean playersSelected { get; set; }
        Boolean showAddButton = false;
        Token addToken;

        List<Button> buttons;
        List<Token> tokens;      
        
        
        public PlayableComponent(Game game)
            : base(game)
        {
            buttons = new List<Button>();
            tokens = new List<Token>();
            
            
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
            playersSelected = false;
            buttons.Add(new Button(10, 498, 1));
            buttons.Add(new Button(75, 495, 2));
            buttons.Add(new Button(75, 545, 2));
            buttons.Add(new Button(175, 545, 2));
            buttons.Add(new Button(751, 12, 2));
            addToken = new Token(-30,-30,1,null);
           //tokens.Add(new Token(400, 300, 3, Color.White));

            

            

            
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            createTokensPositions();
            try
            {

                mouseStateCurrent = Mouse.GetState();
                
                buttons[4].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                if (buttons[4].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
                {
                    drawGuide = !drawGuide;
                }
                foreach (Token tok in tokens)
                {
                    if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
                    {
                        if (tok.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y))
                        {
                            addToken.setTokenPosition(new Vector2(tok.getTokenPosition().X, tok.getTokenPosition().Y - 25));
                            addToken.setTerritorio(tok.getTerritorio());
                        }
                    }
                }
                if (addToken.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
                {
                    addToken.getTerritorio().setNumeroExercitos(addToken.getTerritorio().getNumeroExercito()+1);
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
            mapBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            buttonBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            tokenBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            logoBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            if (drawGuide)
            {
                logoBatch.Draw(mapGuide, new Vector2(90, 40), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            mapBatch.Draw(warMap, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            
            for(int i = 0; i< buttons.Count;i++)
            {
                buttonBatch.Draw(buttons[i].getButtonTexture(), buttons[i].getButtonPosition(), buttons[i].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            for (int i = 0; i < tokens.Count; i++)
            {                
                tokenBatch.Draw(tokens[i].getTokenTexture(), tokens[i].getTokenPosition(), tokens[i].getCurrentFrame(), tokens[i].getColor(), 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                tokenBatch.Draw(addToken.getTokenTexture(), addToken.getTokenPosition(), addToken.getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                Vector2 fontPosition = tokens[i].getTokenPosition();
                fontPosition.X += 30;
                tokenBatch.DrawString(font, string.Format(tokens[i].getNumberOfSoldiers().ToString()), fontPosition, tokens[i].getColor());
                
            }



            
            mapBatch.End();
            buttonBatch.End();
            tokenBatch.End();
            logoBatch.End();
            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            mapBatch = new SpriteBatch(Game.GraphicsDevice);
            buttonBatch = new SpriteBatch(Game.GraphicsDevice);
            tokenBatch = new SpriteBatch(Game.GraphicsDevice);
            logoBatch = new SpriteBatch(Game.GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("font");
            warMap = Game.Content.Load<Texture2D>("warMapNewWindow");
            mapGuide = Game.Content.Load<Texture2D>("mapGuide");
            buttons[0].setButtonTexture(Game.Content.Load<Texture2D>("Cartas/verso-carta"));
            buttons[1].setButtonTexture(Game.Content.Load<Texture2D>("attackButton"));
            buttons[2].setButtonTexture(Game.Content.Load<Texture2D>("realocateButton"));
            buttons[3].setButtonTexture(Game.Content.Load<Texture2D>("endTurnButton"));
            buttons[4].setButtonTexture(Game.Content.Load<Texture2D>("mapGuideButton"));
            addToken.setTokenTexture(Game.Content.Load<Texture2D>("addButton"));
            

            base.LoadContent();
        }
        public void associateNumberOfArmies()
        {

        }
        public void createTokensPositions()
        {
            if (playersSelected)
            {
                tokens.Clear();
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
                        tokens.Add(new Token(territorios[j].getPosX(), territorios[j].getPosY(), 3, cor,territorios[j].getNumeroExercito(),territorios[j]));
                       
                      
                    }
                }
                for (int i = 0; i < tokens.Count; i++)
                {
                    tokens[i].setTokenTexture(Game.Content.Load<Texture2D>("peon"));
                }
            }
        }
        
    }
    
}
