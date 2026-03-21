sealed class Wall : Cell
{
    public override bool IsSolid => true;

    public override void Draw(IGridDisplay screen, Vec2d position)
    {
        screen.DrawText(position, "█", ConsoleColor.DarkGray);
    }
}
