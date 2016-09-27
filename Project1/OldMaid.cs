
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
        public Player[] _allPlayers;
        
        /// <summary>
        /// An array of Player objects, each with a hand of cards.
        /// </summary>
        public List<Player> _currentPlayers = new List<Player>();

        /// <summary>
        /// The game UI object.
        /// </summary>
        public ConsoleTerminal _gameUI = new ConsoleTerminal();

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
            _gameUI = new ConsoleTerminal();
            _numPlayers = _gameUI.GetInt("Input Number of Computer Players ", 2, 6) + 1;
            _allPlayers = new Player[_numPlayers];
            PopulatePlayerList(_numPlayers);
        }

        /// <summary>
        /// Populates the player list with Player objects, each with a hand of cards.
        /// </summary>
        public void PopulatePlayerList(int playerCount)
        {
            _allPlayers = new Player[playerCount];

            _allPlayers[0] = new HumanPlayer("User", playerCount);

            for (int i = 1; i < playerCount; i++)
            {
                _allPlayers[i] = new ComputerPlayer("Player" + i, playerCount);
            }

            for (int i = 0; i < playerCount; i++)
            {
                _currentPlayers.Add(_allPlayers[i]);
            }

            while (Deck._deckIndex >= 0)
            {
                for (int i = 0; i < playerCount; i++)
                {
                    _currentPlayers[i].Deal(_deck.Draw());
                    if (Deck._deckIndex < 0)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Resets the game with the given player count.
        /// </summary>
        private void ResetGame()
        {
            foreach (Player p in _currentPlayers)
            {
                p.ReturnHandToDeck();
            }

            _currentPlayers.Clear();

            for (int i = 0; i < _numPlayers; i++)
            {
                _currentPlayers.Add(_allPlayers[i]);
            }

            while (Deck._deckIndex >= 0)
            {
                for (int i = 0; i < _numPlayers; i++)
                {
                    _currentPlayers[i].Deal(_deck.Draw());
                    if (Deck._deckIndex < 0)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Determines if the player would like to play again.
        /// </summary>
        /// <returns>A bool indicating whether the user would like to play again.</returns>
        public bool PlayAgain()
        {
            bool valid = false;
            while (!valid)
            {
                Console.Write("Do you want to play again? (Y/N)");
                char answer = _gameUI.GetChar("Do you want to play again? (Y/N)", "YNyn");
                if (answer == 'Y')
                {
                    valid = true;
                    return true;
                }
                else if (answer == 'N')
                {
                    valid = true;
                    return false;
                }
                else
                {
                    valid = false;
                    Console.WriteLine("Invalid Response.");
                }
            }
            return false;
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
                DisplayHands(this);

                DiscardAllPairsAtStart();
                ShuffleHands();
                _gameUI.DisplayLine("++++ After Discarding Pairs and Shuffling each Hand ++++");
                DisplayHands(this);
                _gameUI.GetString("Press <Enter> to Continue...", 0);

                bool gameEnd = false;
                while (!gameEnd)
                {
                    Random rnd = new Random();

                    for (int drawer = 0; drawer < _currentPlayers.Count; drawer++)
                    {
                        if (_currentPlayers[drawer] != null)
                        {
                            int drawee = (drawer + 1) % _currentPlayers.Count;
                            int pick = rnd.Next(0, _currentPlayers[drawee]._topIndex + 1);

                            if (_currentPlayers[drawer].IsUser)
                            {
                                _gameUI.DisplayLine("\n******** Now, User's Turn ********");
                                _gameUI.DisplayLine(_currentPlayers[drawer].Name + "\t : " + _currentPlayers[drawer].HandToString());
                                _gameUI.DisplayLine(_currentPlayers[drawee].Name + "\t : " + _currentPlayers[drawee].HandToString());
                                _gameUI.Display("Index \t :");
                                for (int i = 0; i <= _currentPlayers[drawee]._topIndex; i++)
                                {
                                    _gameUI.Display("  " + i);
                                }

                                string prompt = "\nPick one card from " + _currentPlayers[drawee].Name + " : ";
                                pick = _gameUI.GetInt(prompt, 0, _currentPlayers[drawee]._topIndex);
                            }



                            if (_currentPlayers[drawer]._topIndex >= 0)
                            {
                                while (_currentPlayers[drawee]._topIndex < 0)
                                {
                                    drawee++;
                                }
                                if (drawee == drawer)
                                {
                                    gameEnd = true;
                                    break;
                                }

                                _gameUI.DisplayLine("\n" + _currentPlayers[drawer].Name + " picks up " + _currentPlayers[drawee].Name + "'s card at [" + pick + "], Card: " + _currentPlayers[drawee].Hand[pick].ToString());
                                _currentPlayers[drawer].NewAddCard(_currentPlayers[drawee].PickCardAt(pick));

                                if (_currentPlayers[drawee]._topIndex < 0)
                                {
                                    _gameUI.DisplayLine(_currentPlayers[drawee].Name + " is finished.");
                                }

                                if (_currentPlayers[drawer]._topIndex < 0)
                                {
                                    _gameUI.DisplayLine(_currentPlayers[drawer].Name + " is finished.");
                                }
                            }
                            // Fix this bit.
                            //else throw new Exception("Drawer does not have any cards in their hand.");

                            _gameUI.GetString("Press <Enter> to Continue...", 0);
                        }
                    }

                    for (int i = _currentPlayers.Count - 1; i >= 0; i--)
                    {
                        if (_currentPlayers[i]._topIndex < 0)
                        {
                            _gameUI.DisplayLine(_currentPlayers[i].Name + " is finished. Removing from currentPlayers...");
                            _currentPlayers.RemoveAt(i);
                        }
                    }
                    
                    //for (int i = 0; i < _currentPlayers.Count - 1; i++)
                    //{
                    //    if (!_currentPlayers[i]._isFinished)
                    //    {
                    //        if (_currentPlayers[i].IsUser)
                    //        {
                    //            int pick = GetUserPick(_currentPlayers[1]);
                    //            PickUp(_currentPlayers[0], _currentPlayers[1], pick);
                    //            //_gameUI.DisplayLine("Press <Enter> to Continue...");
                    //            //Console.Read();
                    //        }
                    //        else
                    //        {
                    //            // Doesn't work, need to fix the indexing so it skips over any finished players.
                    //            PickUp(_currentPlayers[i], _currentPlayers[i + 1], rnd.Next(0, _currentPlayers[i + 1]._hand.Count - 1));

                    //        }
                    //        //_gameUI.DisplayLine("Press <Enter> to Continue...");
                    //        //Console.Read();
                    //    }
                    //}

                    //if (!_currentPlayers.Last()._isFinished && _currentPlayers.First()._hand.Count > 0)
                    //{
                    //    PickUp(_currentPlayers.Last(), _currentPlayers.First(), rnd.Next(0, _currentPlayers.First()._hand.Count - 1));
                    //    //_gameUI.DisplayLine("Press <Enter> to Continue...");
                    //    //Console.Read();
                    //}

                    //for (int i = _currentPlayers.Count - 1; i >= 0; i--)
                    //{
                    //    if (_currentPlayers[i]._isFinished)
                    //    {
                    //        _currentPlayers.RemoveAt(i);
                    //    }
                    //}

                    _gameUI.DisplayLine("\n==== After the Pick ====");
                    DisplayHands(this);

                    _gameUI.DisplayLine("\n@@@@ One Round has Finished; Shuffling each Hand @@@@");
                    ShuffleHands();

                    if (_currentPlayers.Count <= 1)
                    {
                        _gameUI.DisplayLine("\nGame Over. Loser is " + _currentPlayers[0].Name + ".");
                        gameEnd = true;
                    }
                }
                playAgain = PlayAgain();
                if (playAgain)
                {
                    ResetGame();
                }
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
                p.DiscardAllPairs();
            }
        }

        /// <summary>
        /// Displays all the player's hands.
        /// </summary>
        /// <param name="game"></param>
        public void DisplayHands(OldMaid game)
        {
            foreach (Player p in _currentPlayers)
            {
                _gameUI.DisplayLine(p.Name + "\t: " + p.HandToString());
            }

            //List<string> handList = new List<string>();
            //foreach (Player p in game._playerList)
            //{
            //    if (p != null && !p._isFinished)
            //    {
            //        handList.Add(p.HandToString());
            //    }
            //}
            //Console.WriteLine(game._playerList[0]._name + "    : " + handList[0]);
            //for (int i = 1; i < handList.Count; i++)
            //{
            //    Console.WriteLine(game._playerList[i]._name + " : " + handList[i]);
            //}
        }

        ///// <summary>
        ///// pick up the card and give it to the other player.
        ///// </summary>
        ///// <param name="taker">The one taking a card.</param>
        ///// <param name="giver">The one giving the card.</param>
        ///// <param name="index">The index of the card to take.</param>
        ///// <returns>Returns true if the User has no cards left.</returns>
        //private bool PickUp(Player taker, Player giver, int index)
        //{
        //    Card pick = giver.Remove(index);

        //    _gameUI.DisplayLine(taker.Name + " picks up " + giver.Name + "'s card at [" + index + "], Card: " + pick.ToString());

        //    bool isFinished = taker.AddCard(pick);

        //    if (giver._hand.Count <= 0)
        //    {
        //        _gameUI.DisplayLine(giver.Name + " is finished.");
        //        giver._isFinished = true;
        //        _gameUI.DisplayLine("Press <Enter> to Continue...");
        //        Console.Read();
        //        return true;
        //    }

        //    if (isFinished)
        //    {
        //        _gameUI.DisplayLine(taker.Name + " is finished.");
        //        taker._isFinished = true;
        //        _gameUI.DisplayLine("Press <Enter> to Continue...");
        //        Console.Read();
        //        return true;
        //    }
        //    _gameUI.DisplayLine("Press <Enter> to Continue...");
        //    Console.Read();
        //    return false;
        //}

        /// <summary>
        /// Gets the user's pick from the next player.
        /// </summary>
        /// <param name="p">The player to pick from.</param>
        /// <returns>The index value of the card to pick.</returns>
        //public int GetUserPick(Player p)
        //{
        //    _gameUI.DisplayLine("******** Now, User's Turn ********");
        //    _gameUI.DisplayLine(_currentPlayers[0].Name + "    : " + _currentPlayers[0].HandToString());
        //    _gameUI.DisplayLine(p.Name + " : " + p.HandToString());
        //    _gameUI.Display("Index   :"); 
        //    for (int i = 0; i < p._hand.Count; i++)
        //    {
        //        _gameUI.Display("  " + i);
        //    }

        //    _gameUI.DisplayLine("");

        //    bool isValid = false;
        //    while (!isValid)
        //    {
        //        try {
        //            _gameUI.Display("Pick One Card from " + p.Name + " : ");
        //            string input = Console.ReadLine();

        //            if (input != null && Convert.ToInt32(input) >= 0 && Convert.ToInt32(input) < p._hand.Count)
        //            {
        //                isValid = true;
        //                return Convert.ToInt32(input);
        //            }
        //            else
        //            {
        //                _gameUI.DisplayLine("Input a valid value.");
        //                isValid = false;
        //            }
        //        }
        //        catch (FormatException e)
        //        {
        //            _gameUI.DisplayLine("Invalid Input.");
        //            isValid = false;
        //        }
        //    }
        //    return 0;
        //}
    }
}
