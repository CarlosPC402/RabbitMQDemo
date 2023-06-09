using RabbitMQDemo.Models;

namespace RabbitMQDemo.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUserList();
        public User GetUserById(int id);
        public IEnumerable<User> NewUser(User user);
        public User UpdateUser(User user);
        public IEnumerable<User> DeleteUser(int Id);
    }
}
