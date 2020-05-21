namespace GameOfLife.Library
{
    public class Game
    {
        public Board Board { get; private set; }

        public Game()
        {
        }

        public Board CreateBoard(int width, int height)
        {
            this.Board = new Board(width, height);
            return this.Board;
        }
    }
}
