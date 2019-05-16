using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonApi.Models.Data
{
    public class LandonApiDbContext : DbContext
    {
        public LandonApiDbContext(DbContextOptions<LandonApiDbContext> options)
        : base(options)
        { }

        public DbSet<RoomEntity> RoomEntities { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
