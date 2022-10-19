using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagingService.Models;
namespace MessagingService
{
    public class Generator
    {
        private static readonly Random s_rng = new();


        /// <summary>
        /// Метод для генерации списка пользователей.
        /// Всегда генерируется 10 пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        public static List<User> GenerateUsers()
        {
            List<User> users = new();
            for(int i = 0; i < 10; i++)
            {
                users.Add(new User(GenerateName(), GenerateEmail()));
            }
            return users;
        }


        /// <summary>
        /// Метод для генерации списка сообщений.
        /// Всегда генерируется 10 сообщений.
        /// </summary>
        /// <returns>Список сообщений.</returns>
        public static List<Messages> GenerateMessages(List<User> users)
        {
            List<Messages> messages = new();

            // Извлекаем список почт, для генерации сообщений.
            List<string> emails = users.Select(x => x.Email).ToList();
            for (int i = 0; i < 10; i++)
            {
                messages.Add(GetMessage(emails));
            }
            return messages;
        }


        /// <summary>
        /// Метод для генерации сообщения.
        /// </summary>
        /// <param name="emails">Список почт.</param>
        /// <returns>Сообщение.</returns>
        private static Messages GetMessage(List<string> emails)
        {
            int firstResult = s_rng.Next(0, emails.Count);
            string sender = emails[firstResult];

            // Почта получателя может быть любая другая кроме почты отправителя.
            string receiver = emails[s_rng.Next(firstResult + 1, emails.Count) % emails.Count];
            string subject = $"New message from {sender}";
            string text = $"Some stuff from {sender}";
            return new Messages(subject, text, sender, receiver);
        }


        /// <summary>
        /// Метод для генерации имени пользователя.
        /// </summary>
        /// <returns>Имя.</returns>
        private static string GenerateName()
        {
            int length = s_rng.Next(3, 10);
            StringBuilder name = new();
            name.Append((char)s_rng.Next('A', 'Z' + 1));
            for (int i = 0; i < length; i++)
            {
                name.Append((char)s_rng.Next('a', 'z' + 1));
            }
            return name.ToString();
        }


        /// <summary>
        /// Метод для генерации почты пользователя.
        /// </summary>
        /// <returns>Почта.</returns>
        private static string GenerateEmail()
        {
            int length = s_rng.Next(7, 13);
            StringBuilder email = new();
            for (int i = 0; i < length; i++)
            {
                email.Append((char)s_rng.Next('a', 'z' + 1));
            }
            email.Append("@mail.ru");
            return email.ToString();
        }
    }
}
