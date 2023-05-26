//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JSONtoBlob
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {



//        }
//    }
//}

using Azure.Storage.Blobs;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System;
using System.Threading.Tasks;

namespace JSONtoBlob {
    // Define a class for your data
    internal class Program
    {
        public class MyData
        {
            public string name { get; set; }
            public string description { get; set; }
            public string image { get; set; }    
            // Add other properties as needed
        }

        static async Task Main(string[] args)
        {
            // Generate JSON data
            MyData data = new MyData
            {
                name = "Test JSON",
                description = "This is just some testing",
                image = "https://credentialingblob.blob.core.windows.net/certificate/Yash%20Kumar%20Rajora324.pdf"
                // Set other properties as needed
            };

            string jsonData = JsonConvert.SerializeObject(data);

            // Connect to your Azure Blob storage account
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=mindc;AccountKey=VqU3eyGXTKNwmzaZ/i8NJ3dlqrRJn73/ApR6F6I+OWsJxsAeKDU8q9IZFgSu+h1RY2KYZjB1mgi3SproOV5DHA==;EndpointSuffix=core.windows.net";
            string containerName = "nft";
            string blobName = "TestJson3";

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Create a new blob and upload the JSON data
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
            await blobClient.UploadAsync(stream);
            Console.WriteLine("JSON data stored in Blob successfully!");
            Console.WriteLine("URL : " + blobClient.Uri);
            Console.ReadLine();
        }
    }
}

