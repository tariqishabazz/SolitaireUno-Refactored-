using Xunit;
using SolitaireUno;
using System.Collections.Generic;

namespace SolitaireUno.Tests
{
    public class ComputerTests
    {
        private readonly IInputProvider input;
        MainGame testGame = new(input, output, GameConfiguration.Descending, penaltyCard);
        [Fact]
        public void ComputerPlaysValidCardIfExistsInDescending()
        {
            Computer bot = new Computer();
            RegularCard winningCard = new RegularCard(Suits.Hearts, Values.Four);
            bot.PickupCard(winningCard);
            RegularCard tableCard = new RegularCard(Suits.Spades, Values.Five);
            RegularCard? result = bot.MakeMove(tableCard);
            Assert.NotNull(result);
            Assert.True(result.IsEqual(winningCard));
            Assert.Empty(bot.Hand);
        }
        [Fact]
        public void MakeMove_ReturnsNull_WhenNoValidMovesExist_InDescending()
        {
            MainGame.GameModeChoice = GameConfiguration.Descending;
            Computer bot = new();
            RegularCard badCard = new(Suits.Hearts, Values.Two);
            bot.PickupCard(badCard);
            RegularCard tableCard = new(Suits.Spades, Values.Five);
            RegularCard? result = bot.MakeMove(tableCard);
            Assert.Null(result);
            Assert.Single(bot.Hand);
        }
        [Fact]
        public void ComputerFindsCorrectCardAmongManyInDescending()
        {
            MainGame.GameModeChoice = GameConfiguration.Descending;

            Computer bot = new Computer();

            RegularCard badCard = new RegularCard(Suits.Hearts, Values.Two);
            RegularCard goodCard = new RegularCard(Suits.Hearts, Values.Four); // Valid on a 5

            bot.PickupCard(badCard);
            bot.PickupCard(goodCard);

            RegularCard tableCard = new RegularCard(Suits.Spades, Values.Five);

            RegularCard? result = bot.MakeMove(tableCard);

            Assert.NotNull(result);
            Assert.True(result.IsEqual(goodCard));
            Assert.Single(bot.Hand);
            Assert.True(bot.Hand[0].IsEqual(badCard));
        }
        [Fact]
        public void ComputerMakesMove_BasedOnHigherValueInDescending()
        {
            MainGame.GameModeChoice = GameConfiguration.Descending;

            Computer bot = new();

            RegularCard card1 = new(Suits.Hearts, Values.Nine);
            RegularCard card2 = new(Suits.Diamonds, Values.Five);
            RegularCard card3 = new(Suits.Spades, Values.Nine);

            RegularCard currentCard = new(Suits.Clubs, Values.Ten);

            bot.Hand.Add(card1);
            bot.Hand.Add(card2);
            bot.Hand.Add(card3);

            RegularCard result = bot.MakeMove(currentCard)!;

            Assert.Equal(Suits.Spades, result.Suit);
        }
    }
}