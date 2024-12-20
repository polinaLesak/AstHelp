﻿using System.Text.Json.Serialization;

namespace Identity.Microservice.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }

        public string Fullname { get; set; } = "";

        public string Position { get; set; } = "";

        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; } = null!;
    }
}
