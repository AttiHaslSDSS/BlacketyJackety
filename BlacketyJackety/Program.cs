using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlacketyJackety
{
    public class Program
    {
        private static Deck deck = new Deck();
        private static player player = new player();
        private enum RoundResult
        {
            PUSH,
            PWIN,
            PBUST,
            PBJ,
            DWIN,
            SURRENDER,
            NOPE
        }

        static void Main(string[] args)
        {

            #region variables
            Console.Title = ("Atticus' Casino");

            Console.ForegroundColor = ConsoleColor.Green;
            //Ask the player for their name
            Console.WriteLine("Hey, welcome to my Casino, pal. What's your name?");
            // Set values such as name, age, and cash amount
            string playername = "Wattesigma";
            int age = 18;

            playername = Console.ReadLine();
            Console.ReadKey();
            Console.WriteLine("You look awful young. I'm not sure if you're old enough to play. How old are you?");
            age = int.Parse(Console.ReadLine());
            #endregion
            #region init. questions
            // Get player's age. No minors allowed! There's booze in here.
            if (age < 19)
            {
                Console.Clear();
                Console.WriteLine("Piss off, kid. Go play with your toys.");
            }
            else
            {
                Console.WriteLine("Heyyyy, welcome to the family, " + playername + ", Enjoy playing, and win big for me. Capiche?");

                //Menu options in white, dialogue in green.
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press 1 to play");
                Console.WriteLine("Press 2 to continue");
                Console.WriteLine("Press 3 to insult the owner's mother and imply he was born out of wedlock");
            }
            string MenuChoice = Console.ReadLine();

            // New Game
            switch (MenuChoice)
            {
                case "1":
                    // You sure??
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("From the bottom? You got it, bossman. You sure, though?");
                    Console.WriteLine("\n1. Yes\n2. No");
                    string answer = Console.ReadLine();
                    // Yes     
                    if (answer == "1")
                    {
                        StartRound();
                    }
                    // No
                    if (answer == "2")
                    {
                        //Send back to main menu
                        Console.Clear();
                        Main(args);
                    }
                    break;

                case "2": // TODO XML FILES
                    Console.ForegroundColor = ConsoleColor.Green;
                    // Send to location below variables
 
                    InitHand();
                    break;

                case "3":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Alright, you little prick, that does it. You think you can say that to my face? Get the Hell outta my casino.");
                    Thread.Sleep(5000);
                    Console.Clear();
                    Main(args);
                    return;

            }
            #endregion

        }
        public static void InitHand()
        {
            deck.Shuffleit();

            #region Gameplay
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Shuffling...");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Hey, what if I was cheating you? Wouldn't that be funny?");
            Console.WriteLine("Alright, alright, guess I won't joke around no more. Jackass...");
            Console.WriteLine("Done shuffling, boss.");
            Console.WriteLine("Alright, let's dish 'em out");

            player.Hand = deck.Delthand();
            dealer.DeckCards = deck.Delthand();
            dealer.RevealedCards = new List<Card>();

            // If hand contains two aces, make one Hard.
            if (player.Hand[0].CardFace == CardFace.Ace && player.Hand[1].CardFace == CardFace.Ace)
            {
                player.Hand[1].CardValue = 1;
            }

            if (dealer.DeckCards[0].CardFace == CardFace.Ace && dealer.DeckCards[1].CardFace == CardFace.Ace)
            {
                dealer.DeckCards[1].CardValue = 1;
            }

            dealer.RevealCard();

            player.WriteHand();
            dealer.WriteHand();
        }
        // Start that crap. Now the real game begins.
        static void StartRound()
        {
            deck.Shuffleit();

            Console.Clear();

            if (!TakeBet())
            {
                EndRound(RoundResult.NOPE);
                return;
            }
            Console.Clear();

            InitHand();
            TakeActions();

            dealer.RevealCard();

            Console.Clear();
            player.WriteHand();
            dealer.WriteHand();

            player.HandsCompleted++;

            if (player.Hand.Count == 0)
            {
                EndRound(RoundResult.SURRENDER);
                return;
            }
            else if (player.GetHandValue() > 21)
            {
                EndRound(RoundResult.PBUST);
                return;
            }

            while (dealer.GetHandValue() <= 16)
            {
                // Wait for a moment
                Thread.Sleep(1000);
                dealer.RevealedCards.Add(deck.DrawingCard());

                Console.Clear();
                player.WriteHand();
                dealer.WriteHand();
            }


            if (player.GetHandValue() > dealer.GetHandValue())
            {
                player.Wins++;
                if (dealer.IsHandBlackjack(player.Hand))
                {
                    EndRound(RoundResult.PBJ);
                }
                else
                {
                    EndRound(RoundResult.PWIN);
                }
            }
            // Does dealer get BJ?
            else if (dealer.GetHandValue() > 21)
            {
                player.Wins++;
                EndRound(RoundResult.PWIN);
            }
            // Does dealer's value > player's?
            else if (dealer.GetHandValue() > player.GetHandValue())
            {
                EndRound(RoundResult.DWIN);
            }
            else
            {
                EndRound(RoundResult.PUSH);
            }

        }

        static void TakeActions()
        {
            string action;
            do
            {
                Console.Clear();
                player.WriteHand();
                dealer.WriteHand();

                Console.Write("Enter Action: HIT, STAND, SURRENDER, DOUBLE");
                Console.ForegroundColor = ConsoleColor.White;
                action = Console.ReadLine();
                dealer.ResetColor();

                switch (action.ToUpper())
                {
                    case "HIT":
                        player.Hand.Add(deck.DrawingCard());
                        break;
                    case "STAND":
                        break;
                    case "SURRENDER":
                        player.Hand.Clear();
                        break;
                    case "DOUBLE":
                        if (player.Chips <= player.Bet)
                        {
                            //Chips+++
                            player.AddBet(player.Chips);
                        }
                        else
                        {
                            player.AddBet(player.Bet);
                        }
                        player.Hand.Add(deck.DrawingCard());
                        break;
                }

                if (player.GetHandValue() > 21)
                {
                    foreach (Card card in player.Hand)
                    {
                        if (card.CardValue == 11) // Only a soft ace can have a value of 11
                        {
                            card.CardValue = 1;
                            break;
                        }
                    }
                }
            } while (!action.ToUpper().Equals("STAND") && !action.ToUpper().Equals("DOUBLE")
                && !action.ToUpper().Equals("SURRENDER") && player.GetHandValue() <= 21);
        }

        static bool TakeBet()
        {
            Console.Write("Current Chip Count: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(player.Chips);
            dealer.ResetColor();

            Console.Write("Minimum Bet: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(dealer.MinimumBet);
            dealer.ResetColor();

            Console.Write("Enter bet to begin hand " + player.HandsCompleted + ": ");
            Console.ForegroundColor = ConsoleColor.White;
            string s = Console.ReadLine();
            dealer.ResetColor();

            if (Int32.TryParse(s, out int bet) && bet >= dealer.MinimumBet && player.Chips >= bet)
            {
                player.AddBet(bet);
                return true;
            }
            return false;
        }

        static void EndRound(RoundResult result)
        {
            switch (result)
            {
                case RoundResult.PUSH:
                    player.ReturnBet();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Player and Dealer Push");
                    break;
                case RoundResult.PWIN:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Heyyyy! Nice job, pal. As per my countin', you got " + player.WinBet(false) + " chips");
                    break;
                case RoundResult.PBUST:
                    player.ClearBet();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Hey, maybe next time fly a little lower, Icarus.");
                    break;
                case RoundResult.PBJ:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Hey, nice! you got" + player.WinBet(true) + " chips. Pretty sweet, eh?");
                    break;
                case RoundResult.DWIN:
                    player.ClearBet();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Dealer Wins.");
                    break;
                case RoundResult.SURRENDER:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You're givin' up" + (player.Bet / 2) + " chips, pal. You sure this is worth it?");
                    player.Chips += player.Bet / 2;
                    player.ClearBet();
                    break;
                case RoundResult.NOPE:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Yeah, nice try, buster. No chance.");
                    break;
                    // Override previous XML with new data - player's name and score.
                    //var str = new XElement("product",
                    //new XElement("score", player.Chips),
                    //new XElement("name", playername))
                    //.ToString();
            }

            if (player.Chips <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine();
                Console.WriteLine("Ha! Looks like you ran outta chips, you bum! Only took you " + (player.HandsCompleted - 1) + " round(s)! Surprised it didn't take less!");
                Console.WriteLine("Alright, well, I'm a nice guy. I'll give you some money and we can do this again. I think I've taken enough.");

                player = new player();
            }
            dealer.ResetColor();
            Console.WriteLine("Alright, chin up. Let's keep playing.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            StartRound();
        }
    }
}
#endregion