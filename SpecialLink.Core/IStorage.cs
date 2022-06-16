using SpecialLink.Core.Models.People;
using SpecialLink.Core.Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core
{
    public interface IStorage
    {
        void Save();

        List<Person> GetPersons { get; }

        List<Test> GetTests { get; }
    }
}
