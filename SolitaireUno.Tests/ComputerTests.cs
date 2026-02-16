using Xunit;
using SolitaireUno;
using System.Collections.Generic;

namespace SolitaireUno.Tests
{
    public class ComputerTests
    {
        [Fact]
        public void MakeMove_PlaysValidCard_WhenOneExists()
        {
            // --- ARRANGE (Set the scene) ---
            Computer bot = new Computer();

            // We give the bot a specific card: A 4 of Hearts
            Card winningCard = new Card(Suits.Hearts, Values.Four);
            bot.PickupCard(winningCard);

            // We set the table card to a 5 of Spades (So a 4 is a valid move)
            Card tableCard = new Card(Suits.Spades, Values.Five);

            // --- ACT (Run the logic) ---
            Card? result = bot.MakeMove(tableCard);

            // --- ASSERT (Check the result) ---
            // 1. Did it return the card we expected?
            Assert.NotNull(result);
            Assert.True(result.IsEqual(winningCard));

            // 2. Did it actually remove the card from its hand?
            Assert.Empty(bot.Hand);
        }

        [Fact]
        public void MakeMove_ReturnsNull_WhenNoValidMovesExist()
        {
            // --- ARRANGE ---
            Computer bot = new Computer();

            // Give the bot a 2 of Hearts (This cannot be played on a 5)
            Card badCard = new Card(Suits.Hearts, Values.Two);
            bot.PickupCard(badCard);

            Card tableCard = new Card(Suits.Spades, Values.Five);

            // --- ACT ---
            Card? result = bot.MakeMove(tableCard);

            // --- ASSERT ---
            // 1. It should return null because it can't play
            Assert.Null(result);

            // 2. The bad card should STILL be in its hand
            Assert.Single(bot.Hand);
        }

        [Fact]
        public void MakeMove_finds_CorrectCard_Among_Many()
        {
            // --- ARRANGE ---
            // Let's test if it skips bad cards to find the good one.
            Computer bot = new Computer();

            Card badCard = new Card(Suits.Hearts, Values.Two);
            Card goodCard = new Card(Suits.Hearts, Values.Four); // Valid on a 5

            // Add them in order: Bad, then Good
            bot.PickupCard(badCard);
            bot.PickupCard(goodCard);

            Card tableCard = new Card(Suits.Spades, Values.Five);

            // --- ACT ---
            Card? result = bot.MakeMove(tableCard);

            // --- ASSERT ---
            // It should have skipped the 2 and found the 4
            Assert.NotNull(result);
            Assert.True(result.IsEqual(goodCard));

            // Hand should now only have 1 card left (the bad one)
            Assert.Single(bot.Hand);
            Assert.True(bot.Hand[0].IsEqual(badCard));
        }

        [Fact]
        public void ComputerMakesMoveBased_OnHigherValue()
        {
            // arrange
            Computer bot = new();

            Card card1 = new(Suits.Hearts, Values.Nine);
            Card card2 = new(Suits.Diamonds, Values.Five);
            Card card3 = new(Suits.Spades, Values.Nine);
            
            Card currentCard = new(Suits.Clubs, Values.Ten);

            bot.Hand.Add(card1);
            bot.Hand.Add(card2);
            bot.Hand.Add(card3);

            // act
            Card result = bot.MakeMove(currentCard)!;

            // assert
            Assert.Equal(Suits.Spades, result.Suit);
        }
    }
}