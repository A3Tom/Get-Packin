using System.Collections.Concurrent;
using System.Diagnostics;

var gameConfig = GameHelper.BuildGameConfig();
var knownSolvablePiecePermutations = KnownPermutationData.BuildSolvablePieces(gameConfig);
var _pieces = GameHelper.BuildGamePieces();
var _piecePermutations = GameHelper.BuildPiecePermutationDictionary(gameConfig, _pieces);
var _pieceCartesions = MatrixHelper.GetCartesianProduct(_piecePermutations)
    .Select(x => x.ToDictionary(
        k => k.Item1,
        v => v.Item2
    )
);

List<Dictionary<Piece, ulong>> shortList = [.. knownSolvablePiecePermutations];

var results = new ConcurrentBag<List<PlacedPiece>>();
var timings = new ConcurrentDictionary<long, double>();
long permutationsSolved = 0;
long total = _pieceCartesions.Count();

Console.WriteLine($"Total to solve: {total:N0}");

SolveParallel(gameConfig, shortList, results, timings, permutationsSolved);

PrintHelper.PrintSolutions(gameConfig, results);
Console.WriteLine("Done");



static void SolveParallel(GameConfig gameConfig, List<Dictionary<Piece, ulong>> shortList, ConcurrentBag<List<PlacedPiece>> results, ConcurrentDictionary<long, double> timings, long permutationsSolved)
{
    _ = Parallel.ForEach(shortList, solveTask =>
    {
        var sw = new Stopwatch();
        sw.Start();

        SolveHelper.SolvePermutation(
            gameConfig,
            solveTask,
            new bool[solveTask.Count],
            0u,
            [],
            results
        );

        sw.Stop();

        Interlocked.Increment(ref permutationsSolved);

        var elapsedTime = Math.Ceiling((double)sw.ElapsedMilliseconds);
        timings.TryAdd(permutationsSolved, elapsedTime);
        var avgSolveTime = timings.Average(x => x.Value);
        Console.WriteLine($"\rSolved {permutationsSolved} | found {results.Count()} # {elapsedTime:N0}ms ({avgSolveTime:N2}ms)");
    });
}