using AutoMapper;
using REST.BLL.Models;
using REST.DataAccess.Models;

namespace REST.BLL.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<DbPerson, Person>();
            CreateMap<Person, DbPerson>();
            CreateMap<Person, UpdatePersonRequest>();
            CreateMap<DbPerson, UpdatePersonRequest>();
            CreateMap<UpdatePersonRequest, DbPerson>();
            CreateMap<UpdatePersonRequest, Person>();
        }
    }
}