//NuGet packages for Azure Blob Cloud and Pdfium Viewer(for url to image) + PdfiumViewer.Native.x86.v8-xfa(for dll issue)
using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Azure.Storage.Blobs;
using System.Net;

namespace PDFtoJPG
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
            
                var myUrl = "https://credentialingblob.blob.core.windows.net/certificate/Yash%20Kumar%20Rajora324.pdf";   //"https://www.africau.edu/images/default/sample.pdf"https://royalegroupnyc.com/wp-content/uploads/seating_areas/sample_pdf.pdf
                createImgFromUrl(myUrl).Wait();
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            } 
        }
        static async Task createImgFromUrl(string url)
        {
            try
            {
                // Download the PDF file from the Blob URL
                byte[] pdfBytes;
                using (WebClient webClient = new WebClient())
                {
                    pdfBytes = webClient.DownloadData(url);                                            // converting data of the url to pdfBytes
                }

                // Convert the PDF to an image
                MemoryStream pdfStream = new MemoryStream(pdfBytes);
                PdfiumViewer.PdfDocument pdfDocument = PdfiumViewer.PdfDocument.Load(pdfStream);       // using PdfiumViewer to load the pdf and render the pdf data
                Bitmap image = (Bitmap)pdfDocument.Render(0, 96, 96, false);                           // converting to bitmap image from document
                
               
                // Connect to Azure Blob Storage account
                string connectionString = "DefaultEndpointsProtocol=https;AccountName=mindc;AccountKey=VqU3eyGXTKNwmzaZ/i8NJ3dlqrRJn73/ApR6F6I+OWsJxsAeKDU8q9IZFgSu+h1RY2KYZjB1mgi3SproOV5DHA==;EndpointSuffix=core.windows.net";
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                // Get a reference of the container where to store the image
                string containerName = "nft";
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                // Generate a unique name for the blob
                string blobName = "BlobCertificate.jpg";

                // Upload the image to the blob storage
                MemoryStream imageStream = new MemoryStream();
                    
                image.Save(imageStream, ImageFormat.Jpeg);                                         //saving temporary instance on MemoryStream imageStream
                imageStream.Seek(0, SeekOrigin.Begin);
                await containerClient.UploadBlobAsync(blobName, imageStream);
                    

                Console.WriteLine("Image uploaded successfully.");

                var Url = containerClient.GetBlobClient(blobName).Uri;
                Console.WriteLine("Blob URL: " + Url);
            }            
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}