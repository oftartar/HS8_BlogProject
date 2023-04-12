using HS8_BlogProject.Application.Extensions;
using HS8_BlogProject.Application.Models.VMs;
using HS8_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS8_BlogProject.Application.Models.DTOs.GenreDTOs
{
    public class UpdateGenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
