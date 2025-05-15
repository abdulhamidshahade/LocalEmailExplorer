using AutoMapper;
using LocalEmailExplorer.Application.DTOs.EmailDtos;
using LocalEmailExplorer.Domain.Entities.EmailEntities;

namespace LocalEmailExplorer.Application.Halpers.AutoMappers.EmailAutoMapping
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
