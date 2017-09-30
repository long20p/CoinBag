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

		public AndroidFileService()
		{
			documentsRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CoinBag");
			if (!Directory.Exists(documentsRootPath))
			{
				Directory.CreateDirectory(documentsRootPath);
			}
		}

		public async Task SaveTextFile(string filePath, string content, bool overwrite = true)
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
	}
}