using webAPIThucHanh.Models.Domain;
using webAPIThucHanh.Models.DTO;

namespace webAPIThucHanh.Repositories
{
	public interface IPublisherRepository
	{
		List<PublisherDTO> GetAllPublishers();
		PublisherNoIdDTO GetPublisherById(int id);
		AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
		PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO);
		Publishers? DeletePublisherById(int id);
	}
}
