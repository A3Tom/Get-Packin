using System.Collections.Concurrent;

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

SolveHelper.SolveParallel(gameConfig, shortList, results, timings, permutationsSolved);

PrintHelper.PrintSolutions(gameConfig, results);
Console.WriteLine("Done");
