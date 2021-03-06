﻿using FoodTruckNation.Core.Domain;
using Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;

namespace FoodTruckNation.Data.EF
{
    public class FoodTruckContext : DbContext
    {

        public FoodTruckContext(DbContextOptions options) : base(options)
        {

        }




        public DbSet<FoodTruck> FoodTrucks { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<SocialMediaPlatform> SocialMediaPlatforms { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.ConfigureFoodTruck(modelBuilder);
            this.ConfigureTag(modelBuilder);
            this.ConfigureFoodTruckTag(modelBuilder);
            this.ConfigureReview(modelBuilder);
            this.ConfigureLocation(modelBuilder);
            this.ConfigureSchedule(modelBuilder);
            this.ConfigureSocialMediaPlatform(modelBuilder);
            this.ConfigureSocialMediaAccount(modelBuilder);
        }






        private void ConfigureTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>()
                .ToTable("Tags")
                .HasKey(t => t.TagId);

            modelBuilder.Entity<Tag>().Property(p => p.TagId)
                .HasField("tagId")
                .UseSqlServerIdentityColumn()
                .HasColumnName("TagId");

            modelBuilder.Entity<Tag>().Property(p => p.Text)
                .HasField("tagText")
                .HasColumnName("TagName");
        }


        private void ConfigureFoodTruckTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodTruckTag>()
                .ToTable("FoodTruckTags")
                .HasKey(f => f.FoodTruckTagId);

            modelBuilder.Entity<FoodTruckTag>().Property(p => p.FoodTruckTagId)
               .HasField("foodTruckTagId")
               .UseSqlServerIdentityColumn()
               .HasColumnName("FoodTruckTagId");

            modelBuilder.Entity<FoodTruckTag>()
                .HasOne<FoodTruck>(x => x.FoodTruck)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.FoodTruckId);


