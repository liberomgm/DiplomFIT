namespace Core
{
    public class User
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public float PhoneNumber { get; set; }
        public string Birthday { get; set; }

        public void SetUser(
            long id, 
            string password,
            float phoneNumber,
            string firstName,
            string lastName,
            string fatherName,
            string birthday
            )
        {
            Id = id;
            Password = password;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            FatherName = fatherName;
            Birthday = birthday;
        }
    }
}