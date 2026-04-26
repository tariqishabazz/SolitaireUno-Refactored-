using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    public class ComputerTurnHandler(Computer computer, Deck deck)
    {
        private readonly Computer _computer = computer;
        private readonly Deck _deck = deck;
        private readonly GameDifficulty _gameDifficulty = MainGame.GameDifficulty;

        public Card? HandleTurn(ref Card logicCard, ref Card visualCard, Card penaltyCard, int opponentHandSize)
        {
            //_output.WriteLine("\n                 Computer is Thinking...");

            // Random random = new();
            // Thread.Sleep(random.Next(3000) + 1000);

            Card? potentialComputerPlay = _computer.MakeMove(logicCard, opponentHandSize, _deck.Length(), _gameDifficulty);
            if (potentialComputerPlay != null)
            {
                visualCard = potentialComputerPlay;

                if (potentialComputerPlay is RegularCard)
                    logicCard = potentialComputerPlay;

                _computer.PlayCard(potentialComputerPlay);
                _deck.AddToDiscardPile(potentialComputerPlay);

                return potentialComputerPlay;
            }

            else if (_deck.Length() > 0 || _deck.Length() == 0 && !_deck.deckReshuffled)
            {
                Card card = _deck.DealCard()!;

                _computer.PickupCard(card);

                int computerPotentialPenaltyCount = GameMethods.GetPenaltyCount(card, penaltyCard);

                switch (computerPotentialPenaltyCount)
                {
                    case > 0:
                        {
                            int actualPickupCount = 0;

                            for (int i = 0; i < computerPotentialPenaltyCount; i++)
                            {
                                Card? addtionalPenaltyCard = _deck.DealCard();

                                if (addtionalPenaltyCard is not null)
                                {
                                    _computer.PickupCard(addtionalPenaltyCard);
                                    actualPickupCount++;
                                }
                            }
                            break;
                        }

                    default:
                        break;
                }
            }

            else if (_deck.Length() == 0 && _deck.deckReshuffled)
            {
            }

            return null;
        }
    }
}
