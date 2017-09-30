using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Models;

namespace CoinBag.Services
{
    public interface ISettingService
    {
	    Task<Setting> GetSetting();
	    Task SaveSetting(Setting setting);
    }
}
