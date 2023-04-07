using System;

namespace RestApiProject.Exceptions
{
    //Define the NotFound exception to be thrown when the exception occurs
    public class NotFoundException: Exception
    {
        public NotFoundException(string message): base(message)
        {

        }

    }
}
