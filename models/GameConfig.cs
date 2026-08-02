public class GameConfig
{
    public int BoardHeight { get; init; }
    public int BoardWidth { get; init; }
    public int PieceHeight { get; init; }
    public int PieceWidth { get; init; }
    
    public ulong Board => 0u << (BoardHeight * BoardWidth);
}
