sealed class KeyboardController
{
    public string Instructions => "  [Z/↑] Haut   [S/↓] Bas   [Q/←] Gauche   [D/→] Droite   [Échap] Quitter";

    public (Vec2d? Move, bool IsCanceled) ReadInput()
    {
        var key = Console.ReadKey(true).Key;

        return key switch
        {
            ConsoleKey.Z or ConsoleKey.UpArrow => (new Vec2d(0, -1), false),
            ConsoleKey.S or ConsoleKey.DownArrow => (new Vec2d(0, 1), false),
            ConsoleKey.Q or ConsoleKey.LeftArrow => (new Vec2d(-1, 0), false),
            ConsoleKey.D or ConsoleKey.RightArrow => (new Vec2d(1, 0), false),
            ConsoleKey.Escape => (null, true),
            _ => (null, false)
        };
    }
}
