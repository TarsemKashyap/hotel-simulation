using System;
using Mapster;

public class MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ClassSessionDto, ClassSession>()
        .Map(dest => dest.StartDate, src => DateOnly.FromDateTime(src.StartDate))
        .Map(dest => dest.EndDate, src => DateOnly.FromDateTime(src.EndDate));

        config.NewConfig<ClassSession, ClassSessionDto>()
        .Map(dest => dest.StartDate, src => src.StartDate.ToDateTime(TimeOnly.MinValue))
        .Map(dest => dest.EndDate, src => src.EndDate.ToDateTime(TimeOnly.MinValue));

        config.NewConfig<Instructor, InstructorDto>().TwoWays()
        .Map(dest => dest.FirstName, src => src.FirstName)
        .Map(dest => dest.LastName, src => src.LastName)
        .Map(dest => dest.Email, src => src.Email)
        .Map(dest => dest.Institute, src => src.Institute)
        .Map(dest => dest.UserId, src => src.Id);
    }
}
