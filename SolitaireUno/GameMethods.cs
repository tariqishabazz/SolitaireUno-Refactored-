using SolitaireUno;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

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
            if(potentialPlay is RegularCard potentialCard && currentlyShown is RegularCard currentCard)
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
        
        public static bool IsSpecialCard(Card potentialPlay) =>  potentialPlay is SpecialCard;

        private const int PenaltyCardCount = 5;
        public static int GetPenaltyCount(Card dealtCard, Card penaltyCard)
        {
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
                
                else if(specialCard.CardType.Equals(SpecialCardType.DrawFour))
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
    }
}