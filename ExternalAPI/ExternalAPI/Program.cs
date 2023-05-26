using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

class Program
{
    static HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        string contractAddress = "0xbD208e0dDFA1ddA9Ed846311A70B1e19C4Cdcd16";          //"0xf6253B3700b9295668f3728B6cDE57b699d00DD1"
        string accountAddress = "0xD21489BE412C7037fE70CD172732b85D4F563581";           //"0xB0202cAfD05a1b5d090dad6d88e513f92DFAe431"
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
                int[] tokenID = new int[resultArray.Count];
                for (Int32 i = 0; i < resultArray.Count; i++)
                {
                    tokenID[i] = (int)resultArray[i]["tokenID"];
                }
                Console.Write("Tokens created by the id are : ");
                for (int i = 0; i < tokenID.Length; i++)
                {
                    Console.Write(tokenID[i]+",");
                }
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
}

