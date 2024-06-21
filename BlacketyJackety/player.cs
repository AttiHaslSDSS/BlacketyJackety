using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacketyJackety
{
    public class player
    {

        public int Chips { get; set; } = 100;
        public int Bet { get; set; }
        public int Wins { get; set; }
        public int HandsCompleted { get; set; } = 1;

        public List<Card> Hand { get; set; }

        /// <summary>
        /// Add Player's chips to their bet.
        /// </summary>
        /// <param name="bet">The number of Chips to bet</param>
        public void AddBet(int bet)
        {
            Bet += bet;
            Chips -= bet;
        }

        /// <summary>
        /// Set Bet to 0
        /// </summary>
        public void ClearBet()
        {
            Bet = 0;
        }

        // Cancel bet
        public void ReturnBet()
        {
            Chips += Bet;
            ClearBet();
        }
        // If win
        public int WinBet(bool blackjack)
        {
            int chipsWon;
            if (blackjack)
            {
                // TRIPLE CHIPS
                chipsWon = Bet * 3;
            }
            else
            {
                //Double chips
                chipsWon = Bet * 2;
            }

            Chips += chipsWon;
            ClearBet();
            return chipsWon;
        }

        // Total hand value
        public int GetHandValue()
        {
            int value = 0;
            foreach (Card card in Hand)
            {
                value += card.CardValue;
            }
            return value;
        }
        // Display values
        public void WriteHand()
        {
            // Write Bet, Chip, Win, Amount, write Round number
            Console.Write("Bet: ");
            Console.Write(Bet + "  ");
            Console.Write("Chips: ");
            Console.Write(Chips + "  ");
            Console.Write("Wins: ");
            Console.WriteLine(Wins);
            Console.WriteLine("Round #" + HandsCompleted);

            Console.WriteLine();
            Console.WriteLine("Hand (" + GetHandValue() + "):");
            foreach (Card card in Hand)
            {
                card.CardFigures();
            }
            Console.WriteLine();
        }
    }
}

