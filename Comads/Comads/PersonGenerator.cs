using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using EfficientlyLazy.IdentityGenerator;
using EfficientlyLazy.IdentityGenerator.Entity;

namespace Comads
{
    public class Person
    {
        public string FirstName { get; set; }
        public string MiddleName {get; set;}
        public string LastName {get; set;}
        public Address Address { get; set; }
        public Gender Gender { get; set; }

        public Person()
        {

        }
    }
    public static class PersonGenerator
    {
        static Random R = new Random();

        public static Queue<string> LastNames;
        public static Queue<string> FirstNames;
        public static Queue<Address> Addresses;

        public static void GenerateLastNames(int count)
        {
            LastNames = new Queue<string>(
                Generator
                .SetOptions()
                .IncludeGenderBoth()
                .CreateGenerator()
                .GenerateIdentities(count)
                .Select(n => n.Last));  
        }

        public static void GenerateFirstNames(int count)
        {
            FirstNames = new Queue<string>(
                Generator
            .SetOptions()
            .IncludeGenderBoth()
            .CreateGenerator()
            .GenerateIdentities(count)
            .Select(n => n.First)
            .ToImmutableList());
        }

        public static void GenerateAddresses(int count)
        {
            Addresses = new Queue<Address>(
                Generator
            .SetOptions()
            .IncludeAddress()
            .CreateGenerator()
            .GenerateIdentities(count)
            .Select(identity => identity.Address));
        }

        public static IEnumerable<Person> GenerateFamilies(int count, int size)
        {
            var personCount = count * size;
            GenerateLastNames(count);
            GenerateFirstNames(personCount);
            GenerateAddresses(count);

            for (int i = 0; i < count; i++)
            {
                var surname = LastNames.Dequeue();
                var address = Addresses.Dequeue();

                for (int j = 0; j < R.Next(1, size); j++)
                {
                    var name = FirstNames.Dequeue();
                    yield return Create(name, surname, address);
                }
            }
        }

        public static Person Create(Person person)
        {
            return Create(person.FirstName, person.LastName, person.Address, person.Gender);
        }

        public static Person Create(string firstName, string lastName, Address address, Gender? gender = null, DateTime? dob = null)
        {
            var _generator = Generator
            .SetOptions()
            .IncludeDOB()
            .IncludeGenderBoth()
            .SetAgeRange(8, 40)
            .IncludeAddress()
            .IncludeSSN()
            .CreateGenerator();

            var identity = _generator.GenerateIdentity();
            return new Person()
            {
                FirstName = firstName,
                MiddleName = identity.Middle,
                LastName = lastName,
                Gender = gender ?? (Gender)Enum.Parse(typeof(Gender), identity.Gender.ToString()),
                Address = address
            };
        }


        public static Person Create()
        {
            var _generator = Generator
            .SetOptions()
            .IncludeDOB()
            .IncludeGenderBoth()
            .SetAgeRange(8, 40)
            .IncludeAddress()
            .IncludeSSN()
            .CreateGenerator();

            var identity = _generator.GenerateIdentity();

            return new Person()
            {
                FirstName = identity.First,
                MiddleName = identity.Middle,
                LastName = identity.Last,
                Gender = (Gender)Enum.Parse(typeof(Gender), identity.Gender.ToString())
            };
        }
    }
}
