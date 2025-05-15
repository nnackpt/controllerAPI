using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")] // api/User
public class UserController : ControllerBase
{
    // Mock data
    private static readonly List<User> _users = new List<User>
    {
        new User {
                Id = 1,
                Username = "user1",
                Email = "user1@example.com",
                Fullname = "user one"
            },
            new User {
                Id = 2,
                Username = "user2",
                Email = "user2@example.com",
                Fullname = "user two"
            },
    };

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {

        return Ok(_users);
    }

    [HttpGet("{id}")] // api/User/{id}
    public ActionResult<User> GetUser(int id)
    {
        var user = _users.Find(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // Create user
    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        _users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // update user
    [HttpPut("{id}")] // api/User/{id}
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        var existingUser = _users.Find(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        existingUser.Fullname = user.Fullname;

        return Ok(existingUser);
    }

    // delete user
    [HttpDelete("{id}")] // api/User/{id}
    public ActionResult DeleteUser(int id)
    {
        var user = _users.Find(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        _users.Remove(user);
        return NoContent();
    }

    // delete all user
    [HttpDelete]
    public ActionResult DeleteAllUsers()
    {
        _users.Clear();
        return NoContent();
    }
}