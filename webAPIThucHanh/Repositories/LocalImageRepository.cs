using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using webAPIThucHanh.Data;
using webAPIThucHanh.Models.Domain;

namespace webAPIThucHanh.Repositories
{
	public class LocalImageRepository : IImageRepository
	{
		private readonly IWebHostEnvironment _webHostEnviroment;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly AppDbContext _dbContext;

		public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
		{
			_webHostEnviroment = webHostEnvironment;
			_httpContextAccessor = httpContextAccessor;
			_dbContext = dbContext;
		}

		public Image Upload(Image image)
		{
			var localFilePath = Path.Combine(_webHostEnviroment.ContentRootPath, "Images",
				$"{image.FileName}{image.FileExtension}");

			// upload Image to local Path
			using var stream = new FileStream(localFilePath, FileMode.Create);
			image.File.CopyTo(stream);

			// Construct URL file path
			var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
			image.FilePath = urlFilePath;

			// Add Image to the Images table
			_dbContext.Images.Add(image);
			_dbContext.SaveChanges();

			return image;
		}

		public List<Image> GetAllInfoImages()
		{
			return _dbContext.Images.ToList();
		}

		public (byte[], string, string) DownLoadFile(int Id)
		{
			try
			{
				var fileById = _dbContext.Images.FirstOrDefault(x => x.Id == Id);
				if (fileById == null)
				{
					throw new FileNotFoundException("File not found");
				}

				var path = Path.Combine(_webHostEnviroment.ContentRootPath, "Images", $"{fileById.FileName}{fileById.FileExtension}");
				var fileData = File.ReadAllBytes(path);
				var fileName = fileById.FileName + fileById.FileExtension;

				return (fileData, "application/octet-stream", fileName);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
