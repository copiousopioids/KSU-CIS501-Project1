using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    class Deck
    {
        /// <summary>
        /// The array holding the deck.
        /// </summary>
        private Card[] _deck;

        /// <summary>
        /// A counter to keep track of how many cards have been used.
        /// </summary>
        private int _cardsUsed;

        /// <summary>
        /// Constructs the deck.
        /// </summary>
        public Deck()
        {
            _deck = new Card[53];
            int count = 0;
            for (int suit = 0; suit <= 3; suit++)
            {
                for (int num = 1; num <= 13; num++)
                {
                    _deck[count] = new Card(num, (Card.Suit)suit);
                    count++;
                }
            }
            _deck[52] = new Card(0, Card.Suit.OldMaid);
            _cardsUsed = 0;
            Shuffle();
        }

        /// <summary>
        /// Shuffles the deck.
        /// </summary>
        public void Shuffle()
        {
            new Random().Shuffle(_deck);
            _cardsUsed = 0;
        }

        /// <summary>
        /// Calculates the number of cards left on the deck.
        /// </summary>
        /// <returns>An int giving the number of cards left.</returns>
        public int CardsLeft()
        {
            return _deck.Length - _cardsUsed;
        }

        /// <summary>
        /// Deals a card.
        /// </summary>
        /// <returns>A Card object of the next card in the deck.</returns>
        public Card DealCard()
        {
            if (_cardsUsed == _deck.Length)
            {
                throw new IndexOutOfRangeException("No cards are left in the deck.");
            }
            _cardsUsed++;
            return _deck[_cardsUsed - 1];
        }
    }
}
