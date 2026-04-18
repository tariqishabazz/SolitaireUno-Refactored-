using Xunit;
using SolitaireUno;

namespace SolitaireUno.Tests
{
    public class CardAndDeckTests
    {
        [Fact]
        public void DeckReshufflesCorrectly()
        {
            // arrange 
            string simulatedPickup;
            
            Deck newDeck = new Deck();

            // act
            for(int i = 0; i < newDeck.Length() - 1; i++)
            {
                newDeck.DealCard();
            }

            simulatedPickup = MockInput.SimulatedPickUp();

            // assert
        }
    }
}
