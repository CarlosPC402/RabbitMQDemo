using Microsoft.AspNetCore.Mvc;
using RabbitMQDemo.RabbitMQ;

namespace RabbitMQDemo.Controllers
{
    public class RabbitDemoController : Controller
    {
        private readonly IRabbitMQProducer _rabbitMQProducer;
        public RabbitDemoController(IRabbitMQProducer rabbitMQProducer)
        {
            _rabbitMQProducer = rabbitMQProducer;
        }
        [HttpPost("RabbitMultipleMessages")]
        public bool MultipleMessages(int cantidad)
        {
            int x = 1;
            while (x <= cantidad)
            {
                _rabbitMQProducer.SendMessage($"Mensage{x}");
                x++;
                //Thread.Sleep(5000);
            }
            return true;
        }

        [HttpPost("RabbitSingleMessage")]
        public bool SendMenssage(string message)
        {
            _rabbitMQProducer.SendMessage($"Nuevo: {message}");
            return true;
        }

        [HttpPost("RabbitFailureMessage")]
        public bool FailureMenssage( string message)
        {
            _rabbitMQProducer.SendMessage($"Mensage{message}");
            return true;
        }
    }
}
