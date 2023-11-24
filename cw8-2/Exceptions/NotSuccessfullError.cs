using System;

namespace cw8_2.Exceptions
{
    public class NotSuccessfullError : Exception
    {
        public override string Message => "Operation not successfull !";
    }
}
