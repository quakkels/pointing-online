﻿using System;

namespace PointingPoker.DataAccess.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
