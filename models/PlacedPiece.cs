public record PlacedPiece(Piece Piece, ulong PieceBlocks, int Index)
{
    public string Hash => $"{PrintHelper.PieceCharacterMap[Piece]}{Index}";
};
