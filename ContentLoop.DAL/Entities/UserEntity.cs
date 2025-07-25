﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentLoop.DAL.Entities
{
    [Table("Users")]
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
