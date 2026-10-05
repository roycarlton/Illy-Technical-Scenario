using System;
using System.Collections.Generic;

namespace ValidationLibrary
{
    //Interface for specifc validators on the consumer side to implement
    public interface IValidator<TValid, TError, TInput>
    {
        ValidationResult<TValid, TError> Validate(TInput item);
    }

    public static class ValidationRunnner<TValid, TError>
    {
        public static bool CheckNull<TValid>(TValid i)
        {
            if (i is DateTime)
            {
                return false;
            }
            else
            {
                // return EqualityComparer<TValid>.Default.Equals(i, default(TValid));
                return i == null;
            }
        }

        public static ValidationResult<TValid, TError> RunValidation(TValid Input, Func<TValid, bool> rule, TError NullMissingError, TError OtherError, bool canBeNull)
        {
            //If Input is null, check this is okay, if not: don't run the validation rule and return the given null or missing error
            if (CheckNull<TValid>(Input))
            {
                if ( canBeNull )
                {
                    return new OKResult<TValid, TError>(Input);
                }
                else
                {
                    return new ErrorResult<TValid, TError>(new List<TError> {NullMissingError});
                }
            }

            else
            {
                //Now check if the input passes the rule
                if (rule(Input))
                {
                    return new OKResult<TValid, TError>(Input);
                }
                else
                {
                    return new ErrorResult<TValid, TError>(new List<TError> {OtherError});
                }
            }

        }
    }
}
