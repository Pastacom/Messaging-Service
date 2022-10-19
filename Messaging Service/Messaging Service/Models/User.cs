using System;
using System.ComponentModel.DataAnnotations;

namespace MessagingService.Models
{
    /// <summary>
    /// Класс, представляющий собой пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>    
        [Required]
        public string UserName { get; set; }


        /// <summary>
        /// Почта пользователя, он же идентификатор.
        /// </summary>    
        [Required]
        public string Email { get; set; }


        /// <summary>
        /// Конструктор для задания значений свойствам.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="email">Почта.</param>        
        public User(string name, string email)
        {
            UserName = name;
            Email = email;
        }


        /// <summary>
        /// Беспараметрический конструктор.
        /// </summary>
        public User() { }
    }
}
