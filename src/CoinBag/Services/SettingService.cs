using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Models;

namespace CoinBag.Services
{
    public class SettingService : ISettingService
    {
	    private IPersistenceService persistenceService;

	    public SettingService(IPersistenceService persistenceService)
	    {
		    this.persistenceService = persistenceService;
	    }

	    public async Task<Setting> GetSetting()
	    {
		    var settings = await persistenceService.LoadObject<Setting>(Constants.SettingFile);
		    return settings ?? new Setting();
	    }

	    public async Task SaveSetting(Setting setting)
	    {
		    await persistenceService.SaveObject(setting, Constants.SettingFile);
	    }
    }
}
