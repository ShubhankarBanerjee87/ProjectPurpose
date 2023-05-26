using Microsoft.AspNetCore.Components;
using Nethereum.Contracts.Standards.ERC20.TokenList;
using Newtonsoft.Json.Linq;
using NFTProject.Models;
using NFTProject.Services;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace NFTProject.Pages
{
    public partial class NFTWallet
    {
        [Parameter]
        public string NFTAddress { get; set; }
        public IEnumerable<NFTData> NFTEntries;
        public JArray? NFTArray;
        [Inject]
        public InftData? NFTServices
        {
            get;
            set;
        }
        NFTData NFT = new NFTData();
        protected override async Task OnInitializedAsync()
        {
            //await this.GetNFTDetails(1);
        }
        protected async Task GetNFTDetails(uint[] id)
        {
            Console.WriteLine("Function Called");
            for (uint i = 0; i < id.Length; i++)
            {
                Console.WriteLine("Inside Loop");

                NFTEntries = await NFTServices.GetNFTDataByToken(id[i]);
                Console.WriteLine(NFTEntries);
                //NFTArray.Add(NFTEntries);
            }
            Console.WriteLine(NFTArray);
        }

        public async Task NFTHolderAsync(string Address)
        {
            Console.WriteLine("Button Clicked");
            HttpClient client = new HttpClient();
            string contractAddress = "0xbD208e0dDFA1ddA9Ed846311A70B1e19C4Cdcd16";          //"0xf6253B3700b9295668f3728B6cDE57b699d00DD1"
            string accountAddress = Address;           //"0xB0202cAfD05a1b5d090dad6d88e513f92DFAe431", "0xD21489BE412C7037fE70CD172732b85D4F563581"
            string apiUrl = $"https://api-testnet.polygonscan.com/api?module=account&action=tokennfttx&contractaddress={contractAddress}&address={accountAddress}&page=1&offset=100&sort=asc&apikey=DCVP5SHM7P3TUVSJWZ7XYYM5S2Y5HCIGEA";

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseBody+"\n\n\n\n");

                // Parse the JSON response
                JObject json = JObject.Parse(responseBody);
                JArray resultArray = (JArray)json["result"];
                if (resultArray.Count > 0)
                {
                    uint[] tokenID = new uint[resultArray.Count];
                    for (Int32 i = 0; i < resultArray.Count; i++)
                    {
                        tokenID[i] = (uint)resultArray[i]["tokenID"];
                    }
                    GetNFTDetails(tokenID);
                }
                else
                {
                    Console.WriteLine("No NFT Created by this address.");
                }

                Console.ReadLine();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.ReadLine();

            }

        }
        //protected async Task CreateSales()
        //{
        //    await SalesServices.SaveSalesDetails(sales);
        //    await this.GetSalesDetails();
        //    this.ClearAll();
        //}
        //protected async Task UpdateSales()
        //{
        //    await SalesServices.SaveSalesDetails(sales);
        //    await this.GetSalesDetails();
        //    this.ClearAll();
        //}
        //protected async Task GetSalesById(int SalesId)
        //{
        //    sales = await SalesServices.GetSalesById(SalesId);
        //    sales.IsUpdate = true;
        //    await this.GetSalesDetails();
        //}
        //protected async Task DeleteSales(int SalesId)
        //{
        //    await SalesServices.DeleteSales(SalesId);
        //    await this.GetSalesDetails();
        //}
        //public void ClearAll()
        //{
        //    sales.ProductName = string.Empty;
        //    sales.Quantity = 0;
        //}
    }
}
