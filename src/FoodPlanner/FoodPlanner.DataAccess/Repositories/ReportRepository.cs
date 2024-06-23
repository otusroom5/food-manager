using FoodPlanner.DataAccess.Entities;
using FoodPlanner.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace FoodPlanner.DataAccess.Implementations;

public class ReportRepository: IReportRepository
{
    private readonly DbContext _context;
    private readonly DbSet<ReportEntity> _dbSet;

    public ReportRepository(InMemoryDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<ReportEntity>();
    }

    public void Create(ReportEntity report)
    {
        _dbSet.Add(report);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }

    public byte[]? GetAttachmentById(Guid id)
    {
        return _dbSet.Find(id)?.ReportContent;        
    }
}
