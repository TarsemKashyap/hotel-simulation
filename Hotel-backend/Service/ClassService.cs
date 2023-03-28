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
    Task<ClassSessionDto> Create(ClassSessionDto classSession);
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
    public async Task<ClassSessionDto> Create(ClassSessionDto classSession)
    {
        classSession.CreatedOn = DateTime.Now;
        classSession.Memo="";
        classSession.Code = RandomString();
        ClassSession session = _mapper.Map<ClassSessionDto, ClassSession>(classSession);
        _context.ClassSessions.Add(session);
        await _context.SaveChangesAsync();
        return _mapper.Map<ClassSessionDto>(session);


    }

    private string RandomString()
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public IEnumerable<ClassSessionDto> ClassList()
    {
        return _context.ClassSessions.ProjectToType<ClassSessionDto>().AsEnumerable();
    }

}
