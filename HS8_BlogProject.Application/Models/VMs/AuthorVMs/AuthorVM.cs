using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Models.VMs.AuthorVMs
{
    public class AuthorVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public string Fullname => $"{FirstName} {LastName}";
        //ToDo: Ihtiyaç halinde AppUserName ekle
        public string AppUserName { get; set; }
        public string AppUserId { get; set; }
    }
}
