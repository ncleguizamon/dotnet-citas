using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DatingApp.API.Dtos;
using System.Collections.Generic;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
       private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo , IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var users = await _repo.getUsers();
            var userToreturn = _mapper.Map<IEnumerable<userForListDto>>(users);
            return Ok(userToreturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.getUser(id);
            var userToreturn = _mapper.Map<UserForDetaledDto>(user);
            return Ok(userToreturn);

        }

    }
}