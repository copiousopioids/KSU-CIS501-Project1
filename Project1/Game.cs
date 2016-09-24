using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    class Game
    {
        /// <summary>
        /// An array of Player objects, each with a hand of cards.
        /// </summary>
        public List<Player> _playerList = new List<Player>();

        /// <summary>
        /// The game UI object.
        /// </summary>
        public ITerminal _gameUI = new ITerminal();

        /// <summary>
        /// The game deck.
        /// </summary>
        public Deck _deck = new Deck();

        /// <summary>
        /// Constructs a new game and populates the player list.
        /// </summary>
        public Game()
        {
            PopulatePlayerList();
        }
        /// <summary>
        /// Populates the player list with Player objects, each with a hand of cards.
        /// </summary>
        public void PopulatePlayerList()
        {
            int playerCount = _gameUI.GetNumAI() + 1;

            _playerList.Add(new HumanPlayer("User", playerCount));

            for (int i = 1; i < playerCount; i++)
            {
                _playerList.Add(new ComputerPlayer("Player" + i, playerCount));
            }
            while (_deck.CardsLeft() > 0)
            {
                for (int j = 0; j < playerCount; j++)
                {
                    if (_deck.CardsLeft() > 0)
                    {
                        _playerList[j]._hand.Add(_deck.DealCard());
                    }
                }
            }
        }

        /// <summary>
        /// Starts and runs a game.
        /// </summary>
        /// <returns>Returns true if there was a valid end to the game.</returns>
        public bool PlayGame()
        {
            Console.WriteLine("**** After the Deal ****");
            _gameUI.DisplayHands(this);
            DiscardAllPairsAtStart();
            ShuffleHands();
            
            Console.WriteLine("++++ After Discarding Pairs and Shuffling each Hand ++++");
            _gameUI.DisplayHands(this);
            Console.WriteLine("Press <Enter> to Continue...");
            Console.Read();

            bool gameEnd = false;
            while (!gameEnd)
            {
                Random rnd = new Random();

                for (int i = 0; i < _playerList.Count - 1; i++)
                {
                    if (!_playerList[i]._isFinished)
                    {
                        if (_playerList[i]._isUser)
                        {
                            int pick = GetUserPick(_playerList[1]);
                            PickUp(_playerList[0], _playerList[1], pick);
                            //Console.WriteLine("Press <Enter> to Continue...");
                            //Console.Read();
                        }
                        else
                        {
                            // Doesn't work, need to fix the indexing so it skips over any finished players.
                            PickUp(_playerList[i], _playerList[i + 1], rnd.Next(0, _playerList[i + 1]._hand.Count - 1));

                        }
                        //Console.WriteLine("Press <Enter> to Continue...");
                        //Console.Read();
                    }
                }

                if (!_playerList.Last()._isFinished && _playerList.First()._hand.Count > 0)
                {
                    PickUp(_playerList.Last(), _playerList.First(), rnd.Next(0, _playerList.First()._hand.Count - 1));
                    //Console.WriteLine("Press <Enter> to Continue...");
                    //Console.Read();
                }

                for (int i = _playerList.Count - 1; i >= 0; i--)
                {
                    if (_playerList[i]._isFinished)
                    {
                        _playerList.RemoveAt(i);
                    }
                }

                Console.WriteLine("==== After the Pick ====");
                _gameUI.DisplayHands(this);

                Console.WriteLine("@@@@ One Round has Finished; Shuffling each Hand @@@@");
                ShuffleHands();

                if (_playerList.Count <= 1)
                {
                    Console.WriteLine("Game Over. Loser is " + _playerList[0]._name + ".");
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Shuffles all player's hands.
        /// </summary>
        public void ShuffleHands()
        {
            foreach (Player p in _playerList)
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
            foreach (Player p in _playerList)
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
            
            Console.WriteLine(taker._name + " picks up " + giver._name + "'s card at [" + index + "], Card: " + pick.ToString());

            bool isFinished = taker.AddCard(pick);

            if (giver._hand.Count <= 0)
            {
                Console.WriteLine(giver._name + " is finished.");
                giver._isFinished = true;
                Console.WriteLine("Press <Enter> to Continue...");
                Console.Read();
                return true;
            }

            if (isFinished)
            {
                Console.WriteLine(taker._name + " is finished.");
                taker._isFinished = true;
                Console.WriteLine("Press <Enter> to Continue...");
                Console.Read();
                return true;
            }
            Console.WriteLine("Press <Enter> to Continue...");
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
            Console.WriteLine("******** Now, User's Turn ********");
            Console.WriteLine(_playerList[0]._name + "    : " + _playerList[0].HandToString());
            Console.WriteLine(p._name + " : " + p.HandToString());
            Console.Write("Index   :"); 
            for (int i = 0; i < p._hand.Count; i++)
            {
                Console.Write("  " + i);
            }

            Console.WriteLine();
            
            bool isValid = false;
            while (!isValid)
            {
                try {
                    Console.Write("Pick One Card from " + p._name + " : ");
                    string input = Console.ReadLine();

                    if (input != null && Convert.ToInt32(input) >= 0 && Convert.ToInt32(input) < p._hand.Count)
                    {
                        isValid = true;
                        return Convert.ToInt32(input);
                    }
                    else
                    {
                        Console.WriteLine("Input a valid value.");
                        isValid = false;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid Input.");
                    isValid = false;
                }
            }
            return 0;
        }
    }
}
