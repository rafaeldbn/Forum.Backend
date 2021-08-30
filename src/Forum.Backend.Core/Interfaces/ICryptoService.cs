using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Backend.Core.Interfaces
{
    public interface ICryptoService
    {
        string CreateSalt();
        string CreateHash(string value, string salt);
    }
}
