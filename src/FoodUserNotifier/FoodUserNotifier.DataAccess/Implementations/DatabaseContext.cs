﻿using FoodUserNotifier.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodUserNotifier.DataAccess.Implementations;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Recepient> Recepients { get; set; }
}
