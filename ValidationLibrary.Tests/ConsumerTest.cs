using System;
using Xunit;
using System.Collections.Generic;
// using ValidationLibrary;

namespace ValidationLibrary.Tests
{
    public class ConsumerTest
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            PersonInput person1 = new PersonInput("fifteen Charact", new DateTime(2001, 07, 09), 1);
            // Act
            PersonValidator personValidator = new PersonValidator();
            ValidationResult<ValidPerson, PersonValidations> result = personValidator.Validate(person1);
            // Assert
            Assert.True(result.isSuccess());
        }

        [Fact]
        public void Test2()
        {
            // Arrange
            PersonInput person1 = new PersonInput("fifteen Characte", new DateTime(2001, 07, 09), 2);
            // Act
            PersonValidator personValidator = new PersonValidator();
            ValidationResult<ValidPerson, PersonValidations> result = personValidator.Validate(person1);
            // Assert
            Assert.False(result.isSuccess());
        }

        [Fact]
        public void Test3()
        {
            // Arrange
            PersonInput person1 = new PersonInput(null, new DateTime(2001, 07, 09), 2);
            // Act
            PersonValidator personValidator = new PersonValidator();
            ValidationResult<ValidPerson, PersonValidations> result = personValidator.Validate(person1);
            // Assert
            Assert.False(result.isSuccess());
        }

        [Fact]
        public void Test4()
        {
            // Arrange
            PersonInput person1 = new PersonInput("Roy", new DateTime(2001, 07, 09), null);
            // Act
            PersonValidator personValidator = new PersonValidator();
            ValidationResult<ValidPerson, PersonValidations> result = personValidator.Validate(person1);
            // Assert
            Assert.True(result.isSuccess());
        }

        [Fact]
        public void Test5()
        {
            // Arrange
            PersonInput person1 = new PersonInput("Roy", new DateTime(), null);
            // Act
            PersonValidator personValidator = new PersonValidator();
            ValidationResult<ValidPerson, PersonValidations> result = personValidator.Validate(person1);
            // Assert
            Assert.False(result.isSuccess());
        }

        [Fact]
        public void Test6()
        {
            // Arrange
            PersonInput person1 = new PersonInput(null, new DateTime(), null);
            // Act
            PersonValidator personValidator = new PersonValidator();
            ValidationResult<ValidPerson, PersonValidations> result = personValidator.Validate(person1);
            // Assert
            Assert.False(result.isSuccess());
        }

        [Fact]
        public void Test7()
        {
            // Arrange
            PersonInput person1 = new PersonInput(null, new DateTime(2030, 07, 09), null);
            // Act
            PersonValidator personValidator = new PersonValidator();
            ValidationResult<ValidPerson, PersonValidations> result = personValidator.Validate(person1);
            // Assert
            Assert.False(result.isSuccess());
        }

        [Fact]
        public void Test8()
        {
            // Arrange
            PersonInput person1 = new PersonInput(null, new DateTime(2030, 07, 09), null);
            // Act
            PersonValidator personValidator = new PersonValidator();
            ValidationResult<ValidPerson, PersonValidations> result = personValidator.Validate(person1);
            IHasErrors<PersonValidations> tempError = result as IHasErrors<PersonValidations>;
            // Assert
            Assert.True(tempError.errors.Contains(PersonValidations.NameMissing) && tempError.errors.Contains(PersonValidations.InvalidDate));
        }
    }
}
