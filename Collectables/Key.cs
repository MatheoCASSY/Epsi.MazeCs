sealed class Key : ICollectable
{
    public bool IsPersistent => true;
    public int Points => 0;
    public string Glyph => "🗝";
    public ConsoleColor Color => ConsoleColor.Cyan;
}
