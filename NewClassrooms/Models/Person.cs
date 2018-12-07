﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace NewClassrooms.Models
{
    [ExcludeFromCodeCoverage]
    public class Person
    {
        public string Gender { get; set; }
        public Name Name { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
        public Login Login { get; set; }
        public DateAge Dob { get; set; }
        public DateAge Registered { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public Id Id { get; set; }
        public Picture Picture { get; set; }
        public string Nat { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Name
    {
        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Location
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public Coordinates Coordinates { get; set; }
        public Timezone Timezone { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Coordinates
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Timezone
    {
        public string Offset { get; set; }
        public string Description { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Login
    {
        public string Uuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Md5 { get; set; }
        public string Sha1 { get; set; }
        public string Sha256 { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class DateAge
    {
        public string Date { get; set; }
        public int Age { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Id
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Picture
    {
        public string Large { get; set; }
        public string Medium { get; set; }
        public string Thumbnail { get; set; }
    }
}
