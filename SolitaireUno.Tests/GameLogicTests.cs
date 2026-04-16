using Xunit;
using SolitaireUno;
using System.Collections.Generic;

namespace SolitaireUno.Tests
{
    public class GameLogicTests
    {
        [Fact]
        public void ValidCardTrue_CardOneValueHigherInAscendingMode()
        {
            RegularCard cardInPlay = new(Suits.Hearts, Values.Four);
            RegularCard cardToPlay = new(Suits.Spades, Values.Five);
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay, GameConfiguration.Ascending);
            Assert.True(result);
        }
        [Fact]
        public void VaidCardFalse_CardOneValueLowerInAscendingMode()
        {
            RegularCard cardToPlay = new(Suits.Diamonds, Values.Seven);
            RegularCard cardInPlay = new(Suits.Clubs, Values.Eight);
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay, GameConfiguration.Ascending);
            Assert.False(result);
        }
        [Fact]
        public void ValidCardTrue_AcePlayedOnKingInAscendingMode()
        {
            RegularCard cardInPlay = new(Suits.Hearts, Values.King);
            RegularCard cardToPlay = new(Suits.Diamonds, Values.Ace);
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay, GameConfiguration.Ascending);
            Assert.True(result);
        }
        [Fact]
        public void ValidCardTrue_CardOneValueLowerInDescendingMode()
        {
            var cardToPlay = new RegularCard(Suits.Clubs, Values.Three);
            var cardInPlay = new RegularCard(Suits.Hearts, Values.Four);
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay, GameConfiguration.Descending);
            Assert.True(result);
        }
        [Fact]
        public void ValidCardTrue_KingPlayedOnAceInDescendingMode()
        {
            var cardToPlay = new RegularCard(Suits.Diamonds, Values.King);
            var cardInPlay = new RegularCard(Suits.Spades, Values.Ace);
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay, GameConfiguration.Descending);
            Assert.True(result);
        }
        [Fact]
        public void ValidCardFalse_CardIsHigherInDescendingMode()
        {
            RegularCard cardToPlay = new RegularCard(Suits.Clubs, Values.Ten);
            RegularCard cardInPlay = new RegularCard(Suits.Spades, Values.Nine);
            bool result = GameMethods.ValidCard(cardToPlay, cardInPlay, GameConfiguration.Descending);
            Assert.False(result);
        }
        
        [Fact]
        public void GetPenaltyCount_DealtCardEqualsPenaltyCard()
        {
            var penaltyCard = new RegularCard(Suits.Spades, Values.Queen);
            var dealtCard = new RegularCard(Suits.Spades, Values.Queen);
            int result = GameMethods.GetPenaltyCount(dealtCard, penaltyCard);
            Assert.Equal(5, result);
        }
        
        [Fact]
        public void GetPenaltyCount_DealtCardIsNotPenaltyCard()
        {
            var penaltyCard = new RegularCard(Suits.Spades, Values.Queen);
            var dealtCard = new RegularCard(Suits.Clubs, Values.Two);
            int result = GameMethods.GetPenaltyCount(dealtCard, penaltyCard);
            Assert.Equal(0, result);
        }
        
        [Fact]
        public void ControllingDeck()
        {
            var card1 = new RegularCard(Suits.Spades, Values.Ace);
            var card2 = new RegularCard(Suits.Hearts, Values.Two);
            List < RegularCard > cards = new List<RegularCard> { card1, card2 };
            Deck myDeck = new(cards);
            RegularCard result = myDeck.DealCard()!;
            Assert.Equal(card1, result);
        }
        
        [Fact]
        public void MockInputReturns_ScriptedMoves()
        {
            var robotMoves = new List<string> { "p.u", "1" };
            MockInput robot = new MockInput(robotMoves);
            Assert.Equal("p.u", robot.GetInput());
            Assert.Equal("1", robot.GetInput());
            Assert.Equal("pass", robot.GetInput());
        }

        [Fact]
        public void FullGamePlayedInAscending()
        {
            Computer player1 = new();
            Computer player2 = new();
            Deck gameDeck = new();
            RegularCard currentCard;
            int turnCounter = 0;
            int maxTurns = 100;
        }
    }
}
