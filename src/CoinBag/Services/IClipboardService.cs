using System;
using System.Collections.Generic;
using System.Text;

namespace CoinBag.Services
{
    public interface IClipboardService
    {
        void CopyToClipboard(string text);
    }
}
