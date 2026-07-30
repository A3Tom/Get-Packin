public static class MatrixHelper
{
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
        ulong rowMask = 32u-1;

        while((pieceBlocks & rowMask) == 0)
            pieceBlocks >>= gameConfig.PieceHeight;
    
        pieceBlocks = RotatePiece(gameConfig, pieceBlocks);

        while((pieceBlocks & rowMask) == 0)
            pieceBlocks >>= gameConfig.PieceHeight;
        
        pieceBlocks = RotatePiece(gameConfig, pieceBlocks);
        pieceBlocks = RotatePiece(gameConfig, pieceBlocks);
        pieceBlocks = RotatePiece(gameConfig, pieceBlocks);

        return pieceBlocks;
    }
}