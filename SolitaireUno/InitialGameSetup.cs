using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    public class InitialGameSetup
    {
        public static void SetupGame(Player player, Computer computer, Deck deck)
        {
            for (int i = 0; i < 10; i++)
            {
                Card card = deck.DealCard()!;
                player.PickupCard(card);
            }

            for (int i = 0; i < 10; i++)
            {
                Card card = deck.DealCard()!;
                computer.PickupCard(card);
            }
        }
    }
}
