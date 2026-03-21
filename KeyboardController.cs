sealed class KeyboardController : IController
{
    public string Instructions => "  [Z/↑] Haut   [S/↓] Bas   [Q/←] Gauche   [D/→] Droite   [Échap] Quitter";

    public bool IsUpPressed { get; private set; }
    public bool IsDownPressed { get; private set; }
    public bool IsLeftPressed { get; private set; }
    public bool IsRightPressed { get; private set; }
    public bool IsEscPressed { get; private set; }

    public void ReadInput()
    {
        IsUpPressed = false;
        IsDownPressed = false;
        IsLeftPressed = false;
        IsRightPressed = false;
        IsEscPressed = false;

        var key = Console.ReadKey(true).Key;

        switch (key)
        {
            case ConsoleKey.Z:
            case ConsoleKey.UpArrow:
                IsUpPressed = true;
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                IsDownPressed = true;
                break;
            case ConsoleKey.Q:
            case ConsoleKey.LeftArrow:
                IsLeftPressed = true;
                break;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                IsRightPressed = true;
                break;
            case ConsoleKey.Escape:
                IsEscPressed = true;
                break;
        }
    }
}
