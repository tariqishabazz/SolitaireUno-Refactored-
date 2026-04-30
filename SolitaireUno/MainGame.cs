using System.Text;

namespace SolitaireUno
{
    public class MainGame
    {
        public static readonly Player player = new();
        public static readonly Computer computer = new();
        public static Deck GameDeck { get; set; }

        internal PlayerTurnHandler _playerTurnHandler;
        internal ComputerTurnHandler _computerTurnHandler;

        public static GameMode GameModeChoice { get; set; }
        internal static GameDifficulty GameDifficulty { get; set; }

        public static bool IsPlayerTurn { get; set; }
        internal static bool SuitEnforcement { get; private set; }
        public static bool ComputerSkipped { get; set; }
        public static bool PlayerSkipped { get; set; }

        public static Card? LastPlayedCard { get; private set; }
        public static Card LogicCard;
        public static Card VisualCard;
        internal static RegularCard PenaltyCard = new RegularCard(Suits.Spades, Values.Queen);


        public MainGame(Deck deck, GameMode gameModeChoice, bool suitEnforcement, GameDifficulty gameDifficulty)
        {
            GameDeck = deck;
            GameModeChoice = gameModeChoice;
            GameDifficulty = gameDifficulty;
            SuitEnforcement = suitEnforcement;

            _playerTurnHandler = new PlayerTurnHandler(player, GameDeck);
            _computerTurnHandler = new ComputerTurnHandler(computer, GameDeck);

            GameSetup.SetupGame(player, computer, GameDeck);
        }

        public void StartGame()
        {
            LogicCard = GameDeck.DealCard()!;
            GameDeck.AddToDiscardPile(LogicCard);

            Card? updatedCard = GameMethods.PreventInitalSpecialCard(LogicCard);
            if (updatedCard is not null)
            {
                LogicCard = updatedCard;
                VisualCard = updatedCard;
            }
            else
            {
                VisualCard = LogicCard;
            }

            IsPlayerTurn = true;
        }
        public string AdvanceTurn(string playerDecision = "")
        {
            PlayerSkipped = false;
            ComputerSkipped = false;

            string uiMessage = "";

            if (IsPlayerTurn && LogicCard is not null)
            {
                var (isSuccessful, message, cardPlayed) = _playerTurnHandler.HandleTurn(ref LogicCard, ref VisualCard, PenaltyCard, playerDecision);

                uiMessage = message;

                if (isSuccessful)
                {
                    if (cardPlayed is not null)
                    {
                        LastPlayedCard = cardPlayed;
                        ComputerSkipped = GameMethods.PotentialPlayerAction();
                    }

                    if(!ComputerSkipped)
                        IsPlayerTurn = false;
                }
            }

            else if (!IsPlayerTurn && LogicCard is not null)
            {
                var (message, cardPlayed) = _computerTurnHandler.HandleTurn(ref LogicCard, ref VisualCard, PenaltyCard, player.Hand.Count);

                uiMessage = message;

                if (cardPlayed is not null)
                {
                    LastPlayedCard = cardPlayed;
                    PlayerSkipped = GameMethods.PotentialComputerAction();
                }

                if(!PlayerSkipped)
                    IsPlayerTurn = true;
            }
            
            return uiMessage;
        }
    }
}
