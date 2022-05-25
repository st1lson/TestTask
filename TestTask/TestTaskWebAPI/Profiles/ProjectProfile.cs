using AutoMapper;
using System;
using TestTaskData.Models;
using TestTaskWebAPI.Data.Inputs;

namespace TestTaskWebAPI.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            // Source -> Target
            CreateMap<CreateProjectInput, Project>()
                .ForMember(p => p.Id, options =>
                    options.MapFrom(_ => Guid.NewGuid().ToString()));

            CreateMap<UpdateProjectInput, Project>();
        }
    }
}
