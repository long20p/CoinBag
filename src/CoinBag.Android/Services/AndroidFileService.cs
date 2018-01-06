using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CoinBag.Exceptions;
using CoinBag.Services;
using Environment = System.Environment;

namespace CoinBag.Droid.Services
{
	public class AndroidFileService : IFileService
	{
		private string documentsRootPath;
	    private string backupRootPath;

		public AndroidFileService()
		{
		    documentsRootPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Constants.MainAppFolder); //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CoinBag");
            backupRootPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Constants.BackupFolder);

		    new List<string> {documentsRootPath, backupRootPath}.ForEach(path =>
		    {
		        if (!Directory.Exists(path))
		        {
		            Directory.CreateDirectory(path);
		        }
		    });

		}

	    public string BasePath => documentsRootPath;

	    public async Task SaveTextFile(string filePath, string content, bool overwrite = true)
		{
		    var fullPath = GetFullPath(filePath, overwrite);
			File.WriteAllText(fullPath, content);
		}

		public async Task<string> LoadTextFile(string filePath)
		{
			var fullPath = Path.Combine(documentsRootPath, filePath);
			if (!File.Exists(fullPath))
			{
				return null;
			}
			return await Task.FromResult(File.ReadAllText(fullPath));
		}

	    public async Task SaveToBackupFolder(string fileName, byte[] content)
	    {
	        var fullPath = Path.Combine(backupRootPath, fileName);
            File.WriteAllBytes(fullPath, content);
	    }

	    public async Task SaveFromStream(string filePath, Action<Stream> save, bool overwrite = true)
	    {
	        var fullPath = GetFullPath(filePath, overwrite);
	        using (var fs = File.Open(fullPath, FileMode.Create, FileAccess.Write))
	        {
	            save(fs);
	        }
	    }

	    public async Task<T> LoadFromStream<T>(string filePath, Func<Stream, T> load)
	    {
	        var fullPath = Path.Combine(documentsRootPath, filePath);
	        if (!File.Exists(fullPath))
	        {
	            return default(T);
	        }

	        using (var fs = File.Open(fullPath, FileMode.Open, FileAccess.Read))
	        {
	            return load(fs);
	        }
        }

	    private string GetFullPath(string filePath, bool overwrite)
	    {
	        var fullPath = Path.Combine(documentsRootPath, filePath);
	        var dir = Path.GetDirectoryName(fullPath);
	        if (!Directory.Exists(dir))
	        {
	            Directory.CreateDirectory(dir);
	        }
	        if (!overwrite && File.Exists(fullPath))
	        {
	            throw new FileExistException($"File {filePath} already exists and overwriting is not requested");
	        }

	        return fullPath;
	    }
	}
}