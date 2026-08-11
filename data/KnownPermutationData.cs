public class KnownPermutationData
{
    public static List<Dictionary<Piece, ulong>> BuildSolvablePieces(GameConfig gameConfig)
    {
        var solvablePieces_1 = new Dictionary<Piece, ulong>()
        {
            { Piece.RED,            0b00000_01111_00010 },
            { Piece.GREEN,          0b00110_00010_00011 },
            { Piece.BLUE,           0b00000_00011_00111 },
            { Piece.YELLOW,         0b00111_00010_00010 },
            { Piece.PINK,           0b00010_00011_00001_00001 },
            { Piece.INDIGO,         0b11111 },
            { Piece.PURPLE,         0b00000_11100_10100 },
            { Piece.ORANGE,         0b00100_00110_00011 },
            { Piece.LIME,           0b00000_01111_01000 },
            { Piece.CYAN,           0b00100_00100_00111 },
        };

        var solvablePieces_2 = new Dictionary<Piece, ulong>()
        {
            { Piece.RED,            0b00010_01111 },
            { Piece.GREEN,          0b00110_00010_00011 },
            { Piece.BLUE,           0b00000_00110_00111 },
            { Piece.YELLOW,         0b00001_00111_00001 },
            { Piece.PINK,           0b00001_00011_00010_00010 },
            { Piece.INDIGO,         0b11111 },
            { Piece.PURPLE,         0b00000_10100_11100 },
            { Piece.ORANGE,         0b01100_00110_00010 },
            { Piece.LIME,           0b00000_01111_01000 },
            { Piece.CYAN,           0b00111_00001_00001 },
        };

        var solvablePieces_3 = new Dictionary<Piece, ulong>()
        {
            { Piece.RED,            0b00010_01111 },
            { Piece.GREEN,          0b00001_00111_00100 },
            { Piece.BLUE,           0b00111_00011 },
            { Piece.YELLOW,         0b00010_00010_00111 },
            { Piece.PINK,           0b00001_00011_00010_00010 },
            { Piece.INDIGO,         0b00001_00001_00001_00001_00001 },
            { Piece.PURPLE,         0b00111_00101 },
            { Piece.ORANGE,         0b00001_00011_00110 },
            { Piece.LIME,           0b01000_01111 },
            { Piece.CYAN,           0b00111_00001_00001 },
        };
        
        List<Dictionary<Piece, ulong>> result = [
            solvablePieces_1,
            solvablePieces_2,
            solvablePieces_3,
        ];

        return [.. result.Select(x => x.ToAssPiece(gameConfig))];
    }
}
