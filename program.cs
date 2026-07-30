using System.Security.Cryptography.X509Certificates;

var gameConfig = new GameConfig()
{
    BoardHeight = 5,
    BoardWidth = 10,
    PieceHeight = 5,
    PieceWidth = 5
};
gameConfig.Board = InitBoard(gameConfig);

Dictionary<Piece, ulong> _pieces = new()
{
    { Piece.RED,        0b01111 },
    { Piece.BLUE,       0b00000_01010_01110_00000 },
    { Piece.GREEN,      0B00000_00010_00111_00000 },
    { Piece.PINK,       0b00000_00001_01111_00000 },
    { Piece.YELLOW,     0b00000_00100_01111_00000 },
    { Piece.WHITE,      0b00000_00110_00011_00000 },
};

Dictionary<Piece, ulong[]> _piecePermutations = _pieces.ToDictionary(
    x => x.Key,
    v => GetPiecePermutations(gameConfig, v.Value)
);

PrintAllPiecePermutations(gameConfig, _piecePermutations);

static void PrintAllPiecePermutations(GameConfig gameConfig, Dictionary<Piece, ulong[]> piecePermutations)
{
    foreach (var piece in piecePermutations)
    {
        Console.WriteLine($"\n ~ {Enum.GetName(piece.Key)} ~ ");

        foreach (var perm in piece.Value)
        {
            Console.WriteLine();
            PrintHelper.PrintPiece(gameConfig, piece.Key, perm);
        }

    }
}

static ulong[] GetPiecePermutations(GameConfig gameConfig, ulong pieceBlocks)
{
    var result = new List<ulong>();
    result.AddRange(
        GetPieceRotationPermutations(gameConfig, pieceBlocks)
    );

    return [.. result.ToHashSet()];
}

static IEnumerable<ulong> GetPieceRotationPermutations(GameConfig gameConfig, ulong pieceBlocks)
{
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < i; j++)
            pieceBlocks = MatrixHelper.RotatePiece(gameConfig, pieceBlocks);
        
        yield return MatrixHelper.NormalizePieceBlocks(gameConfig, pieceBlocks);
    }
}

static ulong InitBoard(GameConfig gameConfig) => 0u << (gameConfig.BoardHeight * gameConfig.BoardWidth);