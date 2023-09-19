using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    public class UsersController : BaseAPIDataContextController//  BaseAPIController 
    {
        //public readonly DataContext _context;

        public UsersController(DataContext context): base (context)
        {
        } 


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() // https://localhost:5001/api/users
        {
            var users = await Context.Users.ToListAsync();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)   // https://localhost:5001/api/users/1
        {
            return await Context.Users.FindAsync(id);
        }
    }
}