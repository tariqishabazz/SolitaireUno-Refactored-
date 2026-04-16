using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    public class ComputerTurnHandler(Computer computer, Deck deck, IOutputProvider output)
    {
        private readonly Computer _computer = computer;
        private readonly Deck _deck = deck;
        private readonly IOutputProvider _output = output;
        public Card? HandleTurn(ref Card currentCard, Card penaltyCard, int opponentHandSize)
        {
            Card? potentialComputerPlay = _computer.MakeMove(currentCard, opponentHandSize);
            if (potentialComputerPlay != null)
            {
                if (potentialComputerPlay is RegularCard)
                    currentCard = potentialComputerPlay;
                _output.WriteLine($"\nComputer played: {potentialComputerPlay}");
                
                _computer.PlayCard(potentialComputerPlay);
                
                _deck.AddToDiscardPile(potentialComputerPlay);

                return potentialComputerPlay;
            }
            else if (_deck.Length() > 0 || _deck.Length() == 0 && !Deck.deckReshuffled)
            {
                Card card = _deck.DealCard()!;
                _computer.PickupCard(card);

                int computerPotentialPenaltyCount = GameMethods.GetPenaltyCount(card, penaltyCard);
                if (computerPotentialPenaltyCount > 0)
                {
                    _output.WriteLine("\nThe computer decided to pick up and recieved the Queen of Spades!");
                    _output.WriteLine("It recieved 5 additional cards because... why not...");

                    for (int i = 0; i < computerPotentialPenaltyCount; i++)
                    {
                        _computer.PickupCard(_deck.DealCard()!);
                    }
                }
                else
                {
                    _output.WriteLine("\nComputer couldn't make a move... and picked up");
                }
            }
            else if (_deck.Length() == 0 && Deck.deckReshuffled)
            {
                _output.WriteLine("\nThe Computer couldn't play or pickup, so it passed..."); 
            }
            
            return null;
        }
    }
}
