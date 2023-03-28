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
    }
}
