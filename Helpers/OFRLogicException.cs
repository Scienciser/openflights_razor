class OFRLogicException : Exception
{
    public OFRLogicException(string msg): base(msg) {}

    public OFRLogicException(string msg, Exception inner): base(msg, inner) {}
}