using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web2API.Models;

namespace Web2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DbcontextContext _context;

        public UserController(DbcontextContext context) {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() {

            if (_context.Users == null) {
                return Ok("Нету записей");
            }
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> createUser(UserDTO user)
        {
            try
            {
                User user2 = new User();

                int idUser = _context.Users.Max(x => x.Iduser) + 1;

                user2.Iduser = idUser;
                user2.Surname = user.Surname;
                user2.Name = user.Name;
                user.Patronymic = BCrypt.Net.BCrypt.HashPassword(user.Patronymic);
                user2.Patronymic = user.Patronymic;
                user2.Adress = user.Adress;
                user2.Salary = user.Salary;
                user2.Dateb = user.Dateb;
                user2.Post = user.Post;
                user2.Number = user.Number;
                user2.ChildId = user.ChildId;
                user2.DivisionId = user.DivisionId;
                user2.Child = await _context.Children.FirstAsync(x => x.Idchild == user.ChildId);
                user2.Division = await _context.Divisions.FirstAsync(x => x.IdDivision == user.DivisionId);

                _context.Add(user2);
                await _context.SaveChangesAsync();
                return Ok("Запись добавлена");
            }
            catch { return Ok("Запись не добавлена, что-то не так с данными!"); }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(int id, UserDTO user)
        {
            if (id != user.Iduser)
            {
                return BadRequest();
            }

            var user2 = await _context.Users.FirstOrDefaultAsync(x => x.Iduser == user.Iduser);

            user2.Surname = user.Surname;
            user2.Name = user.Name;
            user.Patronymic = BCrypt.Net.BCrypt.HashPassword(user.Patronymic);
            user2.Patronymic = user.Patronymic;
            user2.Adress = user.Adress;
            user2.Salary = user.Salary;
            user2.Dateb = user.Dateb;
            user2.Post = user.Post;
            user2.Number = user.Number;
            user2.ChildId = user.ChildId;
            user2.DivisionId = user.DivisionId;
            user2.Child = await _context.Children.FirstAsync(x => x.Idchild == user.ChildId);
            user2.Division = await _context.Divisions.FirstAsync(x => x.IdDivision == user.DivisionId);

            _context.Entry(user2).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound();

            }
            return NoContent();
        }

        [HttpPost("SignIn")]
        public async Task<string> SignIn(UserDTO users)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == users.Name && x.Surname == users.Surname);
            if (user is null) return "400";
            if (BCrypt.Net.BCrypt.Verify(users.Patronymic, user.Patronymic))
                return "200";
            else
                return "400";
        }

    }
}
