using API.Result;
using System.Collections.Generic;

namespace Web.Result
{
    public class UnauthorizedResult<T> : Result<T>
    {
        private readonly T _data;
        public UnauthorizedResult()
        {

        }
        public UnauthorizedResult(T data)
        {
            _data = data;
        }
        public override ResultType ResultType => ResultType.Unauthorized;

        public override List<string> Errors => new List<string> { "Invalid client" };

        public override T Data => _data;
    }
}
