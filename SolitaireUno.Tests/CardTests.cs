using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SolitaireUno;

namespace SolitaireUno.Tests
{
    public class CardTests
    {
        [Fact]
        public void IsEqual_ReturnsTrue_ForIndenticalCards()
        {
            // Arrange
            var card1 = new Card(Suits.Spades, Values.Queen);
            var card2 = new Card(Suits.Spades, Values.Queen);

            // Act
            bool areEqual = card1.IsEqual(card2);

            // Assert
            Assert.True(areEqual);
        }

        [Fact]
        public void IsEqual_ReturnsFalse_DifferentSuits()
        {
            // Arrange
            var card1 = new Card(Suits.Spades, Values.Queen);
            var card2 = new Card(Suits.Diamonds, Values.Queen);

            // Act 
            bool areEqual = card1.IsEqual(card2);

            // Assert
            Assert.False(areEqual);
        }
    }
}
