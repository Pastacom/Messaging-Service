using System;
using System.ComponentModel.DataAnnotations;

namespace MessagingService.Models
{
    /// <summary>
    /// �����, �������������� ����� ���������.
    /// </summary>
    public class Messages
    {
        /// <summary>
        /// ���� ���������.
        /// </summary>      
        [Required]
        public string Subject { get; set; }


        /// <summary>
        /// ����� ���������.
        /// </summary>
        [Required]
        public string Message { get; set; }


        /// <summary>
        /// ����� �����������.
        /// </summary>
        [Required]
        public string SenderId { get; set; }


        /// <summary>
        /// ����� ����������.
        /// </summary>
        [Required]
        public string ReceiverId { get; set; }


        /// <summary>
        /// ����������� ��� ������� �������� ���������.
        /// </summary>
        /// <param name="subject">����.</param>
        /// <param name="message">�����.</param>
        /// <param name="sender">����� �����������.</param>
        /// <param name="receiver">����� ����������.</param>
        public Messages(string subject, string message, string sender, string receiver)
        {
            (Subject, Message, SenderId, ReceiverId) = (subject, message, sender, receiver);
        }


        /// <summary>
        /// ������������������ �����������.
        /// </summary>
        public Messages() { }
    }
}
