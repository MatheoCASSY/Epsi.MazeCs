readonly record struct Vec2d(int X, int Y)
{
    public static Vec2d Zero => new(0, 0);

    public Vec2d Add(Vec2d other) => new(X + other.X, Y + other.Y);

    public Vec2d Multiply(int factor) => new(X * factor, Y * factor);

    public Vec2d Midpoint(Vec2d other) => new((X + other.X) / 2, (Y + other.Y) / 2);

    public bool IsInBounds(int width, int height) =>
        X >= 0 && X < width && Y >= 0 && Y < height;
}
