using System;
using System.Collections.Generic;
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
        public async Task SaveTextFile(string filePath, string content, bool overwrite = true)
        {
            
        }

        public async Task<string> LoadTextFile(string filePath)
        {
            return "";
        }

        public async Task SaveToDownloads(string fileName, byte[] content)
        {
            
        }
    }
}