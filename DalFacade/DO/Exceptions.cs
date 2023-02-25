using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Exceptions
    {
        public class RequestedItemNotFoundException : Exception
        {
            public string RequestedItemNotFound { get; set; }

            public RequestedItemNotFoundException(string msg) : base(msg)
            {
               

            }

        }


        public class ItemAlreadyExistsException : Exception
        {
            public string ItemAlreadyExists { get; set; }

            public ItemAlreadyExistsException(string msg) : base(msg)
            {
                
            }
        }
        public class InputNotValidException : Exception
        {
            public string NotValidInput { get; set; }

            public InputNotValidException(string msg) : base(msg)
            {

            }
        }

        [Serializable]
        public class DalConfigException : Exception
        {
            public DalConfigException(string msg) : base(msg) { }
            public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
        }
        
    }
}
