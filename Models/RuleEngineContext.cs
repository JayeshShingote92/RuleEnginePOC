using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RuleEnginePOC.Models
{
    public partial class RuleEngineContext : DbContext
    {
        public RuleEngineContext()
        {
        }

        public RuleEngineContext(DbContextOptions<RuleEngineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FieldMetadatum> FieldMetadata { get; set; } = null!;
        public virtual DbSet<RuleAction> RuleActions { get; set; } = null!;
        public virtual DbSet<RuleCondition> RuleConditions { get; set; } = null!;
        public virtual DbSet<RuleConditionGroup> RuleConditionGroups { get; set; } = null!;
        public virtual DbSet<RuleConditionMapping> RuleConditionMappings { get; set; } = null!;
        public virtual DbSet<RuleConditionValue> RuleConditionValues { get; set; } = null!;
        public virtual DbSet<RuleGroupOperator> RuleGroupOperators { get; set; } = null!;
        public virtual DbSet<RulesMaster> RulesMasters { get; set; } = null!;
        public virtual DbSet<UseCaseMaster> UseCaseMasters { get; set; } = null!;
        public virtual DbSet<UseCaseRuleMapping> UseCaseRuleMappings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=LAPTOP-UP0636HJ\\SPARATA;Database=RuleEngine;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FieldMetadatum>(entity =>
            {
                entity.HasKey(e => e.FieldId)
                    .HasName("PK__FieldMet__C8B6FF07AEBD19A4");

                entity.Property(e => e.DataType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<RuleAction>(entity =>
            {
                entity.HasKey(e => e.ActionId)
                    .HasName("PK__RuleActi__FFE3F4D9891E7C3A");

                entity.ToTable("RuleAction");

                entity.Property(e => e.ActionKey)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ActionValue)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DataType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Rule)
                    .WithMany(p => p.RuleActions)
                    .HasForeignKey(d => d.RuleId)
                    .HasConstraintName("FK__RuleActio__RuleI__778AC167");
            });

            modelBuilder.Entity<RuleCondition>(entity =>
            {
                entity.ToTable("RuleCondition");

                entity.Property(e => e.Operator)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.RuleConditions)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RuleCondi__Field__628FA481");
            });

            modelBuilder.Entity<RuleConditionGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK__RuleCond__149AF36A69AC6A38");

                entity.ToTable("RuleConditionGroup");

                entity.HasOne(d => d.Rule)
                    .WithMany(p => p.RuleConditionGroups)
                    .HasForeignKey(d => d.RuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RuleCondi__RuleI__7A672E12");
            });

            modelBuilder.Entity<RuleConditionMapping>(entity =>
            {
                entity.ToTable("RuleConditionMapping");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.RuleConditionMappings)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RuleCondi__Group__00200768");

                entity.HasOne(d => d.RuleConditionValue)
                    .WithMany(p => p.RuleConditionMappings)
                    .HasForeignKey(d => d.RuleConditionValueId)
                    .HasConstraintName("FK_RuleConditionMapping_RuleConditionValue");

                entity.HasOne(d => d.Rule)
                    .WithMany(p => p.RuleConditionMappings)
                    .HasForeignKey(d => d.RuleId)
                    .HasConstraintName("FK_RuleConditionMapping_RulesMaster");
            });

            modelBuilder.Entity<RuleConditionValue>(entity =>
            {
                entity.ToTable("RuleConditionValue");

                entity.Property(e => e.FieldValue)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.RuleCondition)
                    .WithMany(p => p.RuleConditionValues)
                    .HasForeignKey(d => d.RuleConditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RuleCondi__RuleC__656C112C");

                entity.HasOne(d => d.Rule)
                    .WithMany(p => p.RuleConditionValues)
                    .HasForeignKey(d => d.RuleId)
                    .HasConstraintName("FK_RuleConditionValue_RulesMaster");
            });

            modelBuilder.Entity<RuleGroupOperator>(entity =>
            {
                entity.ToTable("RuleGroupOperator");

                entity.Property(e => e.Operator)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Rule)
                    .WithMany(p => p.RuleGroupOperators)
                    .HasForeignKey(d => d.RuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RuleGroup__RuleI__05D8E0BE");
            });

            modelBuilder.Entity<RulesMaster>(entity =>
            {
                entity.HasKey(e => e.RuleId)
                    .HasName("PK__RulesMas__110458E232525122");

                entity.ToTable("RulesMaster");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.RuleName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.UseCase)
                    .WithMany(p => p.RulesMasters)
                    .HasForeignKey(d => d.UseCaseId)
                    .HasConstraintName("FK_RulesMaster_UseCases");
            });

            modelBuilder.Entity<UseCaseMaster>(entity =>
            {
                entity.HasKey(e => e.UseCaseId)
                    .HasName("PK__UseCaseM__9097341BE1EB7592");

                entity.ToTable("UseCaseMaster");

                entity.HasIndex(e => e.UseCaseCode, "UQ__UseCaseM__6BFC51069948FF31")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UseCaseCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UseCaseName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UseCaseRuleMapping>(entity =>
            {
                entity.ToTable("UseCaseRuleMapping");

                entity.HasOne(d => d.Rule)
                    .WithMany(p => p.UseCaseRuleMappings)
                    .HasForeignKey(d => d.RuleId)
                    .HasConstraintName("FK__UseCaseRu__RuleI__6D0D32F4");

                entity.HasOne(d => d.UseCase)
                    .WithMany(p => p.UseCaseRuleMappings)
                    .HasForeignKey(d => d.UseCaseId)
                    .HasConstraintName("FK__UseCaseRu__UseCa__6C190EBB");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
