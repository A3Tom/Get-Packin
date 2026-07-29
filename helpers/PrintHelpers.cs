public static class PrintHelper
{
    public const char EMPTY_SPACE = '·';
    public const char BLOCK = '█';

    public static void PrintBoard(GameConfig gameConfig)
    {
        for (int row = 0; row < gameConfig.BoardHeight; row++)
        {
            Console.Write($"[{row}] ");
            
            for(int cell = 0; cell < gameConfig.BoardWidth; cell++)
            {
                var idx = (row * gameConfig.BoardWidth) + cell;
                var hing = (gameConfig.Board >> idx) & BitHelper.KENOBI;
                var outputChar = hing == 1 ? BLOCK : EMPTY_SPACE;
                Console.Write($"{outputChar} ");
            }

            Console.WriteLine();
        }
    }

    public static void PrintPieces(GameConfig gameConfig, Dictionary<Piece, ulong> pieces)
    {
        foreach (var piece in pieces)
        {
            Console.WriteLine($"\n ~ {Enum.GetName(piece.Key)} ~ ");
            PrintPiece(gameConfig, piece.Key, piece.Value);
        }
    }

    public static void PrintPiece(GameConfig gameConfig, Piece piece, ulong pieceBlocks)
    {

        for (int row = 0; row < gameConfig.PieceHeight; row++)
        {
            Console.Write($"[{row}] ");
            
            for(int cell = 0; cell < gameConfig.PieceWidth; cell++)
            {
                var idx = (row * gameConfig.PieceWidth) + cell;
                var hing = (pieceBlocks >> idx) & BitHelper.KENOBI;
                var outputChar = hing == 1 ? BLOCK : EMPTY_SPACE;
                Console.ForegroundColor = GetPieceConsoleColour(piece);
                Console.Write($"{outputChar}");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }

    static ConsoleColor GetPieceConsoleColour(Piece piece) => piece switch
    {
        Piece.RED => ConsoleColor.Red,
        Piece.GREEN => ConsoleColor.Green,
        Piece.BLUE => ConsoleColor.Blue,
        Piece.WHITE => ConsoleColor.White,
        Piece.BLACK => ConsoleColor.Black,
        Piece.YELLOW => ConsoleColor.Yellow,
        Piece.PINK => ConsoleColor.Magenta,
        _ => ConsoleColor.White,
    };
}