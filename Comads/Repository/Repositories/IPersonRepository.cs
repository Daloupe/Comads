using System;
using System.Collections.Generic;
using System.Text;
using Domain.Aggregates.Person;

namespace Repository.Repositories
{
    public interface IPersonRepository
    {
        Person GetPerson(string Id);
    }
}
