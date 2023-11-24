namespace cw8_2.Exceptions
{
    public class AlreadyTaken :Exception
    {
        string inputMessage;

        public AlreadyTaken(string plusMessage) 
        {
            inputMessage = plusMessage;
        }
        public override string Message => inputMessage+"already taken";
    }
}
