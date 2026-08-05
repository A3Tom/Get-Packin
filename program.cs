GameConfig gameConfig = GameHelper.BuildGameConfig();

Solve(gameConfig);
// var _pieces = GameHelper.BuildGamePieces();
// var _piecePermutations = GameHelper.BuildPiecePermutationDictionary(gameConfig, _pieces);

// ulong board = 0u;

// PrintHelper.PrintAllPiecePermutations(gameConfig, _piecePermutations);

static List<List<PlacedPiece>> Solve(GameConfig gameConfig)
{
    var _pieces = GameHelper.BuildGamePieces();
    var _piecePermutations = GameHelper.BuildPiecePermutationDictionary(gameConfig, _pieces);
    List<List<PlacedPiece>> results = [];
    
    Backtrack(
        gameConfig,
        _piecePermutations, 
        new bool[_piecePermutations.Keys.Count], 
        0u, 
        [], 
        results
    );

    return results;
}

static void Backtrack(GameConfig gameConfig, Dictionary<Piece, ulong[]> piecePermutations, bool[] used, ulong board, List<PlacedPiece> current, List<List<PlacedPiece>> results)
{
     if(board == gameConfig.FullBoard)
    {
        results.Add([.. current]);
        return;
    }

    for (int i = 0; i < piecePermutations.Keys.Count; i++)
    {
        for (int j = 0; j < piecePermutations[(Piece)i].Length; j++)
        {
            for (int boardIdx = 0; boardIdx < gameConfig.BoardSquares; boardIdx++)
            {
                if (used[i] || (board & (1u << boardIdx)) != 0)
                    break;

                var placedPiece = piecePermutations[(Piece)i][j] << boardIdx;
                
                if (!GameHelper.IsValidPiecePlacement(gameConfig, board, piecePermutations[(Piece)i][j], boardIdx))
                    continue;
                
                current.Add(new((Piece)i, placedPiece, boardIdx));
                used[i] = true;
                board |= placedPiece;

                Backtrack(gameConfig, piecePermutations, used, board, current, results);

                board &= ~placedPiece;
                used[i] = false;
                current.RemoveAt(current.Count - 1);
            }
        }   
    }
}

record PlacedPiece(Piece Piece, ulong PieceBlocks, int Index);
