using Xunit;

namespace SolitaireUno.Tests
{
    public class ComputerTests
    {
        [Fact]
        public static void NullWhenNoMoveFound()
        {
            Card currentCard = new RegularCard(Suits.Clubs, Values.Two);
            Card cardInHand = new RegularCard(Suits.Diamonds, Values.Seven);

            Computer computer = new Computer();
            computer.PickupCard(cardInHand);

            Card? potentialPlay = computer.MakeMove(currentCard, 5, 0, GameDifficulty.Easy);
           
            Assert.Null(potentialPlay);
        }
    
    }
}