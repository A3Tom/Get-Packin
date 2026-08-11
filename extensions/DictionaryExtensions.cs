public static class DictionaryExtensions
{
    public static Dictionary<Piece, ulong> ToAssPiece(this Dictionary<Piece, ulong>? source, GameConfig gameConfig) => 
    source!.ToDictionary(
        k => k.Key,
        v => MatrixHelper.AssimilateToBoardDimensions(gameConfig, v.Value)
    );
}