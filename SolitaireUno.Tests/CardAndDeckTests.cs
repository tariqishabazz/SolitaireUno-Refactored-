using Xunit;
using SolitaireUno;
using System;
using System.IO;

namespace SolitaireUno.Tests
{
    public class CardAndDeckTests
    {
        [Fact]
        public void DeckReshufflesCorrectly()
        {
            // arrange 
            string simulatedPickup = "pu";
            
            using var stringReader = new StringReader(simulatedPickup);
            
            var originalIn = Console.In;
            
            Console.SetIn(stringReader);

            try
            {
                Deck newDeck = new Deck();
                List<Card> discardPile = [];

                int setDeckLength = newDeck.Length();

                // act
                for (int i = 0; i < setDeckLength; i++)
                {
                    Card? dealtCard = newDeck.DealCard();
                    
                    if(dealtCard is not null)
                        discardPile.Add(dealtCard);
                }

                // assert
                Assert.Equal(discardPile.Count, newDeck.Length());
            }
            finally
            {
                // Restore original input stream
                Console.SetIn(originalIn);
            }
        }
    }
}
