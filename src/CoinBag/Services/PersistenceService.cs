using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoinBag.Services
{
    public class PersistenceService : IPersistenceService
    {
	    private readonly IFileService fileService;

	    public PersistenceService(IFileService fileService)
	    {
		    this.fileService = fileService;
	    }

	    public async Task SaveObject<T>(T obj, string filePath, bool overwrite = true)
	    {
		    try
		    {
			    var content = JsonConvert.SerializeObject(obj);
			    await fileService.SaveTextFile(filePath, content, overwrite);
			}
		    catch (Exception e)
		    {
			    Console.WriteLine(e);
			    throw;
		    }
	    }

	    public async Task<T> LoadObject<T>(string filePath)
	    {
		    var content = await fileService.LoadTextFile(filePath);
		    return content != null ? JsonConvert.DeserializeObject<T>(content) : default(T);
	    }

        public async Task SaveFromStream(string filePath, Action<Stream> save, bool overwrite = true)
        {
            await fileService.SaveFromStream(filePath, save, overwrite);
        }

        public async Task<T> LoadFromStream<T>(string filePath, Func<Stream, T> load)
        {
            return await fileService.LoadFromStream(filePath, load);
        }
    }
}
