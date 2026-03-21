#region Constants
const int requestedWidth  = 50;
const int requestedHeight = 20;

const int offsetY = 3;
const int offsetX = 0;

const int marginYMessage = 3;

const string sHeader    = "🏃 LABYRINTHE ASCII C# 🏃";
const string sWin       = "🎉  FÉLICITATIONS !  🎉\nVous avez trouvé la sortie !";
const string sCanceled  = "Partie abandonnée. À bientôt !";
const string sPressKey  = "  Appuyez sur une key pour quitter...";

const ConsoleColor SuccessColor     = ConsoleColor.Green;
const ConsoleColor DangerColor      = ConsoleColor.Red;
const ConsoleColor InfoColor        = ConsoleColor.Cyan;
const ConsoleColor InstructionColor = ConsoleColor.DarkCyan;
#endregion

IController keyboard = new KeyboardController();
var screen   = new ConsoleScreen();
var maxMazeWidth  = Math.Max(3, Console.BufferWidth - offsetX);
var maxMazeHeight = Math.Max(3, Console.BufferHeight - offsetY - 1); // Keep one line for controls
var mazeWidth     = Math.Min(requestedWidth, maxMazeWidth);
var mazeHeight    = Math.Min(requestedHeight, maxMazeHeight);
var maze          = new Maze(new MazeGen(mazeWidth, mazeHeight));
var player   = new Player(maze.StartPosition, keyboard);
var offset   = new Vec2d(offsetX, offsetY);
var mode     = State.Playing;

Console.Clear();
Console.CursorVisible = false;
screen.DrawFramedText(Vec2d.Zero, sHeader, InfoColor);
maze.Draw(screen, offset);
player.Draw(screen, offset);
screen.DrawText(new Vec2d(0, offsetY + maze.Height), keyboard.Instructions, InstructionColor);

while (mode == State.Playing)
{
    if (player.TryMove(maze, screen, offset))
    {
        mode = State.Won;
        continue;
    }

    if (player.IsExitRequested)
    {
        mode = State.Canceled;
        continue;
    }
}

var messagePosition = new Vec2d(0, offsetY + maze.Height + marginYMessage);
var messageHeight   = 1;

if (mode == State.Won)
    messageHeight = screen.DrawFramedText(messagePosition, sWin, SuccessColor);
else
    screen.DrawText(messagePosition, sCanceled, DangerColor);

screen.DrawText(messagePosition.Add(new Vec2d(0, messageHeight + 1)), sPressKey);
Console.CursorVisible = true;
Console.ReadKey(true);
