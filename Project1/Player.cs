using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Project_1
{
    abstract class Player
    {
        /// <summary>
        /// The name of the player.
        /// </summary>
        private string _name = "";

        /// <summary>
        /// Array of card objects.
        /// </summary>
        private Card[] _handArray;

        /// <summary>
        /// A static temporary array of 
        /// </summary>
        public static Card[] _tempCards = new Card[14];

        /// <summary>
        /// Determines whether the given player has used all their cards.
        /// </summary>
        public bool _isFinished = false;

        /// <summary>
        /// Determines whether the player is the user.
        /// </summary>
        private bool _isUser;

        /// <summary>
        /// The index of the top card in the array.
        /// </summary>
        public int _topIndex;

        /// <summary>
        /// Public property for hand array.
        /// </summary>
        public Card[] Hand
        {
            get
            {
                return _handArray;
            }
            
            set
            {
                _handArray = value;
            }
        }

        /// <summary>
        /// Public property for Name.
        /// </summary>
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Public property for _isUser.
        /// </summary>
        public bool IsUser
        {
            get
            {
                return _isUser;
            }
            set
            {
                _isUser = value;
            }
        }


        /// <summary>
        /// Constructs a new player.
        /// </summary>
        public Player()
        {
        
        }

        /// <summary>
        /// Converts the Card object list into a string list, and joins that into one string for output.
        /// </summary>
        /// <returns>returns the string representing the cards in the given player's hand.</returns>
        public string HandToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Card c in _handArray)
            {
                if (c != null)
                    sb.Append(c.ToString() + " ");
            }
            string toReturn = sb.ToString();
            sb.Clear();
            return toReturn;
        }

        /// <summary>
        /// Shuffles the player's hand.
        /// </summary>
        public void Shuffle()
        {
            Randomizer.KnuthShuffle(_handArray, 0, _topIndex);
        }

        /// <summary>
        /// Discards all pairs in the player's hand.
        /// </summary>
        public void DiscardAllPairs()
        {
            for (int c = 0; c < _handArray.Length; c++)
            {
                if (_handArray[c] != null)
                {
                    int j = (int)_handArray[c].Rank;
                    if (_tempCards[j] != null)
                    {
                        Deck.ReturnCard(_tempCards[j]);
                        _tempCards[j] = null;
                        Deck.ReturnCard(_handArray[c]);
                        _handArray[c] = null;
                    }
                    else
                    {
                        _tempCards[j] = _handArray[c];
                        _handArray[c] = null;

                    }
                }
            }

            int count = 0;
            for (int i = 0; i < _tempCards.Length; i++)
            {
                if (_tempCards[i] != null)
                {
                    _handArray[count++] = _tempCards[i];
                    _tempCards[i] = null;
                }
            }

            _topIndex = count - 1;
        }

        /// <summary>
        /// Checks for duplicates before adding the given card into the player's hand.
        /// </summary>
        /// <param name="card">the card to add.</param>
        public void NewAddCard(Card card)
        {
            bool isDuplicate = false;
            for (int i = 0; i <= _topIndex; i++)
            {
                if (_handArray[i].Rank == card.Rank)
                {
                    if (_topIndex == i)
                    {
                        Deck.ReturnCard(_handArray[_topIndex]);
                        Deck.ReturnCard(card);
                        _handArray[_topIndex] = null;
                        _topIndex--;
                    }
                    else
                    {
                        Deck.ReturnCard(_handArray[i]);
                        Deck.ReturnCard(card);
                        _handArray[i] = _handArray[_topIndex];
                        _handArray[_topIndex] = null;
                        _topIndex--;
                    }
                    isDuplicate = true;
                    break;
                }
            }
            if (!isDuplicate)
            {
                _handArray[++_topIndex] = card;
            }
        }

        /// <summary>
        /// Picks a card from this player.
        /// </summary>
        /// <param name="i">The index of the card to pick.</param>
        /// <returns>Returns the Card object at the index specified.</returns>
        public Card PickCardAt(int i)
        {
            if (i >= 0 && i <= _topIndex)
            {
                Card toReturn = _handArray[i];
                _handArray[i] = _handArray[_topIndex];
                _handArray[_topIndex--] = null;
                return toReturn;
            }
            else
            {
                throw new IndexOutOfRangeException("index is not within the range of the hand");
            }
        }

        /// <summary>
        /// Returns player's hands back to the deck array.
        /// </summary>
        public void ReturnHandToDeck()
        {
            for (int i = 0; i < _handArray.Length; i++)
            {
                if (_handArray[i] != null)
                {
                    Deck.ReturnCard(_handArray[i]);
                    _handArray[i] = null;
                }
            }
        }

        public abstract void Deal(Card card);
    }

    /// <summary>
    /// The HumanPlayer subclass.
    /// </summary>
    class HumanPlayer : Player
    {
        public HumanPlayer(string name, int numPlayers)
        {
            IsUser = true;
            Hand = new Card[((53 / numPlayers) + 2)];
            _topIndex = -1;
            Name = name;
        }

        public override void Deal(Card card)
        {
            card.FaceUp = true;
            Hand[++_topIndex] = card;

        }
    }

    /// <summary>
    /// The Computer player subclass.
    /// </summary>
    class ComputerPlayer : Player
    {
        public ComputerPlayer(string name, int numPlayers)
        {
            IsUser = false;
            Hand = new Card[((53 / numPlayers) + 2)];
            _topIndex = 0;
            Name = name;
        }

        public override void Deal(Card card)
        {
#if DEBUG
            card.FaceUp = true;
#else
            card.FaceUp = false;
#endif
            Hand[++_topIndex] = card;
        }
    }
}
