using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Model;
using Database;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Service;
using Mapster;
using MapsterMapper;

public interface IClassSessionService
{
    IEnumerable<ClassSessionDto> ClassList();
    Task Create(ClassSessionDto classSession);
}

public class ClassSessionService : IClassSessionService
{
    private readonly IMapper _mapper;
    private readonly HotelDbContext _context;

    public ClassSessionService(IMapper mapper, HotelDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task Create(ClassSessionDto classSession)
    {
        ClassSession session = _mapper.Map<ClassSessionDto, ClassSession>(classSession);
        _context.ClassSessions.Add(session);
        await _context.SaveChangesAsync();

    }

    public IEnumerable<ClassSessionDto> ClassList()
    {
        return _context.ClassSessions.ProjectToType<ClassSessionDto>().AsEnumerable();
    }

}
