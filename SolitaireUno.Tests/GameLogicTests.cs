using Xunit; // The testing framework
using SolitaireUno; // Giving access to SolitaireUno Code

namespace SolitaireUno.Tests
{
    public class GameMethodsTests
    {
        [Fact] // A "Fact" is a test that is always true, like facts irl
        public void ValidCard_ReturnsTrue_WhenCardIsOneValueLower()
        {
            // Arrange (Setting up the scenario)
            var cardInPlay = new Card(Suits.Hearts, Values.Four);
            var cardToPlay = new Card(Suits.Clubs, Values.Three); // 3 on 4

            // Act (Running the method)
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay);

            // Assert (Verifying the result)
            Assert.True(result, "PLaying a 3 on a 4 should be valid");
        }

        [Fact]
        public void ValidCard_ReturnsTrue_WhenKingPlayedOnAce()
        {
            // Arrange
            var cardInPlay = new Card(Suits.Spades, Values.Ace);
            var cardToPlay = new Card(Suits.Diamonds, Values.King); // King on Ace

            // Act
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay);

            // Assert
            Assert.True(result, "Playing a King on an Ace should be valid");
        }

        [Fact]
        public void ValidCard_ReturnsFalse_WhenCardIsHigher()
        {
            // Arrange
            var cardInPlay = new Card(Suits.Spades, Values.Nine);
            var cardToPlay = new Card(Suits.Clubs, Values.Ten);

            // Act
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay);

            // Assert
            Assert.False(result, "Playing a 10 on a 9 should be invalid");
        }
        [Fact]
        public void GetPenaltyCount_DealtCardEqualsPenaltyCard()
        {
            // Arrange
            var penaltyCard = new Card(Suits.Spades, Values.Queen);
            var dealtCard = new Card(Suits.Spades, Values.Queen);

            // Act
            int result = GameMethods.GetPenaltyCount(dealtCard, penaltyCard);

            // Assert
            Assert.Equal(5, result);
        }
        [Fact]
        public void GetPenaltyCount_DealtCardIsNotPenaltyCard()
        {
            // Arrange
            var penaltyCard = new Card(Suits.Spades, Values.Queen);
            var dealtCard = new Card(Suits.Clubs, Values.Two);

            // Act
            int result = GameMethods.GetPenaltyCount(dealtCard, penaltyCard);

            // Assert
            Assert.Equal(0, result);
        }
        [Fact]
        public void ControllingDeck()
        {
            // arrange
            var card1 = new Card(Suits.Spades, Values.Ace);
            var card2 = new Card(Suits.Hearts, Values.Two);

            List < Card > cards = new List<Card> { card1, card2 };

            // act
            Deck myDeck = new(cards);
            Card result = myDeck.DealCard()!;

            // assert
            Assert.Equal(card1, result);
        }
        [Fact]
        public void MockInputReturns_ScriptedMoves()
        {
            // arrange
            var robotMoves = new List<string> { "p.u", "1" };
            MockInput robot = new MockInput(robotMoves);

            // act and assert
            Assert.Equal("p.u", robot.GetInput());
            Assert.Equal("1", robot.GetInput());
            Assert.Equal("pass", robot.GetInput());
        }
    }
}
