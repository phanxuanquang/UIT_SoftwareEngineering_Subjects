using System;
using System.Collections.Generic;
using System.Numerics;

namespace Command
{
    // Định nghĩa enum KeyCode để đại diện cho các phím.
    public enum KeyCode
    {
        UpArrow,
        DownArrow,
        LeftArrow,
        RightArrow
    }

    // Interface ICommand định nghĩa phương thức Execute và Undo để thực thi và hoàn tác lệnh.
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    // Lớp MoveCommand triển khai ICommand và thực hiện lệnh di chuyển.
    public class MoveCommand : ICommand
    {
        private Player player;
        private Vector2 direction;
        private Vector2 previousPosition;

        public MoveCommand(Player player, Vector2 direction)
        {
            this.player = player;
            this.direction = direction;
        }

        public void Execute()
        {
            previousPosition = player.Position;
            player.Move(direction);
        }

        public void Undo()
        {
            player.Move(previousPosition - player.Position);
        }
    }

    // Lớp Player biểu diễn nhân vật trong game.
    public class Player
    {
        public Vector2 Position { get; private set; }

        public void Move(Vector2 direction)
        {
            Position += direction;
            Console.WriteLine("Player moved to position: " + Position);
        }
    }

    // Lớp InputHandler xử lý các lệnh từ người chơi và hỗ trợ chức năng Undo.
    public class InputHandler
    {
        private Stack<ICommand> commandHistory;

        public InputHandler()
        {
            commandHistory = new Stack<ICommand>();
        }

        public void SetCommand(KeyCode key, ICommand command)
        {
            commandHistory.Push(command);
        }

        public void HandleInput(KeyCode key)
        {
            if (commandHistory.Count > 0)
            {
                ICommand command = commandHistory.Pop();
                command.Execute();
            }
        }

        public void UndoLastCommand()
        {
            if (commandHistory.Count > 0)
            {
                ICommand command = commandHistory.Peek();
                command.Undo();
            }
        }
    }

    // Ví dụ sử dụng mẫu Command trong game.
    public class Game
    {
        public static void Main(string[] args)
        {
            Player player = new Player();
            InputHandler inputHandler = new InputHandler();

            // Đăng ký các lệnh di chuyển với các phím tương ứng.
            inputHandler.SetCommand(KeyCode.UpArrow, new MoveCommand(player, new Vector2(0, 1)));
            inputHandler.SetCommand(KeyCode.DownArrow, new MoveCommand(player, new Vector2(0, -1)));
            inputHandler.SetCommand(KeyCode.LeftArrow, new MoveCommand(player, new Vector2(-1, 0)));
            inputHandler.SetCommand(KeyCode.RightArrow, new MoveCommand(player, new Vector2(1, 0)));

            // Mô phỏng việc người chơi nhấn các phím di chuyển và Undo lại hành động cuối cùng.
            inputHandler.HandleInput(KeyCode.UpArrow);
            inputHandler.HandleInput(KeyCode.LeftArrow);
            inputHandler.UndoLastCommand();
        }
    }
}
