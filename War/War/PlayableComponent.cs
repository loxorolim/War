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
using System.Timers;


namespace War
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PlayableComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Timer phaseLogoTimer = new Timer();

        

        Texture2D warMap;
        Texture2D armyToPass;
        Texture2D mapGuide;
        Texture2D cardsBackground;
        Texture2D phaseLogo;
       
        SpriteBatch mapBatch;
        SpriteBatch tokenBatch;
        SpriteBatch tokenBatch2;
        SpriteBatch buttonBatch;
        SpriteBatch logoBatch;
        SpriteBatch cardsBatch;
        SpriteFont font;
        MouseState mouseStateCurrent, mouseStatePrevious;
        Territorio atacante;
        Territorio defensor;
        Territorio origem;
        Territorio destino;
        GamePhase currentPhase = GamePhase.AddArmyPhase;
        int numArmyToPass = 0;
        int numArmyReallocate = 0;
     //   Boolean firstPhase = true;
        public static int firstCounter { get; set;} 
     //   Boolean addArmyPhase = false;
     //   Boolean attackPhase = false;
     //   Boolean reallocatePhase = false;
        Boolean askArmyPass = false;
        Boolean askArmyReallocation = false;
        Boolean drawGuide = false;
        Boolean drawObj = false;
        Boolean drawCards = false;
        Boolean drawDice = false;
        Boolean firstTime = false;
        Boolean reallocationSelected = false;
        public static Boolean playersSelected { get; set; }
        public static Boolean gameBegin { get; set; }
        Boolean showAddButton = false;
        Token addToken;
        Token minusToken;
        Token okToken;
        Boolean okButtonPressed = true;
        Jogador turnPlayer;
        CartaObjetivo[] objCards;
        List<CartaTerritorio> territCards;
        List<Button> buttons;
        List<Button> cardButtons;
        List<Token> tokens;
        int[] tokenFrames;
        Boolean[]  readinessArray;
        Boolean drawLogo = false;
        List<Button> dadosAtk;
        List<Button> dadosDef;

        public PlayableComponent(Game game)
            : base(game)
        {
            dadosAtk = new List<Button>();
            dadosDef = new List<Button>();
            buttons = new List<Button>();
            cardButtons = new List<Button>();
            tokens = new List<Token>();
            tokenFrames = new int[42];
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
            //Dados
            
            //atk
            dadosAtk.Add(new Button(400, 300, 6));
            dadosAtk.Add(new Button(400, 350, 6));
            dadosAtk.Add(new Button(400, 400, 6));
            //def
            dadosDef.Add(new Button(450, 300, 6));
            dadosDef.Add(new Button(450, 350, 6));
            dadosDef.Add(new Button(450, 400, 6));
            //Botoes pegar carta, atacar, realocar, finalizar
            playersSelected = false;
            gameBegin = false;
            buttons.Add(new Button(10, 498, 2));
            buttons.Add(new Button(75, 495, 2));
            buttons.Add(new Button(75, 545, 2));
            buttons.Add(new Button(175, 545, 2));
            buttons.Add(new Button(751, 12, 2));

        

            cardButtons.Add(new Button(124, 350, 2));
            cardButtons.Add(new Button(598, 350, 2));
            //cartas
            cardButtons.Add(new Button(124, 140, 2));
            cardButtons.Add(new Button(238, 140, 2));
            cardButtons.Add(new Button(352, 140, 2));
            cardButtons.Add(new Button(466, 140, 2));
            cardButtons.Add(new Button(580, 140, 2));
            addToken = new Token(-30, -30, 1, null);
            minusToken = new Token(-30, -30, 1, null);
            okToken = new Token(-30, -30, 1, null);
            
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
            if (!drawLogo)
            {

                turnPlayer = Tabuleiro.jogadorDaVez;
                if (currentPhase.Equals(GamePhase.AddArmyPhase))
                {
                    addArmyPhaseOperations();
                }
                if (currentPhase.Equals(GamePhase.AttackPhase))
                {
                    attackPhaseOperations();
                }
                if (currentPhase.Equals(GamePhase.ReallocatePhase))
                {
                    reallocationPhaseOperations();
                }

                checkButtonsClick();
            }
            if (gameBegin)
            {
                phaseLogo = Game.Content.Load<Texture2D>("firstPhaseLogo");

                phaseLogoTimer.Interval = (1000) * 2;
                phaseLogoTimer.Enabled = true;
                phaseLogoTimer.Elapsed += setDrawLogoFalse;
                phaseLogoTimer.Start();
                drawLogo = true;
                gameBegin = false;
            }
  
            
            




            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            mapBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix); ;
            buttonBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix); 
            tokenBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix);
            tokenBatch2.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.ScalingMatrix); ;
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
                //int i = 2;
                //foreach (CartaTerritorio carta in Tabuleiro.jogadorDaVez.getCartasJogador())
                //{
                //    cardButtons[i].setButtonTexture(carta.getTerritCardTexture());
                //    cardsBatch.Draw(cardButtons[i].getButtonTexture(), cardButtons[i].getButtonPosition(), cardButtons[i].getCurrentFrame(), Color.White, 0, Vector2.Zero, 0.35f, SpriteEffects.None, 1);
                //    i++;
                //}
                
                //Desenhando cartas aleatórias por enquanto
                
                cardsBatch.Draw(cardButtons[2].getButtonTexture(), cardButtons[2].getButtonPosition(), cardButtons[2].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);

                cardsBatch.Draw(cardButtons[3].getButtonTexture(), cardButtons[3].getButtonPosition(), cardButtons[3].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);

                cardsBatch.Draw(cardButtons[4].getButtonTexture(), cardButtons[4].getButtonPosition(), cardButtons[4].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);

                cardsBatch.Draw(cardButtons[5].getButtonTexture(), cardButtons[5].getButtonPosition(), cardButtons[5].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);

                cardsBatch.Draw(cardButtons[6].getButtonTexture(), cardButtons[6].getButtonPosition(), cardButtons[6].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);

                //Desenhando botões de troca de cartas e de visualizar objetivo
                buttonBatch.Draw(cardButtons[0].getButtonTexture(), cardButtons[0].getButtonPosition(), cardButtons[0].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                buttonBatch.Draw(cardButtons[1].getButtonTexture(), cardButtons[1].getButtonPosition(), cardButtons[1].getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            if (drawLogo )
            {            
                
                logoBatch.Draw(phaseLogo, new Vector2(Global.WIDTH/2 - phaseLogo.Width/2, Global.HEIGHT/2-phaseLogo.Height), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            }
            if (drawDice)
            {
                foreach (Button b in dadosAtk)
                {
                    logoBatch.Draw(b.getButtonTexture(), b.getButtonPosition(), b.getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

                }
                foreach (Button b in dadosDef)
                {
                    logoBatch.Draw(b.getButtonTexture(), b.getButtonPosition(), b.getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

                }

            }
            if (drawObj)
            {
                CartaObjetivo obj = turnPlayer.getObjetivo();
                cardsBatch.Draw(obj.getObjCardTexture(), new Vector2((800 / 2) - (obj.getObjCardTexture().Width * 0.6f / 2), (600 / 2) - (obj.getObjCardTexture().Height * 0.6f / 2)), null, Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0);
            }
            if (askArmyPass)
            {
               // logoBatch.Draw(armyToPass, new Vector2(50, 40), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        
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
               // if (currentPhase.Equals(GamePhase.AddArmyPhase))
               // {
                    tokenBatch2.Draw(addToken.getTokenTexture(), addToken.getTokenPosition(), addToken.getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                    tokenBatch2.Draw(minusToken.getTokenTexture(), minusToken.getTokenPosition(), minusToken.getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                    tokenBatch2.Draw(okToken.getTokenTexture(), okToken.getTokenPosition(), okToken.getCurrentFrame(), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
               // }
                Vector2 fontPosition = tokens[i].getTokenPosition();
                fontPosition.X += 30;
                Vector2 fontPosition2 = tokens[i].getTokenPosition();
                fontPosition2.X += 32;
                fontPosition2.Y += 20;
                tokenBatch.DrawString(font, string.Format(tokens[i].getNumberOfSoldiers().ToString()), fontPosition, tokens[i].getColor());
                if(tokens[i].getTerritorio().getDono().Equals(turnPlayer) && (currentPhase == GamePhase.AddArmyPhase || currentPhase == GamePhase.ReallocatePhase))
                     tokenBatch.DrawString(font, string.Format(tokens[i].getTerritorio().getNumeroExercitoRemanejavel().ToString()), fontPosition2, Color.Purple);


            }
            if (playersSelected)
            {
                String playerColor = Global.getColorName(turnPlayer.getCor());
                Vector2 posPlayerColor =new Vector2((Global.WIDTH / 2 - font.MeasureString(playerColor).X/2), -5);
                tokenBatch.DrawString(font, string.Format(playerColor),posPlayerColor, Global.getColor(turnPlayer.getCor()));
                tokenBatch.DrawString(font, string.Format("Player"), new Vector2(posPlayerColor.X + font.MeasureString(playerColor).X + 5, -5), Color.Black);
                tokenBatch.DrawString(font, string.Format("Army Income:" + turnPlayer.getNumExercitoParacolocar() ), new Vector2(0, -5), Color.Black);
            }

            
            mapBatch.End();
            tokenBatch.End();
            tokenBatch2.End();
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
            tokenBatch2 = new SpriteBatch(Game.GraphicsDevice);
            logoBatch = new SpriteBatch(Game.GraphicsDevice);
            cardsBatch = new SpriteBatch(Game.GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("font");
            phaseLogo= Game.Content.Load<Texture2D>("incomePhaseLogo");
            warMap = Game.Content.Load<Texture2D>("WarMapNewWindow");
            armyToPass = Game.Content.Load<Texture2D>("numExercitosPassar");
            mapGuide = Game.Content.Load<Texture2D>("mapGuide");
            cardsBackground = Game.Content.Load<Texture2D>("cardsBackground");
            buttons[0].setButtonTexture(Game.Content.Load<Texture2D>("cardsButton"));
            buttons[1].setButtonTexture(Game.Content.Load<Texture2D>("attackButton"));
            buttons[2].setButtonTexture(Game.Content.Load<Texture2D>("realocateButton"));
            buttons[3].setButtonTexture(Game.Content.Load<Texture2D>("endTurnButton"));
            buttons[4].setButtonTexture(Game.Content.Load<Texture2D>("mapGuideButton"));
            cardButtons[0].setButtonTexture(Game.Content.Load<Texture2D>("tradeButton"));
            cardButtons[1].setButtonTexture(Game.Content.Load<Texture2D>("objectiveButton"));
            cardButtons[2].setButtonTexture(MaquinaDeRegras.cartas[5].getTerritCardTexture());
            cardButtons[3].setButtonTexture(MaquinaDeRegras.cartas[16].getTerritCardTexture());
            cardButtons[4].setButtonTexture(MaquinaDeRegras.cartas[20].getTerritCardTexture());
            cardButtons[5].setButtonTexture(MaquinaDeRegras.cartas[2].getTerritCardTexture());
            cardButtons[6].setButtonTexture(MaquinaDeRegras.cartas[40].getTerritCardTexture());
            dadosAtk[0].setButtonTexture(Game.Content.Load<Texture2D>("Dados/dados-vermelhos"));
            dadosAtk[1].setButtonTexture(Game.Content.Load<Texture2D>("Dados/dados-vermelhos"));
            dadosAtk[2].setButtonTexture(Game.Content.Load<Texture2D>("Dados/dados-vermelhos"));
            dadosDef[0].setButtonTexture(Game.Content.Load<Texture2D>("Dados/dados-amarelo"));
            dadosDef[1].setButtonTexture(Game.Content.Load<Texture2D>("Dados/dados-amarelo"));
            dadosDef[2].setButtonTexture(Game.Content.Load<Texture2D>("Dados/dados-amarelo"));
            
            addToken.setTokenTexture(Game.Content.Load<Texture2D>("addButton"));
            minusToken.setTokenTexture(Game.Content.Load<Texture2D>("minusButton"));
            okToken.setTokenTexture(Game.Content.Load<Texture2D>("okButton"));

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
                        tokens.Add(new Token(territorios[j].getPosX(), territorios[j].getPosY(), 4, Global.getColor(jogadores[i].getCor()), territorios[j].getNumeroExercito(), territorios[j]));


                    }
                }
                for (int i = 0; i < tokens.Count; i++)
                {
                    tokens[i].setTokenTexture(Game.Content.Load<Texture2D>("peon"));
                    tokens[i].setFrame(tokenFrames[i]);
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
                cardButtons[2].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                cardButtons[3].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                cardButtons[4].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                cardButtons[5].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
                cardButtons[6].changeCurrentFrame(mouseStateCurrent.X, mouseStateCurrent.Y);
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
                    addToken.setTokenPosition(new Vector2(-30, -30));
                    minusToken.setTokenPosition(new Vector2(-30, -30));
                    okToken.setTokenPosition(new Vector2(-30, -30));
                    if (firstCounter > 0)
                    {
                        if (turnPlayer.getNumExercitoParacolocar() == 0)
                        {
                            foreach (Territorio t in Tabuleiro.mapa)
                            {
                                t.atribuirExercitosPendentes();
                            }

                            if (firstCounter > 1)
                            {
                                phaseLogo = Game.Content.Load<Texture2D>("firstPhaseLogo");

                                phaseLogoTimer.Interval = (1000) * 2;
                                phaseLogoTimer.Enabled = true;
                                phaseLogoTimer.Elapsed += setDrawLogoFalse;
                                phaseLogoTimer.Start();
                                drawLogo = true;
                            }
                            if (firstCounter == 1)
                            {
                                phaseLogo = Game.Content.Load<Texture2D>("incomePhaseLogo");
                                phaseLogoTimer.Interval = (1000) * 2;
                                phaseLogoTimer.Enabled = true;
                                phaseLogoTimer.Elapsed += setDrawLogoFalse;
                                phaseLogoTimer.Start();
                                drawLogo = true;
                            }

                            MaquinaDeRegras.passaVez();
                            firstCounter--;
                        }
                    }
                    else
                    {
                        if (currentPhase.Equals(GamePhase.ReallocatePhase))
                        {
                            changeToNextPhase();
                            
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
            zerarVetor(tokenFrames);

            if (currentPhase == GamePhase.AddArmyPhase)
            {
                if (turnPlayer.getNumExercitoParacolocar() == 0)
                {
                    foreach (Territorio t in Tabuleiro.mapa)
                    {
                        t.atribuirExercitosPendentes();
                    }
                    addToken.setTokenPosition(new Vector2(-30, -30));
                    minusToken.setTokenPosition(new Vector2(-30, -30));
                    okToken.setTokenPosition(new Vector2(-30, -30));
                    currentPhase = GamePhase.AttackPhase;
                    phaseLogo = Game.Content.Load<Texture2D>("attackPhaseLogo");
                    phaseLogoTimer.Interval = (1000) * 2;
                    phaseLogoTimer.Enabled = true;
                    phaseLogoTimer.Elapsed += setDrawLogoFalse;
                    phaseLogoTimer.Start();
                    drawLogo = true;
                }
            }
            else
            {
                if (currentPhase == GamePhase.AttackPhase)
                {
                    addToken.setTokenPosition(new Vector2(-30, -30));
                    minusToken.setTokenPosition(new Vector2(-30, -30));
                    okToken.setTokenPosition(new Vector2(-30, -30));
                    okButtonPressed = true;
                    askArmyPass = false;
                    phaseLogo = Game.Content.Load<Texture2D>("reallocationPhaseLogo");
                    currentPhase = GamePhase.ReallocatePhase;
                    phaseLogoTimer.Interval = (1000) * 2;
                    phaseLogoTimer.Enabled = true;
                    phaseLogoTimer.Elapsed += setDrawLogoFalse;
                    phaseLogoTimer.Start();
                    drawLogo = true;
                }
                else
                {
                    if (currentPhase == GamePhase.ReallocatePhase)
                    {
                        foreach (Territorio t in Tabuleiro.mapa)
                        {
                            t.atribuirExercitosPendentes();
                        }
                        addToken.setTokenPosition(new Vector2(-30, -30));
                        minusToken.setTokenPosition(new Vector2(-30, -30));
                        okToken.setTokenPosition(new Vector2(-30, -30));
                        okButtonPressed = true;
                        askArmyReallocation = false;
                        phaseLogo = Game.Content.Load<Texture2D>("incomePhaseLogo");
                        currentPhase = GamePhase.AddArmyPhase;
                        phaseLogoTimer.Interval = (1000) * 2;
                        phaseLogoTimer.Enabled = true;
                        phaseLogoTimer.Elapsed += setDrawLogoFalse;
                        phaseLogoTimer.Start();
                        drawLogo = true;
                        
                    }
                }
            }
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
                            addToken.setTokenPosition(new Vector2(tok.getTokenPosition().X -15, tok.getTokenPosition().Y - 25));
                            addToken.setTerritorio(tok.getTerritorio());
                           
                            minusToken.setTokenPosition(new Vector2(tok.getTokenPosition().X + 15, tok.getTokenPosition().Y - 25));
                            minusToken.setTerritorio(tok.getTerritorio());
                            okToken.setTokenPosition(new Vector2(tok.getTokenPosition().X + 45, tok.getTokenPosition().Y -25));
                            okToken.setTerritorio(tok.getTerritorio());
                        }
                    }
                }
                if (addToken.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawCards && !drawGuide)
                {
                    if (turnPlayer.getNumExercitoParacolocar() > 0)
                    {
                       // addToken.getTerritorio().setNumeroExercitos(addToken.getTerritorio().getNumeroExercito() + 1);
                        addToken.getTerritorio().setNumeroExercitosRemanejavel(addToken.getTerritorio().getNumeroExercitoRemanejavel() + 1);
                        turnPlayer.removeExercitoParacolocar();
                    }
                }
                if (minusToken.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawCards && !drawGuide)
                {
                    if (minusToken.getTerritorio().getNumeroExercitoRemanejavel() > 0)
                    {
                       // addToken.getTerritorio().setNumeroExercitos(addToken.getTerritorio().getNumeroExercito()-1);
                        minusToken.getTerritorio().setNumeroExercitosRemanejavel(minusToken.getTerritorio().getNumeroExercitoRemanejavel() - 1);
                        turnPlayer.addExercitosParaColocar(1);
                        
                    }
                }
                if (okToken.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawCards && !drawGuide)
                {
                    addToken.setTokenPosition(new Vector2(-30, -30));
                    minusToken.setTokenPosition(new Vector2(-30, -30));
                    okToken.setTokenPosition(new Vector2(-30, -30));
                               
                }
                
            }
            catch(Exception e)
            {

            }
        }
        public void attackPhaseOperations()
        {
            try
            {
                
                mouseStateCurrent = Mouse.GetState();
                if (okButtonPressed)
                {
                    foreach (Token tok in tokens)
                    {
                        if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
                        {
                            if (tok.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && tok.getColor().Equals(Global.getColor(turnPlayer.getCor())) && !drawCards && !drawGuide)
                            {
                                zerarVetor(tokenFrames);
                                if (tok.getTerritorio().getNumeroExercito() > 1)
                                    changeTokenAttackFrames(tok);

                            }
                            if (tok.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && tokenFrames[verifyTokenFrameLocation(tok.getTerritorio())] == 2 && !drawCards && !drawGuide)
                            {
                                atacante = getAttackingTerritory();
                                defensor = tok.getTerritorio();
                                Batalha battle = new Batalha(turnPlayer, defensor.getDono(), atacante, defensor);
                                battle.iniciar();
                                

                                if (battle.getNumExercitosParaPassar() > 0)
                                {
                                    askArmyPass = true;
                                    numArmyToPass = battle.getNumExercitosParaPassar();
                                    okButtonPressed = false;


                                }
                                
                                setDados(battle.getDadosAt(),battle.getDadosDef());


                                phaseLogoTimer.Interval = (1000) * 2;
                                phaseLogoTimer.Enabled = true;
                                phaseLogoTimer.Elapsed += setDrawDiceFalse;
                                phaseLogoTimer.Start();
                                drawDice = true;
                                

                          
                                zerarVetor(tokenFrames);
                            }
                        }

                    }
                }
                if (askArmyPass)
                {
                    addToken.setTokenPosition(new Vector2(defensor.getPosX() - 15, defensor.getPosY() - 25));
                    addToken.setTerritorio(defensor);
                    minusToken.setTokenPosition(new Vector2(defensor.getPosX() + 15, defensor.getPosY() - 25));
                    minusToken.setTerritorio(defensor);
                    okToken.setTokenPosition(new Vector2(defensor.getPosX() + 45, defensor.getPosY() - 25));
                    okToken.setTerritorio(defensor);

                    if (addToken.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawCards && !drawGuide)
                    {
                        if (numArmyToPass > 0)
                        {
                            addToken.getTerritorio().setNumeroExercitos(addToken.getTerritorio().getNumeroExercito() + 1);
                            numArmyToPass--;
                            atacante.setNumeroExercitos(atacante.getNumeroExercito() - 1);
                        }
                    }
                    if (minusToken.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawCards && !drawGuide)
                    {
                        if (minusToken.getTerritorio().getNumeroExercito() > 1)
                        {
                            addToken.getTerritorio().setNumeroExercitos(addToken.getTerritorio().getNumeroExercito() - 1);
                            atacante.setNumeroExercitos(atacante.getNumeroExercito() + 1);
                            numArmyToPass++;
                        }
                    }
                    if (okToken.getTerritorio().getNumeroExercito() > 0 && okToken.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawCards && !drawGuide)
                    {
                        addToken.setTokenPosition(new Vector2(-30, -30));
                        minusToken.setTokenPosition(new Vector2(-30, -30));
                        okToken.setTokenPosition(new Vector2(-30, -30));
                        okButtonPressed = true;
                        askArmyPass = false;

                        if (MaquinaDeRegras.verificaVitoria())
                        {
                            //mostra tela de vitoria do jogador atual
                        }
                        Jogador defesa = defensor.getDono();
                        if (defesa.getTerritorios().Count == 0)
                            defesa.setJogadorMorto();

                    }
                   
                }
            }
            catch (Exception e)
            {

            }
        }
        public void changeTokenAttackFrames(Object o)
        {
            Token tok = (Token)o;
            List<Territorio> enemies = tok.getTerritorio().getListaVizinhosInimigos();
            tokenFrames[verifyTokenFrameLocation(tok.getTerritorio())] = 1 ;
            foreach (Token t in tokens)
            {
                foreach (Territorio ter in enemies)
                {
                    if (t.getTerritorio().Equals(ter))
                    {
                        tokenFrames[verifyTokenFrameLocation(t.getTerritorio())] = 2;
                        
                    }
                }
            }

        }
        public void changeTokenReallocateFrames(Object o)
        {
            Token tok = (Token)o;
            List<Territorio> enemies = tok.getTerritorio().getListaVizinhosAmigos();
            tokenFrames[verifyTokenFrameLocation(tok.getTerritorio())] = 3;
            foreach (Token t in tokens)
            {
                foreach (Territorio ter in enemies)
                {
                    if (t.getTerritorio().Equals(ter))
                    {
                        tokenFrames[verifyTokenFrameLocation(t.getTerritorio())] = 3;

                    }
                }
            }

        }
        public void reallocationPhaseOperations()
        {
            try
            {

                mouseStateCurrent = Mouse.GetState();
                if (okButtonPressed)
                {
                    foreach (Token tok in tokens)
                    {
                        if (mouseStateCurrent.LeftButton == ButtonState.Pressed  && mouseStatePrevious.LeftButton == ButtonState.Released)
                        {
                            if (tok.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && !reallocationSelected && tok.getColor().Equals(Global.getColor(turnPlayer.getCor())) && !drawCards && !drawGuide)
                            {
                                zerarVetor(tokenFrames);
                                if (tok.getTerritorio().getNumeroExercito() > 1)
                                {
                                    origem = tok.getTerritorio();
                                    changeTokenReallocateFrames(tok);
                                    reallocationSelected = true;
                                }
                                
                            }
                            else 
                            if (tok.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && !tok.getTerritorio().Equals(origem) && tokenFrames[verifyTokenFrameLocation(tok.getTerritorio())] == 3 && !drawCards && !drawGuide)
                            {
                                destino = tok.getTerritorio();
                                askArmyReallocation = true;
                                okButtonPressed = false;
                                reallocationSelected = false;

                            }
                        }

                    }
                }
                if (askArmyReallocation)
                {
                    addToken.setTokenPosition(new Vector2(destino.getPosX() - 15, destino.getPosY() - 25));
                    addToken.setTerritorio(destino);
                    minusToken.setTokenPosition(new Vector2(destino.getPosX() + 15, destino.getPosY() - 25));
                    minusToken.setTerritorio(destino);
                    okToken.setTokenPosition(new Vector2(destino.getPosX() + 45, destino.getPosY() - 25));
                    okToken.setTerritorio(destino);

                    if (addToken.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawCards && !drawGuide)
                    {

                        if (origem.getNumeroExercito() > 1)
                        {
                            addToken.getTerritorio().setNumeroExercitosRemanejavel(addToken.getTerritorio().getNumeroExercitoRemanejavel() + 1);
                            origem.diminuirNumeroDeExercito(1);
                        }
                           // atacante.setNumeroExercitos(atacante.getNumeroExercito() - 1);
                        
                    }
                    if (minusToken.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawCards && !drawGuide)
                    {
                        if (destino.getNumeroExercitoRemanejavel() > 0)
                        {
                            destino.setNumeroExercitosRemanejavel(destino.getNumeroExercitoRemanejavel() - 1);
                            origem.setNumeroExercitos(origem.getNumeroExercito() + 1);
                          //  atacante.setNumeroExercitos(atacante.getNumeroExercito() + 1);
                          //  numArmyReallocate++;
                        }
                    }
                    if (okToken.isCollided(mouseStateCurrent.X, mouseStateCurrent.Y) && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && !drawCards && !drawGuide)
                    {
                        addToken.setTokenPosition(new Vector2(-30, -30));
                        minusToken.setTokenPosition(new Vector2(-30, -30));
                        okToken.setTokenPosition(new Vector2(-30, -30));
                        okButtonPressed = true;
                        zerarVetor(tokenFrames);
                        askArmyReallocation = false;
                    }

                }
            }
            catch (Exception e)
            {

            }
        }
        public void verifyReadiness()
        {
        }
        public int verifyTokenFrameLocation(Territorio ter)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                if(tokens[i].getTerritorio().Equals(ter))
                    return i;
            }
            return 0;
        }
        public void zerarVetor(int[] v)
        {
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = 0;
            }
        }
        public Territorio getAttackingTerritory()
        {
            int aux = 0;
            for (int i = 0; i < tokenFrames.Length; i++)
            {
                if (tokenFrames[i] == 1)
                    aux = i;
            }
            return tokens[aux].getTerritorio();
        }
        public void passArmy(int n)
        {

        }

        private void setDrawLogoFalse(object source, ElapsedEventArgs e)
        {
            ((Timer)(source)).Enabled = false;
            drawLogo = false;
        }
        private void setDrawDiceFalse(object source, ElapsedEventArgs e)
        {
            ((Timer)(source)).Enabled = false;
            drawDice = false;
        }
        public void setDados(int[] dadosA,int[] dadosD)
        {
            dadosAtk.Clear();
            dadosDef.Clear();
            int aux = 0;
            int aux2 = 0;
            for (int i = 0; i < dadosA.Length; i++)
            {
                dadosAtk.Add(new Button(375, 200+ aux, 6));
                dadosAtk[i].setButtonTexture(Game.Content.Load<Texture2D>("Dados/dados-vermelhos"));
                dadosAtk[i].setFrame(dadosA[i]-1);
                aux += 70;
            }
            for (int i = 0; i < dadosD.Length; i++)
            {
                dadosDef.Add(new Button(450, 200 + aux2,6));
                dadosDef[i].setButtonTexture(Game.Content.Load<Texture2D>("Dados/dados-amarelo"));
                dadosDef[i].setFrame(dadosD[i] - 1);
                aux2 += 70;
            }
            
        }
   
      

    }


}
