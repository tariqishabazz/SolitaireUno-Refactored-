using System.Text;

namespace SolitaireUno
{
    public class MainGame
    {
        public static readonly Player player = new();
        public static readonly Computer computer = new();
        internal static Deck GameDeck { get; set; }

        internal static IInputProvider Input { get; set; }
        internal static IOutputProvider Output { get; set; }

        internal PlayerTurnHandler _playerTurnHandler;
        internal ComputerTurnHandler _computerTurnHandler;

        internal static GameMode GameModeChoice { get; set; }
        internal static bool SuitEnforcement { get; set; }

        internal static Card? LastPlayedCard { get; private set; }
        internal static bool IsPlayerTurn { get; set; }

        public MainGame(IInputProvider input, IOutputProvider output, Deck deck, GameMode gameModeChoice, bool suitEnforcement)
        {
            Input = input;
            Output = output;
            GameDeck = deck;
            GameModeChoice = gameModeChoice;
            SuitEnforcement = suitEnforcement;

            _playerTurnHandler = new(player, GameDeck, Input, Output);
            _computerTurnHandler = new(computer, GameDeck, Output);

            GameSetup.SetupGame(player, computer, GameDeck);
        }

        public void StartGame()
        {
            RegularCard penaltyCard = new(Suits.Spades, Values.Queen);
            Card logicCard = GameDeck.DealCard()!;
            Card visualCard = logicCard;

            GameDeck.AddToDiscardPile(logicCard);

            if (logicCard is not null)
            {
                List<Card> temporarySpecialCards = [];

                while (logicCard is SpecialCard)
                {
                    temporarySpecialCards.Add(logicCard);

                    if (GameDeck.Length() > 0)
                        logicCard = GameDeck.DealCard()!;

                    visualCard = logicCard;

                    GameDeck.AddRange(temporarySpecialCards);
                    GameDeck.InHouseShuffle();
                }
            }

            IsPlayerTurn = true;
            
            do
            {
                if (IsPlayerTurn && logicCard is not null)
                {                    
                    Card? cardPlayed = _playerTurnHandler.HandleTurn(ref logicCard, ref visualCard, penaltyCard);
                    
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
                else if (!IsPlayerTurn && logicCard is not null)
                {
                    Card? cardPlayed = _computerTurnHandler.HandleTurn(ref logicCard, ref visualCard, penaltyCard, player.Hand.Count);
                    
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
            Console.OutputEncoding = Encoding.UTF8;
            GameIntroduction.ShowGameIntroduction();
        }
    }
}
