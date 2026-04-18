using SolitaireUno;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

//Console.OutputEncoding = System.Text.Encoding.UTF8;

namespace SolitaireUno
{
    public class GameMethods
    {
        public static bool ValidCard(Card potentialPlay, Card currentlyShown, GameMode gameMode)
        {
            bool isValidSequence = false;

            if (potentialPlay is RegularCard firstRegularCard && currentlyShown is RegularCard secondRegularCard)
            {
                isValidSequence = gameMode == GameMode.Descending
                    ? IsValidDescending(potentialPlay, currentlyShown)
                    : IsValidAscending(potentialPlay, currentlyShown);

                if (!isValidSequence)
                {
                    return false;
                }

                return MainGame.SuitEnforcement ? SameColor(firstRegularCard, secondRegularCard) : true;
            }

            else
            {
                return IsSpecialCard(potentialPlay);
            }
        }

        private static bool IsValidDescending(Card potentialPlay, Card currentlyShown)
        {
            if (potentialPlay is RegularCard potentialCard && currentlyShown is RegularCard currentCard)
                if ((int)potentialCard.Value == (int)currentCard.Value - 1)
                    return true;

            return IsWrapAround(potentialPlay, currentlyShown, GameMode.Descending);
        }

        private static bool IsValidAscending(Card potentialPlay, Card currentlyShown)
        {
            if (potentialPlay is RegularCard potentialCard && currentlyShown is RegularCard currentCard)
                if ((int)potentialCard.Value == (int)currentCard.Value + 1)
                    return true;

            return IsWrapAround(potentialPlay, currentlyShown, GameMode.Ascending);
        }

        private static bool IsWrapAround(Card potentalPlay, Card currentlyShown, GameMode gameMode)
        {
            if (potentalPlay is RegularCard potentialCard && currentlyShown is RegularCard currentCard)
                if (gameMode == GameMode.Descending)
                    return potentialCard.Value == Values.King && currentCard.Value == Values.Ace;
                else
                    return potentialCard.Value == Values.Ace && currentCard.Value == Values.King;
            else
                return false;
        }

        public static bool IsSpecialCard(Card potentialPlay) => potentialPlay is SpecialCard;

        public static int GetPenaltyCount(Card dealtCard, Card penaltyCard)
        {
            const int PenaltyCardCount = 4;

            if (dealtCard is RegularCard regularCard)
                return regularCard.IsEqual(penaltyCard) ? PenaltyCardCount : 0;
            else
                return 0;
        }

        public static ActionInstruction SpecialCardAction(Card currentCard)
        {
            if (currentCard is SpecialCard specialCard)
            {
                if (specialCard.CardType.Equals(SpecialCardType.Skip))
                    return ActionInstruction.SkipTurn;

                else if (specialCard.CardType.Equals(SpecialCardType.ChangeOrder))
                    return ActionInstruction.ChangeOrder;

                else if (specialCard.CardType.Equals(SpecialCardType.DrawFour))
                    return ActionInstruction.DrawFour;

                else
                    return ActionInstruction.DrawTwo;
            }
            else
            {
                return ActionInstruction.DoNothing;
            }
        }

        private static bool SameColor(RegularCard firstRegularCard, RegularCard secondRegularCard)
        {
            bool isFirstCardRed = (firstRegularCard.Suit.Equals(Suits.Hearts) || firstRegularCard.Suit.Equals(Suits.Diamonds));
            bool isSecondCardRed = (secondRegularCard.Suit.Equals(Suits.Hearts) || secondRegularCard.Suit.Equals(Suits.Diamonds));

            return isFirstCardRed != isSecondCardRed;
        }

        public static void ShowHand()
        {
            MainGame.Output.WriteLine("\n---------------------------------------------------------------------");
            MainGame.Output.WriteLine("Your Hand: ");

            int index = 0;
            foreach (Card card in MainGame.player.Hand)
            {
                MainGame.Output.WriteLine($"   {index + 1}) {card}");
                index++;
            }

            MainGame.Output.WriteLine($"\nYou now have {MainGame.player.Hand.Count} cards");
            MainGame.Output.WriteLine($"The Computer now has {MainGame.computer.Hand.Count} cards");
        }

        public static Player? GameOverStats()
        {
            MainGame.Output.WriteLine("\n\n\n-------------------------------------------------------------");
            MainGame.Output.WriteLine("Game Over!");

            if (MainGame.computer.Hand.Count == 0)
            {
                MainGame.Output.WriteLine("\nYou Lose! You've been bested by the machine :(");
                return MainGame.computer;
            }
            else if (MainGame.player.Hand.Count == 0)
            {
                MainGame.Output.WriteLine("\nYou Win! You beat the computer! Congrats! :)");
                return MainGame.player;
            }

            return null;
        }

