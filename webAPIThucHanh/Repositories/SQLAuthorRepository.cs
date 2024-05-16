using webAPIThucHanh.Data;
using webAPIThucHanh.Models.Domain;
using webAPIThucHanh.Models.DTO;
using webAPIThucHanh.Repositories;

namespace webAPIThucHanh.Repositories
{
    public class SQLAuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _dbContext;
        public SQLAuthorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<AuthorDTO> GellAllAuthors()
        {
            //Get Data From Database -Domain Model 
            var allAuthorsDomain = _dbContext.Author.ToList();
            //Map domain models to DTOs 
            var allAuthorDTO = new List<AuthorDTO>();
            foreach (var authorDomain in allAuthorsDomain)
            {
                allAuthorDTO.Add(new AuthorDTO()
                {
                    Id = authorDomain.ID,
                    FullName = authorDomain.FullName
                });
            }
            //return DTOs 
            return allAuthorDTO;
        }
        public AuthorNoIdDTO GetAuthorById(int id)
        {
            // get book Domain model from Db
            var authorWithIdDomain = _dbContext.Author.FirstOrDefault(x => x.ID ==
           id);
            if (authorWithIdDomain == null)
            {
                return null;
            }
            //Map Domain Model to DTOs 
            var authorNoIdDTO = new AuthorNoIdDTO
            {
                FullName = authorWithIdDomain.FullName,
            };
            return authorNoIdDTO;
        }
        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var authorDomainModel = new Authors
            {
                FullName = addAuthorRequestDTO.FullName,
            };
            //Use Domain Model to create Author 
            _dbContext.Author.Add(authorDomainModel);
            _dbContext.SaveChanges();
            return addAuthorRequestDTO;
        }
        public AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var authorDomain = _dbContext.Author.FirstOrDefault(n => n.ID == id);
            if (authorDomain != null)
            {
                authorDomain.FullName = authorNoIdDTO.FullName;
                _dbContext.SaveChanges();
            }
            return authorNoIdDTO;
        }
        public Authors? DeleteAuthorById(int id)
        {
            var authorDomain = _dbContext.Author.FirstOrDefault(n => n.ID == id);
            if (authorDomain != null)
            {
                _dbContext.Author.Remove(authorDomain);
                _dbContext.SaveChanges();
            }
            return null;
        }
    }
}
