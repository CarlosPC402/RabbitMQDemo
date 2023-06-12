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
                _rabbitMQProducer.SendMessage($"Mensaje {x}: ", "Notificación");
                x++;
                //Thread.Sleep(5000);
            }
            return true;
        }

        [HttpPost("RabbitSingleMessage")]
        public bool SendMenssage(string message)
        {
            _rabbitMQProducer.SendMessage($"Nuevo: {message}", "Notificación");
            return true;
        }

        [HttpPost("RabbitFailureMessage")]
        public bool FailureMenssage(int cantidad)
        {
            int x = 1;
            while (x <= cantidad)
            {
                _rabbitMQProducer.SendMessage($"Error de prueba{x}: ", null);
                x++;
                //Thread.Sleep(5000);
            }
            return true;
            //_rabbitMQProducer.SendMessage("Prueba error", null);
            //return true;
        }
    }
}
