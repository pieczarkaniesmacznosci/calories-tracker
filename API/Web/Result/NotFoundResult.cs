using System.Collections.Generic;

namespace API.Web.Result
{
    public class NotFoundResult<T> : Result<T>
    {
        private readonly T _data;
        public NotFoundResult()
        {
            
        }
        public NotFoundResult(T data)
        {
            _data = data;
        }
        public override ResultType ResultType => ResultType.NotFound;
    
        public override List<string> Errors => new List<string>();
    
        public override T Data => _data;
    }
}