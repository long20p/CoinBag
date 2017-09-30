using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoinBag.Services
{
    public interface IFileService
    {
	    Task SaveTextFile(string filePath, string content, bool overwrite = true);
	    Task<string> LoadTextFile(string filePath);
	}
}
