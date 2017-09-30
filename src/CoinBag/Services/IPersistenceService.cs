using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoinBag.Services
{
    public interface IPersistenceService
    {
	    Task SaveObject<T>(T obj, string filePath, bool overwrite = true);
	    Task<T> LoadObject<T>(string filePath);
    }
}
