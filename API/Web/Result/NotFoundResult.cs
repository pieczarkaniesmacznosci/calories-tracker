using System.Collections.Generic;

namespace API.Web.Result
{
    public class NotFoundResult<T> : Result<T>
    {
        private string _error;
        private readonly T _data;

        public NotFoundResult(){
            
        }
        public NotFoundResult(string error)
        {
            _error = error;
        }

        public NotFoundResult(T data)
        {
            _data = data;
        }
        public override ResultType ResultType => ResultType.NotFound;
    
        public override List<string> Errors => new List<string>() { _error ?? $"Entity not found" };
    
        public override T Data => _data;
    }
}