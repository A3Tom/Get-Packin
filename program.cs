GameConfig gameConfig = GameHelper.BuildGameConfig();
Dictionary<Piece, ulong> _pieces = GameHelper.BuildGamePieces();
Dictionary<Piece, ulong[]> _piecePermutations = GameHelper.BuildPiecePermutationDictionary(gameConfig, _pieces);

ulong board = 0u;

