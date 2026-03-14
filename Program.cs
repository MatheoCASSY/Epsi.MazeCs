#region Constants
const int width = 50;
const int height = 20;

const int offsetY = 3;
const int offsetX = 0;

const int marginYMessage = 3;

const string sHeader = "🏃 LABYRINTHE ASCII C# 🏃";
const string sWin = """
    🎉  FÉLICITATIONS !  🎉
    Vous avez trouvé la sortie !
""";
const string sCanceled = "Partie abandonnée. À bientôt !";
const string sPressKey = "  Appuyez sur une key pour quitter...";

const ConsoleColor SuccessColor     = ConsoleColor.Green;
const ConsoleColor DangerColor      = ConsoleColor.Red;
const ConsoleColor InfoColor        = ConsoleColor.Cyan;
const ConsoleColor InstructionColor = ConsoleColor.DarkCyan;
const ConsoleColor WallColor        = ConsoleColor.DarkGray;
const ConsoleColor CorridorColor    = ConsoleColor.DarkBlue;
const ConsoleColor PlayerColor      = ConsoleColor.Yellow;
const ConsoleColor ExitColor        = ConsoleColor.Green;
#endregion 

var grid = new CellType[width, height];
var keyboard = new KeyboardController();
var screen = new ConsoleScreen();

var playerPosition = Vec2d.Zero;
var mode = State.Playing;

GenerateMaze(grid, playerPosition);
DrawScreen();

while (mode == State.Playing)
{
    var input = keyboard.ReadInput();

    if (input.IsCanceled)
    {
        mode = State.Canceled;
        continue;
    }

    if (input.Move is not { } delta)
    {
        continue;
    }

    var nextPosition = playerPosition.Add(delta);
    if (nextPosition.IsInBounds(width, height) && GetCell(nextPosition) != CellType.Wall)
    {
        if (GetCell(nextPosition) == CellType.Exit) mode = State.Won;

        UpdateCell(playerPosition, CellType.Corridor);
        playerPosition = nextPosition;
        UpdateCell(playerPosition, CellType.Player);
    }
}

var messagePosition = new Vec2d(0, offsetY + height + marginYMessage);
var messageHeight = 1;

if (mode == State.Won)
{
    messageHeight = screen.DrawFramedText(messagePosition, sWin, SuccessColor);
}
else
{
    screen.DrawText(messagePosition, sCanceled, DangerColor);
}

screen.DrawText(messagePosition.Add(new Vec2d(0, messageHeight + 1)), sPressKey);
Console.CursorVisible = true;
Console.ReadKey(true);

#region Functions

CellType GetCell(Vec2d position) => grid[position.X, position.Y];

void SetCell(Vec2d position, CellType type) => grid[position.X, position.Y] = type;

void DrawCell(Vec2d position) => screen.DrawText(
    new Vec2d(offsetX, offsetY).Add(position),
    GetCell(position) switch
    {
        CellType.Wall   => "█",
        CellType.Player => "@",
        CellType.Exit   => "★",
        _               => "·"
    },
    GetCell(position) switch
    {
        CellType.Wall   => WallColor,
        CellType.Player => PlayerColor,
        CellType.Exit   => ExitColor,
        _               => CorridorColor
    });

void UpdateCell(Vec2d position, CellType type)
{
    SetCell(position, type);
    DrawCell(position);
}

void DrawScreen()
{
    Console.Clear();
    Console.CursorVisible = false;

    screen.DrawFramedText(Vec2d.Zero, sHeader, InfoColor);
    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            DrawCell(new Vec2d(x, y));
        }
    }
    screen.DrawText(new Vec2d(0, offsetY + height), keyboard.Instructions, InstructionColor);
}

void GenerateMaze(CellType[,] grid, Vec2d playerStart)
{
    for (var y = 0; y < height; y++)
        for (var x = 0; x < width; x++)
            grid[x, y] = CellType.Wall;

    Vec2d[] directions = [
        new Vec2d(0, -1),
        new Vec2d(1, 0),
        new Vec2d(0, 1),
        new Vec2d(-1, 0)
    ];
    int[][] orders = [
        [ 0, 1, 2, 3 ], [ 0, 1, 3, 2 ], [ 0, 2, 1, 3 ], [ 0, 2, 3, 1 ], [ 0, 3, 1, 2 ], [ 0, 3, 2, 1 ],
        [ 1, 0, 2, 3 ], [ 1, 0, 3, 2 ], [ 1, 2, 0, 3 ], [ 1, 2, 3, 0 ], [ 1, 3, 0, 2 ], [ 1, 3, 2, 0 ],
        [ 2, 0, 1, 3 ], [ 2, 0, 3, 1 ], [ 2, 1, 0, 3 ], [ 2, 1, 3, 0 ], [ 2, 3, 0, 1 ], [ 2, 3, 1, 0 ],
        [ 3, 0, 1, 2 ], [ 3, 0, 2, 1 ], [ 3, 1, 0, 2 ], [ 3, 1, 2, 0 ], [ 3, 2, 0, 1 ], [ 3, 2, 1, 0 ]
    ];
    var rng = new Random();

    GenerateMazeRec(playerStart);

    var exitPosition = new Vec2d((width  - 1) & ~1, (height - 1) & ~1);

    SetCell(playerStart, CellType.Player);
    SetCell(exitPosition, CellType.Exit);
    
    void GenerateMazeRec(Vec2d position)
    {
        SetCell(position, CellType.Corridor);
        foreach (var dir in orders[rng.Next(orders.Length)])
        {
            var direction = directions[dir];
            var nextPosition = position.Add(direction.Multiply(2));

            if (nextPosition.IsInBounds(width, height) && GetCell(nextPosition) == CellType.Wall)
            {
                SetCell(position.Midpoint(nextPosition), CellType.Corridor);
                GenerateMazeRec(nextPosition);
            }
        }
    }
}
#endregion
