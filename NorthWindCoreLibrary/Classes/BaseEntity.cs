﻿using System;
using Newtonsoft.Json;

namespace NorthWindCoreLibrary.Classes
{
    public class BaseEntity
    {
       
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        [JsonIgnore]
        public string LastUser { get; set; }
        [JsonIgnore]
        public bool? IsDeleted { get; set; }
    }
}