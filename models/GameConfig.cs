public class GameConfig
{
    public int BoardHeight { get; init; }
    public int BoardWidth { get; init; }
    public int PieceHeight { get; init; }
    public int PieceWidth { get; init; }
    
    public ulong Board => 0u << (BoardHeight * BoardWidth);

    public ulong BoardColumnMask => BuidColumnMask(BoardWidth, BoardHeight);
    public ulong BoardRowMask => (ulong)(Math.Pow(2, BoardWidth) - 1);
    public ulong PieceColumnMask => BuidColumnMask(PieceWidth, PieceHeight);
    public ulong PieceRowMask => (ulong)(Math.Pow(2, PieceWidth) - 1);

    public ulong FullBoard => (ulong)(Math.Pow(2, BoardWidth * BoardHeight) - 1);

    public static ulong BuidColumnMask(int width, int height)
    {
        ulong columnMask = 1u;

        for (int i = 1; i < height; i++)
        {
            columnMask |= columnMask << width;
        }

        return columnMask;
    }
}
