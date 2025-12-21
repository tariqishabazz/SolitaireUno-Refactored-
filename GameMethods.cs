using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    public class GameMethods
    {
        public static bool ValidCard(Card potentialPlay, Card currentlyShown)
        {
            if((int)potentialPlay.Value == (int)currentlyShown.Value - 1)
            {
                return true;
            }
            else if(potentialPlay.Value == Values.King && currentlyShown.Value == Values.Ace)
            {
                return true; 
            }

            return false;
        }
    }
}
