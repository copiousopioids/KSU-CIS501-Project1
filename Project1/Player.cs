using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    class Player
    {
        /// <summary>
        /// The name of the player.
        /// </summary>
        public string _name = "";

        /// <summary>
        /// An array of Card objects forming the Player's hand.
        /// </summary>
        public List<Card> _hand;

        /// <summary>
        /// Determines whether the given player has used all their cards.
        /// </summary>
        public bool _isFinished = false;

        /// <summary>
        /// Determines whether the player is the user.
        /// </summary>
        public bool _isUser;


        /// <summary>
        /// Constructs a new player.
        /// </summary>
        public Player()
        {
            _hand = new List<Card>();
        }

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
                            if (_hand[i].Value == _hand[j].Value)
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
        /// Checks if the given card has a pair in the hand.
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
                if (c.Value == _hand[i].Value)
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
    }

    class HumanPlayer : Player
    {
        private /*override*/ void Deal(Card card)
        {
           
        }
    }

    class ComputerPlayer : Player
    {

    }
}
