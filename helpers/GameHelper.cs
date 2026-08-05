public static class GameHelper
{
    public static GameConfig BuildGameConfig() => new()
    {
        BoardHeight = 5,
        BoardWidth = 10,
        PieceHeight = 5,
        PieceWidth = 5
    };

    public static Dictionary<Piece, ulong> BuildGamePieces() => new()
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

    public static Dictionary<Piece, ulong[]> BuildPiecePermutationDictionary(GameConfig gameConfig, Dictionary<Piece, ulong> _pieces) => 
        _pieces.ToDictionary(
            x => x.Key,
            v => MatrixHelper.GetPiecePermutations(gameConfig, v.Value)
                    .Select(x => MatrixHelper.AssimilateToBoardDimensions(gameConfig, x))
                    .ToArray()
        );

    public static bool IsValidPiecePlacement(GameConfig gameConfig, ulong board, ulong piece, int index) 
    { 
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
}