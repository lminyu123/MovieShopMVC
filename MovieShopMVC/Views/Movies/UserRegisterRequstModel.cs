using System;
namespace MovieShopMVC.Views.Movies
{
    public class UserRegisterRequstModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
