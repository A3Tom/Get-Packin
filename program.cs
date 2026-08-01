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
    { Piece.INDIGO,         0b11111 },
    { Piece.LIME,           0b00000_00001_01111 },
    { Piece.PURPLE,         0b00000_01010_01110 },
    { Piece.YELLOW,         0b00010_00010_00111 },
    { Piece.RED,            0b00000_00100_01111 },
    { Piece.ORANGE,         0b01100_00110_00010 },
    { Piece.GREEN,          0b00110_00010_00011 },
    { Piece.CYAN,           0b00111_00001_00001 },
    { Piece.BLUE,           0b00110_01110_00000 },
    { Piece.PINK,           0b00011_01110_00000 },
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
    result.AddRange(GetPieceRotationPermutations(gameConfig, pieceBlocks));
    result.AddRange(GetPieceMirrorPermutations(gameConfig, pieceBlocks));

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

static ulong[] GetPieceMirrorPermutations(GameConfig gameConfig, ulong pieceBlocks)
{
    ulong resultUppyDowny = FlippyFlippyUppyDowny(gameConfig, pieceBlocks);
    ulong resultSideySidey = FlippyFlippyUppyDowny(gameConfig, MatrixHelper.RotatePiece(gameConfig, pieceBlocks));
    resultSideySidey = MatrixHelper.RotatePiece(gameConfig, resultSideySidey, 2);

    return [
        MatrixHelper.NormalizePieceBlocks(gameConfig, resultUppyDowny),
        MatrixHelper.NormalizePieceBlocks(gameConfig, resultSideySidey)
    ];
}

static ulong FlippyFlippyUppyDowny(GameConfig gameConfig, ulong pieceBlocks)
{
    ulong result = 0b0;

    for (int i = 0; i < gameConfig.PieceHeight; i++)
    {
        var rowShift = i * gameConfig.PieceHeight;
        ulong insertValue = (BitHelper.ROW_MASK << rowShift) & pieceBlocks;

        var mid = (gameConfig.PieceHeight - 1) / 2;
        var magnatude = (i - mid) * 2;

        if (magnatude < 0)
            insertValue <<= Math.Abs(magnatude) * gameConfig.PieceHeight;
        else if (magnatude > 0)
            insertValue >>= magnatude * gameConfig.PieceHeight;

        result |= insertValue;
    }

    return result;
}

static ulong InitBoard(GameConfig gameConfig) => 0u << (gameConfig.BoardHeight * gameConfig.BoardWidth);