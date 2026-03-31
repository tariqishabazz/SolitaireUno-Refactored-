using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    public class ComputerTurnHandler
    {
        private readonly Computer _computer;
        private readonly Deck _deck;
        private readonly IOutputProvider _output;

        public ComputerTurnHandler(Computer computer, Deck deck, IOutputProvider output)
        {
            _computer = computer;
            _deck = deck;
            _output = output;
        }

        public void HandleTurn(ref Card currentCard, Card penaltyCard)
        {
            Card? potentialComputerPlay = _computer.MakeMove(currentCard);

            if (potentialComputerPlay != null)
            {
                currentCard = potentialComputerPlay;
                _output.WriteLine($"\nComputer played: {potentialComputerPlay}");
            }
            else
            {
                if (_deck.Length() > 0)
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
                else
                {
                    _output.WriteLine("\nComputer couldn't play a move and couldn't pick up... so it passed");
                }
            }
        }
    }
}
