using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CoinBag.Services
{
    public interface IPersistenceService
    {
	    Task SaveObject<T>(T obj, string filePath, bool overwrite = true);
	    Task<T> LoadObject<T>(string filePath);
        Task SaveFromStream(string filePath, Action<Stream> save, bool overwrite = true);
        Task<T> LoadFromStream<T>(string filePath, Func<Stream, T> load);
    }
}
