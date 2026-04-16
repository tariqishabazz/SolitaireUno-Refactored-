namespace SolitaireUno
{
    public class MainGame
    {
        Player player = new();
        Computer computer = new();
        Deck gameDeck = new();
        
        private readonly IInputProvider _input;
        private readonly IOutputProvider _output;
        private readonly PlayerTurnHandler _playerTurnHandler;
        private readonly ComputerTurnHandler _computerTurnHandler;
        
        internal static GameMode GameModeChoice { get; set; }
        internal static bool SuitEnforcement { get; set; }

        public MainGame(IInputProvider input, IOutputProvider output, Deck deck, GameMode gameModeChoice, bool suitEnforcement)
        {
            _input = input;
            _output = output;
            gameDeck = deck;
            GameModeChoice = gameModeChoice;
            SuitEnforcement = suitEnforcement;

            _playerTurnHandler = new(player, gameDeck, _input, _output);
            _computerTurnHandler = new(computer, gameDeck, _output);

            InitialGameSetup.SetupGame(player, computer, gameDeck);
        }

        public void StartGame()
        {
            RegularCard penaltyCard = new(Suits.Spades, Values.Queen);
            Card currentCard = gameDeck.DealCard()!;

            bool isPlayerTurn = true;

            while (player.Hand.Count > 0 && computer.Hand.Count > 0)
            {
                if (isPlayerTurn)
                    ShowHand();

                if (isPlayerTurn)
                {
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
                                _output.WriteLine($"\nThe game mode is now {GameModeChoice}");
                                break;

                            case ActionInstruction.SkipTurn:
                                _output.WriteLine($"\nThe computer has been skipped!");
                                break;

                            case ActionInstruction.DrawFour:
                                for (int i = 0; i < 4; i++)
                                {
                                    Card drawnCard = gameDeck.DealCard()!;
                                    computer.PickupCard(drawnCard);
                                }

                                _output.WriteLine($"\nThe computer just picked up 4 cards.");
                                break;

                            case ActionInstruction.DrawTwo:
                                for (int i = 0; i < 2; i++)
                                {
                                    Card drawnCard = gameDeck.DealCard()!;
                                    computer.PickupCard(drawnCard);
                                }

                                _output.WriteLine("\nThe computer just picked up 2 cards");
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
                                _output.WriteLine("\n---------------------------------------------------------------------");
                                _output.WriteLine($"\nThe game mode is now {GameModeChoice}");
                                break;

                            case ActionInstruction.SkipTurn:
                                _output.WriteLine("\n---------------------------------------------------------------------");
                                _output.WriteLine($"\nYou have been skipped!");
                                break;

                            case ActionInstruction.DrawFour:
                                for (int i = 0; i < 4; i++)
                                {
                                    Card drawnCard = gameDeck.DealCard()!;
                                    player.PickupCard(drawnCard);
                                }

                                _output.WriteLine("\n---------------------------------------------------------------------");
                                _output.WriteLine($"\nYou had to pick up 4 cards");
                                break;

                            case ActionInstruction.DrawTwo:
                                for (int i = 0; i < 2; i++)
                                {
                                    Card drawnCard = gameDeck.DealCard()!;
                                    player.PickupCard(drawnCard);
                                }

                                _output.WriteLine("\n---------------------------------------------------------------------");
                                _output.WriteLine($"\nYou had to pick up 2 cards");
                                break;
                        };
                    }
                    else
                    {
                        isPlayerTurn = true;
                    }
                }
            }
            GameOverStats();
        }

        static void Main()
        {
            GameIntroduction.ShowGameIntroduction();
        }

        public void ShowHand()
        {
            _output.WriteLine("\n---------------------------------------------------------------------");
            _output.WriteLine("Your Hand: ");

            int index = 0;
            foreach (Card card in player.Hand)
            {
                _output.WriteLine($"   {index + 1}) {card}");
                index++;
            }

            _output.WriteLine($"\nYou now have {player.Hand.Count} cards");
            _output.WriteLine($"The Computer now has {computer.Hand.Count} cards");
        }

        public Player? GameOverStats()
        {
            _output.WriteLine("\n\n\n-------------------------------------------------------------");
            _output.WriteLine("Game Over!");

            if (computer.Hand.Count == 0)
            {
                _output.WriteLine("\nYou Lose! You've been bested by the machine :(");
                return computer;
            }
            else if (player.Hand.Count == 0)
            {
                _output.WriteLine("\nYou Win! You beat the computer! Congrats! :)");
                return player;
            }

            return null;
        }
    }
}
