[Serializable]
public class CesopNotAuthorizedException : Exception
{
    public CesopNotAuthorizedException() : base()
    {
    }

    public CesopNotAuthorizedException(string message) : base(message)
    {
    }

    public CesopNotAuthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

