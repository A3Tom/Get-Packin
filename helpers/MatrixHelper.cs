public static class MatrixHelper
{
    public static ulong RotatePiece(GameConfig gameConfig, ulong pieceBlocks, int rotations = 1)
    {
        ulong result = pieceBlocks;
        
        for (int i = 0; i <= rotations; i++)
        {
            result = RotatePiece(gameConfig, result);
        }

        return result;
    }
    
    public static ulong RotatePiece(GameConfig gameConfig, ulong pieceBlocks)
    {
        ulong result = 0;

        for (int row = gameConfig.PieceHeight -1; row >= 0; row--)
        {
            for (int cell = 0; cell < gameConfig.PieceWidth; cell++)
            {
                var idx = (cell * gameConfig.PieceWidth) + row;
                var cellValue = (pieceBlocks >> idx) & BitHelper.KENOBI;
                if (cellValue == 1)
                    result |= 1u << (((row + 1) * gameConfig.PieceWidth) - (cell + 1));
            }
        }

        return result;
    }

    public static ulong NormalizePieceBlocks(GameConfig gameConfig, ulong pieceBlocks)
    {
        while((pieceBlocks & BitHelper.ROW_MASK) == 0)
            pieceBlocks >>= gameConfig.PieceHeight;
    
        pieceBlocks = RotatePiece(gameConfig, pieceBlocks);

        while((pieceBlocks & BitHelper.ROW_MASK) == 0)
            pieceBlocks >>= gameConfig.PieceHeight;
        
        pieceBlocks = RotatePiece(gameConfig, pieceBlocks);
        pieceBlocks = RotatePiece(gameConfig, pieceBlocks);
        pieceBlocks = RotatePiece(gameConfig, pieceBlocks);

        return pieceBlocks;
    }
}