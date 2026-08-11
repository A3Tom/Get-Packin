using System.Collections.Concurrent;

public static class SolveHelper
{
    public static void SolvePermutation(GameConfig gameConfig, Dictionary<Piece, ulong> pieces, bool[] used, ulong board, List<PlacedPiece> current, List<List<PlacedPiece>> results)
    {
        if(board == gameConfig.FullBoard && !IsAlreadyFoundSolution(results, current))
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

    public static void SolvePermutation(GameConfig gameConfig, Dictionary<Piece, ulong> pieces, bool[] used, ulong board, List<PlacedPiece> current, ConcurrentBag<List<PlacedPiece>> results)
    {
        if(board == gameConfig.FullBoard && !IsAlreadyFoundSolution(results, current))
        {
            results.Add([.. current.OrderBy(x => x.Index)]);
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

    static bool IsAlreadyFoundSolution(IEnumerable<List<PlacedPiece>> foundSolutions, List<PlacedPiece> newSolution) => 
        foundSolutions.Any(existingSolution => 
            PrintHelper.BuildSolutionHash(existingSolution) == PrintHelper.BuildSolutionHash([.. newSolution.OrderBy(x => x.Index)])
        );

}