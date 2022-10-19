using System;
using System.ComponentModel.DataAnnotations;

namespace MessagingService.Models
{
    /// <summary>
    /// �����, �������������� ����� ������������.
    /// </summary>
    public class User
    {
        /// <summary>
        /// ��� ������������.
        /// </summary>    
        [Required]
        public string UserName { get; set; }


        /// <summary>
        /// ����� ������������, �� �� �������������.
        /// </summary>    
        [Required]
        public string Email { get; set; }


        /// <summary>
        /// ����������� ��� ������� �������� ���������.
        /// </summary>
        /// <param name="name">���.</param>
        /// <param name="email">�����.</param>        
        public User(string name, string email)
        {
            UserName = name;
            Email = email;
        }


        /// <summary>
        /// ������������������ �����������.
        /// </summary>
        public User() { }
    }
}
