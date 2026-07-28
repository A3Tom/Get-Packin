using System.Diagnostics;

const char EMPTY_SPACE = '·';
const char BLOCK = '█';

const ulong board = 0b00000000_00000000_00000000;
Dictionary<Piece, ulong> pieces = new()
{
    { Piece.RED,        0b00001111 },
    { Piece.BLUE,       0b00000101_00000111 },
    { Piece.GREEN,      0b00000010_00000111 },
    { Piece.PINK,       0b00000001_00001111 },
    { Piece.YELLOW,     0b00000100_00001111 },
};

Console.WriteLine("~~~ board ~~~");
printBoard(board);
Console.WriteLine("\n~~~ pieces ~~~");
printPieces(pieces);

static void printBoard(ulong board)
{
    const int width = 8;
    const int mask = 0b1;

    for (int row = 0; row < 3; row++)
    {
        Console.Write($"[{row}] ");
        
        for(int cell = 0; cell < width; cell++)
        {
            var idx = (row * width) + cell;
            var hing = (board >> idx) & mask;
            var outputChar = hing == 1 ? BLOCK : EMPTY_SPACE;
            Console.Write($"{outputChar} ");
        }

        Console.WriteLine();
    }
}

static void printPieces(Dictionary<Piece, ulong> pieces)
{
    foreach (var piece in pieces)
    {
        Console.WriteLine($"\n :: {Enum.GetName(piece.Key)} :: ");
        printPiece(piece.Key, piece.Value);
    }
}

static void printPiece(Piece piece, ulong pieceBlocks)
{

    const int width = 8;
    const int mask = 0b1;

    for (int row = 0; row < 3; row++)
    {
        Console.Write($"[{row}] ");
        
        for(int cell = 0; cell < width; cell++)
        {
            var idx = (row * width) + cell;
            var hing = (pieceBlocks >> idx) & mask;
            var outputChar = hing == 1 ? BLOCK : EMPTY_SPACE;
            Console.ForegroundColor = GetPieceConsoleColour(piece);
            Console.Write($"{outputChar}");
            Console.ResetColor();
        }

        Console.WriteLine();
    }

}

static ConsoleColor GetPieceConsoleColour(Piece piece) => piece switch
{
    Piece.RED => ConsoleColor.Red,
    Piece.GREEN => ConsoleColor.Green,
    Piece.BLUE => ConsoleColor.Blue,
    Piece.WHITE => ConsoleColor.White,
    Piece.BLACK => ConsoleColor.Black,
    Piece.YELLOW => ConsoleColor.Yellow,
    Piece.PINK => ConsoleColor.Magenta,
    _ => ConsoleColor.White,
};