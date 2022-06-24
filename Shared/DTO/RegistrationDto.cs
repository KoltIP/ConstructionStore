using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class RegistrationDto
    {
        public string SurName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool ErrorEmail { get; set; } = false;
        public bool ErrorTel { get; set; } = false;
        public bool ErrorLogin { get; set; } = false;
    }
}
