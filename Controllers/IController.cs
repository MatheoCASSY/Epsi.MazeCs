interface IController
{
    string Instructions { get; }
    bool IsUpPressed { get; }
    bool IsDownPressed { get; }
    bool IsLeftPressed { get; }
    bool IsRightPressed { get; }
    bool IsEscPressed { get; }
    void ReadInput();
}