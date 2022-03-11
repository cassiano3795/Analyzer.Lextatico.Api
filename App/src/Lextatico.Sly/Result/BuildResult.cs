using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;

namespace Lextatico.Sly.Result
{
    public class BuildResult<T>
    {
        public BuildResult() : this(default(T))
        { }

        public BuildResult(T result)
        {
            Result = result;
            Errors = new List<InitializationError>();
        }

        public List<InitializationError> Errors { get; }

        public T Result { get; set; }

        public bool IsError
        {
            get { return Errors.Any(e => e.Level != ErrorLevel.Warn); }
        }

        public bool IsOk => !IsError;

        public void AddError(InitializationError error)
        {
            Errors.Add(error);
        }

        public void AddErrors(List<InitializationError> errors)
        {
            Errors.AddRange(errors);
        }
    }
}