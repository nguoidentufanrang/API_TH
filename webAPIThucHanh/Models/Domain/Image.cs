﻿using System.ComponentModel.DataAnnotations.Schema;
using webAPIThucHanh.Repositories;
using Microsoft.AspNetCore.Http;

namespace webAPIThucHanh.Models.Domain
{
	public class Image
	{
		public int Id { get; set; }
		[NotMapped]
		public IFormFile? File { get; set; }
		public string? FileName { get; set; }
		public string? FileDescription { get; set; }
		public string? FileExtension { get; set; }
		public long FileSizeInBytes { get; set; }
		public string? FilePath { get; set; }
	}
}
