using ConferenceManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ConferenceManager.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        // Check if data already exists
        if (context.Roles.Any())
        {
            return; // Database has been seeded
        }

        // Add Roles
        var roles = new Role[]
        {
            new Role { Name = "Admin", Description = "Administrator with full access" },
            new Role { Name = "Organizer", Description = "Conference organizer" },
            new Role { Name = "Speaker", Description = "Conference speaker" },
            new Role { Name = "Attendee", Description = "Conference attendee" },
            new Role { Name = "Sponsor", Description = "Conference sponsor" }
        };
        context.Roles.AddRange(roles);
        context.SaveChanges();

        // Add Users
        var users = new User[]
        {
            new User { Username = "admin", Password = "admin123", Email = "admin@example.com", RoleId = roles[0].Id },
            new User { Username = "organizer1", Password = "org123", Email = "org1@example.com", RoleId = roles[1].Id },
            new User { Username = "organizer2", Password = "org123", Email = "org2@example.com", RoleId = roles[1].Id },
            new User { Username = "speaker1", Password = "speak123", Email = "speak1@example.com", RoleId = roles[2].Id },
            new User { Username = "speaker2", Password = "speak123", Email = "speak2@example.com", RoleId = roles[2].Id },
            new User { Username = "attendee1", Password = "att123", Email = "att1@example.com", RoleId = roles[3].Id },
            new User { Username = "attendee2", Password = "att123", Email = "att2@example.com", RoleId = roles[3].Id },
            new User { Username = "sponsor1", Password = "spon123", Email = "spon1@example.com", RoleId = roles[4].Id }
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        // Add User Profiles
        var profiles = new UserProfile[]
        {
            new UserProfile { UserId = users[0].Id, FirstName = "Admin", LastName = "User", Bio = "System administrator" },
            new UserProfile { UserId = users[1].Id, FirstName = "John", LastName = "Organizer", Bio = "Conference organizer", Affiliation = "Tech Events Inc." },
            new UserProfile { UserId = users[2].Id, FirstName = "Jane", LastName = "Organizer", Bio = "Event manager", Affiliation = "EventPro" },
            new UserProfile { UserId = users[3].Id, FirstName = "Mike", LastName = "Speaker", Bio = "Tech expert", Affiliation = "Tech University" },
            new UserProfile { UserId = users[4].Id, FirstName = "Sarah", LastName = "Speaker", Bio = "Research scientist", Affiliation = "Science Lab" }
        };
        context.UserProfiles.AddRange(profiles);
        context.SaveChanges();

        // Add Locations
        var locations = new Location[]
        {
            new Location { Name = "Tech Center", Address = "123 Tech Street", City = "San Francisco", Country = "USA", Description = "Modern tech conference center" },
            new Location { Name = "Business Hub", Address = "456 Business Ave", City = "New York", Country = "USA", Description = "Professional business venue" },
            new Location { Name = "Innovation Space", Address = "789 Innovation Rd", City = "London", Country = "UK", Description = "Creative space for events" },
            new Location { Name = "Science Park", Address = "101 Science Blvd", City = "Berlin", Country = "Germany", Description = "Research and conference center" }
        };
        context.Locations.AddRange(locations);
        context.SaveChanges();

        // Add Location Reviews
        var reviews = new LocationReview[]
        {
            new LocationReview { LocationId = locations[0].Id, UserId = users[5].Id, Rating = 5, Comment = "Great venue!" },
            new LocationReview { LocationId = locations[0].Id, UserId = users[6].Id, Rating = 4, Comment = "Good facilities" },
            new LocationReview { LocationId = locations[1].Id, UserId = users[5].Id, Rating = 5, Comment = "Excellent location" }
        };
        context.LocationReviews.AddRange(reviews);
        context.SaveChanges();

        // Add Conferences
        var conferences = new Conference[]
        {
            new Conference
            {
                Title = "Tech Summit 2024",
                Description = "Annual technology conference",
                LocationId = locations[0].Id,
                StartDate = DateTime.Now.AddDays(30),
                EndDate = DateTime.Now.AddDays(32),
                OrganizerId = users[1].Id,
                IsAnnounced = true
            },
            new Conference
            {
                Title = "Business Innovation Forum",
                Description = "Business and innovation conference",
                LocationId = locations[1].Id,
                StartDate = DateTime.Now.AddDays(60),
                EndDate = DateTime.Now.AddDays(62),
                OrganizerId = users[2].Id,
                IsAnnounced = true
            },
            new Conference
            {
                Title = "Science Symposium",
                Description = "Scientific research conference",
                LocationId = locations[3].Id,
                StartDate = DateTime.Now.AddDays(90),
                EndDate = DateTime.Now.AddDays(92),
                OrganizerId = users[1].Id,
                IsAnnounced = false
            }
        };
        context.Conferences.AddRange(conferences);
        context.SaveChanges();

        // Add Conference Organizers
        var organizers = new ConferenceOrganizer[]
        {
            new ConferenceOrganizer { ConferenceId = conferences[0].Id, UserId = users[1].Id },
            new ConferenceOrganizer { ConferenceId = conferences[0].Id, UserId = users[2].Id },
            new ConferenceOrganizer { ConferenceId = conferences[1].Id, UserId = users[2].Id }
        };
        context.ConferenceOrganizers.AddRange(organizers);
        context.SaveChanges();

        // Add Conference Attendees
        var attendees = new ConferenceAttendee[]
        {
            new ConferenceAttendee { ConferenceId = conferences[0].Id, UserId = users[5].Id },
            new ConferenceAttendee { ConferenceId = conferences[0].Id, UserId = users[6].Id },
            new ConferenceAttendee { ConferenceId = conferences[1].Id, UserId = users[5].Id }
        };
        context.ConferenceAttendees.AddRange(attendees);
        context.SaveChanges();

        // Add Sponsors
        var sponsors = new Sponsor[]
        {
            new Sponsor { Name = "Tech Corp", Description = "Technology solutions", Website = "https://techcorp.com" },
            new Sponsor { Name = "Innovate Inc", Description = "Innovation company", Website = "https://innovate.com" },
            new Sponsor { Name = "Science Labs", Description = "Research organization", Website = "https://sciencelabs.com" }
        };
        context.Sponsors.AddRange(sponsors);
        context.SaveChanges();

        // Добавляем спонсоров к конференциям
        var conference1 = context.Conferences.First();
        var sponsor1 = context.Sponsors.First();
        conference1.Sponsors.Add(sponsor1);

        var conference2 = context.Conferences.Skip(1).First();
        var sponsor2 = context.Sponsors.Skip(1).First();
        conference2.Sponsors.Add(sponsor2);

        context.SaveChanges();

        // Add Presentations
        var presentations = new Presentation[]
        {
            new Presentation
            {
                ConferenceId = conferences[0].Id,
                Title = "Future of AI",
                Description = "Exploring the latest developments in artificial intelligence",
                StartTime = new DateTime(2024, 6, 15, 10, 0, 0),
                EndTime = new DateTime(2024, 6, 15, 11, 30, 0),
                Room = "Main Hall",
                IsApproved = true,
                Speakers = new List<User> { users[3] }
            },
            new Presentation
            {
                ConferenceId = conferences[0].Id,
                Title = "Cloud Computing Trends",
                Description = "Overview of current cloud computing technologies and future trends",
                StartTime = new DateTime(2024, 6, 15, 13, 0, 0),
                EndTime = new DateTime(2024, 6, 15, 14, 30, 0),
                Room = "Room A",
                IsApproved = true,
                Speakers = new List<User> { users[4] }
            },
            new Presentation
            {
                ConferenceId = conferences[1].Id,
                Title = "Business Innovation",
                Description = "Strategies for driving innovation in modern business",
                StartTime = new DateTime(2024, 7, 1, 9, 0, 0),
                EndTime = new DateTime(2024, 7, 1, 10, 30, 0),
                Room = "Conference Room 1",
                IsApproved = true,
                Speakers = new List<User> { users[3] }
            }
        };
        context.Presentations.AddRange(presentations);
        context.SaveChanges();

        // Add Conference Subscriptions
        var subscriptions = new ConferenceSubscription[]
        {
            new ConferenceSubscription { ConferenceId = conferences[0].Id, UserId = users[5].Id },
            new ConferenceSubscription { ConferenceId = conferences[0].Id, UserId = users[6].Id },
            new ConferenceSubscription { ConferenceId = conferences[1].Id, UserId = users[5].Id }
        };
        context.ConferenceSubscriptions.AddRange(subscriptions);
        context.SaveChanges();

        // Add Notifications
        var notifications = new Notification[]
        {
            new Notification
            {
                UserId = users[5].Id,
                Message = "Welcome to Tech Summit 2024!",
                Type = "Welcome",
                IsRead = false
            },
            new Notification
            {
                UserId = users[6].Id,
                Message = "Your presentation has been approved",
                Type = "Approval",
                IsRead = true
            },
            new Notification
            {
                UserId = users[3].Id,
                Message = "New comment on your presentation",
                Type = "Comment",
                IsRead = false
            }
        };
        context.Notifications.AddRange(notifications);
        context.SaveChanges();
    }
} 