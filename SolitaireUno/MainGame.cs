namespace SolitaireUno
{
    public class MainGame
    {
        public static Player player = new();
        public static Computer computer = new();
        public static Deck gameDeck = new();

        internal static IInputProvider Input { get; set; }
        internal static IOutputProvider Output { get; set; }

        internal PlayerTurnHandler _playerTurnHandler;
        internal ComputerTurnHandler _computerTurnHandler;

        internal static GameMode GameModeChoice { get; set; }
        internal static bool SuitEnforcement { get; set; }

        public static Card? LastPlayedCard { get; private set; }
        public static bool IsPlayerTurn { get; set; }

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
            RegularCard penaltyCard = new(Suits.Spades, Values.Queen);
            Card currentCard = gameDeck.DealCard()!;

            if (currentCard is not null)
            {
                List<Card> temporarySpecialCards = [];

                while (currentCard is SpecialCard)
                {
                    temporarySpecialCards.Add(currentCard);

                    if (gameDeck.Length() > 0)
                        currentCard = gameDeck.DealCard()!;

                    gameDeck.AddRange(temporarySpecialCards);
                    gameDeck.InHouseShuffle();
                }
            }

            IsPlayerTurn = true;

            do
            {
                if (currentCard is not null)
                    GameMethods.ShowRoundSummary(currentCard);

                if (IsPlayerTurn && currentCard is not null)
                {
                    GameMethods.ShowHand();

                    Card? cardPlayed = _playerTurnHandler.HandleTurn(ref currentCard, penaltyCard);
                    if (cardPlayed is not null)
                    {
                        LastPlayedCard = cardPlayed;
                        
                        bool computerSkipped = GameMethods.PotentialPlayerAction();
                        
                        if (computerSkipped)
                            continue;
                    }
                    else
                    {
                        IsPlayerTurn = false;
                    }
                }
                else if (!IsPlayerTurn && currentCard is not null)
                {
                    Card? cardPlayed = _computerTurnHandler.HandleTurn(ref currentCard, penaltyCard, player.Hand.Count);
                    if (cardPlayed is not null)
                    {
                        LastPlayedCard = cardPlayed;
                        
                        bool playerSkipped = GameMethods.PotentialComputerAction();
                        
                        if (playerSkipped)
                            continue;
                    }
                    else
                    {
                        IsPlayerTurn = true;
                    }
                }

            }
            while (player.Hand.Count > 0 && computer.Hand.Count > 0);

            GameMethods.GameOverStats();
        }

        static void Main()
        {
            GameIntroduction.ShowGameIntroduction();
        }

    }
}
