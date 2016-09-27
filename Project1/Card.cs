using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    public class Card
    {
        /// <summary>
        /// Constant int values enumerating the non-numeric cards.
        /// </summary>
        public const int ACE = 1, JACK = 11, QUEEN = 12, KING = 13;
        static readonly string[] convertValToString = { "M", "A", "2", "3", "4", "5", "6", "7", "8",
                                                        "9", "0", "J", "Q", "K" };

        /// <summary>
        /// Enumerating the suits.
        /// </summary>
        public enum Suit { Spades, Hearts, Diamonds, Clubs, OldMaid };
        static readonly string[] convertSuitToString = { "S", "H", "D", "C", "O" };

        /// <summary>
        /// This card's suit.
        /// </summary>
        private Suit _suit;

        /// <summary>
        /// This card's value.
        /// </summary>
        private int _value;

        /// <summary>
        /// Indicates whether the card is face up or down.
        /// </summary>
        private bool _faceUp;

        /// <summary>
        /// Default card constructor. Default card is Ace of Spades.
        /// </summary>
        public Card()
        {
            _suit = Suit.Spades;
            _value = ACE;
            _faceUp = true;
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
                if ((int)value >= 0 && (int)value <= 4)
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
        public int Rank
        {
            get
            {
                return _value;
            }
            set
            {
                if (value >= 0 && value <= 13)
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
        /// The boolean parameter setting the orientation of the card.
        /// _faceUp = true:  ToString() = "suit_value"
        /// _faceUp = fale:  ToString() = "XX"
        /// </summary>
        public bool FaceUp
        {
            get
            {
                return _faceUp;
            }

            set
            {
                _faceUp = value;
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
            if (thisSuit != Suit.OldMaid && (thisVal < 0 || thisVal > 13))
            {
                throw new IndexOutOfRangeException("The value is not valid.");
            }
            this.Rank = thisVal;
            this.CardSuit = thisSuit;
            this.FaceUp = true;
        }

        /// <summary>
        /// Constructs the card given valid values, and throws an exception otherwise.
        /// </summary>
        /// <param name="thisVal">This card's value.</param>
        /// <param name="thisSuit">This card's suit.</param>
        public Card(int thisVal, Suit thisSuit, bool faceUp)
        {
            if ((int)thisSuit < 0 || (int)thisSuit > 4)
            {
                throw new IndexOutOfRangeException("The suit is not valid.");
            }
            if (thisSuit != Suit.OldMaid && (thisVal < 0 || thisVal > 13))
            {
                throw new IndexOutOfRangeException("The value is not valid.");
            }
            this.Rank = thisVal;
            this.CardSuit = thisSuit;
            this.FaceUp = faceUp;
        }

        /// <summary>
        /// Gets the suit as a string value.
        /// </summary>
        /// <returns>Returns the initials of the card's string.</returns>
        private string SuitToString()
        {
            return convertSuitToString[(int)CardSuit];
        }

        /// <summary>
        /// Returns the value as a string.
        /// </summary>
        /// <returns>Returns the numerical value of the card</returns>
        private string ValueToString()
        {
            return convertValToString[Rank];
        }

        /// <summary>
        /// Overrides default ToString to give a shorthand value and suit of each card.
        /// </summary>
        /// <returns>Returns a two-character representation of the card (e.g. "SA" for Ace of Spades).</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (_faceUp == true)
            {
                sb.Append(SuitToString());
                sb.Append(ValueToString());
            }
            else
                sb.Append("XX");
            return sb.ToString();
        }
    }
} // end class Card
