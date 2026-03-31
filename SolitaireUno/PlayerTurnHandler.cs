using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    public class PlayerTurnHandler
    {
        private readonly Player _player;
        private readonly Deck _deck;
        private readonly IInputProvider _input;
        private readonly IOutputProvider _output;

        public PlayerTurnHandler(Player player, Deck deck, IInputProvider input, IOutputProvider output)
        {
            _player = player;
            _deck = deck;
            _input = input;
            _output = output;
        }

        public void HandleTurn(ref Card currentCard, Card penaltyCard)
        {
            bool playerChoiceValid = false; // bool representing whether player made a correct option

            while (!playerChoiceValid)
            {
                _output.WriteLine("---------------------------------------------------------------------");
                _output.WriteLine($"\n            The Current Card is... {currentCard}");


                if (_deck.Length() > 0)
                {
                    _output.WriteLine($"\n                There are {_deck.Length()} cards in the deck!");
                }
                else
                {
                    _output.WriteLine("\nThere are no more cards in the deck!");
                }

                _output.Write("\nPlay a Card (1 - # of Cards in Hand), Pick-Up (p.u), or Pass(p) >> ");
                string playerDecision = _input.GetInput().ToLower();

                if (playerDecision != null)
                {
                    if (int.TryParse(playerDecision, out int decisionAsNumber))
                    {
                        if (decisionAsNumber > 0 && decisionAsNumber <= _player.Hand.Count)
                        {
                            Card potentialCard = _player.Hand[decisionAsNumber - 1];

                            if (GameMethods.ValidCard(potentialCard, currentCard))
                            {
                                _player.PlayCard(potentialCard);
                                currentCard = potentialCard;

                                _output.WriteLine("\n---------------------------------------------------------------------");
                                _output.WriteLine($"You played {potentialCard}, so...");
                                playerChoiceValid = true;
                            }
                            else
                            {
                                _output.WriteLine("\n---------------------------------------------------------------------");
                                _output.WriteLine("That is not a valid play, please choose again");
                            }
                        }
                        else
                        {
                            _output.WriteLine("\n---------------------------------------------------------------------");
                            _output.WriteLine("That is an invalid input based on your current cards, please choose again.");
                        }
                    }

                    else if (playerDecision == "p.u" || playerDecision == "pu" || playerDecision == "pick up" || playerDecision == "pickup")
                    {
                        if (_deck.Length() > 0)
                        {
                            Card card = _deck.DealCard()!;
                            _player.PickupCard(card);

                            int playerPotentialPenaltyCount = GameMethods.GetPenaltyCount(card, penaltyCard);
                            
                            if (playerPotentialPenaltyCount > 0)
                            {
                                _output.WriteLine("\n---------------------------------------------------------------------");
                                _output.WriteLine("You decided to pick up and recieved the Queen of Spades! HAHAHAHA");
                                _output.WriteLine("You recieved 5 additional cards because... why not...");
                            }
                            else
                            {
                                _output.WriteLine("\n---------------------------------------------------------------------");
                                _output.WriteLine("You decided to pick up!");
                            }

                            for (int i = 0; i < playerPotentialPenaltyCount; i++)
                            {
                                _player.PickupCard(_deck.DealCard()!);
                            }

                            playerChoiceValid = true;
                        }
                        else
                        {
                            _output.WriteLine("\n---------------------------------------------------------------------");
                            _output.WriteLine("There are no more cards in the deck! Either play or pass!");
                        }
                    }

                    else if (playerDecision == "pass" || playerDecision == "p")
                    {
                        if (_deck.Length() > 0)
                        {
                            _output.WriteLine("\n---------------------------------------------------------------------");
                            _output.WriteLine("There are still cards in the deck, either play or pick up!");
                        }
                        else
                        {
                            playerChoiceValid = true;
                        }
                    }
                }
                else
                {
                    _output.WriteLine("\n---------------------------------------------------------------------");
                    _output.WriteLine("You did not make a decision, please try again");
                }
            }
        }
    }
}
