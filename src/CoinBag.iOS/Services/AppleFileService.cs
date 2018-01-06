using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Services;
using Foundation;
using UIKit;

namespace CoinBag.iOS.Services
{
    public class AppleFileService : IFileService
    {
        public string BasePath => "";

        public async Task SaveTextFile(string filePath, string content, bool overwrite = true)
        {
            
        }

        public async Task<string> LoadTextFile(string filePath)
        {
            return "";
        }

        public async Task SaveToBackupFolder(string fileName, byte[] content)
        {
            
        }

        public async Task SaveFromStream(string filePath, Action<Stream> save, bool overwrite = true)
        {
            
        }

        public async Task<T> LoadFromStream<T>(string filePath, Func<Stream, T> load)
        {
            return default(T);
        }
    }
}