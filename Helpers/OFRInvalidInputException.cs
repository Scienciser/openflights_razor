class OFRInvalidInputException : Exception
{
    public OFRInvalidInputException(string msg): base(msg) {}

    public OFRInvalidInputException(string msg, Exception inner): base(msg, inner) {}
}