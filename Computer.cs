using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    public class Computer
    {
        public List<Card> computerHand = new List<Card>();
        
        public void PickupCard(Card card)
        {
            computerHand.Add(card);
        }
        
        public void PlayCard(Card card)
        {
            computerHand.Remove(card);
        }

        public Card? MakeMove(Card currentCard)
        {
            foreach(Card card in computerHand)
            {
                if(GameMethods.ValidCard(card, currentCard))
                {
                    PlayCard(card);
                    return card;
                }
            }
            return null;
        }
    }

}
