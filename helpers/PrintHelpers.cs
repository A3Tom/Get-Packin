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

    public static void PrintAssPieces(GameConfig gameConfig, Dictionary<Piece, ulong> assPieces)
    {
        foreach (var piece in assPieces)
        {
            Console.WriteLine($"\n ~ {Enum.GetName(piece.Key)} ~ ");

            Console.WriteLine();
            PrintBoard(gameConfig, piece.Value);
        }
    }

    public static void PrintAllPiecePermutations(GameConfig gameConfig, Dictionary<Piece, ulong[]> piecePermutations)
    {
        foreach (var piece in piecePermutations)
        {
            Console.WriteLine($"\n ~ {Enum.GetName(piece.Key)} ~ \n");

            foreach (var perm in piece.Value)
            {
                var assPieceBin = $"0{perm:B64}";
                string pieceBin = string.Empty;

                for (int i = 0; i < assPieceBin.Length; i++)
                {
                    if(i % 10 >= 5)
                        continue;
                    
                    if(i > 0 && i % 10 == 0)
                        pieceBin += '_';

                    pieceBin += assPieceBin[i];
                }

                var sanitisedStr = string.Join('_', pieceBin.Split('_').Where(x => x != "00000"));

                Console.WriteLine(sanitisedStr);
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

    public static void PrintSolutions(GameConfig gameConfig, IEnumerable<List<PlacedPiece>> results)
    {
        for (int i = 0; i < results.Count(); i++)
        {
            var placedPieces = results.ElementAt(i);
            Console.WriteLine($"\nSolution {i + 1} ({BuildSolutionHash(placedPieces)})\n");
            PrintPlacedPieces(gameConfig, placedPieces);
        }
    }

    public static void PrintPlacedPieces(GameConfig gameConfig, List<PlacedPiece> placedPieces)
    {
        var pixelPlacements = new Dictionary<int, Piece>(gameConfig.BoardSquares);

        foreach (var piece in placedPieces)
        {
            var pieceBlocks = piece.PieceBlocks;
            var idx = 0;

            while (pieceBlocks != 0)
            {
                if ((pieceBlocks & BitHelper.KENOBI) == 1)
                    pixelPlacements.Add(idx, piece.Piece);

                pieceBlocks >>= 1;
                idx++;
            }
        }

        for (int row = 0; row < gameConfig.BoardHeight; row++)
        {
            Console.Write($"[{row}] ");
            
            for(int cell = 0; cell < gameConfig.BoardWidth; cell++)
            {
                var idx = (row * gameConfig.BoardWidth) + cell;
                Console.ForegroundColor = GetPieceConsoleColour(pixelPlacements[idx]);
                Console.Write($"{BLOCK}");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }

    public static Dictionary<Piece, char> PieceCharacterMap => new()
    {
        { Piece.RED, 'R'},
        { Piece.GREEN, 'G'},
        { Piece.BLUE, 'B'},
        { Piece.YELLOW, 'Y'},
        { Piece.PINK, 'P'},
        { Piece.INDIGO, 'I'},
        { Piece.PURPLE, 'P'},
        { Piece.ORANGE, 'O'},
        { Piece.LIME, 'L'},
        { Piece.CYAN, 'C'},
    };

    public static string BuildSolutionHash(List<PlacedPiece> solution) => 
        string.Concat(solution.Select(x => x.Hash));
}