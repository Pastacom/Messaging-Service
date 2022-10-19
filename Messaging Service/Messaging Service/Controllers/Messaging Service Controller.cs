using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MessagingService.Models;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;


namespace MessagingService.Controllers
{
    /// <summary>
    /// Контроллер, с помощью которого происходит взаимодействие с API.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Сервис сообщений")]
    public class MessagingService : ControllerBase
    {
        /// <summary>
        /// Свойство, используемое для записи в JSON файл messages.json и считывания информации из него.
        /// </summary>
        private static List<Messages> Messages
        {
            get
            {
                if (System.IO.File.Exists("messages.json"))
                {
                    return JsonSerializer.Deserialize<List<Messages>>(System.IO.File.ReadAllText("messages.json"));
                }
                return new();
            }

            set
            {
                System.IO.File.WriteAllText("messages.json", JsonSerializer.Serialize(value));
            }
        }


        /// <summary>
        /// Свойство, используемое для записи в JSON файл users.json и считывания информации из него.
        /// </summary>
        private static List<User> Users
        {
            get
            {
                if (System.IO.File.Exists("users.json"))
                {
                    return JsonSerializer.Deserialize<List<User>>(System.IO.File.ReadAllText("users.json"));
                }
                return new();
            }

            set
            {
                System.IO.File.WriteAllText("users.json", JsonSerializer.Serialize(value));
            }
        }


