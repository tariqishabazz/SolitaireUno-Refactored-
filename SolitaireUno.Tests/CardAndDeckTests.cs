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
            Deck gameDeck = new Deck();
          
            int startingDeckLength = gameDeck.Length();

            // act
            for (int i = 0; i < startingDeckLength; i++)
            {
                gameDeck.AddToDiscardPile(gameDeck.DealCard());
            }

            Card? rescueCard = gameDeck.DealCard();
            int reshuffledLength = gameDeck.Length();

            // assert
   
            Assert.Equal(56, reshuffledLength);
        }
    }
}
