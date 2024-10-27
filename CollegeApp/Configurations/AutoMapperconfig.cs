using System.Runtime;
using AutoMapper;
using CollegeApp.DTO;
using CollegeApp.Model;

namespace CollegeApp.Configurations
{
    public class AutoMapperconfig: Profile
    {

        public AutoMapperconfig()
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(n => n.Name, opt => opt.MapFrom(x=>x.StudentName))
                .ReverseMap();

            CreateMap<RoleDTO, Roles>().ReverseMap();
            CreateMap<RolePrivilegeDTO, RolePrivilege>().ReverseMap();
            CreateMap<UserDTO, Usertable>().ReverseMap();
        }

    }
}
