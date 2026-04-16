namespace SolitaireUno
{
    public class MainGame
    {
        public static Player player = new();
        public static Computer computer = new();
        public Deck gameDeck = new();
        
        internal static IInputProvider Input { get; set; }
        internal static IOutputProvider Output { get; set; }
        
        internal PlayerTurnHandler _playerTurnHandler;
        internal ComputerTurnHandler _computerTurnHandler;
        
        internal static GameMode GameModeChoice { get; set; }
        internal static bool SuitEnforcement { get; set; }

        public MainGame(IInputProvider input, IOutputProvider output, Deck deck, GameMode gameModeChoice, bool suitEnforcement)
        {
            Input = input;
            Output = output;
            gameDeck = deck;
            GameModeChoice = gameModeChoice;
            SuitEnforcement = suitEnforcement;

            _playerTurnHandler = new(player, gameDeck, Input, Output);
            _computerTurnHandler = new(computer, gameDeck, Output);

            InitialGameSetup.SetupGame(player, computer, gameDeck);
        }

        public void StartGame()
        {
            bool isPlayerTurn = true;

            RegularCard penaltyCard = new(Suits.Spades, Values.Queen);


            Card currentCard = gameDeck.DealCard()!;

            if (currentCard is not null)
            {
                List<Card> temporarySpecialCards = [];

                while (currentCard is SpecialCard)
                {
                    temporarySpecialCards.Add(currentCard);

                    if (gameDeck.Length() > 0)
                    {
                        currentCard = gameDeck.DealCard()!;
                    }
                    gameDeck.AddRange(temporarySpecialCards);
                    gameDeck.InHouseShuffle();

                }
            }

            while (player.Hand.Count > 0 && computer.Hand.Count > 0)
            {
                if (isPlayerTurn)
                {
                    GameMethods.ShowHand();

                    Card? cardPlayed = _playerTurnHandler.HandleTurn(ref currentCard, penaltyCard);
                    if (cardPlayed != null)
                    {
                        ActionInstruction message = GameMethods.SpecialCardAction(cardPlayed);
                        switch (message)
                        {
                            case ActionInstruction.DoNothing:
                                isPlayerTurn = false;
                                break;

                            case ActionInstruction.ChangeOrder:
                                GameModeChoice = GameModeChoice == GameMode.Ascending ? GameMode.Descending : GameMode.Ascending;
                                Output.WriteLine($"\nThe game mode is now {GameModeChoice}");
                                
                                isPlayerTurn = false;
                                break;

                            case ActionInstruction.SkipTurn:
                                Output.WriteLine($"\nThe computer has been skipped!");
                                break;

                            case ActionInstruction.DrawFour:
                                for (int i = 0; i < 4; i++)
                                {
                                    Card? drawnCard = gameDeck.DealCard();
                                    if (drawnCard is not null)
                                    {
                                        computer.PickupCard(drawnCard);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                Output.WriteLine($"\nThe computer just picked up 4 cards.");
                                break;

                            case ActionInstruction.DrawTwo:
                                for (int i = 0; i < 2; i++)
                                {
                                    Card? drawnCard = gameDeck.DealCard();
                                    if (drawnCard is not null)
                                    {
                                        computer.PickupCard(drawnCard);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                Output.WriteLine("\nThe computer just picked up 2 cards");
                                break;
                        };
                    }
                    else
                    {
                        isPlayerTurn = false;
                    }
                }
                else
                {
                    Card? cardPlayed = _computerTurnHandler.HandleTurn(ref currentCard, penaltyCard, player.Hand.Count);
                    if (cardPlayed != null)
                    {
                        ActionInstruction message = GameMethods.SpecialCardAction(cardPlayed);
                        switch (message)
                        {
                            case ActionInstruction.DoNothing:
                                isPlayerTurn = true;
                                break;

                            case ActionInstruction.ChangeOrder:
                                GameModeChoice = GameModeChoice == GameMode.Ascending ? GameMode.Descending : GameMode.Ascending;
                                Output.WriteLine("\n---------------------------------------------------------------------");
                                Output.WriteLine($"\nThe game mode is now {GameModeChoice}");
                                
                                isPlayerTurn = true;
                                break;

                            case ActionInstruction.SkipTurn:
                                Output.WriteLine("\n---------------------------------------------------------------------");
                                Output.WriteLine($"\nYou have been skipped!");
                                break;

                            case ActionInstruction.DrawFour:
                                for (int i = 0; i < 4; i++)
                                {
                                    Card? drawnCard = gameDeck.DealCard();
                                    if (drawnCard is not null)
                                    {
                                        player.PickupCard(drawnCard);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                Output.WriteLine("\n---------------------------------------------------------------------");
                                Output.WriteLine($"\nYou had to pick up 4 cards");
                                break;

                            case ActionInstruction.DrawTwo:
                                for (int i = 0; i < 2; i++)
                                {
                                    Card? drawnCard = gameDeck.DealCard();
                                    if (drawnCard is not null)
                                    {
                                        player.PickupCard(drawnCard);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                Output.WriteLine("\n---------------------------------------------------------------------");
                                Output.WriteLine($"\nYou had to pick up 2 cards");
                                break;
                        };
                    }
                    else
                    {
                        isPlayerTurn = true;
                    }
                }
            }
            GameMethods.GameOverStats();
        }

        static void Main()
        {
            GameIntroduction.ShowGameIntroduction();
        }

    }
}