        public static bool PotentialPlayerAction()
        {
            bool computerSkipped = false;

            if (MainGame.LastPlayedCard is not null)
            {
                ActionInstruction message = SpecialCardAction(MainGame.LastPlayedCard);
                switch (message)
                {
                    case ActionInstruction.DoNothing:
                        MainGame.IsPlayerTurn = false;
                        break;

                    case ActionInstruction.ChangeOrder:
                        MainGame.GameModeChoice = MainGame.GameModeChoice == GameMode.Ascending ? GameMode.Descending : GameMode.Ascending;
                        MainGame.Output.WriteLine($"\nThe game mode is now {MainGame.GameModeChoice}");

                        MainGame.IsPlayerTurn = false;
                        break;

                    case ActionInstruction.SkipTurn:
                        MainGame.Output.WriteLine($"\nThe computer has been skipped!");
                        computerSkipped = true;
                        break;

                    case ActionInstruction.DrawFour:
                        for (int i = 0; i < 4; i++)
                        {
                            Card? drawnCard = MainGame.gameDeck.DealCard();
                            if (drawnCard is not null)
                            {
                                MainGame.computer.PickupCard(drawnCard);
                            }
                            else
                            {
                                break;
                            }
                        }

                        MainGame.Output.WriteLine($"\nThe computer just picked up 4 cards.");
                        break;

                    case ActionInstruction.DrawTwo:
                        for (int i = 0; i < 2; i++)
                        {
                            Card? drawnCard = MainGame.gameDeck.DealCard();
                            if (drawnCard is not null)
                            {
                                MainGame.computer.PickupCard(drawnCard);
                            }
                            else
                            {
                                break;
                            }
                        }

                        MainGame.Output.WriteLine("\nThe computer just picked up 2 cards");
                        break;
                };
            }
            return computerSkipped;
        }
        public static bool PotentialComputerAction()
        {
            bool playerSkipped = false;

            if (MainGame.LastPlayedCard is not null)
            {
                ActionInstruction message = SpecialCardAction(MainGame.LastPlayedCard);
                switch (message)
                {
                    case ActionInstruction.DoNothing:
                        MainGame.IsPlayerTurn = true;
                        break;

                    case ActionInstruction.ChangeOrder:
                        MainGame.GameModeChoice = MainGame.GameModeChoice == GameMode.Ascending ? GameMode.Descending : GameMode.Ascending;
                        MainGame.Output.WriteLine("\n---------------------------------------------------------------------");
                        MainGame.Output.WriteLine($"\nThe game mode is now {MainGame.GameModeChoice}");

                        MainGame.IsPlayerTurn = true;
                        break;

                    case ActionInstruction.SkipTurn:
                        MainGame.Output.WriteLine("\n---------------------------------------------------------------------");
                        MainGame.Output.WriteLine($"\nYou have been skipped!");
                        
                        playerSkipped = true;
                        break;

                    case ActionInstruction.DrawFour:
                        for (int i = 0; i < 4; i++)
                        {
                            Card? drawnCard = MainGame.gameDeck.DealCard();

                            if (drawnCard is not null)
                                MainGame.player.PickupCard(drawnCard);
                            else
                                break;
                        }

                        MainGame.Output.WriteLine("\n---------------------------------------------------------------------");
                        MainGame.Output.WriteLine($"\nYou had to pick up 4 cards");

                        break;

                    case ActionInstruction.DrawTwo:
                        for (int i = 0; i < 2; i++)
                        {
                            Card? drawnCard = MainGame.gameDeck.DealCard();

                            if (drawnCard is not null)
                                MainGame.player.PickupCard(drawnCard);
                            else
                                break;
                        }

                        MainGame.Output.WriteLine("\n---------------------------------------------------------------------");
                        MainGame.Output.WriteLine($"\nYou had to pick up 2 cards");

                        break;
                }
                ;
            }
            return playerSkipped;
        }

        public static void ShowRoundSummary(Card currentCard)
        {
            MainGame.Output.WriteLine("\n---------------------------------------------------------------------");
            MainGame.Output.WriteLine($"\nCurrent Card: {currentCard}");
            
            int deckLength = MainGame.gameDeck.Length();
            
            if (deckLength > 0)
                MainGame.Output.WriteLine($"\nDeck has {deckLength} cards remaining.");
            
            else if (Deck.deckReshuffled)
                MainGame.Output.WriteLine("\nNo more cards in the deck!");
            
            else
                MainGame.Output.WriteLine("No more cards in the deck, but someone can pick up to reshuffle!");
        }
    }
}