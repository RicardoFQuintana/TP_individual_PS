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
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=UserDB;Trusted_Connection=True;TrustServerCertificate=True");
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

            modelBuilder.Entity<Area>().ToTable("Area");
            modelBuilder.Entity<Area>().Property(a => a.Name).IsRequired().HasMaxLength(25);

            modelBuilder.Entity<ProjectType>().ToTable("ProjectType");
            modelBuilder.Entity<ProjectType>().Property(a => a.Name).IsRequired().HasMaxLength(25);

            modelBuilder.Entity<ApprovalStatus>().ToTable("ApprovalStatus");
            modelBuilder.Entity<ApprovalStatus>().Property(a => a.Name).IsRequired().HasMaxLength(25);

            modelBuilder.Entity<ApproverRole>().ToTable("ApproverRole");
            modelBuilder.Entity<ApproverRole>().Property(a => a.Name).IsRequired().HasMaxLength(25);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().Property(a => a.Name).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<User>().Property(e => e.Email).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<ApprovalRule>().ToTable("ApprovalRule");
            modelBuilder.Entity<ApprovalRule>().Property(p => p.MinAmount).HasPrecision(18, 2);
            modelBuilder.Entity<ApprovalRule>().Property(p => p.MaxAmount).HasPrecision(18, 2);

            modelBuilder.Entity<ProjectApprovalStep>().ToTable("ProjectApprovalStep");
            modelBuilder.Entity<ProjectApprovalStep>().HasOne(p => p.ProjectProposal).WithMany().HasForeignKey(p => p.ProjectProposal_ID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProjectApprovalStep>().HasOne(p => p.ApproverRole).WithMany().HasForeignKey(p => p.ApproverRole_ID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProjectApprovalStep>().HasOne(p => p.Status).WithMany().HasForeignKey(p => p.Status_ID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProjectApprovalStep>().HasOne(p => p.ApproverUser).WithMany().HasForeignKey(p => p.ApproverUser_ID).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ProjectProposal>().ToTable("ProjectProposal");
            modelBuilder.Entity<ProjectProposal>().Property(t => t.Title).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<ProjectProposal>().Property(p => p.EstimatedAmount).HasPrecision(18, 2);



            modelBuilder.Entity<Area>().HasData(
                new Area { id = 1, Name = "Finanzas" },
                new Area { id = 2, Name = "Tecnología" },
                new Area { id = 3, Name = "Recursos Humanos" },
                new Area { id = 4, Name = "Operaciones" }
                );

            modelBuilder.Entity<ProjectType>().HasData(
                new ProjectType { id = 1, Name = "Mejora de Procesos" },
                new ProjectType { id = 2, Name = "Innovación y Desarrollo" },
                new ProjectType { id = 3, Name = "Infraestructura" },
                new ProjectType { id = 4, Name = "Capacitación Interna" }
                );

            modelBuilder.Entity<ApprovalStatus>().HasData(
                new ApprovalStatus { id = 1, Name = "Pending" },
                new ApprovalStatus { id = 2, Name = "Approved" },
                new ApprovalStatus { id = 3, Name = "Rejected" },
                new ApprovalStatus { id = 4, Name = "Observed" }
                );

            modelBuilder.Entity<ApproverRole>().HasData(
                new ApproverRole { id = 1, Name = "Líder de Área" },
                new ApproverRole { id = 2, Name = "Gerente" },
                new ApproverRole { id = 3, Name = "Director" },
                new ApproverRole { id = 4, Name = "Comité Tecnico" }
                );

            modelBuilder.Entity<User>().HasData(
                new User { id = 1, Name = "José Ferreyra",   Email = "jferreyra@unaj.com", Role_ID = 2 },
                new User { id = 2, Name = "Ana Lucero",      Email = "alucero@unaj.com",   Role_ID = 1 },
                new User { id = 3, Name = "Gonzalo Molinas", Email = "gmolinas@unaj.com",  Role_ID = 2 },
                new User { id = 4, Name = "Lucas Olivera",   Email = "lolivera@unaj.com",  Role_ID = 3 },
                new User { id = 5, Name = "Danilo Fagundez", Email = "dfagundez@unaj.com", Role_ID = 4 },
                new User { id = 6, Name = "Gabriel Galli",   Email = "ggalli@unaj.com",    Role_ID = 4 }
                );

            modelBuilder.Entity<ApprovalRule>().HasData(
                new ApprovalRule 
                { 
                    Id = 1,
                    MinAmount = 0,
                    MaxAmount = 100000,
                    Area_ID = null,
                    Type_ID = null,
                    StepOrder = 1,
                    ApproverRole_ID = 1 
                },
                new ApprovalRule 
                { 
                    Id = 2, 
                    MinAmount = 5000,  
                    MaxAmount = 20000,  
                    Area_ID = null, 
                    Type_ID = null, 
                    StepOrder = 2, 
                    ApproverRole_ID = 2 
                },
                new ApprovalRule 
                { 
                    Id = 3, 
                    MinAmount = 0,     
                    MaxAmount = 20000,  
                    Area_ID = 2,    
                    Type_ID = 2,    
                    StepOrder = 1, 
                    ApproverRole_ID = 2 
                },
                new ApprovalRule 
                { 
                    Id = 4, 
                    MinAmount = 20000, 
                    MaxAmount = 0,      
                    Area_ID = null, 
                    Type_ID = null, 
                    StepOrder = 3, 
                    ApproverRole_ID = 3 
                },
                new ApprovalRule 
                { 
                    Id = 5, 
                    MinAmount = 5000,  
                    MaxAmount = 0,      
                    Area_ID = 1,    
                    Type_ID = 1,    
                    StepOrder = 2, 
                    ApproverRole_ID = 2 
                },
                new ApprovalRule 
                { 
                    Id = 6, 
                    MinAmount = 0,     
                    MaxAmount = 10000,  
                    Area_ID = null, 
                    Type_ID = 2,    
                    StepOrder = 1, 
                    ApproverRole_ID = 1 
                },
                new ApprovalRule 
                { 
                    Id = 7, 
                    MinAmount = 0,     
                    MaxAmount = 10000,  
                    Area_ID = 2,    
                    Type_ID = 1,    
                    StepOrder = 1, 
                    ApproverRole_ID = 4 
                },
                new ApprovalRule 
                { 
                    Id = 8, 
                    MinAmount = 10000, 
                    MaxAmount = 30000,  
                    Area_ID = 2,    
                    Type_ID = null, 
                    StepOrder = 2, 
                    ApproverRole_ID = 2 
                },
                new ApprovalRule 
                { 
                    Id = 9, 
                    MinAmount = 30000, 
                    MaxAmount = 0,     
                    Area_ID = 3,    
                    Type_ID = null, 
                    StepOrder = 2, 
                    ApproverRole_ID = 3 
                },
                new ApprovalRule 
                { 
                    Id = 10, 
                    MinAmount = 0,    
                    MaxAmount = 50000,  
                    Area_ID = null, 
                    Type_ID = 4,    
                    StepOrder = 1, 
                    ApproverRole_ID = 4 
                }
                );
        }

    }
}
