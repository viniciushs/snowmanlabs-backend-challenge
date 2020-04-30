namespace System
{
    public class SnowmanLabsChallengeException : Exception
    {
        public SnowmanLabsChallengeException()
        {
        }

        public SnowmanLabsChallengeException(string message)
            : base(message)
        {
        }

        public SnowmanLabsChallengeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
