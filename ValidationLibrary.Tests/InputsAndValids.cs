using System;
using ValidationLibrary;

//Example person object that might be recieved from the client
public class PersonInput
{
    public string? Name;
    public DateTime DOB;
    public int? Borough;
    
    public PersonInput(string? name, DateTime dob, int? borough)
    {
        Name = name;
        DOB = dob;
        Borough = borough;
    }
}

//Example person object that has been validated into the correct form for saving
public class ValidPerson
{
    public string Name;
    public DateTime DOB;
    public int? Borough;
    
    public ValidPerson(string name, DateTime dob, int? borough)
    {
        Name = name;
        dob = dob;
        Borough = borough;
    }
}