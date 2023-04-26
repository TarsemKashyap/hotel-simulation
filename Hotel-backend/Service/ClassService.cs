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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

public interface IClassSessionService
{
    Task<IEnumerable<ClassGroupDto>> AddGroupAsync(int classId, ClassGroupDto[] classGroup);
    IEnumerable<ClassSessionDto> ClassList();
    Task<ClassSessionDto> Create(ClassSessionDto classSession);
    IEnumerable<ClassSessionDto> List(string instructorId = null);
    Task<ClassSessionUpdateDto> GetById(int classId);
    Task<ClassSessionDto> Update(int id, ClassSessionUpdateDto account);
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
        classSession.Memo = "";
        classSession.Code = RandomString();
        ClassSession session = _mapper.Map<ClassSessionDto, ClassSession>(classSession);
        _context.ClassSessions.Add(session);
        await _context.SaveChangesAsync();
        return _mapper.Map<ClassSessionDto>(session);


    }
    public async Task<ClassSessionDto> Update(int classId, ClassSessionUpdateDto classSession)
    {
        var appUser = _context.ClassSessions.SingleOrDefault(x => x.ClassId == classId);
        foreach (var item in classSession.Removed)
        {
            var classGroup = item.Adapt<ClassGroup>();
            appUser.Groups.Remove(classGroup);
        }
        foreach (var item in classSession.Added)
        {
            var classGroup = item.Adapt<ClassGroup>();
            appUser.Groups.Add(classGroup);
        }
        appUser.Adapt(classSession);
        _context.ClassSessions.Update(appUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<ClassSessionDto>(appUser);

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

    public async Task<IEnumerable<ClassGroupDto>> AddGroupAsync(int classId, ClassGroupDto[] classGroup)
    {
        ClassSession classSession = await _context.ClassSessions.FindAsync(classId);
        var group = classGroup.Adapt<ClassGroup[]>();
        foreach (var item in group)
        {
            classSession.Groups.Add(item);
        }
        await _context.SaveChangesAsync();
        return classSession.Groups.Adapt<IEnumerable<ClassGroupDto>>();
    }

    public IEnumerable<ClassSessionDto> List(string instructorId = null)
    {
        IQueryable<ClassSession> query = _context.ClassSessions;
        if (!string.IsNullOrEmpty(instructorId))
        {
            query = query.Where(x => x.CreatedBy == instructorId);
        }

        return query.OrderByDescending(x => x.CreatedOn).ProjectToType<ClassSessionDto>().AsEnumerable();

    }

    public async Task<ClassSessionUpdateDto> GetById(int classId)
    {
        var appUser = _context.ClassSessions.Include(x => x.Groups).FirstOrDefault(x => x.ClassId == classId);
        ;
        if (appUser == null)
            throw new ValidationException("class not found for given classId");
        return appUser.Adapt<ClassSessionUpdateDto>();
    }

 
}