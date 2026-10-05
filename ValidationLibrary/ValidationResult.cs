using System;
using System.Collections.Generic;

namespace ValidationLibrary
{
    public abstract class ValidationResult<TValid, TError>
    {
        //Required bool isSuccess to tell if the ValidationResult is valid or error without having to check the type
        public abstract bool isSuccess();
    }

    public class OKResult<TValid, TError> : ValidationResult<TValid, TError>
    {
        //This will be the more restrictive, validated type that is safe to save
        public TValid value {get; }
        public override bool isSuccess() => true;

        public OKResult(TValid r)
        {
            value = r;
        }
    }

    //HasErrors interface needed so that consumer can access error list from child class
    public interface IHasErrors<TError>
    {
        List<TError> errors {get; }
    }

    public class ErrorResult<TValid, TError> : ValidationResult<TValid, TError>, IHasErrors<TError>
    {
        //This will be a list of error messages to say what was wrong with the input
        public List<TError> errors {get; }
        public override bool isSuccess() => false;

        public ErrorResult(List<TError> r)
        {
            errors = r;
        }
        
    }
}