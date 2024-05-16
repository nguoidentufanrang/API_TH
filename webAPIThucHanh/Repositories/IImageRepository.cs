using webAPIThucHanh.Models.Domain;
using webAPIThucHanh.Repositories;

namespace webAPIThucHanh.Repositories
{
	public interface IImageRepository
	{
		Image Upload(Image image);
		List<Image> GetAllInfoImages();
		(byte[], string, string) DownLoadFile(int Id);
	}
}
