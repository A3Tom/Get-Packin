using System.Collections.Concurrent;

GameConfig gameConfig = GameHelper.BuildGameConfig();

// Solve(gameConfig);
var _pieces = GameHelper.BuildGamePieces();
var _piecePermutations = GameHelper.BuildPiecePermutationDictionary(gameConfig, _pieces);

PrintHelper.PrintAllPiecePermutations(gameConfig, _piecePermutations);

var results = new ConcurrentBag<ConcurrentBag<PlacedPiece>>();
var cartii = MatrixHelper.GetCartesianProduct(_piecePermutations)
    .Select(x => x.ToDictionary(
        k => k.Item1,
        v => v.Item2
    )
);

long permutationsSolved = 0;
long total = cartii.Count();

Parallel.ForEach(cartii, solveTask =>
{
    SolvePermutation(
        gameConfig,
        solveTask,
        new bool[solveTask.Count],
        0u,
        [],
        results
    );

    Interlocked.Increment(ref permutationsSolved);

    Console.WriteLine($"Solved {permutationsSolved} | found {results.Count()}");
});

// var _workingPieces = GameHelper.BuildWorkingPieces()
//     .ToDictionary(
//         k => k.Key,
//         x => MatrixHelper.AssimilateToBoardDimensions(gameConfig, x.Value)
//     );

// SolvePermutation(
//     gameConfig, 
//     _workingPieces, 
//     new bool[_workingPieces.Count],
//     0u,
//     [],
//     results);

Console.WriteLine("Done");


static void SolvePermutation(GameConfig gameConfig, Dictionary<Piece, ulong> pieces, bool[] used, ulong board, List<PlacedPiece> current, ConcurrentBag<ConcurrentBag<PlacedPiece>> results)
{
    if(board == gameConfig.FullBoard)
    {
        results.Add([.. current]);
        return;
    }

    for (int i = 0; i < pieces.Count; i++)
    {
        for (int boardIdx = 0; boardIdx < gameConfig.BoardSquares; boardIdx++)
        {
            if (used[i] || (board & (1u << boardIdx)) != 0)
                break;

            var placedPiece = pieces[(Piece)i] << boardIdx;
            
            if (!GameHelper.IsValidPiecePlacement(gameConfig, board, pieces[(Piece)i], boardIdx))
                continue;
            
            current.Add(new((Piece)i, placedPiece, boardIdx));
            used[i] = true;
            board |= placedPiece;

            SolvePermutation(gameConfig, pieces, used, board, current, results);

            board &= ~placedPiece;
            used[i] = false;
            current.RemoveAt(current.Count - 1);
        }
    }
}

record PlacedPiece(Piece Piece, ulong PieceBlocks, int Index);