        /// <summary>
        /// Запрос для получения списка всех пользователей.
        /// </summary>
        /// <response code="200">Получен список пользователей.</response>
        /// <response code="400">Произошла какая-то ошибка.</response>
        /// <response code="404">Список пользователей пуст.</response>
        /// <returns>Статус 200 и список пользователей, иначе статус 404,
        /// если список пользователей пуст.</returns>
        [HttpGet("Get-all-users")]
        public IActionResult GetAllUsers()
        {
            try
            {
                if (Users.Count == 0)
                {
                    return NotFound("There are no users.");
                }
                return Ok(Users);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        /// <summary>
        /// Запрос для получения списка всех сообщений.
        /// </summary>
        /// <response code="200">Получен список сообщений.</response>
        /// <response code="400">Произошла какая-то ошибка.</response>
        /// <response code="404">Список сообщений пуст.</response>
        /// <returns>Статус 200 и список сообщений, иначе статус 404,
        /// если список сообщений пуст.</returns>
        [HttpGet("Get-all-messages")]
        public IActionResult GetAllMessages()
        {
            try
            {
                if (Messages.Count == 0)
                {
                    return NotFound("There are no messages.");
                }
                return Ok(Messages);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        /// <summary>
        /// Запрос для получения списка пользователей
        /// с учетом параметров сдвига и количества.
        /// </summary>
        /// <response code="200">Получен список пользователей.</response>
        /// <response code="400">Неверное значение сдвига или количества./Произошла какая-то ошибка.</response>
        /// <response code="404">Список пользователей пуст.</response>
        /// <param name="limit">Количество пользователей.</param>
        /// <param name="offset">Сдвиг поиска.</param>
        /// <returns>Статус 200 и список пользователей, статус 404, если список пуст,
        /// статус 400, если количество неположительное или сдивг отрицательный.</returns>
        [HttpGet("Get-users-by-limit-and-offset")]
        public IActionResult GetUsers([Required] int limit, [Required] int offset)
        {
            try
            {
                if (Users.Count == 0)
                {
                    return NotFound("There are no users.");
                }
                if (limit <= 0 || offset < 0)
                {
                    return BadRequest(new
                    {
                        Message = $"Offset or limit value was invalid."
                    });
                }
                if (offset >= Users.Count)
                {
                    return BadRequest(new
                    {
                        Message = $"Offset must be less than {Users.Count}"
                    });
                }
                List<User> users = new();
                for (int i = offset; i < Math.Min(Users.Count, offset + limit); i++)
                {
                    users.Add(Users[i]);
                }
                return Ok(users);
            }
            catch 
            { 
                return BadRequest("Something went wrong."); 
            }
        }


        /// <summary>
        /// Запрос для получения пользователя по его почте.
        /// </summary>
        /// <response code="200">Получен пользователь.</response>
        /// <response code="400">Произошла какая-то ошибка.</response>
        /// <response code="404">Пользователя с такой почтой не существует.</response>
        /// <param name="email">Почта пользователя.</param>
        /// <returns>Статус 200 и искомый пользователь, иначе статус 404,
        /// если пользователя с такой почтой не существует.</returns>
        [HttpGet("Get-user-by-email")]
        public IActionResult GetUser([Required] string email)
        {
            try 
            {
                var user = Users.SingleOrDefault(x => x.Email == email);
                if (user == null)
                {
                    return NotFound(new { Message = $"User with email {email} doesn't exist." });
                }
                return Ok(user);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        /// <summary>
        /// Запрос для получения сообщений по отправителю и получателю.
        /// </summary>
        /// <response code="200">Получен список сообщений.</response>
        /// <response code="400">Произошла какая-то ошибка.</response>
        /// <response code="404">Нет сообщений с таким отправителем и получателем.</response>
        /// <param name="sender">Почта отправителя.</param>
        /// <param name="receiver">Почта получателя.</param>
        /// <returns>Статус 200 и список сообщений, иначе статус 404,
        /// если нет сообщений с таким получателем и отправителем.</returns>
        [HttpGet("Get-messages-by-sender-and-receiver")]
        public IActionResult GetMessages([Required] string sender, [Required] string receiver)
        {
            try
            {
                var result = Messages.FindAll(x => x.SenderId == sender && x.ReceiverId == receiver);
                if (result.Count == 0)
                {
                    return NotFound(new { Message = "No messages with such sender and receiver." });
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        /// <summary>
        /// Запрос для получения сообщений по отправителю.
        /// </summary>
        /// <response code="200">Получен список сообщений.</response>
        /// <response code="400">Произошла какая-то ошибка.</response>
        /// <response code="404">Нет сообщений с таким отправителем.</response>
        /// <param name="sender">Почта отправителя.</param>
        /// <returns>Статус 200 и список сообщений, иначе статус 404,
        /// если нет сообщений с таким отправителем.</returns>
        [HttpGet("Get-messages-by-sender")]
        public IActionResult GetMessagesBySender([Required] string sender)
        {
            try
            {
                var result = Messages.FindAll(x => x.SenderId == sender);
                if (result.Count == 0)
                {
                    return NotFound(new { Message = "No messages with such sender." });
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }



        /// <summary>
        /// Запрос для получения сообщений по получателю.
        /// </summary>
        /// <response code="200">Получен список сообщений.</response>
        /// <response code="400">Произошла какая-то ошибка.</response>
        /// <response code="404">Нет сообщений с таким получателем.</response>
        /// <param name="receiver">Почта получателя.</param>
        /// <returns>Статус 200 и список сообщений, иначе статус 404,
        /// если нет сообщений с таким получателем.</returns>
        [HttpGet("Get-messages-by-receiver")]
        public IActionResult GetMessagesByReceiver([Required] string receiver)
        {
            try
            {
                var result = Messages.FindAll(x => x.ReceiverId == receiver);
                if (result.Count == 0)
                {
                    return NotFound(new { Message = "No messages with such receiver." });
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        /// <summary>
        /// Генерирует списки пользователей и сообщений.
        /// При этом проводит сортировку пользователей по почте.
        /// </summary>
        /// <response code="200">Сгенерирован новые списки сообщений и пользователей.</response>
        /// <response code="400">Произошла какая-то ошибка.</response>
        /// <returns>Статус 200.</returns>
        [HttpPost("Generate-users-and-messages")]
        public IActionResult Generate()
        {
            try
            {
                var myList = Generator.GenerateUsers();
                myList.Sort((user1, user2) => user1.Email.CompareTo(user2.Email));
                Users = myList;
                Messages = Generator.GenerateMessages(Users);
                return Ok(new { Message = "Users and messages were generated." });
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        /// <summary>
        /// Запрос для добавления пользователя.
        /// При этом проводит сортировку пользователей по почте.
        /// </summary>
        /// <response code="200">Добавлен новый пользователь.</response>
        /// <response code="400">Пользователь с такой почтой уже существует./Произошла какая-то ошибка.</response>
        /// <param name="user">Пользователь.</param>
        /// <returns>Статус 200 и добавляет пользователя, иначе статус 400,
        /// если пользователь с такой почтой уже есть.</returns>
        [HttpPost("Add-User")]
        public IActionResult AddUser([FromQuery] User user)
        {
            try
            {
                if (GetUser(user.Email) is OkObjectResult)
                    return BadRequest(new { Message = $"User with email {user.Email} already exists." });
                var myList = Users;
                myList.Add(user);
                myList.Sort((user1, user2) => user1.Email.CompareTo(user2.Email));
                Users = myList;
                return Ok(new { Message = "New user added." });
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        /// <summary>
        /// Запрос для добавления сообщения.
        /// </summary>
        /// <response code="200">Добавлено новое сообщение.</response>
        /// <response code="400">Не существует отправителя или получателя с такой почтой./Произошла какая-то ошибка.</response>
        /// <param name="newMessage">Сообщение.</param>
        /// <returns>Статус 200 и добавляет сообщение, иначе статус 400,
        /// если отправителя или получателя с такой почтой нет.</returns>
        [HttpPost("Add Message")]
        public IActionResult AddMessage([FromQuery] Messages newMessage)
        {
            try
            {
                if (GetUser(newMessage.ReceiverId) is NotFoundObjectResult)
                    return NotFound(new
                    {
                        Message = $"Can't create this message because" +
                    $" receiver with email {newMessage.ReceiverId} doesn't exist."
                    });
                if (GetUser(newMessage.SenderId) is NotFoundObjectResult)
                    return NotFound(new
                    {
                        Message = $"Can't create this message because" +
                    $" sender with email {newMessage.SenderId} doesn't exist."
                    });
                var myList = Messages;
                myList.Add(newMessage);
                Messages = myList;
                return Ok(new { Message = "New message added." });
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }
    }
}
