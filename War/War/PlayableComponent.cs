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
        Texture2D cardsBackground;
        SpriteBatch mapBatch;
        SpriteBatch tokenBatch;
        SpriteBatch buttonBatch;
        SpriteBatch logoBatch;
        SpriteBatch cardsBatch;
        SpriteFont font;
        MouseState mouseStateCurrent, mouseStatePrevious;
        GamePhase currentPhase = GamePhase.AddArmyPhase;
     //   Boolean firstPhase = true;
        public static int firstCounter { get; set;} 
     //   Boolean addArmyPhase = false;
     //   Boolean attackPhase = false;
     //   Boolean reallocatePhase = false;
        Boolean drawGuide = false;
        Boolean drawObj = false;
        Boolean drawCards = false;
        public static Boolean playersSelected { get; set; }
        Boolean showAddButton = false;
        Token addToken;
        Jogador turnPlayer;
        CartaObjetivo[] objCards;
        List<CartaTerritorio> territCards;
        List<Button> buttons;
        List<Button> cardButtons;
        List<Token> tokens;
        Boolean[]  readinessArray;


        public PlayableComponent(Game game)
            : base(game)
        {
            buttons = new List<Button>();
            cardButtons = new List<Button>();
            tokens = new List<Token>();
            objCards = MaquinaDeRegras.objetivos;
            territCards = MaquinaDeRegras.cartas;
            readinessArray = new Boolean[Tabuleiro.jogadores.Count];

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
            buttons.Add(new Button(10, 498, 2));
            buttons.Add(new Button(75, 495, 2));
            buttons.Add(new Button(75, 545, 2));
            buttons.Add(new Button(175, 545, 2));
            buttons.Add(new Button(751, 12, 2));
            cardButtons.Add(new Button(124, 350, 2));
            cardButtons.Add(new Button(598, 350, 2));
            addToken = new Token(-30, -30, 1, null);
            //tokens.Add(new Token(400, 300, 3, Color.White));
            foreach (CartaObjetivo obj in objCards)
                obj.setObjCardTexture(Game.Content.Load<Texture2D>(obj.getImgFile()));
            foreach (CartaTerritorio territ in territCards)
                territ.setTerritCardTexture(Game.Content.Load<Texture2D>(territ.getFigura()));
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            createTokensPositions();
            turnPlayer = Tabuleiro.jogadorDaVez;
            if (currentPhase.Equals(GamePhase.AddArmyPhase))
            {
                addArmyPhaseOperations();
            }
            checkButtonsClick();




            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            mapBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix); ;
            buttonBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix); ;
            tokenBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix); ;
            logoBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix); ;
            cardsBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix);
            if (drawGuide)
            {
                logoBatch.Draw(mapGuide, new Vector2(90, 40), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            //Ao clicar no botão para visualizar cartas o boolean será true
            if (drawCards)
            {
                //Desenhando background das cartas (pergaminho)
                cardsBatch.Draw(cardsBackground, new Vector2(40, 90), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                Random r = new Random();
                //Será utilizado quando os jogadores tiverem cartas
                //int distancia = 114;
                //foreach (CartaTerritorio carta in Tabuleiro.jogadorDaVez.getCartasJogador())
                //{
                //    cardsBatch.Draw(carta.getTerritCardTexture(), new Vector2(10 + distancia, 140), null, Color.White, 0, Vector2.Zero, 0.35f, SpriteEffects.None, 0.5f);
                //    distancia += 114;
                //}
                //Desenhando cartas aleatórias por enquanto
                cardsBatch.Draw(MaquinaDeRegras.cartas[5].getTerritCardTexture(), new Vector2(10 + 114, 140), null, Color.White, 0, Vector2.Zero, 0.35f, SpriteEffects.None, 0.5f);
                cardsBatch.Draw(MaquinaDeRegras.cartas[16].getTerritCardTexture(), new Vector2(10 + 228, 140), null, Color.White, 0, Vector2.Zero, 0.35f, SpriteEffects.None, 0.5f);
                cardsBatch.Draw(MaquinaDeRegras.cartas[20].getTerritCardTexture(), new Vector2(10 + 342, 140), null, Color.White, 0, Vector2.Zero, 0.35f, SpriteEffects.None, 0.5f);
                cardsBatch.Draw(MaquinaDeRegras.cartas[2].getTerritCardTexture(), new Vector2(10 + 456, 140), null, Color.White, 0, Vector2.Zero, 0.35f, SpriteEffects.None, 0.5f);
                cardsBatch.Draw(MaquinaDeRegras.cartas[40].getTerritCardTexture(), new Vector2(10 + 570, 140), null, Color.White, 0, Vector2.Zero, 0.35f, SpriteEffects.None, 0.5f);
                //Desenhando botões de troca de cartas e de visualizar objetivo
                buttonBatch.Draw(cardButtons[0].getButtonTexture(), cardButtons[0].getButtonPosition(), cardButtons[0].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                buttonBatch.Draw(cardButtons[1].getButtonTexture(), cardButtons[1].getButtonPosition(), cardButtons[1].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            if (drawObj)
            {
                CartaObjetivo obj = turnPlayer.getObjetivo();
                cardsBatch.Draw(obj.getObjCardTexture(), new Vector2((800 / 2) - (obj.getObjCardTexture().Width * 0.6f / 2), (600 / 2) - (obj.getObjCardTexture().Height * 0.6f / 2)), null, Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0);
            }
            mapBatch.Draw(warMap, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

           // for (int i = 0; i < buttons.Count; i++)
          //  {
                if (currentPhase == GamePhase.AddArmyPhase)
                {
                    buttonBatch.Draw(buttons[0].getButtonTexture(), buttons[0].getButtonPosition(), buttons[0].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    buttonBatch.Draw(buttons[4].getButtonTexture(), buttons[4].getButtonPosition(), buttons[4].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    buttonBatch.Draw(buttons[3].getButtonTexture(), buttons[3].getButtonPosition(), buttons[3].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
                if (currentPhase == GamePhase.AttackPhase)
                {
                    buttonBatch.Draw(buttons[0].getButtonTexture(), buttons[0].getButtonPosition(), buttons[0].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    buttonBatch.Draw(buttons[4].getButtonTexture(), buttons[4].getButtonPosition(), buttons[4].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    buttonBatch.Draw(buttons[3].getButtonTexture(), buttons[3].getButtonPosition(), buttons[3].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    buttonBatch.Draw(buttons[1].getButtonTexture(), buttons[1].getButtonPosition(), buttons[1].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
                if (currentPhase == GamePhase.ReallocatePhase)
                {
                    buttonBatch.Draw(buttons[0].getButtonTexture(), buttons[0].getButtonPosition(), buttons[0].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    buttonBatch.Draw(buttons[4].getButtonTexture(), buttons[4].getButtonPosition(), buttons[4].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    buttonBatch.Draw(buttons[3].getButtonTexture(), buttons[3].getButtonPosition(), buttons[3].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    buttonBatch.Draw(buttons[2].getButtonTexture(), buttons[2].getButtonPosition(), buttons[2].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
                //buttonBatch.Draw(buttons[i].getButtonTexture(), buttons[i].getButtonPosition(), buttons[i].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
          //  }
            for (int i = 0; i < tokens.Count; i++)
            {
                tokenBatch.Draw(tokens[i].getTokenTexture(), tokens[i].getTokenPosition(), tokens[i].getCurrentFrame(), tokens[i].getColor(), 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                if(currentPhase.Equals(GamePhase.AddArmyPhase))
                    tokenBatch.Draw(addToken.getTokenTexture(), addToken.getTokenPosition(), addToken.getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                Vector2 fontPosition = tokens[i].getTokenPosition();
                fontPosition.X += 30;
                tokenBatch.DrawString(font, string.Format(tokens[i].getNumberOfSoldiers().ToString()), fontPosition, tokens[i].getColor());

            }
            if (playersSelected)
            {
                tokenBatch.DrawString(font, string.Format(Global.getColorName(turnPlayer.getCor()) + "Player"), new Vector2(340, 0), Global.getColor(turnPlayer.getCor()));
            }


            mapBatch.End();
            tokenBatch.End();
            logoBatch.End();
            cardsBatch.End();
            buttonBatch.End();
            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            mapBatch = new SpriteBatch(Game.GraphicsDevice);
            buttonBatch = new SpriteBatch(Game.GraphicsDevice);
            tokenBatch = new SpriteBatch(Game.GraphicsDevice);
            logoBatch = new SpriteBatch(Game.GraphicsDevice);
            cardsBatch = new SpriteBatch(Game.GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("font");
            warMap = Game.Content.Load<Texture2D>("WarMapNewWindow");
            mapGuide = Game.Content.Load<Texture2D>("mapGuide");
            cardsBackground = Game.Content.Load<Texture2D>("cardsBackground");
            buttons[0].setButtonTexture(Game.Content.Load<Texture2D>("cardsButton"));
            buttons[1].setButtonTexture(Game.Content.Load<Texture2D>("attackButton"));
            buttons[2].setButtonTexture(Game.Content.Load<Texture2D>("realocateButton"));
            buttons[3].setButtonTexture(Game.Content.Load<Texture2D>("endTurnButton"));
            buttons[4].setButtonTexture(Game.Content.Load<Texture2D>("mapGuideButton"));
            cardButtons[0].setButtonTexture(Game.Content.Load<Texture2D>("tradeButton"));
            cardButtons[1].setButtonTexture(Game.Content.Load<Texture2D>("objectiveButton"));
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
                        tokens.Add(new Token(territorios[j].getPosX(), territorios[j].getPosY(), 3, Global.getColor(jogadores[i].getCor()), territorios[j].getNumeroExercito(), territorios[j]));


                    }
                }
                for (int i = 0; i < tokens.Count; i++)
                {
                    tokens[i].setTokenTexture(Game.Content.Load<Texture2D>("peon"));
                }
            }
        }
        public void checkClickableTokens()
        {
            turnPlayer.getCor();

        }
        public void checkButtonsClick()
        {
            try
            {

                mouseStateCurrent = Mouse.GetState();

                buttons[0].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                cardButtons[0].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                cardButtons[1].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                if (cardButtons[1].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && drawCards)
                {
                    drawObj = !drawObj;
                }

                if (buttons[0].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawGuide)
                {
                    if (!drawObj)
                        drawCards = !drawCards;
                }
                buttons[3].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                if (buttons[3].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawGuide)
                {
                    if (firstCounter > 0)
                    {
                        MaquinaDeRegras.passaVez();
                        firstCounter--;
                    }
                    else
                    {
                        if (currentPhase.Equals(GamePhase.ReallocatePhase))
                        {
                            currentPhase = GamePhase.AddArmyPhase;
                            MaquinaDeRegras.passaVez();
                        }
                        else
                        {

                            changeToNextPhase();
                        }
                     }
                }

                buttons[4].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                if (buttons[4].isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawCards)
                {
                    drawGuide = !drawGuide;

                }
                
                //foreach (Token tok in tokens)
                //{
                //    if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
                //    {
                //        if (tok.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && tok.getColor().Equals(Global.getColor(turnPlayer.getCor())) && !drawCards && !drawGuide)
                //        {
                //            addToken.setTokenPosition(new Vector2(tok.getTokenPosition().X, tok.getTokenPosition().Y - 25));
                //            addToken.setTerritorio(tok.getTerritorio());
                //        }
                //    }
                //}
                //if (addToken.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawCards && !drawGuide)
                //{
                //    if (turnPlayer.getNumExercitoParacolocar() > 0)
                //    {
                //        addToken.getTerritorio().setNumeroExercitos(addToken.getTerritorio().getNumeroExercito() + 1);
                //        turnPlayer.removeExercitoParacolocar();
                //    }
                //}



                mouseStatePrevious = mouseStateCurrent;
            }
            catch (Exception e)
            {
            }
        }
        public void distributeArmyPhase()
        {

        }
        public enum GamePhase
        {
            AddArmyPhase, AttackPhase, ReallocatePhase
        }
        public void changeToNextPhase()
        {
            if(currentPhase == GamePhase.AddArmyPhase)
                currentPhase = GamePhase.AttackPhase;
            else
            if (currentPhase == GamePhase.AttackPhase)
                currentPhase = GamePhase.ReallocatePhase;
            else
            if (currentPhase == GamePhase.ReallocatePhase)
                currentPhase = GamePhase.AddArmyPhase;
        }
        public void addArmyPhaseOperations()
        {
            try
            {
                if(firstCounter > 0)
                     buttons[3].setButtonTexture(Game.Content.Load<Texture2D>("endTurnButton"));
                else
                     buttons[3].setButtonTexture(Game.Content.Load<Texture2D>("nextPhaseButton"));
                mouseStateCurrent = Mouse.GetState();
                foreach (Token tok in tokens)
                {
                    if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
                    {
                        if (tok.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && tok.getColor().Equals(Global.getColor(turnPlayer.getCor())) && !drawCards && !drawGuide)
                        {
                            addToken.setTokenPosition(new Vector2(tok.getTokenPosition().X, tok.getTokenPosition().Y - 25));
                            addToken.setTerritorio(tok.getTerritorio());
                        }
                    }
                }
                if (addToken.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawCards && !drawGuide)
                {
                    if (turnPlayer.getNumExercitoParacolocar() > 0)
                    {
                        addToken.getTerritorio().setNumeroExercitos(addToken.getTerritorio().getNumeroExercito() + 1);
                        turnPlayer.removeExercitoParacolocar();
                    }
                }
            }
            catch(Exception e)
            {

            }
        }
        public void AttackPhaseOperations()
        {
        }
        public void ReallocatePhaseOperations()
        {
        }
        public void verifyReadiness()
        {
        }
      

    }


}
