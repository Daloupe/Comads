using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
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
        static readonly Random R = new Random();

        static Queue<string> LastNames;
        static Queue<Name> FirstNames;
        static Queue<Address> Addresses;

        static void GenerateLastNames(int count)
        {
            LastNames = new Queue<string>(
                Enumerable
                .Range(0, count)
                .Select(n => Generator.GenerateName().Last));
        }

        static void GenerateFirstNames(int count)
        {
            FirstNames = new Queue<Name>(
               Enumerable
               .Range(0, count)
               .Select(n => Generator.GenerateName(true, true)));
        }

        static void GenerateAddresses(int count)
        {
            Addresses = new Queue<Address>(
                Enumerable
                .Range(0, count)
                .Select(n => Generator.GenerateAddress()));
        }

        public static IEnumerable<Person> GenerateFamilies(int count, int size)
        {
            var personCount = count * size;

            GenerateLastNames(count);
            GenerateAddresses(count);
            GenerateFirstNames(personCount);

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

        public static Person Create(Name firstName, string lastName, Address address)
        {
            return new Person()
            {
                FirstName = firstName.First,
                MiddleName = firstName.Middle,
                LastName = lastName,
                Gender = firstName.Gender,
                Address = address
            };
        }

        public static Person Create(string firstName, string lastName, Address address, Gender gender)
        {
            var identity = Generator.GenerateName();

            return new Person()
            {
                FirstName = firstName,
                MiddleName = identity.Middle,
                LastName = lastName,
                Gender = gender,
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
