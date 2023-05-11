using Microsoft.AspNetCore.Mvc;
using Parking.Business.Business.Interface;
using Parking.Model.Account;
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
namespace ParkingApp.Controllers
{
    public class AccountController : Controller
    {
        private IAccountBusiness accountBusiness;


        public AccountController(IAccountBusiness accountBusiness)
        {
            this.accountBusiness = accountBusiness;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAccount(UserMaster um)
        {

            if (um.email == null)
            {
                ModelState.AddModelError("email", "This field is required");
                return View("Index", um);
            }
            if (um.email.Length>40)
            {
                ModelState.AddModelError("email", "Max length is 40");
                return View("Index", um);
            }
            if (um.password == null)
            {
                ModelState.AddModelError("password", "This field is required");
                return View("Index", um);
            }
            if (um.password.Length > 12)
            {
                ModelState.AddModelError("password", "Max length is 12");
                return View("Index", um);
            }
            if (um.name == null)
            {
                ModelState.AddModelError("name", "This field is required");
                return View("Index", um);
            }
            if (um.name.Length > 40)
            {
                ModelState.AddModelError("password", "Max length is 40");
                return View("Index", um);
            }
            string connectionstring = "DefaultEndpointsProtocol=https;AccountName=myblobmvcaccount;AccountKey=3893vSQbopCiFi+8SFd2iF0leNATVa/EMk0LTjUkaIokBFkhHz9ZXe/VqbPXl2T0ybO9uomXHrjl+AStvxLTpA==;EndpointSuffix=core.windows.net";
            string containerName = "blob-container";
            string blobName = "";
            string filePath = "";
            string blob_url = "";

            if (um.File != null && um.File.Length != 0)
            {
                string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "wwwroot", "userprofile"));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (var fileStream = new FileStream(Path.Combine(path, um.email + "_" + um.File.FileName), FileMode.Create))
                {
                    await um.File.CopyToAsync(fileStream);
                }
                filePath = path + "/" + um.email + "_" + um.File.FileName;
                blobName = um.email + "_" + um.File.FileName;
                BlobContainerClient blobServiceClient = new BlobContainerClient(connectionstring, containerName);

                BlobClient blobClient = blobServiceClient.GetBlobClient(blobName);

                await blobClient.UploadAsync(filePath, true);

                //blob_url = blobClient.Uri.AbsoluteUri;
                um.file_path = blobClient.Uri.AbsoluteUri;

            }
            //um.file_path = blob_url;


            await this.accountBusiness.Create(um);
            TempData["Message"] = "Account created";
            return View("Index");
        }
    }
}
