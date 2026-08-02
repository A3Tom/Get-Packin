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

PrintHelper.PrintAllPiecePermutations(gameConfig, _piecePermutations);

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
    ulong resultUppyDowny = MatrixHelper.FlippyFlippyUppyDowny(gameConfig, pieceBlocks);
    ulong resultSideySidey = MatrixHelper.FlippyFlippyUppyDowny(gameConfig, MatrixHelper.RotatePiece(gameConfig, pieceBlocks));
    resultSideySidey = MatrixHelper.RotatePiece(gameConfig, resultSideySidey, 2);

    return [
        MatrixHelper.NormalizePieceBlocks(gameConfig, resultUppyDowny),
        MatrixHelper.NormalizePieceBlocks(gameConfig, resultSideySidey)
    ];
}

static ulong InitBoard(GameConfig gameConfig) => 0u << (gameConfig.BoardHeight * gameConfig.BoardWidth);