using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NBitcoin.Protocol;

namespace CoinBag.Services
{
    public class AddressManagerService : IAddressManagerService
    {
        private IFileService fileService;
        private string addrManagerFilePath;

        public AddressManagerService(IFileService fileService)
        {
            this.fileService = fileService;
            addrManagerFilePath = Path.Combine(this.fileService.BasePath, Constants.AddressManagerFile);
        }

        public AddressManager LoadAddressManager()
        {
            try
            {
                return AddressManager.LoadPeerFile(addrManagerFilePath);
            }
            catch (Exception e)
            {
                return new AddressManager();
            }
        }

        public void SaveAddressManager(AddressManager addrManager)
        {
            addrManager.SavePeerFile(addrManagerFilePath, Constants.CurrentNetwork);
        }
    }
}
