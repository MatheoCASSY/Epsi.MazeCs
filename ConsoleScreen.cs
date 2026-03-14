sealed class ConsoleScreen
{
    public void DrawText(Vec2d position, string text, ConsoleColor? color = null)
    {
        Console.SetCursorPosition(position.X, position.Y);
        if (color.HasValue)
        {
            Console.ForegroundColor = color.Value;
        }

        Console.Write(text);
        Console.ResetColor();
    }

    public int DrawFramedText(Vec2d position, string text, ConsoleColor? color = null)
    {
        var lines = text.ReplaceLineEndings("\n").Split('\n');
        var contentWidth = lines.Max(static line => line.Length);
        var topBorder = $"╔{new string('═', contentWidth + 2)}╗";
        var bottomBorder = $"╚{new string('═', contentWidth + 2)}╝";

        DrawText(position, topBorder, color);

        for (var index = 0; index < lines.Length; index++)
        {
            var line = lines[index];
            var paddedLine = line.PadRight(contentWidth);
            DrawText(position.Add(new Vec2d(0, index + 1)), $"║ {paddedLine} ║", color);
        }

        DrawText(position.Add(new Vec2d(0, lines.Length + 1)), bottomBorder, color);
        return lines.Length + 2;
    }
}
