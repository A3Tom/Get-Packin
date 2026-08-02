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
);

