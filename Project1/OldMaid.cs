using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    class OldMaid
    {
        /// <summary>
        /// An array of all players; this allows easy reusing of objects.
        /// </summary>
        //public Player[] _allPlayers;
        
        /// <summary>
        /// An array of Player objects, each with a hand of cards.
        /// </summary>
        public List<Player> _currentPlayers = new List<Player>();

        /// <summary>
        /// The game UI object.
        /// </summary>
        public ITerminal _gameUI;

        /// <summary>
        /// The game deck.
        /// </summary>
        public Deck _deck = new Deck();

        /// <summary>
        /// The total number of players in the game.
        /// </summary>
        public int _numPlayers;

        /// <summary>
        /// Constructs a new game and populates the player list.
        /// </summary>
        public OldMaid()
        {
            _gameUI = new ITerminal();
            _numPlayers = _gameUI.GetNumAI() + 1;
            //_allPlayers = new Player[_numPlayers];
            PopulatePlayerList(_numPlayers);
        }
        /// <summary>
        /// Populates the player list with Player objects, each with a hand of cards.
        /// </summary>
        public void PopulatePlayerList(int playerCount)
        {
            _currentPlayers.Add(new HumanPlayer("User", playerCount));

            for (int i = 1; i < playerCount; i++)
            {
                _currentPlayers.Add(new ComputerPlayer("Player" + i, playerCount));
            }
            while (_deck.CardsLeft() > 0)
            {
                for (int j = 0; j < playerCount; j++)
                {
                    if (_deck.CardsLeft() > 0)
                    {
                        _currentPlayers[j]._hand.Add(_deck.DealCard());
                    }
                }
            }
        }

        /// <summary>
        /// Starts and runs a game.
        /// </summary>
        /// <returns>Returns true if there was a valid end to the game.</returns>
        public void PlayGame()
        {
            bool playAgain = true;
            while (playAgain)
            {
                _gameUI.DisplayLine("**** After the Deal ****");
                _gameUI.DisplayHands(this);
                DiscardAllPairsAtStart();
                ShuffleHands();

                _gameUI.DisplayLine("++++ After Discarding Pairs and Shuffling each Hand ++++");
                _gameUI.DisplayHands(this);
                _gameUI.DisplayLine("Press <Enter> to Continue...");
                Console.Read();

                bool gameEnd = false;
                while (!gameEnd)
                {
                    Random rnd = new Random();

                    for (int i = 0; i < _currentPlayers.Count - 1; i++)
                    {
                        if (!_currentPlayers[i]._isFinished)
                        {
                            if (_currentPlayers[i].IsUser)
                            {
                                int pick = GetUserPick(_currentPlayers[1]);
                                PickUp(_currentPlayers[0], _currentPlayers[1], pick);
                                //_gameUI.DisplayLine("Press <Enter> to Continue...");
                                //Console.Read();
                            }
                            else
                            {
                                // Doesn't work, need to fix the indexing so it skips over any finished players.
                                PickUp(_currentPlayers[i], _currentPlayers[i + 1], rnd.Next(0, _currentPlayers[i + 1]._hand.Count - 1));

                            }
                            //_gameUI.DisplayLine("Press <Enter> to Continue...");
                            //Console.Read();
                        }
                    }

                    if (!_currentPlayers.Last()._isFinished && _currentPlayers.First()._hand.Count > 0)
                    {
                        PickUp(_currentPlayers.Last(), _currentPlayers.First(), rnd.Next(0, _currentPlayers.First()._hand.Count - 1));
                        //_gameUI.DisplayLine("Press <Enter> to Continue...");
                        //Console.Read();
                    }

                    for (int i = _currentPlayers.Count - 1; i >= 0; i--)
                    {
                        if (_currentPlayers[i]._isFinished)
                        {
                            _currentPlayers.RemoveAt(i);
                        }
                    }

                    _gameUI.DisplayLine("==== After the Pick ====");
                    _gameUI.DisplayHands(this);

                    _gameUI.DisplayLine("@@@@ One Round has Finished; Shuffling each Hand @@@@");
                    ShuffleHands();

                    if (_currentPlayers.Count <= 1)
                    {
                        _gameUI.DisplayLine("Game Over. Loser is " + _currentPlayers[0].Name + ".");
                        gameEnd = true;
                    }
                }
                playAgain = _gameUI.PlayAgain();
            }
        }

        /// <summary>
        /// Shuffles all player's hands.
        /// </summary>
        public void ShuffleHands()
        {
            foreach (Player p in _currentPlayers)
            {
                p.Shuffle();
            }
        }

        /// <summary>
        /// Discards all pairs in all hands. Runs through entire hand.
        /// Meant to be used right after dealing all cards.
        /// </summary>
        public void DiscardAllPairsAtStart()
        {
            foreach (Player p in _currentPlayers)
            {
                p.DiscardPairsAtStart();
            }
        }

        /// <summary>
        /// pick up the card and give it to the other player.
        /// </summary>
        /// <param name="taker">The one taking a card.</param>
        /// <param name="giver">The one giving the card.</param>
        /// <param name="index">The index of the card to take.</param>
        /// <returns>Returns true if the User has no cards left.</returns>
        public bool PickUp(Player taker, Player giver, int index)
        {
            Card pick = giver.Remove(index);
            
            _gameUI.DisplayLine(taker.Name + " picks up " + giver.Name + "'s card at [" + index + "], Card: " + pick.ToString());

            bool isFinished = taker.AddCard(pick);

            if (giver._hand.Count <= 0)
            {
                _gameUI.DisplayLine(giver.Name + " is finished.");
                giver._isFinished = true;
                _gameUI.DisplayLine("Press <Enter> to Continue...");
                Console.Read();
                return true;
            }

            if (isFinished)
            {
                _gameUI.DisplayLine(taker.Name + " is finished.");
                taker._isFinished = true;
                _gameUI.DisplayLine("Press <Enter> to Continue...");
                Console.Read();
                return true;
            }
            _gameUI.DisplayLine("Press <Enter> to Continue...");
            Console.Read();
            return false;
        }

        /// <summary>
        /// Gets the user's pick from the next player.
        /// </summary>
        /// <param name="p">The player to pick from.</param>
        /// <returns>The index value of the card to pick.</returns>
        public int GetUserPick(Player p)
        {
            _gameUI.DisplayLine("******** Now, User's Turn ********");
            _gameUI.DisplayLine(_currentPlayers[0].Name + "    : " + _currentPlayers[0].HandToString());
            _gameUI.DisplayLine(p.Name + " : " + p.HandToString());
            _gameUI.Display("Index   :"); 
            for (int i = 0; i < p._hand.Count; i++)
            {
                _gameUI.Display("  " + i);
            }

            _gameUI.DisplayLine("");
            
            bool isValid = false;
            while (!isValid)
            {
                try {
                    _gameUI.Display("Pick One Card from " + p.Name + " : ");
                    string input = Console.ReadLine();

                    if (input != null && Convert.ToInt32(input) >= 0 && Convert.ToInt32(input) < p._hand.Count)
                    {
                        isValid = true;
                        return Convert.ToInt32(input);
                    }
                    else
                    {
                        _gameUI.DisplayLine("Input a valid value.");
                        isValid = false;
                    }
                }
                catch (FormatException e)
                {
                    _gameUI.DisplayLine("Invalid Input.");
                    isValid = false;
                }
            }
            return 0;
        }
    }
}
