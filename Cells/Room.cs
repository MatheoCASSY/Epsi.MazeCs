class Room : Cell
{
    public override bool IsSolid => false;

    public override void Draw(IGridDisplay screen, Vec2d position)
    {
        screen.DrawText(position, "·", ConsoleColor.DarkBlue);
    }
}
