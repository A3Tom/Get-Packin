using System.Diagnostics;

const char EMPTY_SPACE = '·';
const char BLOCK = '█';

const int CELL_MASK = 0b1;
const int BOARD_HEIGHT = 5;
const int BOARD_WIDTH = 10;
const int PIECE_HEIGHT = 5;
const int PIECE_WIDTH = 5;

const ulong board = 0b0000000000_0000000000_0000000000_0000000000_0000000000;

Dictionary<Piece, ulong> pieces = new()
{
    { Piece.RED,        0b01111 },
    // { Piece.BLUE,       0b00101_00111 },
    // { Piece.GREEN,      0B00010_00111 },
    // { Piece.PINK,       0b00001_01111 },
    // { Piece.YELLOW,     0b00100_01111 },
    // { Piece.WHITE,      0b00110_00011 },
};

// Console.WriteLine("~~~ board ~~~");
// printBoard(board);
Console.WriteLine("\n~~~ pieces ~~~");
printPieces(pieces);

static void printBoard(ulong board)
{
    const int mask = 0b1;

    for (int row = 0; row < BOARD_HEIGHT; row++)
    {
        Console.Write($"[{row}] ");
        
        for(int cell = 0; cell < BOARD_WIDTH; cell++)
        {
            var idx = (row * BOARD_WIDTH) + cell;
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
        Console.WriteLine($"\n ~ {Enum.GetName(piece.Key)} ~ ");
        printPiece(piece.Key, piece.Value);

        var rotPiece = piece.Value;
        for (int i = 0; i < 3; i++)
        {
            rotPiece = rotatePiece(rotPiece);
            Console.WriteLine($"\n ~ Rotated {i + 1}");
            printPiece(piece.Key, rotPiece);
        }
    }
}

static void printPiece(Piece piece, ulong pieceBlocks)
{

    for (int row = 0; row < PIECE_HEIGHT; row++)
    {
        Console.Write($"[{row}] ");
        
        for(int cell = 0; cell < PIECE_WIDTH; cell++)
        {
            var idx = (row * PIECE_WIDTH) + cell;
            var hing = (pieceBlocks >> idx) & CELL_MASK;
            var outputChar = hing == 1 ? BLOCK : EMPTY_SPACE;
            Console.ForegroundColor = GetPieceConsoleColour(piece);
            Console.Write($"{outputChar}");
            Console.ResetColor();
        }

        Console.WriteLine();
    }
}

static ulong rotatePiece(ulong pieceBlocks)
{
    ulong result = 0;

    for (int row = PIECE_HEIGHT -1; row >= 0; row--)
    {
        for (int cell = 0; cell < PIECE_WIDTH; cell++)
        {
            var idx = (cell * PIECE_WIDTH) + row;
            var cellValue = (pieceBlocks >> idx) & CELL_MASK;
            if (cellValue == 1)
                result |= 1u << (((row + 1) * PIECE_WIDTH) - (cell + 1));
        }
    }

    return result;
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