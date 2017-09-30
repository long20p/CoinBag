using System;
using System.Collections.Generic;
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
    }
}
