public static class MatrixHelper
{
    public static ulong RotatePiece(ulong pieceBlocks, int pieceHeight, int pieceWidth)
    {
        ulong result = 0;

        for (int row = pieceHeight -1; row >= 0; row--)
        {
            for (int cell = 0; cell < pieceWidth; cell++)
            {
                var idx = (cell * pieceWidth) + row;
                var cellValue = (pieceBlocks >> idx) & BitHelper.KENOBI;
                if (cellValue == 1)
                    result |= 1u << (((row + 1) * pieceWidth) - (cell + 1));
            }
        }

        return result;
    }
}