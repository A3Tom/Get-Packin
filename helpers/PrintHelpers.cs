public static class PrintHelper
{
    public const char EMPTY_SPACE = '·';
    public const char BLOCK = '█';

    public static void PrintBoard(GameConfig gameConfig, ulong board)
    {
        for (int row = 0; row < gameConfig.BoardHeight; row++)
        {
            Console.Write($"[{row}] ");
            
            for(int cell = 0; cell < gameConfig.BoardWidth; cell++)
            {
                var idx = (row * gameConfig.BoardWidth) + cell;
                var hing = (board >> idx) & BitHelper.KENOBI;
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

    public static void PrintAllPiecePermutations(GameConfig gameConfig, Dictionary<Piece, ulong[]> piecePermutations)
    {
        foreach (var piece in piecePermutations)
        {
            Console.WriteLine($"\n ~ {Enum.GetName(piece.Key)} ~ ");

            foreach (var perm in piece.Value)
            {
                Console.WriteLine();
                PrintBoard(gameConfig, perm);
            }
        }
    }

    static ConsoleColor GetPieceConsoleColour(Piece piece) => piece switch
    {
        Piece.RED => ConsoleColor.Red,
        Piece.GREEN => ConsoleColor.DarkGreen,
        Piece.BLUE => ConsoleColor.Blue,
        Piece.YELLOW => ConsoleColor.Yellow,
        Piece.PINK => ConsoleColor.Magenta,
        Piece.INDIGO => ConsoleColor.DarkBlue,
        Piece.PURPLE => ConsoleColor.DarkMagenta,
        Piece.ORANGE => ConsoleColor.DarkYellow,
        Piece.LIME => ConsoleColor.Green,
        Piece.CYAN => ConsoleColor.White,
        _ => ConsoleColor.White,
    };
}