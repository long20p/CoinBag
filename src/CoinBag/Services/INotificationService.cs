using System;
using System.Collections.Generic;
using System.Text;

namespace CoinBag.Services
{
    public interface INotificationService
    {
        void ShowInfo(string message);
        void ShowError(string errorMessage);
    }
}
