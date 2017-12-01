using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CoinBag.Services
{
    public interface IFileService
    {
        string BasePath { get; }
        Task SaveTextFile(string filePath, string content, bool overwrite = true);
	    Task<string> LoadTextFile(string filePath);
        Task SaveToDownloads(string fileName, byte[] content);
        Task SaveFromStream(string filePath, Action<Stream> save, bool overwrite = true);
        Task<T> LoadFromStream<T>(string filePath, Func<Stream, T> load);
    }
}
