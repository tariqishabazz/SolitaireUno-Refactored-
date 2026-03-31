namespace SolitaireUno
{
    /// <summary>
    /// Represents the main game logic and flow for a card game between a human player and the computer.
    /// </summary>
    /// <remarks>The Game class manages the initialization of players and the deck, handles turn-based
    /// gameplay, and determines the win condition. It provides the entry point for starting and running a complete game
    /// session via the Main method. User interaction is performed through the console.</remarks>
    public class MainGame
    {
        Player player = new();
        Computer computer = new();
        Deck gameDeck = new();
        Card currentCard;

        private readonly IInputProvider _input;
        private readonly IOutputProvider _output;

        private readonly PlayerTurnHandler _playerTurnHandler;
        private readonly ComputerTurnHandler _computerTurnHandler;

        internal static string _gameModeChoice;

        public MainGame(IInputProvider input, IOutputProvider output, Deck deck, string gameModeChoice)
        {
            _input = input;
            _output = output;
            gameDeck = deck;
            _gameModeChoice = gameModeChoice;

            _playerTurnHandler = new(player, gameDeck, _input, _output);
            _computerTurnHandler = new(computer, gameDeck, _output);

            InitialGameSetup.SetupGame(player, computer, gameDeck);
        }
        
        public Player? StartGame()
        {
            Card penaltyCard = new(Suits.Spades, Values.Queen); // setting the penalty card
            
            currentCard = gameDeck.DealCard()!;

            while (player.Hand.Count > 0 && computer.Hand.Count > 0)
            {

                ShowHand();
                _playerTurnHandler.HandleTurn(ref currentCard, penaltyCard);
                _computerTurnHandler.HandleTurn(ref currentCard, penaltyCard);

            }

            /* --------------------- WIN CONDITION ---------------------- */

            _output.WriteLine("\n\n\n-------------------------------------------------------------");
            _output.WriteLine("Game Over!");

            if(computer.Hand.Count == 0)
            {
                _output.WriteLine("\nYou Lose! You've been bested by the machine :(");
                return computer;
            }
            else if(player.Hand.Count == 0)
            {
                _output.WriteLine("\nYou Win! You beat the computer! Congrats! :)");
                return player;
            }

            return null;
        }

        /* --------------------- INTRO CONVERSATION ---------------------- */
        static void Main(string[] args)
        {
            GameIntroduction.ShowGameIntroduction(); 
        }

        // ------------------------------------------- Game Methods ----------------------------------------------

        public void ShowHand()
        {
            _output.WriteLine("\nYour Hand: ");    // Title showing the player's hand

            int index = 0;                       // index to keep track of iteration
            foreach (Card card in player.Hand)     // a foreach loop that goes through every Card object in the players hand (which is in memory before being shown)
            {
                _output.WriteLine($"   {index + 1}) {card}"); // "For each" card, it properly formats to be more pleasing, starting at 1, 1) Value of Suit
                index++;                                      // increment to properly number every card
            }
        }
    }
}

// Add new game modes/card types?
// Add new GameMethods test for new logic?