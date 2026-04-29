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
        public void AdvanceTurn(string playerDecision = "")
        {
            if (IsPlayerTurn && LogicCard is not null)
            {
                Card? cardPlayed = _playerTurnHandler.HandleTurn(ref LogicCard, ref VisualCard, PenaltyCard, playerDecision);

                if (cardPlayed is not null)
                {
                    LastPlayedCard = cardPlayed;
                    ComputerSkipped = GameMethods.PotentialPlayerAction();

                    if (ComputerSkipped)
                        return;
                }
                else
                {
                    IsPlayerTurn = false;
                }
            }
            
            else if (!IsPlayerTurn && LogicCard is not null)
            {
                Card? cardPlayed = _computerTurnHandler.HandleTurn(ref LogicCard, ref VisualCard, PenaltyCard, player.Hand.Count);

                if (cardPlayed is not null)
                {
                    LastPlayedCard = cardPlayed;
                    bool playerSkipped = GameMethods.PotentialComputerAction();

                    if (playerSkipped)
                        return;
                }
                else
                {
                    IsPlayerTurn = true;
                }
            }

            if (player.Hand.Count == 0 || computer.Hand.Count == 0)
                GameMethods.GameOverStats();

        }
    }
}
