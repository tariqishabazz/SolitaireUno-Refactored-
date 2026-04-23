using Xunit;

namespace SolitaireUno.Tests
{
    public class GameLogicTests
    {

        [Fact]
        public static void AscendingLogicFunctional()
        {
            Card card1 = new RegularCard(Suits.Clubs, Values.Five);
            Card card2 = new RegularCard(Suits.Hearts, Values.Six);

            bool result = GameMethods.ValidCard(card2, card1, GameMode.Ascending);

            Assert.True(result);
        }

        [Fact]
        public static void DescendingLogicFunctional()
        {
            Card card1 = new RegularCard(Suits.Clubs, Values.Five);
            Card card2 = new RegularCard(Suits.Hearts, Values.Six);

            bool result = GameMethods.ValidCard(card1, card2, GameMode.Descending);

            Assert.True(result);
        }

        [Fact]
        public static void WrapAroundLogicFunctionalAscending()
        {
            Card card1 = new RegularCard(Suits.Clubs, Values.Ace);
            Card card2 = new RegularCard(Suits.Hearts, Values.King);

            bool result = GameMethods.ValidCard(card1, card2, GameMode.Ascending);

            Assert.True(result);
        }

        [Fact]
        public static void WrapAroundLogicFunctionalDescending()
        {
            Card card1 = new RegularCard(Suits.Clubs, Values.Ace);
            Card card2 = new RegularCard(Suits.Hearts, Values.King);

            bool result = GameMethods.ValidCard(card2, card1, GameMode.Descending);

            Assert.True(result);
        }

        [Fact]
        public static void IsSpecialCardLogicFunctional()
        {
            Card specialCard = new SpecialCard(SpecialCardType.Skip);

            bool result = GameMethods.IsSpecialCard(specialCard);

            Assert.True(result);
        }

        [Fact]
        public static void PenaltyCountLogicFunctional()
        {
            Card dealtCard = new RegularCard(Suits.Spades, Values.Queen);
            Card penaltyCard = new RegularCard(Suits.Spades, Values.Queen);

            int result = GameMethods.GetPenaltyCount(dealtCard, penaltyCard);

            Assert.Equal(4, result);
        }
    }
}
