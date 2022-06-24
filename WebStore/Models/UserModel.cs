using DataAccess.Entity;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class UserModel
    {
        public int? ID { get; set; }
        public int Type { get; set; }
        public LoginDto LoginDto { get; set; }
        public RegistrationDto RegistrationDto { get; set; }
        public ProfileDto ProfileDto { get; set; }
        public ProductDto ProductDto { get; set; }
        public BasketResponseDto BasketResponsetDto { get; set; }
        public int IdProduct { get; set; }

    }
}
