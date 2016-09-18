using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_0
{
    public class Card
    {
        /// <summary>
        /// Constant int values enumerating the non-numeric cards.
        /// </summary>
        public const int ACE = 1, JACK = 11, QUEEN = 12, KING = 13;

        /// <summary>
        /// Enumerating the suits.
        /// </summary>
        public enum Suit { SPADES, HEARTS, DIAMONDS, CLUBS, OLDMAID };

        /// <summary>
        /// This card's suit.
        /// </summary>
        private Suit _suit;

        /// <summary>
        /// This card's value.
        /// </summary>
        private int _value;

        /// <summary>
        /// Default card constructor. Default card is Ace of Spades.
        /// </summary>
        public Card()
        {
            _suit = Suit.SPADES;
            _value = ACE;
        }

        /// <summary>
        /// Public getter/setter for this card's suit parameter.
        /// </summary>
        public Suit CardSuit
        {
            get
            {
                return _suit;
            }
            set
            {
                if ((int)value > 0 && (int)value < 4)
                {
                    _suit = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Invalid Suit.");
                }
            }
        }

        /// <summary>
        /// Public getter/setter for this card's value parameter.
        /// </summary>
        public int CardValue
        {
            get
            {
                return _value;
            }
            set
            {
                if (value > 0 && value < 13)
                {
                    _value = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Invalid card value.");
                }
            }
        }

        /// <summary>
        /// Constructs the card given valid values, and throws an exception otherwise.
        /// </summary>
        /// <param name="thisVal">This card's value.</param>
        /// <param name="thisSuit">This card's suit.</param>
        public Card(int thisVal, Suit thisSuit)
        {
            if ((int)thisSuit < 0 || (int)thisSuit > 4)
            {
                throw new IndexOutOfRangeException("The suit is not valid.");
            }
            if (thisSuit != Suit.OLDMAID && (thisVal < 0 || thisVal > 13))
            {
                throw new IndexOutOfRangeException("The value is not valid.");
            }
            _value = thisVal;
            _suit = thisSuit;
        }

        /// <summary>
        /// Gets the suit as a string value.
        /// </summary>
        /// <returns>Returns the initials of the card's string.</returns>
        public string suitToString()
        {
            switch(_suit)
            {
                case Suit.SPADES:
                    return "S";
                case Suit.HEARTS:
                    return "H";
                case Suit.DIAMONDS:
                    return "D";
                case Suit.CLUBS:
                    return "C";
                default:
                    return "OM";
            }
        }

        /// <summary>
        /// Returns the value as a string.
        /// </summary>
        /// <returns>Returns the numerical value of the card</returns>
        public string valueToString()
        {
            if (_suit == Suit.OLDMAID)
            {
                return "";
            }
            else
            {
                switch (_value)
                {
                    case 1:
                        return "A";
                    case 2:
                        return "2";
                    case 3:
                        return "3";
                    case 4:
                        return "4";
                    case 5:
                        return "5";
                    case 6:
                        return "6";
                    case 7:
                        return "7";
                    case 8:
                        return "8";
                    case 9:
                        return "9";
                    case 10:
                        return "0";
                    case 11:
                        return "J";
                    case 12:
                        return "Q";
                    default:
                        return "K";
                }
            }
        }

        /// <summary>
        /// Overrides default ToString to give a shorthand value and suit of each card.
        /// </summary>
        /// <returns>Returns a two-character representation of the card (e.g. "SA" for Ace of Spades).</returns>
        public override string ToString()
        {
            return suitToString() + valueToString();
        }
    }
} // end class Card
