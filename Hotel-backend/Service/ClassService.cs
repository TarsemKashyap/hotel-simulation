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

using Common.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

public interface IClassSessionService
{
    Task<IEnumerable<ClassGroupDto>> AddGroupAsync(int classId, ClassGroupDto[] classGroup);
    IEnumerable<ClassSessionDto> ClassList();
    Task<ClassSessionDto> Create(ClassSessionDto classSession);
    IEnumerable<ClassSessionDto> List(string instructorId = null);
    Task<ClassSessionUpdateDto> GetById(int classId);
    Task<ClassSessionDto> Update(int id, ClassSessionUpdateDto account);
    Task DeleteId(int classId);
    Task<IList<ClassGroupDto>> StudentGroupList();

    Task<IList<MonthDto>> MonthFilterList(int classId);
    Task<IList<ClassGroupDto>> GetGroupList(int classId);
    Task AddStudentInClass(string studentId, string classCode);


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
        var classSessionEntity = _context.ClassSessions.Include(x => x.Groups).SingleOrDefault(x => x.ClassId == classId);

        var groups = classSession.Groups.ToLookup(x => x.Action);
        var newlyAdded = groups[ActionOnRecord.Added];
        var removedItems = groups[ActionOnRecord.Removed].Select(x => x.GroupId);
        var updated = groups[ActionOnRecord.Updated];


        classSessionEntity.Groups.RemoveAll(x => removedItems.Contains(x.GroupId));


        foreach (var removed in newlyAdded)
        {
            var group = removed.Adapt<ClassGroup>();
            classSessionEntity.Groups.Add(group);
        }


        foreach (var removed in updated)
        {

            int index = classSessionEntity.Groups.FindIndex(x => x.GroupId == removed.GroupId);
            var group = removed.Adapt<ClassGroup>();
            classSessionEntity.Groups[index] = group;

        }

        classSessionEntity.CurrentQuater = classSession.CurrentQuater;
        classSessionEntity.Title = classSession.Title;
        classSessionEntity.CurrentQuater = classSession.CurrentQuater;
        classSessionEntity.HotelsCount = classSession.Groups.Count();
        var result = _context.ClassSessions.Update(classSessionEntity);
        await _context.SaveChangesAsync();
        return _mapper.Map<ClassSessionDto>(result);

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
    public async Task<IList<ClassGroupDto>> StudentGroupList()
    {
        var users = _context.ClassGroups.ToList();
        return users.Adapt<IList<ClassGroupDto>>();

    }

    public async Task<IList<ClassGroupDto>> GetGroupList(int classId)
    {
        var groups = _context.ClassGroups.Where(c => c.ClassId == classId).ToList();
        return groups.Adapt<IList<ClassGroupDto>>();
    }
    public async Task<IList<MonthDto>> MonthFilterList(int classId)
    {
        var sequence = _context.Months.Where(c => c.ClassId == classId && c.Sequence != 0).OrderBy(s => s.Sequence).ToList();
        return sequence.Adapt<IList<MonthDto>>();

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
        var result = query
        .Join(
            _context.AppUsers,
            session => session.CreatedBy,
            user => user.Id,
            (session, user) => new { Session = session, User = user })
        .OrderByDescending(x => x.Session.CreatedOn)
        .Select(x => new ClassSessionDto
        {
            StartDate = x.Session.StartDate,
            EndDate = x.Session.EndDate,
            ClassId = x.Session.ClassId,
            Title = x.Session.Title,
            CreatedBy = $"{x.User.FirstName} {x.User.LastName}",
            CreatedOn = x.Session.CreatedOn,
            Code = x.Session.Code,
        })
        .ToList();

        return result;
        //return query.OrderByDescending(x => x.CreatedOn).ProjectToType<ClassSessionDto>().AsEnumerable();

    }


    public async Task<ClassSessionUpdateDto> GetById(int classId)
    {
        var appUser = _context.ClassSessions.Include(x => x.Groups).FirstOrDefault(x => x.ClassId == classId);
        ;
        if (appUser == null)
            throw new ValidationException("class not found for given classId");
        return appUser.Adapt<ClassSessionUpdateDto>();
    }

    public async Task DeleteId(int classId)
    {
        var appUser = _context.ClassSessions.Include(x => x.Groups).FirstOrDefault(x => x.ClassId == classId);
        if (appUser != null)
        {
            var data = _context.ClassSessions.Remove(appUser);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddStudentInClass(string studentId, string classCode)
    {
        Student student = await _context.Students.FindAsync(studentId);
        if (student == null)
            throw new ValidationException($"Student does not exist with id {studentId}");
        ClassSession classSession = await _context.ClassSessions.Where(x => x.Code.Equals(classCode, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync();
        if (classSession == null)
            throw new ValidationException($"Class does not exists with class code {classCode}");

        bool alreadyExist = await _context.StudentClassMapping.AnyAsync(x => x.StudentId == studentId && x.ClassId == classSession.ClassId);
        if (alreadyExist)
        {
            throw new ValidationException($"Mapping already exist for this student and class");
        }

        var mapping = new Database.Domain.StudentClassMapping { Class = classSession, Student = student };
        _context.StudentClassMapping.Add(mapping);
        await _context.SaveChangesAsync();
    }
}