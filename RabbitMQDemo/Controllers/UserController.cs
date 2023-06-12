using Microsoft.AspNetCore.Mvc;
using RabbitMQDemo.Interfaces;
using RabbitMQDemo.Models;
using RabbitMQDemo.RabbitMQ;

namespace RabbitMQDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRabbitMQProducer _rabbitMQProducer;
        public UserController(IUserRepository userRepository, IRabbitMQProducer rabbitMQProducer)
        {
            _userRepository = userRepository;
            _rabbitMQProducer = rabbitMQProducer;
        }
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserList()
        {
            var users = _userRepository.GetUserList();
            return Ok(users);
        }

        [HttpGet("Users/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null || user.Id == 0)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPost("Users")]
        public async Task<ActionResult<IEnumerable<User>>> NewUser([FromBody]User user)
        {
            var users = _userRepository.NewUser(user);
            _rabbitMQProducer.SendMessage(user, "Notificación");
            return Ok(users);
        }

        [HttpDelete("Users")]
        public async Task<ActionResult<IEnumerable<User>>> DeleteUser(int id)
        {
            var users = _userRepository.DeleteUser(id);
            return Ok(users);
        }
    }
}
