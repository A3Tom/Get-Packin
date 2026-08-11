using System.Collections.Concurrent;
using Xunit;

public class SolvePermutationTests
{
    [Fact]
    public void SolvePermutation_ShouldCorrectlySolve_WhenGivenAKnownWorkingPiece()
    {
        var gameConfig = GameHelper.BuildGameConfig();
        var results = new ConcurrentBag<ConcurrentBag<PlacedPiece>>();
        var pieces = KnownPermutationData.BuildSolvablePieces(gameConfig);
        var expectedSolves = 1;

        // SolveHelper.SolvePermutation(
        //     gameConfig, 
        //     pieces,
        //     new bool[pieces.Count()],
        //     0u,
        //     [],
        //     results);

        Assert.Equal(results.Count(), expectedSolves);
    }
}

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
        
        List<Dictionary<Piece, ulong>> result = [
            solvablePieces_1,
            solvablePieces_2
        ];

        return [.. result.Select(x => x.ToAssPiece(gameConfig))];
    }
}

public static class DictionaryExtensions
{
    public static Dictionary<Piece, ulong> ToAssPiece(this Dictionary<Piece, ulong>? source, GameConfig gameConfig) => 
    source!.ToDictionary(
        k => k.Key,
        v => MatrixHelper.AssimilateToBoardDimensions(gameConfig, v.Value)
    );
}