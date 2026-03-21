interface IGridDisplay
{
    void DrawText(Vec2d position, string text, ConsoleColor? color = null);
    int DrawFramedText(Vec2d position, string text, ConsoleColor? color = null);
}
