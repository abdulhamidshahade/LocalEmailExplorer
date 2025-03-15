using AutoMapper;
using LocalEmailExplorer.Services.EmailAPI.Models.DTOs;

namespace LocalEmailExplorer.Services.EmailAPI.Halpers.AutoMappers
{
    public class EmailAutoMapping : Profile
    {
        public EmailAutoMapping()
        {
            CreateMap<Email, EmailDto>().ReverseMap();
            CreateMap<Email, CreateEmailDto>().ReverseMap();
            CreateMap<Email, UpdateEmailDto>().ReverseMap();
            CreateMap<Email, DeleteEmailDto>().ReverseMap();
        }
    }
}
