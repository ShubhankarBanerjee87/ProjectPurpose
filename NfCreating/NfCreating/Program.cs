using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Contracts;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfCreating
{
    internal class Program
    {
		private readonly string contract_abi = @"[
	{
		'inputs': [],
		'stateMutability': 'nonpayable',
		'type': 'constructor'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': true,
				'internalType': 'address',
				'name': 'owner',
				'type': 'address'
			},
			{
				'indexed': true,
				'internalType': 'address',
				'name': 'approved',
				'type': 'address'
			},
			{
				'indexed': true,
				'internalType': 'uint256',
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'Approval',
		'type': 'event'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': true,
				'internalType': 'address',
				'name': 'owner',
				'type': 'address'
			},
			{
				'indexed': true,
				'internalType': 'address',
				'name': 'operator',
				'type': 'address'
			},
			{
				'indexed': false,
				'internalType': 'bool',
				'name': 'approved',
				'type': 'bool'
			}
		],
		'name': 'ApprovalForAll',
		'type': 'event'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': true,
				'internalType': 'address',
				'name': 'previousOwner',
				'type': 'address'
			},
			{
				'indexed': true,
				'internalType': 'address',
				'name': 'newOwner',
				'type': 'address'
			}
		],
		'name': 'OwnershipTransferred',
		'type': 'event'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': true,
				'internalType': 'address',
				'name': 'from',
				'type': 'address'
			},
			{
				'indexed': true,
				'internalType': 'address',
				'name': 'to',
				'type': 'address'
			},
			{
				'indexed': true,
				'internalType': 'uint256',
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'Transfer',
		'type': 'event'
	},
	{
		'inputs': [
			{
				'internalType': 'uint256',
				'name': 'tokenId',
				'type': 'uint256'
			},
			{
				'internalType': 'bytes32',
				'name': '_imageHash',
				'type': 'bytes32'
			}
		],
		'name': 'VerifyMetadata',
		'outputs': [
			{
				'internalType': 'bool',
				'name': '',
				'type': 'bool'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'address',
				'name': 'to',
				'type': 'address'
			},
			{
				'internalType': 'uint256',
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'approve',
		'outputs': [],
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'address',
				'name': 'owner',
				'type': 'address'
			}
		],
		'name': 'balanceOf',
		'outputs': [
			{
				'internalType': 'uint256',
				'name': '',
				'type': 'uint256'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'uint256',
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'getApproved',
		'outputs': [
			{
				'internalType': 'address',
				'name': '',
				'type': 'address'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'address',
				'name': 'owner',
				'type': 'address'
			},
			{
				'internalType': 'address',
				'name': 'operator',
				'type': 'address'
			}
		],
		'name': 'isApprovedForAll',
		'outputs': [
			{
				'internalType': 'bool',
				'name': '',
				'type': 'bool'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [],
		'name': 'name',
		'outputs': [
			{
				'internalType': 'string',
				'name': '',
				'type': 'string'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [],
		'name': 'owner',
		'outputs': [
			{
				'internalType': 'address',
				'name': '',
				'type': 'address'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'uint256',
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'ownerOf',
		'outputs': [
			{
				'internalType': 'address',
				'name': '',
				'type': 'address'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [],
		'name': 'renounceOwnership',
		'outputs': [],
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'address',
				'name': 'to',
				'type': 'address'
			},
			{
				'internalType': 'string',
				'name': '_name',
				'type': 'string'
			},
			{
				'internalType': 'string',
				'name': '_description',
				'type': 'string'
			},
			{
				'internalType': 'bytes32',
				'name': '_image',
				'type': 'bytes32'
			}
		],
		'name': 'safeMint',
		'outputs': [],
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'address',
				'name': 'from',
				'type': 'address'
			},
			{
				'internalType': 'address',
				'name': 'to',
				'type': 'address'
			},
			{
				'internalType': 'uint256',
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'safeTransferFrom',
		'outputs': [],
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'address',
				'name': 'from',
				'type': 'address'
			},
			{
				'internalType': 'address',
				'name': 'to',
				'type': 'address'
			},
			{
				'internalType': 'uint256',
				'name': 'tokenId',
				'type': 'uint256'
			},
			{
				'internalType': 'bytes',
				'name': 'data',
				'type': 'bytes'
			}
		],
		'name': 'safeTransferFrom',
		'outputs': [],
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'address',
				'name': 'operator',
				'type': 'address'
			},
			{
				'internalType': 'bool',
				'name': 'approved',
				'type': 'bool'
			}
		],
		'name': 'setApprovalForAll',
		'outputs': [],
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'bytes4',
				'name': 'interfaceId',
				'type': 'bytes4'
			}
		],
		'name': 'supportsInterface',
		'outputs': [
			{
				'internalType': 'bool',
				'name': '',
				'type': 'bool'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [],
		'name': 'symbol',
		'outputs': [
			{
				'internalType': 'string',
				'name': '',
				'type': 'string'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'uint256',
				'name': 'index',
				'type': 'uint256'
			}
		],
		'name': 'tokenByIndex',
		'outputs': [
			{
				'internalType': 'uint256',
				'name': '',
				'type': 'uint256'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'address',
				'name': 'owner',
				'type': 'address'
			},
			{
				'internalType': 'uint256',
				'name': 'index',
				'type': 'uint256'
			}
		],
		'name': 'tokenOfOwnerByIndex',
		'outputs': [
			{
				'internalType': 'uint256',
				'name': '',
				'type': 'uint256'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'uint256',
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'tokenURI',
		'outputs': [
			{
				'internalType': 'string',
				'name': '',
				'type': 'string'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [],
		'name': 'token_Id',
		'outputs': [
			{
				'internalType': 'uint256',
				'name': '',
				'type': 'uint256'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [],
		'name': 'totalSupply',
		'outputs': [
			{
				'internalType': 'uint256',
				'name': '',
				'type': 'uint256'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'address',
				'name': 'from',
				'type': 'address'
			},
			{
				'internalType': 'address',
				'name': 'to',
				'type': 'address'
			},
			{
				'internalType': 'uint256',
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'transferFrom',
		'outputs': [],
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'internalType': 'address',
				'name': 'newOwner',
				'type': 'address'
			}
		],
		'name': 'transferOwnership',
		'outputs': [],
		'stateMutability': 'nonpayable',
		'type': 'function'
	}
]";
		private readonly string contract_address = "0xbD208e0dDFA1ddA9Ed846311A70B1e19C4Cdcd16";
        static void Main(string[] args)
        {
            var web3 = new Web3("https://polygon-mumbai.g.alchemy.com/v2/uC5SCJuLhV_bH5W5FArY_xgOatHnc8JK");
			//InteractWithGetFunctionsinContract().Wait();
			MintNFT().Wait();
            Console.ReadLine();

        }

        static async  Task<uint> GetTokenID()
        {
			try
			{


				var p = new Program();
				var web3 = new Web3("https://polygon-mumbai.g.alchemy.com/v2/uC5SCJuLhV_bH5W5FArY_xgOatHnc8JK");
                //var Contract = await web3.Eth.GetContract(p.contract_address);
                //var contract_abi = Contract.Abi;
                var contract = web3.Eth.GetContract(p.contract_abi, p.contract_address);                                     // creating an instance of the contract
																															 // Call the contract method
				var result = await contract.GetFunction("token_Id").CallAsync<uint>();                                       // calling a view function which do not needs any kind of transaction

				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return 0;
			}
        }

		static async Task MintNFT()
		{
			try
			{
				var p = new Program();
				var senderAddress = "0xD21489BE412C7037fE70CD172732b85D4F563581";                                               //transaction sender's address

				Console.WriteLine("Enter the address of the user whose NFT is to be minted : ");
				var user_address = (Console.ReadLine());
				Console.WriteLine("Enter the Name of the NFT : ");
				var NFT_Name = (Console.ReadLine());
                Console.WriteLine("Enter the Description of the NFT : ");
                var NFT_Description = (Console.ReadLine());
				

				//Image URL
                var imageUrl = "https://credentialingblob.blob.core.windows.net/user/Divyansh_Profile5/3/2023%204:47:30%20AM.jpeg";

                // Download the image data from the URL
                byte[] imageData;
                using (var webClient = new WebClient())
                {
                    imageData = webClient.DownloadData(imageUrl);
                }

                // Calculate the hash of the image data
                byte[] imageHash;
                using (var sha256 = SHA256.Create())
                {
                    imageHash = sha256.ComputeHash(imageData);
                }

                // Ensure the hash length is exactly 32 bytes
                if (imageHash.Length != 32)
                {
                    Console.WriteLine("Invalid image hash length. Expected 32 bytes.");
                    return;
                }

                // Use the imageHash directly as bytes32Value
                byte[] Imagebytes32Value = imageHash;


                var url = "https://polygon-mumbai.g.alchemy.com/v2/uC5SCJuLhV_bH5W5FArY_xgOatHnc8JK";                           //Alchemy RPC-URL
				var privateKey = "0x55a605e45fddcf7eadbc785aa248ba5dbaecec40589b654069796b426e5c7c6e";
				var account = new Account(privateKey);                                                                          // creating instance of the account   {using Nethereum.account}
				//Console.WriteLine(account.Address);
				var web3 = new Web3(account, url);                                                                              // creating instance of the web3 to sign using account and rpc-url
				var contract = web3.Eth.GetContract(p.contract_abi, p.contract_address);                                        // creating instance of the contract
				var safeMintFunction = contract.GetFunction("safeMint");                                                        // creating an instance of the function of the contract : here setValue

				var gas = await safeMintFunction.EstimateGasAsync(senderAddress, null, null, user_address, NFT_Name, NFT_Description, Imagebytes32Value);            //estimating the gas which will reduce the gas limit
				var receipt =
					await safeMintFunction.SendTransactionAndWaitForReceiptAsync(senderAddress, gas, null, null, user_address, NFT_Name, NFT_Description, Imagebytes32Value);       //getting the recipt and signing the transaction : here 'number' is the parameter of the function in the contract
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
				var tokenID = await GetTokenID();
				Console.WriteLine($"Token ID = {tokenID}");
			}
			catch( Exception ex )
			{
				Console.WriteLine( ex.ToString() );
			}
		}
	}
}