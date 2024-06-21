using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacketyJackety
{
    #region public values
    // Public values 
    public enum CardSuit
    {
        Club, Spade, Diamond, Heart
    }
    public enum CardFace
    {
        Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
    }
    #endregion
    public class Card
    {
        // Set card values
        public CardSuit CardSuit { get; }
        public CardFace CardFace { get; }
        public int CardValue { get; set; }

        #region card values
        // Send card values to console
        public void CardFigures()
        {
            #region colours
            // Blacks (Now Cyans)
            if (CardSuit == CardSuit.Club)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            if (CardSuit == CardSuit.Spade)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            // Reds
            if (CardSuit == CardSuit.Heart)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (CardSuit == CardSuit.Diamond)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            #endregion
            #region information
            if (CardFace == CardFace.Ace)
            {
                if (CardValue == 11)
                {
                    Console.WriteLine("Full" + CardFace + " of " + CardSuit);
                }
                else
                {
                    Console.WriteLine("Diminuative" + CardFace + " of " + CardSuit);

                }
            }

            // Inform player of the cards they have received
            Console.WriteLine(CardFace + " of " + CardSuit);

            #endregion
        }
        #endregion
        #region card values
        public Card(CardSuit cardSuit, CardFace cardFace)
        {
            CardSuit = cardSuit;
            CardFace = cardFace;
            //CardValue = cardValue;

            // Set suit colours
            switch (CardFace)
            {
                case CardFace.Ten:
                case CardFace.Jack:
                case CardFace.Queen:
                case CardFace.King:
                    CardValue = 10;
                    break;
                case CardFace.Ace:
                    CardValue = 11;
                    break;
                default:
                    CardValue = (int)CardFace+1;
                    break;
            }
        }
        #endregion
    }
}