            modelBuilder.Entity<FoodTruckTag>()
                .HasOne<Tag>(x => x.Tag)
                .WithMany()
                .HasForeignKey(x => x.TagId);
        }


        private void ConfigureLocation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .ToTable("Locations")
                .HasKey(l => l.LocationId);

            modelBuilder.Entity<Location>().Property(p => p.LocationId)
                .HasField("locationId")
                .UseSqlServerIdentityColumn()
                .HasColumnName("LocationId");

            modelBuilder.Entity<Location>().Property(p => p.Name)
                .HasField("locationName")                
                .HasColumnName("LocationName");

            modelBuilder.Entity<Location>().Property(p => p.StreetAddress)
                .HasField("streetAddress")
                .HasColumnName("StreetAddress");

            modelBuilder.Entity<Location>().Property(p => p.City)
                .HasField("city")
                .HasColumnName("City");

            modelBuilder.Entity<Location>().Property(p => p.State)
                .HasField("state")
                .HasColumnName("State");

            modelBuilder.Entity<Location>().Property(p => p.ZipCode)
                .HasField("zipCode")
                .HasColumnName("ZipCode");

        }


        private void ConfigureFoodTruck(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodTruck>()
                .ToTable("FoodTrucks")
                .HasKey(f => f.FoodTruckId);

            modelBuilder.Entity<FoodTruck>().Property(p => p.FoodTruckId)
                .HasField("foodTruckId")
                .UseSqlServerIdentityColumn()
                .HasColumnName("FoodTruckId");

            modelBuilder.Entity<FoodTruck>().Property(p => p.Name)
                .HasField("name")
                .HasColumnName("TruckName");

            modelBuilder.Entity<FoodTruck>().Property(p => p.Description)
                .HasField("description")
                .HasColumnName("Description");

            modelBuilder.Entity<FoodTruck>().Property(p => p.Website)
                .HasField("website")
                .HasColumnName("Website");

            // So EF can set the backing field on the navigation property
            // https://blog.oneunicorn.com/2016/10/28/collection-navigation-properties-and-fields-in-ef-core-1-1/
            var navigationTags = modelBuilder.Entity<FoodTruck>().Metadata
                .FindNavigation(nameof(FoodTruck.Tags));
            navigationTags.SetPropertyAccessMode(PropertyAccessMode.Field);

            var navigationSocialAccounts = modelBuilder.Entity<FoodTruck>().Metadata
                .FindNavigation(nameof(FoodTruck.SocialMediaAccounts));
            navigationSocialAccounts.SetPropertyAccessMode(PropertyAccessMode.Field);

            var navigationReviews = modelBuilder.Entity<FoodTruck>().Metadata
                .FindNavigation(nameof(FoodTruck.Reviews));
            navigationReviews.SetPropertyAccessMode(PropertyAccessMode.Field);

            var navigationSchedules = modelBuilder.Entity<FoodTruck>().Metadata
                .FindNavigation(nameof(FoodTruck.Schedules));
            navigationSchedules.SetPropertyAccessMode(PropertyAccessMode.Field);
        }




        private void ConfigureReview(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .ToTable("Reviews")
                .HasKey(r => r.ReviewId);

            modelBuilder.Entity<Review>().Property(x => x.ReviewId)
                .HasField("_reviewId")
                .UseSqlServerIdentityColumn()
                .HasColumnName("ReviewId");

            modelBuilder.Entity<Review>().Property(x => x.FoodTruckId)
                .HasField("_foodTruckId")
                .HasColumnName("FoodTruckId");

            modelBuilder.Entity<Review>().Property(x => x.ReviewDate)
                .HasField("_reviewDate")
                .HasColumnName("ReviewDate");

            modelBuilder.Entity<Review>().Property(x => x.Rating)
                .HasField("_rating")
                .HasColumnName("Rating");


            modelBuilder.Entity<Review>().Property(x => x.Details)
                .HasField("_details")
                .HasColumnName("Details");

            modelBuilder.Entity<Review>()
                .HasOne<FoodTruck>(x => x.FoodTruck)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.FoodTruckId);
        }


        private void ConfigureSchedule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .ToTable("Schedules")
                .HasKey(s => s.ScheduleId);

            modelBuilder.Entity<Schedule>().Property(x => x.ScheduleId)
                .HasField("scheduleId")
                .UseSqlServerIdentityColumn()
                .HasColumnName("ScheduleId");

            modelBuilder.Entity<Schedule>().Property(x => x.FoodTruckId)
                .HasField("foodTruckId")
                .HasColumnName("FoodTruckId");
            
            modelBuilder.Entity<Schedule>().Property(x => x.LocationId)
                .HasField("locationId")
                .HasColumnName("LocationId");

            modelBuilder.Entity<Schedule>().Property(x => x.ScheduledStart)
                .HasField("scheduleStart")
                .HasColumnName("StartTime");

            modelBuilder.Entity<Schedule>().Property(x => x.ScheduledEnd)
                .HasField("scheduleEnd")
                .HasColumnName("EndTime");



            modelBuilder.Entity<Schedule>()
                .HasOne<FoodTruck>(x => x.FoodTruck)
                .WithMany(x => x.Schedules)
                .HasForeignKey(x => x.FoodTruckId);

            modelBuilder.Entity<Schedule>()
                .HasOne<Location>(x => x.Location);
        }



        private void ConfigureSocialMediaPlatform(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocialMediaPlatform>()
                .ToTable("SocialMediaPlatforms")
                .HasKey(p => p.PlatformId);

            modelBuilder.Entity<SocialMediaPlatform>().Property(p => p.PlatformId)
                .HasField("_platformId")
                .UseSqlServerIdentityColumn()
                .HasColumnName("PlatformId");

            modelBuilder.Entity<SocialMediaPlatform>().Property(p => p.Name)
                .HasField("_name")
                .HasColumnName("PlatformName");

            modelBuilder.Entity<SocialMediaPlatform>().Property(p => p.UrlTemplate)
                .HasField("_urlTemplate")
                .HasColumnName("UrlTemplate");
        }



        private void ConfigureSocialMediaAccount(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocialMediaAccount>()
                .ToTable("SocialMediaAccounts")
                .HasKey(a => a.SocialMediaAccountId);

            modelBuilder.Entity<SocialMediaAccount>().Property(p => p.SocialMediaAccountId)
               .HasField("_socialMediaAccountId")
               .UseSqlServerIdentityColumn()
               .HasColumnName("SocialMediaAccountId");

            modelBuilder.Entity<SocialMediaAccount>().Property(p => p.AccountName)
                .HasField("_accountName")
                .HasColumnName("AccountName");

            modelBuilder.Entity<SocialMediaAccount>()
                .HasOne<FoodTruck>(x => x.FoodTruck)
                .WithMany(x => x.SocialMediaAccounts)
                .HasForeignKey(x => x.FoodTruckId);

            modelBuilder.Entity<SocialMediaAccount>()
                .HasOne<SocialMediaPlatform>(x => x.Platform)
                .WithMany()
                .HasForeignKey(x => x.PlatformId);
        }


    }
}
