using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class LoginDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Error { get; set; } = false;
    }
}
