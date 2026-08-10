using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;

public class SolvingBenchmarker
{
    private static GameConfig _gameConfig => GameHelper.BuildGameConfig();
    private static Dictionary<Piece, ulong> WorkingPieces => KnownPermutationData.BuildWorkingPieces();

    [Benchmark]
    public void SingleSolve_Should_CorrectlyReturnOneSolve()
    {
        var results = new ConcurrentBag<ConcurrentBag<PlacedPiece>>();

        SolveHelper.SolvePermutation(
            _gameConfig,
            WorkingPieces,
            new bool[WorkingPieces.Count()],
            0u,
            [],
            results);
    }
}