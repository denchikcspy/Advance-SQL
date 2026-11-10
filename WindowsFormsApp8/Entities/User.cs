using System;

namespace WindowsFormsApp8.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime BirthDay { get; set; }

        public string GetFormattedDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
    }
}
