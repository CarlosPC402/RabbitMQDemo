using RabbitMQDemo.Interfaces;
using RabbitMQDemo.Models;
using System.Collections;

namespace RabbitMQDemo.Services
{
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>
            {
                new User { Id = 1, Name = "Juan", LastName = "Martinez", Email ="Juan@gmail.com", 
                    BirthDate = new DateTime(1992, 1, 1) 
                },
                new User { Id = 2, Name = "Maria", LastName = "Gonzales", Email ="Maria@gmail.com",
                    BirthDate = new DateTime(1997, 5, 12) 
                },
                new User { Id = 3, Name = "Pedro", LastName = "Paredes", Email ="Pedro@gmail.com", 
                    BirthDate = new DateTime(1982, 9, 20) 
            },
        };
        public IEnumerable<User> DeleteUser(int Id)
        {
            users.RemoveAt(users.FindIndex(x => x.Id == Id));
            return users.ToList();
        }

        public User GetUserById(int id)
        {
            var prueba = users.Where(x => x.Id == id).FirstOrDefault();//prueba
            return users.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetUserList()
        {
            return users.ToList();
        }

        public IEnumerable<User> NewUser(User user)
        {
            int lastId = users.Max(x=>x.Id);
            user.Id = lastId + 1;
            users.Add(user);
            return users.ToList();
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
