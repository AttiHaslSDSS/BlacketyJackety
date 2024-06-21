using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacketyJackety
{
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            Shuffleit();
        }

        // Shuffles deck manually - remarkably fancy (Thank you GPT)
        public List<Card> Unshuffled()
        {
            List<Card> orgDeck = new List<Card>();
            // Every card type
            for (int i = 0; i < 13; i++)
            {
                // Every card face
                for (int j = 0; j < 4; j++)
                {
                    orgDeck.Add(new Card((CardSuit)j, (CardFace)i));
                }
            }

            return orgDeck;
        }
        public void ShuffleDeck()
        {
            // New random generator
            Random random = new Random();
            int Cardcount = cards.Count;
            // Until all cards are shuffled
            while (Cardcount > 1)
            {
                Cardcount--;
                // Shuffle each card sequentially
                int CardNext = random.Next(Cardcount + 1);
                Card card = cards[CardNext];
                cards[CardNext] = cards[Cardcount];
                cards[Cardcount] = card;
                // I LOVE AUTOFILL-CODE. I <3 PRESSING TAB
            }
        }
        public void Shuffleit()
        {
            cards = Unshuffled();
            ShuffleDeck();
        }

        public List<Card> Delthand()
        {
            // Altered version of Mr. T's temp. list, top 2 cards code
            List<Card> hand = new List<Card>();
            hand.Add(cards[0]);
            hand.Add(cards[1]);
            // REMOVE THEM
            cards.RemoveRange(0, 2);
            return hand;

        }
        public Card DrawingCard()
        {
            Card card = cards[0];
            cards.Remove(card);
            return card;
        }
    }

}
