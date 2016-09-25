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
        /// An array of Card objects forming the Player's hand.
        /// </summary>
        public List<Card> _hand;

        /// <summary>
        /// Change _handArray to _hand
        /// Change NewAddCard to AddCard
        /// </summary>
        private Card[] _handArray = new Card[13];

        /// <summary>
        /// A static temporary array of 
        /// </summary>
        public static Card[] _tempCards;

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
            _hand = new List<Card>();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="numPlayers"></param>
        //public Player(int numPlayers)
        //{
        //    _handArray = new Card[(53 / numPlayers + 2)];
        //    _topIndex = _handArray.Length - 1;
        //}

        /// <summary>
        /// Constructs a new player with a name.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="isUser">Determines if this player is the user.</param>
        public Player(string name, bool isUser)
        {
            _name = name;
            _hand = new List<Card>();
            _isUser = isUser;
        }

        /// <summary>
        /// Converts the Card object list into a string list, and joins that into one string for output.
        /// </summary>
        /// <returns>returns the string representing the cards in the given player's hand.</returns>
        public string HandToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Card c in _hand)
            {
                sb.Append(c.ToString() + " ");
            }
            string toReturn = sb.ToString();
            sb.Clear();
            return toReturn;
            //List<string> stringHandList = new List<string>();
            //for (int i = 0; i < _hand.Count; i++)
            //{
            //    stringHandList.Add(_hand[i].ToString());
            //}
            //return string.Join(" ", stringHandList.ToArray());
        }

        /// <summary>
        /// Shuffles the player's hand.
        /// </summary>
        public void Shuffle()
        {
            new Random().Shuffle(_hand);
        }

        /// <summary>
        /// Discards all pairs in a player's hand. Designed for use at the beginning of the program.
        /// </summary>
        public void DiscardPairsAtStart()
        {
            bool[] markedForDiscard = new bool[_hand.Count];
            for (int i = 0; i < _hand.Count; i++)
            {
                if (!markedForDiscard[i])
                {
                    for (int j = 0; j < _hand.Count; j++)
                    {
                        if (_hand[i] != _hand[j] && !markedForDiscard[i] && !markedForDiscard[j])
                        {
                            if (_hand[i].Rank == _hand[j].Rank)
                            {
                                markedForDiscard[i] = true;
                                markedForDiscard[j] = true;
                            }
                        }
                    }
                }
            }

            for (int k = _hand.Count - 1; k >= 0; k--)
            {
                if (markedForDiscard[k] == true)
                {
                    _hand.RemoveAt(k);
                }
            }
        }

        /// <summary>
        /// Discards all pairs in the player's hand.
        /// </summary>
        public void DiscardAllPairs()
        {
            for (int c = 0; c < _handArray.Length; c++)
            {
                int j = (int)_handArray[c].Rank;
                if (_tempCards[j] == null)
                {
                    _tempCards[j] = _handArray[c];
                }
                else
                {
                    Deck.ReturnCard(_tempCards[j]);
                    _tempCards[j] = null;
                    Deck.ReturnCard(_handArray[c]);
                    _handArray[c] = null;
                }
            }

            int count = 0;
            for (int i = 0; i < _tempCards.Length; i++)
            {
                if (_tempCards[i] != null)
                {
                    count++;
                }
            }

            _handArray = new Card[count];

            // Better way to do this so I don't have to use two for loops?
            // Could possibly double efficiency.
            count = 0;
            for (int i = 0; i < _tempCards.Length; i++)
            {
                if (_tempCards[i] != null)
                {
                    _handArray[count++] = _tempCards[i];
                    _tempCards[i] = null;
                }
            }
        }

        /// <summary>
        /// OLD Checks if the given card has a pair in the hand.
        /// Returns true if their hand is empty.
        /// Otherwise, returns false.
        /// </summary>
        /// <param name="c">The card to add/match.</param>
        /// <returns>Returns true if their hand is empty. Returns false otherwise.</returns>
        public bool AddCard(Card c)
        {
            bool containedCard = false;
            for (int i = _hand.Count - 1; i >= 0; i--)
            {
                if (c.Rank == _hand[i].Rank)
                {
                    _hand.RemoveAt(i);
                    containedCard = true;
                    if (_hand.Count <= 0)
                    {
                        return true;
                    }
                }
            }
            if (!containedCard)
            {
                _hand.Add(c);
            }
            return false;
        }

        /// <summary>
        /// Checks for duplicates before adding the given card into the player's hand.
        /// </summary>
        /// <param name="card">the card to add.</param>
        public void NewAddCard(Card card)
        {
            bool isDuplicate = false;
            for (int i = _handArray.Length - 1; i >= 0; i--)
            {
                if (_handArray[i].Rank == card.Rank)
                {
                    Card iCard = _handArray[i];
                    _handArray[i] = _handArray[_topIndex--];
                    //Add cards back into Deck? How?
                    Deck.ReturnCard(iCard);
                    Deck.ReturnCard(card);
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
            if (i > 0 && i <= _topIndex)
            {
                Card toReturn = _handArray[i];
                _handArray[i] = _handArray[_topIndex--];
                return toReturn;
            }
            else
            {
                throw new IndexOutOfRangeException("index is not within the range of the hand");
            }

        }

        /// <summary>
        /// Removes a card at the given index.
        /// </summary>
        /// <param name="index">The index of the card to remove.</param>
        /// <returns>A Card value of the card removed.</returns>
        public Card Remove(int index)
        {
            Card toReturn = _hand[index];
            _hand.RemoveAt(index);
            return toReturn;
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
            Hand = new Card[(53 / numPlayers + 2)];
            _topIndex = Hand.Length - 1;
            Name = name;
        }

        public override void Deal(Card card)
        {
            card.FaceUp = true;
            _hand[++_topIndex] = card;

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
            Hand = new Card[(53 / numPlayers + 2)];
            _topIndex = Hand.Length - 1;
            Name = name;
        }

        public override void Deal(Card card)
        {
#if DEBUG
            card.FaceUp = true;
#else
            card.FaceUp = false;
#endif
            _hand[++_topIndex] = card;
        }
    }
}
