 using Microsoft.EntityFrameworkCore;
using patient_lifeCycle.Enums.Medixa_AI.Domain.Enums;
using patient_lifeCycle.Models;

namespace patient_lifeCycle.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // ── DbSets ──
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<TestNormalRange> TestNormalRanges { get; set; }
        public DbSet<TestPrerequisite> TestPrerequisites { get; set; }
        public DbSet<TestCheckupPolicy> TestCheckupPolicies { get; set; }
        public DbSet<TestMedicalRule> TestMedicalRules { get; set; }
        public DbSet<TestOrder> TestOrders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<AIInterpretation> AIInterpretations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<CheckupRecommendation> CheckupRecommendations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<MembershipCategory> MembershipCategories { get; set; }
        public DbSet<PatientMembership> PatientMemberships { get; set; }
        public DbSet<UploadedMedicalFile> UploadedMedicalFiles { get; set; }
        public DbSet<HealthMetricSnapshot> HealthMetricSnapshots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             
            // Enum → String Conversions
            // ══════════════════════════════════════════
            modelBuilder.Entity<Patient>()
                .Property(p => p.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<TestNormalRange>()
                .Property(t => t.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<TestCheckupPolicy>()
                .Property(t => t.IsMandatoryForGender)
                .HasConversion<string>();

            modelBuilder.Entity<TestMedicalRule>()
                .Property(t => t.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<TestMedicalRule>()
                .Property(t => t.RiskLevel)
                .HasConversion<string>();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Role)
                .HasConversion<string>();

            modelBuilder.Entity<TestOrder>()
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<OrderDetail>()
                .Property(o => o.Status)
                .HasConversion<string>();

            modelBuilder.Entity<AIInterpretation>()
                .Property(a => a.RiskLevel)
                .HasConversion<string>();

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<CheckupRecommendation>()
                .Property(c => c.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentMethod)
                .HasConversion<string>();

            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentStatus)
                .HasConversion<string>();

            modelBuilder.Entity<HealthMetricSnapshot>()
                .Property(h => h.TrendType)
                .HasConversion<string>();

            // ══════════════════════════════════════════════════════════
            // Relationships
            // ══════════════════════════════════════════════════════════

            // ── Specialization → Doctors (One-to-Many) ────────────────
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecializationID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Specialization → TestMedicalRules (One-to-Many) ───────
            modelBuilder.Entity<TestMedicalRule>()
                .HasOne(t => t.SuggestedSpecialization)
                .WithMany(s => s.MedicalRules)
                .HasForeignKey(t => t.SuggestedSpecializationID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            // ── Specialization → AIInterpretations (One-to-Many) ──────
            modelBuilder.Entity<AIInterpretation>()
                .HasOne(a => a.SuggestedSpecialization)
                .WithMany(s => s.AIInterpretations)
                .HasForeignKey(a => a.SuggestedSpecializationID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            // ── LabTest → TestNormalRanges (One-to-Many) ──────────────
            modelBuilder.Entity<TestNormalRange>()
                .HasOne(t => t.Test)
                .WithMany(l => l.NormalRanges)
                .HasForeignKey(t => t.TestID)
                .OnDelete(DeleteBehavior.Cascade);

            // ── LabTest → TestPrerequisites (One-to-Many) ─────────────
            modelBuilder.Entity<TestPrerequisite>()
                .HasOne(t => t.Test)
                .WithMany(l => l.Prerequisites)
                .HasForeignKey(t => t.TestID)
                .OnDelete(DeleteBehavior.Cascade);

            // ── LabTest → TestCheckupPolicies (One-to-Many) ───────────
            modelBuilder.Entity<TestCheckupPolicy>()
                .HasOne(t => t.Test)
                .WithMany(l => l.CheckupPolicies)
                .HasForeignKey(t => t.TestID)
                .OnDelete(DeleteBehavior.Cascade);

            // ── LabTest → TestMedicalRules (One-to-Many) ──────────────
            modelBuilder.Entity<TestMedicalRule>()
                .HasOne(t => t.Test)
                .WithMany(l => l.MedicalRules)
                .HasForeignKey(t => t.TestID)
                .OnDelete(DeleteBehavior.Cascade);

            // ── LabTest → OrderDetails (One-to-Many) ──────────────────
            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Test)
                .WithMany(l => l.OrderDetails)
                .HasForeignKey(o => o.TestID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── LabTest → CheckupRecommendations (One-to-Many) ────────
            modelBuilder.Entity<CheckupRecommendation>()
                .HasOne(c => c.Test)
                .WithMany(l => l.CheckupRecommendations)
                .HasForeignKey(c => c.TestID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            // ── LabTest → HealthMetricSnapshots (One-to-Many) ─────────
            modelBuilder.Entity<HealthMetricSnapshot>()
                .HasOne(h => h.Test)
                .WithMany(l => l.HealthSnapshots)
                .HasForeignKey(h => h.TestID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Patient → Appointments (One-to-Many) ──────────────────
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Patient → TestOrders (One-to-Many) ────────────────────
            modelBuilder.Entity<TestOrder>()
                .HasOne(t => t.Patient)
                .WithMany(p => p.Orders)
                .HasForeignKey(t => t.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Patient → CheckupRecommendations (One-to-Many) ────────
            modelBuilder.Entity<CheckupRecommendation>()
                .HasOne(c => c.Patient)
                .WithMany(p => p.CheckupRecommendations)
                .HasForeignKey(c => c.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Patient → PatientMemberships (One-to-Many) ────────────
            modelBuilder.Entity<PatientMembership>()
                .HasOne(pm => pm.Patient)
                .WithMany(p => p.Memberships)
                .HasForeignKey(pm => pm.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Patient → UploadedMedicalFiles (One-to-Many) ──────────
            modelBuilder.Entity<UploadedMedicalFile>()
                .HasOne(u => u.Patient)
                .WithMany(p => p.UploadedFiles)
                .HasForeignKey(u => u.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Patient → HealthMetricSnapshots (One-to-Many) ─────────
            modelBuilder.Entity<HealthMetricSnapshot>()
                .HasOne(h => h.Patient)
                .WithMany(p => p.HealthSnapshots)
                .HasForeignKey(h => h.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Doctor → TestOrders (One-to-Many) ─────────────────────
            modelBuilder.Entity<TestOrder>()
                .HasOne(t => t.Doctor)
               .WithMany(d => d.TestOrders)
                .HasForeignKey(t => t.DoctorID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            // ── Employee → TestOrders (One-to-Many) ───────────────────
            modelBuilder.Entity<TestOrder>()
                .HasOne(t => t.CreatedByEmployee)
                .WithMany(e => e.CreatedOrders)
                .HasForeignKey(t => t.CreatedByEmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Employee → TestResults (One-to-Many) ──────────────────
            modelBuilder.Entity<TestResult>()
                .HasOne(t => t.Technician)
                .WithMany(e => e.Results)
                .HasForeignKey(t => t.TechnicianID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── TestOrder → OrderDetails (One-to-Many) ────────────────
            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Order)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(o => o.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // ── TestOrder → Payment (One-to-One) ──────────────────────
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(t => t.Payment)
                .HasForeignKey<Payment>(p => p.OrderID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── OrderDetail → TestResult (One-to-One) ─────────────────
            modelBuilder.Entity<TestResult>()
                .HasOne(t => t.OrderDetail)
                .WithOne(o => o.Result)
                .HasForeignKey<TestResult>(t => t.OrderDetailID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── TestResult → AIInterpretation (One-to-One) ────────────
            modelBuilder.Entity<AIInterpretation>()
                .HasOne(a => a.Result)
                .WithOne(t => t.AIInterpretation)
                .HasForeignKey<AIInterpretation>(a => a.ResultID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── MembershipCategory → PatientMemberships (One-to-Many) ─
            modelBuilder.Entity<PatientMembership>()
                .HasOne(pm => pm.Category)
                .WithMany(mc => mc.PatientMemberships)
                .HasForeignKey(pm => pm.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

          
            // Unique Indexes
            // ══════════════════════════════════
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.NationalID)
                .IsUnique();

            modelBuilder.Entity<Doctor>()
                .HasIndex(d => d.Email)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();
        }
    }
}
