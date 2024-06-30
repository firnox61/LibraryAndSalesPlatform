using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.BooksDetail;
using Entities.DTOs.NoteDetail;
using Entities.DTOs.ShareDetail;
using Entities.DTOs.UsersDetail;

namespace WebAPI.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Book,BookDetailDto>().ReverseMap();
            CreateMap<Book, BookListDto>().ReverseMap();
            CreateMap<Book, BookSearchDto>().ReverseMap();
            CreateMap<Book, BookUpdateDto>().ReverseMap();
            CreateMap<Book, BookCreateDto>().ReverseMap();

            CreateMap<Share, CreateShareDto>().ReverseMap();
            CreateMap<Share, ShareDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User,LoginUserDto>().ReverseMap();
            CreateMap<User, RegisterUserDto>().ReverseMap();

            CreateMap<Note, CreateNoteDTo>().ReverseMap();

        }
    }
}
