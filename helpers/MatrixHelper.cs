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
    
    public static ulong RotateAssPiece(GameConfig gameConfig, ulong pieceBlocks)
    {
        ulong result = 0;

        for (int row = gameConfig.BoardHeight -1; row >= 0; row--)
        {
            for (int cell = 0; cell < gameConfig.BoardWidth; cell++)
            {
                var idx = (cell * gameConfig.BoardWidth) + row;
                var cellValue = (pieceBlocks >> idx) & BitHelper.KENOBI;
                if (cellValue == 1)
                    result |= 1u << (((row + 1) * gameConfig.BoardWidth) - (cell + 1));
            }
        }

        return result;
    }

    public static ulong FlippyFlippyUppyDowny(GameConfig gameConfig, ulong pieceBlocks)
    {
        ulong result = 0b0;

        for (int i = 0; i < gameConfig.PieceHeight; i++)
        {
            var rowShift = i * gameConfig.PieceHeight;
            ulong insertValue = (BitHelper.ROW_MASK << rowShift) & pieceBlocks;

            var mid = (gameConfig.PieceHeight - 1) / 2;
            var magnatude = (i - mid) * 2;

            if (magnatude < 0)
                insertValue <<= Math.Abs(magnatude) * gameConfig.PieceHeight;
            else if (magnatude > 0)
                insertValue >>= magnatude * gameConfig.PieceHeight;

            result |= insertValue;
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

    public static ulong[] GetPiecePermutations(GameConfig gameConfig, ulong pieceBlocks)
    {
        var result = new List<ulong>();
        result.AddRange(GetPieceRotationPermutations(gameConfig, pieceBlocks));
        result.AddRange(GetPieceMirrorPermutations(gameConfig, pieceBlocks));

        return [.. result.ToHashSet()];
    }

    public static IEnumerable<ulong> GetPieceRotationPermutations(GameConfig gameConfig, ulong pieceBlocks)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < i; j++)
                pieceBlocks = RotatePiece(gameConfig, pieceBlocks);
            
            yield return NormalizePieceBlocks(gameConfig, pieceBlocks);
        }
    }

    public static ulong[] GetPieceMirrorPermutations(GameConfig gameConfig, ulong pieceBlocks)
    {
        ulong resultUppyDowny = FlippyFlippyUppyDowny(gameConfig, pieceBlocks);
        ulong resultSideySidey = FlippyFlippyUppyDowny(gameConfig, RotatePiece(gameConfig, pieceBlocks));
        resultSideySidey = RotatePiece(gameConfig, resultSideySidey, 2);

        return [
            NormalizePieceBlocks(gameConfig, resultUppyDowny),
            NormalizePieceBlocks(gameConfig, resultSideySidey)
        ];
    }

    public static ulong AssimilateToBoardDimensions(GameConfig gameConfig, ulong piece)
    {
        var columnsToAdd = gameConfig.BoardWidth - gameConfig.PieceWidth;
        ulong assPiece = 0u;

        for (int i = 0; i < gameConfig.PieceHeight; i++)
        {
            var buffer = piece & BitHelper.ROW_MASK << (i * gameConfig.PieceWidth);

            if(buffer == 0)
                break;

            buffer <<= i * columnsToAdd;
            assPiece |= buffer;
        }
        
        return assPiece;
    }
    
    public static int GetPieceWidth(GameConfig gameConfig, ulong piece)
    {
        for (int result = 0; result < gameConfig.BoardWidth; result++)
        {
            if ((piece & (gameConfig.BoardColumnMask << result)) == 0) 
                return result;
        }

        return gameConfig.BoardHeight;
    }

    public static int GetPieceHeight(GameConfig gameConfig, ulong piece)
    {
        for (int result = 0; result < gameConfig.BoardHeight; result++)
        {
            if ((piece & (gameConfig.BoardRowMask << (result * gameConfig.BoardWidth))) == 0) 
                return result;
        }

        return gameConfig.BoardHeight;
    }

    public static List<List<Tuple<Piece, ulong>>> GetCartesianProduct(Dictionary<Piece, ulong[]> piecePermutations)
    {
        var results = new List<List<Tuple<Piece, ulong>>>();
        BacktrackCartesian(piecePermutations, 0, [], results);
        return results;
    }

    public static void BacktrackCartesian(Dictionary<Piece, ulong[]> piecePermutations, int row, List<Tuple<Piece, ulong>> current, List<List<Tuple<Piece, ulong>>> results)
    {
        if (current.Count == piecePermutations.Count)
        {
            results.Add([.. current]);
            return;
        }

        for (int i = 0; i < piecePermutations[(Piece)row].Length; i++)
        {
            current.Add(new ((Piece)row, piecePermutations[(Piece)row][i]));

            BacktrackCartesian(piecePermutations, row + 1, current, results);

            current.RemoveAt(current.Count - 1);
        }
    }
}