using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace BO
{
    //public class blExceptions:Exception
    //{
    //    public blExceptions(string message) : base(message) { }
    //    public blExceptions(string message,Exception ex) : base(message,ex) { }
    //}

    //public class UnvalidID: Exception
    //{
    //    public UnvalidID() : base("The id you entered isn't valid") { }
    //}
    //public class NotFound : blExceptions
    //{
    //    public NotFound() : base("entity not found") { }
    //}



    public class InvalidValueException : Exception
    {
        public InvalidValueException(string message, Exception inner): base(message, inner) { }

        public InvalidValueException(string message) : base(message) { }
        public override string ToString()
        {
            return ($@" exception name: InvalidValueException,
                    exception message: {Message}
                     ");
        }
    }

    public class NotFound : Exception
    {
        public NotFound(string message, Exception inner) : base(message, inner) { }

        public NotFound(string message) : base(message) { }
        public override string ToString()
        {
            return ($@" exception name: NotFound,
                    exception message: {Message}
                     ");
        }
    }

    public class TheOperationFailed : Exception
    {
        public TheOperationFailed(string message, Exception inner) : base(message, inner) { }

        public TheOperationFailed(string message) : base(message) { }
        public override string ToString()
        {
            return ($@" exception name: TheOperationFailed,
                    exception message: {Message}
                     ");
        }
    }
}
