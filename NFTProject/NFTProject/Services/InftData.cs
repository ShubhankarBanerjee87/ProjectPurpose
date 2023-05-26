using NFTProject.Models;

namespace NFTProject.Services
{
    public interface InftData
    {
        

        public Task<bool> AddNewNFT(NFTData data);
        public Task<IEnumerable<NFTData>?> GetNFTDataByToken(uint id);
    }
}
