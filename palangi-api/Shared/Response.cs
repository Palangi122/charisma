using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class Response
    {
        public ActResult ActionResult { get; set; }
        public Exception Exception { get; set; }
        public List<Message> Messages { get; set; }
        public bool HasError
        {
            get
            {
                return Exception != null;
            }
        }
    }
    public class Response<ResultType> : Response where ResultType : class
    {
        public ResultType Result { get; set; }
    }
    public enum ActResult
    {
        Unsuccessful = 0,
        Successful = 1,
        LetterIdNotFound = 2,
        NotTimeRange = 3,
        CustomerNotBeNull = 4,
        NotValidaiton = 5,
        PersistFailed = 6,

    }
    public class Message
    {
        public ActResult ActResult { get; set; }
        public string Body { get; set; }
    }
}
