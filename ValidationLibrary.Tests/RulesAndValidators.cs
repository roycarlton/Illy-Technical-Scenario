using ValidationLibrary;
using System;
using System.Collections.Generic;

public enum PersonValidations
{
    NameMissing,
    NameTooLong,
    InvalidDate,
    BoroughOutOfRange,
    UnknownError
}

//Validation rules for the Person type
public static class PersonRules
{
    // public ValidationResult<string, PersonValidations> nameLengthCheck(string name)
    public static bool nameLengthCheck(string name)
    {
        //Name must be 15 characters or less
        return name.Length <= 15;
    }

    public static bool DOBInPastCheck(DateTime dateTime)
    {
        return dateTime >= new DateTime(1900, 01, 01) && dateTime < DateTime.Now;
    }

    public static bool BoroughInRangeCheck(int? borough)
    {
        int definiteBorough = borough ?? -1;
        return definiteBorough >=0 && definiteBorough < 10;
    }
}

//Specific validator for Person objects
public class PersonValidator : ValidationLibrary.IValidator<ValidPerson, PersonValidations, PersonInput>
{

    private bool valid;
    List<PersonValidations> errorList;
    private string validName;
    private DateTime validDOB;
    private int? validBorough;

    public PersonValidator()
    {
        valid = true;
        errorList = new List<PersonValidations> {};
    }

    public ValidationResult<ValidPerson, PersonValidations> Validate(PersonInput input)
    {
        //Run validation rules through ValidationRunnner.RunValidation
        ValidationResult<string?, PersonValidations> stringValid = ValidationRunnner<string?, PersonValidations>.RunValidation(input.Name, PersonRules.nameLengthCheck, PersonValidations.NameMissing, PersonValidations.NameTooLong, false);
        if (!stringValid.isSuccess())
        {
            IHasErrors<PersonValidations> tempError = stringValid as IHasErrors<PersonValidations>;
            errorList.AddRange(tempError.errors);
            valid = false;
        }
        else
        {
            validName = input.Name ?? "tempName";
        }

        ValidationResult<DateTime, PersonValidations> dateTimeValid = ValidationRunnner<DateTime, PersonValidations>.RunValidation(input.DOB, PersonRules.DOBInPastCheck, PersonValidations.InvalidDate, PersonValidations.InvalidDate, false);
        if (!dateTimeValid.isSuccess())
        {
            IHasErrors<PersonValidations> tempError = dateTimeValid as IHasErrors<PersonValidations>;
            errorList.AddRange(tempError.errors);
            valid = false;
        }
        else
        {
            validDOB = input.DOB;
        }

        ValidationResult<int?, PersonValidations> intValid = ValidationRunnner<int?, PersonValidations>.RunValidation(input.Borough, PersonRules.BoroughInRangeCheck, PersonValidations.UnknownError, PersonValidations.BoroughOutOfRange, true);
        if (!intValid.isSuccess())
        {
            IHasErrors<PersonValidations> tempError = intValid as IHasErrors<PersonValidations>;
            errorList.AddRange(tempError.errors);
            valid = false;
        }
        else
        {
            validBorough = input.Borough;
        }


        //Once input has been tested against all rules, if all passed, return the new valid object, otherwise return the error list
        if (valid)
        {
            return new OKResult<ValidPerson, PersonValidations>(new ValidPerson(validName, validDOB, validBorough));
        }
        else
        {
            return new ErrorResult<ValidPerson, PersonValidations>(errorList);
        }
    }
}