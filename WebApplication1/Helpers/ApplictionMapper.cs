using AutoMapper;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public class ApplictionMapper : Profile
    {
        public ApplictionMapper()
        {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
