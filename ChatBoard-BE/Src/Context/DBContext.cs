using Common.Helpers;
using DTO.Model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Context
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountConfirmation> AccountConfirmations { get; set; } = null!;
        public virtual DbSet<DeviceActivity> DeviceActivities { get; set; } = null!;
        public virtual DbSet<UserDevice> UserDevices { get; set; } = null!;
        public virtual DbSet<ConstantSetting> ConstantSettings { get; set; } = null!;
        public virtual DbSet<Messages> Messages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql(AppSettingHelper.GetDefaultConnection());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_buffercache")
                .HasPostgresExtension("pg_stat_statements");

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.ToTable("Messages");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Accounts");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            });
            modelBuilder.Entity<AccountConfirmation>(entity =>
            {
                entity.ToTable("AccountConfirmations");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            });
            modelBuilder.Entity<DeviceActivity>(entity =>
            {
                entity.ToTable("DeviceActivities");
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

                entity.HasOne(d => d.Account)
                    .WithMany(x => x.DeviceActivities)
                    .HasForeignKey(at => at.AccountId);
            });
            modelBuilder.Entity<UserDevice>(entity =>
            {
                entity.ToTable("UserDevices");
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

                entity.HasOne(d => d.Account)
                    .WithMany(x => x.UserDevices)
                    .HasForeignKey(at => at.AccountId);
            });
            modelBuilder.Entity<ConstantSetting>(entity =>
            {
                entity.ToTable("ConstantSetting");
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            });
            
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
