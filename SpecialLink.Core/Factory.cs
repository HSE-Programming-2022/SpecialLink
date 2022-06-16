using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core
{
    public class Factory
    {
        private Factory() { }

        private static Factory _instance;

        public static Factory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Factory();
            }

            return _instance;
        }


        private IStorage _storage;

        public IStorage Storage => _storage ?? new JSONStorage();
    }
}
