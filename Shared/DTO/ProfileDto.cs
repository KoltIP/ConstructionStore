using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class ProfileDto
    {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronimic { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
            public bool Flag { get; set; } = false;
            public bool ErrorEmail { get; set; } = false;
            public bool ErrorTel { get; set; } = false;
            public bool ErrorPassword { get; set; } = false;

    }
}
