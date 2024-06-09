using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using CookingBlog.Database.Models;
using CookingBlog.Database.Models.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CookingBlog.Database;

public partial class CookingBlogContext : IdentityDbContext<IdentityApplicationUser, IdentityApplicationRole, Guid>
{
    public static string? GlobalConnectionString { get; set; }

    public CookingBlogContext()
    {
        Database.SetConnectionString(GlobalConnectionString);
    }

    public CookingBlogContext(DbContextOptions<CookingBlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FileExtension> FileExtensions { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientToNutrient> IngredientToNutrients { get; set; }

    public virtual DbSet<Measurement> Measurements { get; set; }

    public virtual DbSet<MediaSizeType> MediaSizeTypes { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Nutrient> Nutrients { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeGroup> RecipeGroups { get; set; }

    public virtual DbSet<RecipeGroupIngredient> RecipeGroupIngredients { get; set; }

    public virtual DbSet<RecipeGroupStep> RecipeGroupSteps { get; set; }

    public virtual DbSet<RecipeMediaToSizedMedium> RecipeMediaToSizedMedia { get; set; }

    public virtual DbSet<RecipeNote> RecipeNotes { get; set; }

    public virtual DbSet<RecipeStory> RecipeStories { get; set; }

    public virtual DbSet<RecipeMedia> RecipeMedia { get; set; }

    public virtual DbSet<RecipeToTag> RecipeToTags { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<VRecipeGroupIngredient> VRecipeGroupIngredients { get; set; }

    public virtual DbSet<VRecipeTag> VRecipeTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder
           .UseSqlServer(GlobalConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FileExtension>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("file_extension");

            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.Extension).IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");

            entity.Property(e => e.Name).HasMaxLength(32);
            entity.Property(e => e.Extension).HasMaxLength(32);
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ingredie__3214EC271AAAE9EA");

            entity.ToTable("ingredient");

            entity.HasIndex(e => e.Name, "UQ__ingredie__737584F6D45A4863").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<IngredientToNutrient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ingredie__3214EC27648170C9");

            entity.ToTable("ingredient_to_nutrient");

            entity.HasIndex(e => new { e.IngredientId, e.NutrientId }, "UQ__ingredie__328886F52B3CD262").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
            entity.Property(e => e.NutrientId).HasColumnName("NutrientID");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.IngredientToNutrients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingredien__Ingre__534D60F1");

            entity.HasOne(d => d.Nutrient).WithMany(p => p.IngredientToNutrients)
                .HasForeignKey(d => d.NutrientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingredien__Nutri__5441852A");
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("measurement");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");

            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.UnitName).IsUnique();

            entity.Property(e => e.Name).HasMaxLength(64);
            entity.Property(e => e.UnitName).HasMaxLength(32);
        });

        modelBuilder.Entity<MediaSizeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__media_si__3214EC27D3B0AEAD");

            entity.ToTable("media_size_type");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__media_ty__3214EC2757A53775");

            entity.ToTable("media_type");

            entity.HasIndex(e => e.Name, "UQ__media_ty__737584F605EA9C3C").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Nutrient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__nutrient__3214EC2759FF8A45");

            entity.ToTable("nutrient");

            entity.HasIndex(e => e.Name, "UQ__nutrient__737584F63E5796E3").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recipe__3214EC27696BF2E4");

            entity.ToTable("recipe");

            entity.HasIndex(e => e.Name, "UQ__recipe__737584F6719A6224").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Description).HasMaxLength(1024);

            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<RecipeMediaToSizedMedium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recipe_m__3214EC2745625541");

            entity.ToTable("recipe_media_to_sized_media");

            entity.HasIndex(e => new { e.RecipeMediaId, e.MediaSizeTypeId }, "UQ__recipe_m__CC3BAB463B4E5891").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.MediaSizeTypeId).HasColumnName("MediaSizeTypeID");
            entity.Property(e => e.RecipeMediaId).HasColumnName("RecipeMediaID");

            entity.HasOne(d => d.MediaSizeType).WithMany(p => p.RecipeMediaToSizedMedia)
                .HasForeignKey(d => d.MediaSizeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipe_me__Media__01142BA1");

            entity.HasOne(d => d.RecipeMedia).WithMany(p => p.RecipeMediaToSizedMedia)
                .HasForeignKey(d => d.RecipeMediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipe_me__Recip__00200768"); 
        });

        modelBuilder.Entity<RecipeNote>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("recipe_note");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");

            entity.Property(e => e.NoteText).HasMaxLength(2048);
            entity.HasIndex(e => new { e.RecipeId, e.NoteNumber }).IsUnique();
        });

        modelBuilder.Entity<RecipeGroupStep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recipe_s__3214EC2709DE3DFB");

            entity.ToTable("recipe_group_step");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");

            entity.Property(e => e.RecipeGroupId)
                .HasColumnName("RecipeGroupID");
        });

        modelBuilder.Entity<RecipeStory>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("recipe_story");

            entity.Property(e => e.ParagraphNumber)
                .HasDefaultValueSql("(1)");
        });

        modelBuilder.Entity<RecipeGroup>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("recipe_group");
        });

        modelBuilder.Entity<RecipeGroupIngredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recipe_t__3214EC27E0057095");

            entity.ToTable("recipe_group_ingredient");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
            entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");
        });

        modelBuilder.Entity<RecipeMedia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recipe_t__3214EC275565CF15");

            entity.ToTable("recipe_to_media");

            entity.HasIndex(e => new { e.RecipeId, e.MediaNumber }, "UQ__recipe_t__665567CA1F71DDC0").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

            entity.Property(e => e.FileName).HasMaxLength(128);

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeToMedia)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipe_to__Recip__778AC167");
        });

        modelBuilder.Entity<RecipeToTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recipe_t__3214EC27FCB1EFAA");

            entity.ToTable("recipe_to_tag");

            entity.HasIndex(e => new { e.RecipeId, e.TagId }, "UQ__recipe_t__2B8E4775543B72B5").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.TagId).HasColumnName("TagID");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tag__3214EC2753633A0F");

            entity.ToTable("tag");

            entity.HasIndex(e => e.Name, "UQ__tag__737584F6225C0EC5").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<VRecipeGroupIngredient>(entity =>
        {
            entity.ToView("v_recipe_ingredient");
            entity.HasNoKey();
        });

        modelBuilder.Entity<VRecipeTag>(entity =>
        {
            entity.ToView("v_recipe_tag");
            entity.HasNoKey();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
