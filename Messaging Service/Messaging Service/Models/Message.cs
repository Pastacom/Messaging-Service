using System;
using System.ComponentModel.DataAnnotations;

namespace MessagingService.Models
{
    /// <summary>
    /// Класс, представляющий собой сообщение.
    /// </summary>
    public class Messages
    {
        /// <summary>
        /// Тема сообщения.
        /// </summary>      
        [Required]
        public string Subject { get; set; }


        /// <summary>
        /// Текст сообщения.
        /// </summary>
        [Required]
        public string Message { get; set; }


        /// <summary>
        /// Почта отправителя.
        /// </summary>
        [Required]
        public string SenderId { get; set; }


        /// <summary>
        /// Почта получателя.
        /// </summary>
        [Required]
        public string ReceiverId { get; set; }


        /// <summary>
        /// Конструктор для задания значений свойствам.
        /// </summary>
        /// <param name="subject">Тема.</param>
        /// <param name="message">Текст.</param>
        /// <param name="sender">Почта отправителя.</param>
        /// <param name="receiver">Почта получателя.</param>
        public Messages(string subject, string message, string sender, string receiver)
        {
            (Subject, Message, SenderId, ReceiverId) = (subject, message, sender, receiver);
        }


        /// <summary>
        /// Беспараметрический конструктор.
        /// </summary>
        public Messages() { }
    }
}
