using System;

namespace Core
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }

        public void SetUser(
            long id, 
            string password,
            string phoneNumber,
            string firstName,
            string lastName,
            string fatherName,
            DateTime birthday,
            string login
            )
        {
            Id = id;
            Password = password;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            FatherName = fatherName;
            Birthday = birthday;
            Login = login;
        }
    }
}