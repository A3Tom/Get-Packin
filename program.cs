var gameConfig = new GameConfig()
{
    BoardHeight = 5,
    BoardWidth = 10,
    PieceHeight = 5,
    PieceWidth = 5
};

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
    v => MatrixHelper.GetPiecePermutations(gameConfig, v.Value)
            .Select(x => MatrixHelper.AssimilateToBoardDimensions(gameConfig, x))
            .ToArray()
);

ulong position = 1u;
ulong board = 0u;

foreach (var piecePerm in _piecePermutations)
{
    bool placed = false;
    for (int p = 0; p < piecePerm.Value.Length; p++)
    {
        if (placed)
            break;
        
        for (int i = 0; i < 50; i++)
        {
            position <<= 1;
            var piece = piecePerm.Value[0];
            var placedPiece = piece << i;
            
            if (IsValidPiecePlacement(gameConfig, board, piece, i))
            {
                Console.WriteLine($"Placed {Enum.GetName(piecePerm.Key)} at {i}");
                board |= placedPiece;
                placed = true;
                break;
            } 
        }
    }
    
}
Console.WriteLine();
PrintHelper.PrintBoard(gameConfig, board);

static bool IsValidPiecePlacement(GameConfig gameConfig, ulong board, ulong piece, int index) { 
    if ((board & (piece << index)) != 0) 
        return false;

    var colsRemaining = gameConfig.BoardWidth - (index % gameConfig.BoardWidth);
    if (colsRemaining < MatrixHelper.GetPieceWidth(gameConfig, piece))
        return false;

    var rowsRemaining = gameConfig.BoardHeight - Math.Ceiling((double)index / gameConfig.BoardWidth);
    if (rowsRemaining < MatrixHelper.GetPieceHeight(gameConfig, piece))
        return false;

    return true;
}