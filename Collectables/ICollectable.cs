interface ICollectable
{
    bool IsPersistent { get; }
    int Points { get; }
    string Glyph { get; }
    ConsoleColor Color { get; }
}