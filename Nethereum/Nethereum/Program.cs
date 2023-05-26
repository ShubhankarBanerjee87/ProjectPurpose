using System;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using Nethereum.Util;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
using Nethereum.RPC.Eth.DTOs;
using NBitcoin;
using Nethereum.HdWallet;


namespace Nethereum.Web3
{
    class Program
    {
        string contractAddress = "0x228e4561c02E1d301A8064a92Fd160cfdF2b63ec";                                       // contract address of the deployed contract
        // contract abi of the same deployed contract
        string contractAbi = @"[                 
    {
        ""inputs"": [],
        ""name"": ""getValue"",
        ""outputs"": [
            {
                ""internalType"": ""uint256"",
                ""name"": """",
                ""type"": ""uint256""
            }
        ],
        ""stateMutability"": ""view"",
        ""type"": ""function""
    },
    {
        ""inputs"": [
            {
                ""internalType"": ""uint256"",
                ""name"": ""_a"",
                ""type"": ""uint256""
            }
        ],
        ""name"": ""setValue"",
        ""outputs"": [],
        ""stateMutability"": ""nonpayable"",
        ""type"": ""function""
    }
]";
        string mnemo = "pluck word voyage math proof tell margin cave black version soldier raw";
        static void Main(string[] args)
        {
            //GetAccountBalance().Wait();                                                                                    // Function which will fetch the balance of the account
            //InteractWithGetFunctionsinContract().Wait();                                                                   // calling a function which will interact with a view or pure type function in smart contract          
            //InteractWithSetFunctionsinContract().Wait();                                                                   // calling a function which will interact with contract and sign a transaction
            MemonicsToAccount().Wait();         //0xB0202cAfD05a1b5d090dad6d88e513f92DFAe431                                                                              // recovering account address and private key if mnemonics are present.
            Console.ReadLine();
        }
        //Function which will fetch the balance of the account
        static async Task GetAccountBalance()
        {
            string account_address = "0xD21489BE412C7037fE70CD172732b85D4F563581";
            var web3 = new Web3("https://polygon-mumbai.g.alchemy.com/v2/uC5SCJuLhV_bH5W5FArY_xgOatHnc8JK");              // connecting to polygon network using rpc-endpoint
            var balance = await web3.Eth.GetBalance.SendRequestAsync(account_address);                                    // fetching the balance of the account
            Console.WriteLine($"Balance in Wei: {balance.Value}");

            var maticAmount = Web3.Convert.FromWei(balance.Value);
            Console.WriteLine($"Balance in Matic: {maticAmount}");
        }
        //function which will interact with a view or pure type function in smart contract
        static async Task InteractWithGetFunctionsinContract()
        {
            var p = new Program();
            var web3 = new Web3("https://polygon-mumbai.g.alchemy.com/v2/uC5SCJuLhV_bH5W5FArY_xgOatHnc8JK");
            var contract = web3.Eth.GetContract(p.contractAbi, p.contractAddress);                                        // creating an instance of the contract
            // Call the contract method
            var result = await contract.GetFunction("getValue").CallAsync<uint>();                                       // calling a view function which do not needs any kind of transaction
            Console.WriteLine(result);
        }
        //function which will interact with contract and sign a transaction
        static async Task InteractWithSetFunctionsinContract()
        {
            var p = new Program();
            var senderAddress = "0xD21489BE412C7037fE70CD172732b85D4F563581";                                           //transaction sender's address
            
            Console.WriteLine("Enter the number you want to set : ");         
            var number = Convert.ToInt32(Console.ReadLine());

            var url = "https://polygon-mumbai.g.alchemy.com/v2/uC5SCJuLhV_bH5W5FArY_xgOatHnc8JK";                           //Alchemy RPC-URL
            var privateKey = "0x55a605e45fddcf7eadbc785aa248ba5dbaecec40589b654069796b426e5c7c6e";
            var account = new Account(privateKey);                                                                          // creating instance of the account

            var web3 = new Web3(account, url);                                                                              // creating instance of the web3 to sign using account and rpc-url
            var contract = web3.Eth.GetContract(p.contractAbi, p.contractAddress);                                                      // creating instance of the contract
            var setValueFunction = contract.GetFunction("setValue");                                                        // creating an instance of the function of the contract : here setValue

            var gas = await setValueFunction.EstimateGasAsync(senderAddress, null, null, number);                           //estimating the gas which will reduce the gas limit
            var receipt =
                await setValueFunction.SendTransactionAndWaitForReceiptAsync(senderAddress, gas, null, null, number);       //getting the recipt and signing the transaction : here 'number' is the parameter of the function in the contract
            var transactionValidityStatus = receipt.Status.Value;                                                           //extracting the status value of the transaction as boolean 
            var transactionHash = receipt.TransactionHash;                                                                  //extracting transaction hash from receipt

            if (transactionValidityStatus == 1)
            {
                Console.WriteLine($"status :  SUCCESS");
                Console.WriteLine($"transaction hash :  {transactionHash}");
            } //end of if
            else
            { 
                Console.WriteLine($"status :  FAILED"); 
            }   //end of else
            InteractWithGetFunctionsinContract().Wait();                                                                    //calling a function which will interact with a view or pure type function in smart contract
        }

        static async Task  MemonicsToAccount(){
            //Mnemonic mnemo1 = new Mnemonic(Wordlist.English, WordCount.Twelve);
            Program p = new Program();
            //var backupSeed = p.mnemo.ToString();

            //Console.WriteLine(mnemo1);
            //Console.WriteLine(p.mnemo);
            //Console.WriteLine(typeof(mnemo1);
            //Console.WriteLine(typeof(p.mnemo));

            //var wallet3 = new Wallet(backupSeed, Password2);
            //var recoveredAccount = wallet3.GetAccount(0);
            try
            {
                Console.WriteLine("In the function");
                var web3 = new Web3("https://polygon-mumbai.g.alchemy.com/v2/uC5SCJuLhV_bH5W5FArY_xgOatHnc8JK");

                var wallet = new Wallet(p.mnemo, "");
                for (int i = 0; i < 12; i++)
                {
                    var address = wallet.GetAccount(i).Address;
                    Console.WriteLine($"Address{i+1} : " + address);
                }
                var privateKey = wallet.GetPrivateKey(0);
                //Console.WriteLine("Private Key : " + privateKey);
                var privateKeyBytes = new byte[] { /* your private key byte array goes here */ };
                var privateKeyHex = BitConverter.ToString(privateKey).Replace("-", string.Empty).ToLower();

                Console.WriteLine($"Private Key (hex): {privateKeyHex}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}



