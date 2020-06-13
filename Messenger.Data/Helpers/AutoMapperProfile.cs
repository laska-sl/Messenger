using AutoMapper;

using Messenger.Data.DTOs;
using Messenger.Data.Models;

namespace Messenger.Data.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<MessageDTO, Message>();
        }
    }
}
