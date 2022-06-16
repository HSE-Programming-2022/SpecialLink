using SpecialLink.Core.Models.People;
using SpecialLink.Core.Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core
{
    class JSONStorage : IStorage
    {
        readonly Repository<Test> _tests;
        readonly Repository<Person> _persons;

        public JSONStorage()
        {
            // тут путь надо понять
            _tests = new Repository<Test>("../../../SpecialLink.Core/Data/tests.json");
            _persons = new Repository<Person>("../../../SpecialLink.Core/Data/persons.json");
        }


        public List<Test> GetTests => _tests.GetCollection;

        public List<Person> GetPersons => _persons.GetCollection;

        public void Save()
        {
            _tests.Save();
            _persons.Save();
        }
    }
}
