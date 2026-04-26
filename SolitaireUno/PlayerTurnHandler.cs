using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    /// <summary>
    /// Handles the logic for a human player's turn, including input, validation, and card actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the PlayerTurnHandler class.
    /// </remarks>
    /// <param name="player">The human player.</param>
    /// <param name="deck">The deck used for drawing cards.</param>
    /// <param name="input">Input provider for user input.</param>
    /// <param name="output">Output provider for user output.</param>
    public class PlayerTurnHandler(Player player, Deck deck)
    {
        /// <summary>
        /// Handles the logic for a single human player turn, including input, validation, and card actions.
        /// </summary>
        /// <param name="currentCard">Reference to the current card in play (may be updated).</param>
        /// <param name="penaltyCard">The penalty card for special rules.</param>
        public Card? HandleTurn(ref Card logicCard, ref Card visualCard, Card penaltyCard, string playerDecision)
        {

            playerDecision = playerDecision?.ToLower().Trim() ?? "";

            if (playerDecision != null)
            {
                if (int.TryParse(playerDecision, out int decisionAsNumber)) // If input is a number
                {
                    if (decisionAsNumber > 0 && decisionAsNumber <= player.Hand.Count) // Valid card index
                    {
                        Card potentialCard = player.Hand[decisionAsNumber - 1]; // Get selected card

                        if (GameMethods.ValidCard(potentialCard, logicCard, MainGame.GameModeChoice)) // Validate move
                        {
                            player.PlayCard(potentialCard); // Play the card
                            deck.AddToDiscardPile(potentialCard);

                            visualCard = potentialCard;

                            if (potentialCard is RegularCard)
                            {
                                logicCard = potentialCard; // Update current card
                            }

                            MainGame.IsPlayerTurn = false;
                            return potentialCard;
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                    }
                }
                else if (playerDecision == "p.u" || playerDecision == "pu" || playerDecision == "pick up" || playerDecision == "pickup") // Pick up
                {
                    if (deck.Length() > 0 || deck.Length() == 0 && !deck.deckReshuffled)
                    {
                        Card card = deck.DealCard()!; // Draw a card
                        player.PickupCard(card); // Add to hand

                        int playerPotentialPenaltyCount = GameMethods.GetPenaltyCount(card, penaltyCard); // Check penalty

                        switch (playerPotentialPenaltyCount)
                        {
                            case > 0:
                                int actualPickupCount = 0;

                                for (int i = 0; i < playerPotentialPenaltyCount; i++) // Add penalty cards
                                {
                                    Card? additionalPenaltyCard = deck.DealCard();

                                    if (additionalPenaltyCard is not null)
                                    {
                                        player.PickupCard(additionalPenaltyCard);
                                        actualPickupCount++;
                                    }
                                }

                                break;

                            default:

                                break;
                        }

                        MainGame.IsPlayerTurn = false;
                        return null;
                    }

                    else if (deck.Length() == 0 && deck.deckReshuffled)
                    {
                    }

                }
                else if (playerDecision == "pass" || playerDecision == "p") // Pass
                {
                    if (deck.Length() > 0)
                    {
                    }
                    else
                    {
                        MainGame.IsPlayerTurn = false;
                    }
                }
                else
                {
                }
            }
            else
            {
            }

            return null;
        }
    }
}
