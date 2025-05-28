using Microsoft.EntityFrameworkCore;
using _4_Dominio;

namespace _2_Infraestructura
{
    public class ProyectosContext : DbContext
    {
        public ProyectosContext() 
        {
        }
        public ProyectosContext(DbContextOptions<ProyectosContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ProyectosDB;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }


        public DbSet<Area> Areas { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<ApprovalStatus> ApprovalStatuses { get; set; }
        public DbSet<ApproverRole> ApproverRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ApprovalRule> ApprovalRules { get; set; }
        public DbSet<ProjectProposal> ProjectProposals { get; set; }
        public DbSet<ProjectApprovalStep> ProjectApprovalSteps { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Area
            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");
                // Clave primaria
                entity.HasKey(a => a.Id);
                // Configuración de columnas
                entity.Property(a => a.Id).HasColumnName("Id").IsRequired();
                entity.Property(a => a.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(25)");
            });

            // ProjectType
            modelBuilder.Entity<ProjectType>(entity =>
            {
                entity.ToTable("ProjectType");
                // Clave primaria
                entity.HasKey(p => p.Id);
                // Configuración de columnas
                entity.Property(p => p.Id).HasColumnName("Id").IsRequired();
                entity.Property(p => p.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(25)");
            });

            // ApprovalStatus
            modelBuilder.Entity<ApprovalStatus>(entity =>
            {
                entity.ToTable("ApprovalStatus");
                // Clave primaria
                entity.HasKey(a => a.Id);
                // Configuración de columnas
                entity.Property(a => a.Id).HasColumnName("Id").IsRequired();
                entity.Property(a => a.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(25)");
            });

            // ApproverRole
            modelBuilder.Entity<ApproverRole>(entity =>
            {
                entity.ToTable("ApproverRole");
                // Clave primaria
                entity.HasKey(a => a.Id);
                // Configuración de columnas
                entity.Property(a => a.Id).HasColumnName("Id").IsRequired();
                entity.Property(a => a.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(25)");
            });

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                // Clave primaria
                entity.HasKey(u => u.Id);
                // Configuración de columnas
                entity.Property(u => u.Id).HasColumnName("Id").IsRequired();
                entity.Property(u => u.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(25)");
                entity.Property(u => u.Email).HasColumnName("Email").IsRequired().HasColumnType("varchar(100)");
                entity.Property(u => u.RoleId).HasColumnName("Role").IsRequired();
                // Relación FK
                entity.HasOne<ApproverRole>(u => u.Role)
                      .WithMany()
                      .HasForeignKey(u => u.RoleId);
            });

            // ProjectProposal
            modelBuilder.Entity<ProjectProposal>(entity =>
            {
                entity.ToTable("ProjectProposal");
                // Clave primaria
                entity.HasKey(p => p.Id);
                // Configuración de propiedades
                entity.Property(p => p.Id).HasColumnName("Id").IsRequired();
                entity.Property(p => p.Title).HasColumnName("Title").IsRequired().HasColumnType("varchar(255)");
                entity.Property(p => p.Description).HasColumnName("Description").HasColumnType("varchar(max)");
                entity.Property(p => p.AreaId).HasColumnName("Area").IsRequired();
                entity.Property(p => p.TypeId).HasColumnName("Type").IsRequired();
                entity.Property(p => p.EstimatedAmount).HasColumnName("EstimatedAmount").HasPrecision(18, 2);
                entity.Property(p => p.EstimatedDuration).HasColumnName("EstimatedDuration").IsRequired();
                entity.Property(p => p.StatusId).HasColumnName("Status").IsRequired();
                entity.Property(p => p.CreateAt).HasColumnName("CreateAt").HasColumnType("datetime").IsRequired();
                entity.Property(p => p.CreateById).HasColumnName("CreateBy").IsRequired();
                // Relación FK
                entity.HasOne<Area>(p => p.Area)
                      .WithMany()
                      .HasForeignKey(p => p.AreaId);
                entity.HasOne<ProjectType>(p => p.Type)
                      .WithMany()
                      .HasForeignKey(p => p.TypeId);
                entity.HasOne<ApprovalStatus>(p => p.Status)
                      .WithMany()
                      .HasForeignKey(p => p.StatusId);
                entity.HasOne<User>(p => p.CreateBy)
                      .WithMany()
                      .HasForeignKey(p => p.CreateById);
            });

            // ProjectApprovalStep
            modelBuilder.Entity<ProjectApprovalStep>(entity =>
            {
                entity.ToTable("ProjectApprovalStep");
                // Clave primaria
                entity.HasKey(p => p.Id);
                // Configuración de propiedades
                entity.Property(p => p.Id).HasColumnName("Id").IsRequired();
                entity.Property(p => p.ProjectProposalId).HasColumnName("ProjectProposalId").IsRequired();
                entity.Property(p => p.ApproverUserId).HasColumnName("ApproverUserId").IsRequired(false);
                entity.Property(p => p.ApproverRoleId).HasColumnName("ApproverRoleId").IsRequired();
                entity.Property(p => p.StatusId).HasColumnName("Status").IsRequired();
                entity.Property(p => p.StepOrder).HasColumnName("StepOrder").IsRequired();
                entity.Property(p => p.DecisionDate).HasColumnName("DecisionDate").HasColumnType("datetime").IsRequired(false);
                entity.Property(p => p.Observations).HasColumnName("Observations").IsRequired(false).HasColumnType("varchar(max)");
                // Relación FK
                entity.HasOne<ProjectProposal>(p => p.ProjectProposal)
                      .WithMany()
                      .HasForeignKey(p => p.ProjectProposalId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<User>(p => p.ApproverUser)
                      .WithMany()
                      .HasForeignKey(p => p.ApproverUserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<ApproverRole>(p => p.ApproverRole)
                      .WithMany()
                      .HasForeignKey(p => p.ApproverRoleId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<ApprovalStatus>(p => p.Status)
                      .WithMany()
                      .HasForeignKey(p => p.StatusId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ApprovalRule
            modelBuilder.Entity<ApprovalRule>(entity =>
            {
                entity.ToTable("ApprovalRule");
                // Clave primaria
                entity.HasKey(a => a.Id);
                // Configuración de propiedades
                entity.Property(a => a.MinAmount).HasColumnName("MinAmount").HasPrecision(18, 2);
                entity.Property(a => a.MaxAmount).HasColumnName("MaxAmount").HasPrecision(18, 2);
                entity.Property(a => a.AreaId).HasColumnName("Area").IsRequired(false);
                entity.Property(a => a.TypeId).HasColumnName("Type").IsRequired(false);
                entity.Property(a => a.StepOrder).HasColumnName("StepOrder").IsRequired();
                entity.Property(a => a.ApproverRoleId).HasColumnName("ApproverRoleId").IsRequired();
                // Relación FK
                entity.HasOne<Area>(a => a.Area)
                      .WithMany()
                      .HasForeignKey(a => a.AreaId);
                entity.HasOne<ProjectType>(a => a.Type)
                      .WithMany()
                      .HasForeignKey(a => a.TypeId);
                entity.HasOne<ApproverRole>(a => a.ApproverRole)
                      .WithMany()
                      .HasForeignKey(a => a.ApproverRoleId); 
            });



            modelBuilder.Entity<Area>().HasData(
                new Area { Id = 1, Name = "Finanzas" },
                new Area { Id = 2, Name = "Tecnología" },
                new Area { Id = 3, Name = "Recursos Humanos" },
                new Area { Id = 4, Name = "Operaciones" }
                );

            modelBuilder.Entity<ProjectType>().HasData(
                new ProjectType { Id = 1, Name = "Mejora de Procesos" },
                new ProjectType { Id = 2, Name = "Innovación y Desarrollo" },
                new ProjectType { Id = 3, Name = "Infraestructura" },
                new ProjectType { Id = 4, Name = "Capacitación Interna" }
                );

            modelBuilder.Entity<ApprovalStatus>().HasData(
                new ApprovalStatus { Id = 1, Name = "Pending" },
                new ApprovalStatus { Id = 2, Name = "Approved" },
                new ApprovalStatus { Id = 3, Name = "Rejected" },
                new ApprovalStatus { Id = 4, Name = "Observed" }
                );

            modelBuilder.Entity<ApproverRole>().HasData(
                new ApproverRole { Id = 1, Name = "Líder de Área" },
                new ApproverRole { Id = 2, Name = "Gerente" },
                new ApproverRole { Id = 3, Name = "Director" },
                new ApproverRole { Id = 4, Name = "Comité Tecnico" }
                );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "José Ferreyra",   Email = "jferreyra@unaj.com", RoleId = 2 },
                new User { Id = 2, Name = "Ana Lucero",      Email = "alucero@unaj.com",   RoleId = 1 },
                new User { Id = 3, Name = "Gonzalo Molinas", Email = "gmolinas@unaj.com",  RoleId = 2 },
                new User { Id = 4, Name = "Lucas Olivera",   Email = "lolivera@unaj.com",  RoleId = 3 },
                new User { Id = 5, Name = "Danilo Fagundez", Email = "dfagundez@unaj.com", RoleId = 4 },
                new User { Id = 6, Name = "Gabriel Galli",   Email = "ggalli@unaj.com",    RoleId = 4 }
                );

            modelBuilder.Entity<ApprovalRule>().HasData(
                new ApprovalRule 
                { 
                    Id = 1,
                    MinAmount = 0,
                    MaxAmount = 100000,
                    AreaId = null,
                    TypeId = null,
                    StepOrder = 1,
                    ApproverRoleId = 1 
                },
                new ApprovalRule 
                { 
                    Id = 2, 
                    MinAmount = 5000,  
                    MaxAmount = 20000,  
                    AreaId = null, 
                    TypeId = null, 
                    StepOrder = 2, 
                    ApproverRoleId = 2 
                },
                new ApprovalRule 
                { 
                    Id = 3, 
                    MinAmount = 0,     
                    MaxAmount = 20000,  
                    AreaId = 2,    
                    TypeId = 2,    
                    StepOrder = 1, 
                    ApproverRoleId = 2 
                },
                new ApprovalRule 
                { 
                    Id = 4, 
                    MinAmount = 20000, 
                    MaxAmount = 0,      
                    AreaId = null, 
                    TypeId = null, 
                    StepOrder = 3, 
                    ApproverRoleId = 3 
                },
                new ApprovalRule 
                { 
                    Id = 5, 
                    MinAmount = 5000,  
                    MaxAmount = 0,      
                    AreaId = 1,    
                    TypeId = 1,    
                    StepOrder = 2, 
                    ApproverRoleId = 2 
                },
                new ApprovalRule 
                { 
                    Id = 6, 
                    MinAmount = 0,     
                    MaxAmount = 10000,  
                    AreaId = null, 
                    TypeId = 2,    
                    StepOrder = 1, 
                    ApproverRoleId = 1 
                },
                new ApprovalRule 
                { 
                    Id = 7, 
                    MinAmount = 0,     
                    MaxAmount = 10000,  
                    AreaId = 2,    
                    TypeId = 1,    
                    StepOrder = 1, 
                    ApproverRoleId = 4 
                },
                new ApprovalRule 
                { 
                    Id = 8, 
                    MinAmount = 10000, 
                    MaxAmount = 30000,  
                    AreaId = 2,    
                    TypeId = null, 
                    StepOrder = 2, 
                    ApproverRoleId = 2 
                },
                new ApprovalRule 
                { 
                    Id = 9, 
                    MinAmount = 30000, 
                    MaxAmount = 0,     
                    AreaId = 3,    
                    TypeId = null, 
                    StepOrder = 2, 
                    ApproverRoleId = 3 
                },
                new ApprovalRule 
                { 
                    Id = 10, 
                    MinAmount = 0,    
                    MaxAmount = 50000,  
                    AreaId = null, 
                    TypeId = 4,    
                    StepOrder = 1, 
                    ApproverRoleId = 4 
                }
                );
        }

    }
}
