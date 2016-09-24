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
        public static Card[] _deck;

        /// <summary>
        /// The top index of _deck.
        /// </summary>
        public static int _deckIndex;

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
            _deckIndex = _deck.Length - 1;
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

        /// <summary>
        /// Draws a card from the deck.
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
            Card toReturn;
            if (_deckIndex >= 0)
            {
                toReturn = _deck[_deckIndex];
                _deck[_deckIndex--] = null;
                return toReturn;
            }
            else
            {
                _deckIndex = -1;
                throw new Exception("Deck is empty; nothing can be drawn. Are you checking for an empty deck before calling this command?");
            }
        }

        /// <summary>
        /// Returns a card to the top of the deck.
        /// </summary>
        /// <param name="card">the card to be returned.</param>
        public static void ReturnCard(Card card)
        {
            if (_deckIndex - 1 != _deck.Length)
            {
                _deck[_deckIndex++] = card;
            }
            else
            {
                throw new Exception("Deck is full; nothing can be added. Are you duplicating a card instance?");
            }
        }
    }
}
