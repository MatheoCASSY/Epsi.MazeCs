sealed class Coin : ICollectable
{
    public bool IsPersistent => false;
    public int Points => 10;
    public string Glyph => "¤";
    public ConsoleColor Color => ConsoleColor.Yellow;
}