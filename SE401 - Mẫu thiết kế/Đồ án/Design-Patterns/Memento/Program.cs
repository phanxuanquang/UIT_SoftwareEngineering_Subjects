using System.Collections.Generic;

namespace Memento
{
    // Originator (Game)
    class Game
    {
        private int score;
        private int level;

        public void Play()
        {
            // Logic to play the game and update score and level
        }

        public GameMemento Save()
        {
            return new GameMemento(score, level);
        }

        public void Restore(GameMemento memento)
        {
            score = memento.Score;
            level = memento.Level;
        }
    }

    // Memento (GameMemento)
    class GameMemento
    {
        public int Score { get; }
        public int Level { get; }

        public GameMemento(int score, int level)
        {
            Score = score;
            Level = level;
        }
    }

    // Caretaker (GameManager)
    class GameManager
    {
        private Stack<GameMemento> mementoStack = new Stack<GameMemento>();

        public void SaveGame(Game game)
        {
            mementoStack.Push(game.Save());
        }

        public void Undo(Game game)
        {
            if (mementoStack.Count > 0)
            {
                var memento = mementoStack.Pop();
                game.Restore(memento);
            }
        }
    }

    // Client code
    class Program
    {
        static void Main()
        {
            Game game = new Game();
            GameManager gameManager = new GameManager();

            game.Play(); // Play the game and update score and level
            gameManager.SaveGame(game); // Save the game state

            game.Play(); // Play the game and update score and level
            game.Play(); // Play the game and update score and level
            gameManager.SaveGame(game); // Save the game state again

            game.Play(); // Play the game and update score and level

            gameManager.Undo(game); // Undo the last saved game state

            // The game is now restored to the previous state
        }
    }

}
