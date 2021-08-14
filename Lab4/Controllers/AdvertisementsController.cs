using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab4.Models.ViewModels;
using Lab4.Data;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Azure;
using System.IO;
using Lab4.Models;

namespace Lab4.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly SchoolCommunityContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string containerName = "advertisementimages";

        public AdvertisementsController(SchoolCommunityContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

        // GET: AdvertisementsController
        public async Task<ActionResult> Index(string Id)
        {
            if(Id != null) {
                var viewModel = new CommunityViewModel();
                viewModel.Communities = await _context.Communities
                    .Include(i => i.CommunityAdvertisement)
                    .ThenInclude(i => i.Advertisements).ToListAsync();

                ViewData["CommunityID"] = Id;
                viewModel.CommunityAdvertisements = viewModel.Communities
                    .Where(i => i.Id == Id)
                    .Single().CommunityAdvertisement;

                return View(viewModel);
            }
            return View("ERROR");
        }

         // GET: AdvertisementsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvertisementsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id, IFormFile file)
        {
            BlobContainerClient containerClient;

            try
            {
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }
            catch (RequestFailedException)
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }

            try
            {
                var blockBlob = containerClient.GetBlobClient(file.FileName);
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);


                    memoryStream.Position = 0;

                    await blockBlob.UploadAsync(memoryStream);
                    memoryStream.Close();
                }


                var image = new Advertisements();
                image.Url = blockBlob.Uri.AbsoluteUri;
                image.FileName = file.FileName;

                _context.Advertisements.Add(image);

                await _context.SaveChangesAsync();

                var cs = new CommunityAdvertisement();
                cs.AdvertisementID = image.AdId;
                cs.CommunityID = id;
                _context.CommunityAdvertisements.Add(cs);

                await _context.SaveChangesAsync();

            }
            catch (RequestFailedException)
            {
                View("Error");
            }

            return RedirectToAction("Index", new { id });
        }

        // GET: AdvertisementsController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Advertisements
                .FirstOrDefaultAsync(m => m.AdId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Advertisements.FindAsync(id);
            var communityId = _context.CommunityAdvertisements.Where(i => i.AdvertisementID == id).Single().CommunityID;

            BlobContainerClient containerClient;

            try
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }
            catch (RequestFailedException)
            {
                return View("Error");
            }

            try
            {
                var blockBlob = containerClient.GetBlobClient(image.FileName);
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                _context.Advertisements.Remove(image);
                await _context.SaveChangesAsync();

            }
            catch (RequestFailedException)
            {
                return View("Error");
            }

            return RedirectToAction("Index", new { id = communityId });
        }
    }
}
