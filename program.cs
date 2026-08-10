using System.Collections.Concurrent;
using System.Diagnostics;

var gameConfig = GameHelper.BuildGameConfig();
var _pieces = GameHelper.BuildGamePieces();
var _piecePermutations = GameHelper.BuildPiecePermutationDictionary(gameConfig, _pieces);
var _pieceCartesions = MatrixHelper.GetCartesianProduct(_piecePermutations)
    .Select(x => x.ToDictionary(
        k => k.Item1,
        v => v.Item2
    )
);

var results = new ConcurrentBag<ConcurrentBag<PlacedPiece>>();
var timings = new ConcurrentDictionary<long, double>();
long permutationsSolved = 0;
long total = _pieceCartesions.Count();

Console.WriteLine($"Total to solve: {total:N0}");

Parallel.ForEach(_pieceCartesions, solveTask =>
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

    var elapsedTime = Math.Ceiling((double)sw.ElapsedMilliseconds);
    timings.TryAdd(permutationsSolved, elapsedTime);
    Interlocked.Increment(ref permutationsSolved);

    var avgSolveTime = timings.Average(x => x.Value);
    Console.WriteLine($"\rSolved {permutationsSolved} | found {results.Count()} # {elapsedTime:N0}ms ({avgSolveTime:N2}ms)");
});

Console.WriteLine("Done");
