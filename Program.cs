#region Constants
const int width  = 50;
const int height = 20;

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

var keyboard = new KeyboardController();
var screen   = new ConsoleScreen();
var maze     = new Maze(new MazeGen(width, height));
var player   = new Player(maze.StartPosition);
var offset   = new Vec2d(offsetX, offsetY);
var mode     = State.Playing;

Console.Clear();
Console.CursorVisible = false;
screen.DrawFramedText(Vec2d.Zero, sHeader, InfoColor);
maze.Draw(screen, offset);
player.Draw(screen, offset);
screen.DrawText(new Vec2d(0, offsetY + height), keyboard.Instructions, InstructionColor);

while (mode == State.Playing)
{
    var input = keyboard.ReadInput();

    if (input.IsCanceled)
    {
        mode = State.Canceled;
        continue;
    }

    if (input.Move is not { } delta)
        continue;

    if (player.TryMove(delta, maze, screen, offset))
        mode = State.Won;
}

var messagePosition = new Vec2d(0, offsetY + height + marginYMessage);
var messageHeight   = 1;

if (mode == State.Won)
    messageHeight = screen.DrawFramedText(messagePosition, sWin, SuccessColor);
else
    screen.DrawText(messagePosition, sCanceled, DangerColor);

screen.DrawText(messagePosition.Add(new Vec2d(0, messageHeight + 1)), sPressKey);
Console.CursorVisible = true;
Console.ReadKey(true);
