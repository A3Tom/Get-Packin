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
    { Piece.BLUE,       0b00101_00111 },
    { Piece.GREEN,      0B00010_00111 },
    { Piece.PINK,       0b00001_01111 },
    { Piece.YELLOW,     0b00100_01111 },
    { Piece.WHITE,      0b00110_00011 },
};

Dictionary<Piece, ulong[]> _piecePermutations = [];

Console.WriteLine("~~~ board ~~~");
PrintHelper.PrintBoard(gameConfig);
Console.WriteLine("\n~~~ pieces ~~~");
PrintHelper.PrintPieces(gameConfig, _pieces);

static ulong InitBoard(GameConfig gameConfig) => 0u << (gameConfig.BoardHeight * gameConfig.BoardWidth);