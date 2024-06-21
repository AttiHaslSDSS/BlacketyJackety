using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacketyJackety
{
    public class dealer
    {
        public static List<Card> DeckCards { get; set; } = new List<Card>();
        public static List<Card> RevealedCards { get; set; } = new List<Card>();
        public static int MinimumBet { get; } = 10;
        // Remove card from deck
        public static void RevealCard()
        {
            RevealedCards.Add(DeckCards[0]);
            DeckCards.RemoveAt(0);
        }

        public static int GetHandValue()
        {
            int value = 0;
            foreach (Card card in RevealedCards)
            {
                value += card.CardValue;
            }
            return value;
        }
        // Dealer showncards
        public static void WriteHand()
        {
            Console.WriteLine("Dealer's Hand (" + GetHandValue() + "):");
            foreach (Card card in RevealedCards)
            {
                card.CardFigures();
            }
            for (int i = 0; i < DeckCards.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("<hidden>");
            }
            Console.WriteLine();
        }

        public static bool IsHandBlackjack(List<Card> hand)
        {
            if (hand.Count == 2)
            {
                if (hand[0].CardFace == CardFace.Ace && hand[1].CardValue == 10) return true;
                else if (hand[1].CardFace == CardFace.Ace && hand[0].CardValue == 10) return true;
            }
            return false;
        }

        /// <summary>
        /// Reset Console Colors to DarkGray on Black
        /// </summary>
        public static void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }

}

