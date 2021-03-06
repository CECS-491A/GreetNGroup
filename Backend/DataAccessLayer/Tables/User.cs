﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gucci.DataAccessLayer.Tables
{
    [Table("User")]
    public class User
    {
        public User()
        {
            FirstName = null;
            LastName = null;
            UserName = null;
            City = null;
            State = null;
            Country = null;
            DoB = DateTime.Now;
            IsActivated = false;
            EventCreationCount = 0;
        }

        public User(int uId, string firstName, string lastName, string userName, string city,
                    string state, string country, DateTime dob, bool isActivated)
        {
            UserId = uId;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            City = city;
            State = state;
            Country = country;
            DoB = dob;
            IsActivated = isActivated;
            EventCreationCount = 0;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public DateTime DoB { get; set; }

        public bool IsActivated { get; set; }

        public int EventCreationCount { get; set; }
    }
}