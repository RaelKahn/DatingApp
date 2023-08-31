using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public enum enGender
    {
        Male,
        Female,
        Undefined
    }

    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        //public enGender GenderId { get; set; }
    }
}