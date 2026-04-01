using Xunit; // The testing framework
using SolitaireUno; // Giving access to SolitaireUno Code

namespace SolitaireUno.Tests
{
    public class GameLogicTests
    {
        [Fact]
        public void ValidCardTrue_CardOneValueHigherInAscendingMode()
        {
            // Arrange
            RegularCard cardInPlay = new(Suits.Hearts, Values.Four);
            RegularCard cardToPlay = new(Suits.Spades, Values.Five);

            // Act
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay, GameConfiguration.Ascending);

            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void VaidCardFalse_CardOneValueLowerInAscendingMode()
        {
            // Arrange

            RegularCard cardToPlay = new(Suits.Diamonds, Values.Seven);
            RegularCard cardInPlay = new(Suits.Clubs, Values.Eight);

            // Act
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay, GameConfiguration.Ascending);

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void ValidCardTrue_AcePlayedOnKingInAscendingMode()
        {
            // Arrange

            RegularCard cardInPlay = new(Suits.Hearts, Values.King);
            RegularCard cardToPlay = new(Suits.Diamonds, Values.Ace);

            // Act
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay, GameConfiguration.Ascending);

            // Assert
            Assert.True(result);
        }

        [Fact] // A "Fact" is a test that is always true, like facts irl
        public void ValidCardTrue_CardOneValueLowerInDescendingMode()
        {
            // Arrange (Setting up the scenario)

            var cardToPlay = new RegularCard(Suits.Clubs, Values.Three); // 3 on 4
            var cardInPlay = new RegularCard(Suits.Hearts, Values.Four);

            // Act (Running the method)
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay, GameConfiguration.Descending);

            // Assert (Verifying the result)
            Assert.True(result);
        }

        [Fact]
        public void ValidCardTrue_KingPlayedOnAceInDescendingMode()
        {
            // Arrange

            var cardToPlay = new RegularCard(Suits.Diamonds, Values.King); // King on Ace
            var cardInPlay = new RegularCard(Suits.Spades, Values.Ace);

            // Act
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay, GameConfiguration.Descending);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidCardFalse_CardIsHigherInDescendingMode()
        {
            // Arrange

            RegularCard cardToPlay = new RegularCard(Suits.Clubs, Values.Ten);
            RegularCard cardInPlay = new RegularCard(Suits.Spades, Values.Nine);

            // Act
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay, GameConfiguration.Descending);

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void GetPenaltyCount_DealtCardEqualsPenaltyCard()
        {
            // Arrange
            var penaltyCard = new RegularCard(Suits.Spades, Values.Queen);
            var dealtCard = new RegularCard(Suits.Spades, Values.Queen);

            // Act
            int result = GameMethods.GetPenaltyCount(dealtCard, penaltyCard);

            // Assert
            Assert.Equal(5, result);
        }
        
        [Fact]
        public void GetPenaltyCount_DealtCardIsNotPenaltyCard()
        {
            // Arrange
            var penaltyCard = new RegularCard(Suits.Spades, Values.Queen);
            var dealtCard = new RegularCard(Suits.Clubs, Values.Two);

            // Act
            int result = GameMethods.GetPenaltyCount(dealtCard, penaltyCard);

            // Assert
            Assert.Equal(0, result);
        }
        
        [Fact]
        public void ControllingDeck()
        {
            // arrange
            var card1 = new RegularCard(Suits.Spades, Values.Ace);
            var card2 = new RegularCard(Suits.Hearts, Values.Two);

            List < RegularCard > cards = new List<RegularCard> { card1, card2 };

            // act
            Deck myDeck = new(cards);
            RegularCard result = myDeck.DealCard()!;

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

        [Fact]
        public void FullGamePlayedInAscending()
        {
            // Arrange
            
            Computer player1 = new();
            Computer player2 = new();

            Deck gameDeck = new();
            RegularCard currentCard;

            int turnCounter = 0;
            int maxTurns = 100;

            // Act


            // Assert


        }
    }
}
