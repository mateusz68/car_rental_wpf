using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace kck_projekt.Controller
{
    public class ManageUsers
    {
        private Model.AppContext model { get; set; }
        private ViewInterface view { get; set; }
        public ManageUsers(Model.AppContext model, ViewInterface view)
        {
            this.model = model;
            this.view = view;
        }
        public void AddUser(string name, string password, string email)
        {
            var salt = GenerateSalt();
            var hashPassword = GenerateHash(password, salt);
            model.Users.Add(new Model.User { UserName = name, UserPassword = hashPassword, Salt = salt, Email = email, Rola=Model.UserRole.User});
            model.SaveChanges();
            view.ShowMessage("Pomyślnie dodano nowego użytkownika.");
        }

        public void ChangePassword(string oldPassword, string newPassword, int userId)
        {
            if(UserExists(userId))
            {
                var tempUser = model.Users.Where(u => u.UserId == userId).FirstOrDefault();
                var salt = GetUserSalt(tempUser.UserName);
                var hasPassword = GenerateHash(oldPassword, salt);
                if(hasPassword == tempUser.UserPassword)
                {
                    tempUser.UserPassword = GenerateHash(newPassword, salt);
                    model.SaveChanges();
                    view.ShowMessage("Pomyślnie zmienione hasło.");
                    return;
                }
            }
            view.ShowMessage("Nie udało się zmienić hasła.");
        }

        public void ChangeUserSettings(string name, string surname, int phone, string adres1, string adres2, string adres3, int userId)
        {
            if(UserExists(userId))
            {
                var tempUser = model.Users.Where(u => u.UserId == userId).FirstOrDefault();
                tempUser.Name = name;
                tempUser.Surname = surname;
                tempUser.Phone = phone;
                tempUser.Adres1 = adres1;
                tempUser.Adres2 = adres2;
                tempUser.Adres3 = adres3;
                model.SaveChanges();
                view.ShowMessage("Pomyslnie zmienione dane użytkownika.");
                return;
            }
            view.ShowMessage("Błąd. Nie udał osię zmienić danych użytkownika.");
        }


        public bool UserExists(string name)
        {
            var user = model.Users
                .Where(b => b.UserName == name)
                .FirstOrDefault();
            if(user == null)
            {
                return false;
            }
            return true;
        }

        public bool UserExists(int userId)
        {
            var user = model.Users
                .Where(b => b.UserId == userId)
                .FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public String GetUserSalt(string name)
        {
            var user = model.Users
                .Where(b => b.UserName == name)
                .FirstOrDefault();
            if( user != null)
            {
                return user.Salt;
            }
            return null;
        }

        public int GetUserId(string name, string password)
        {
            var salt = GetUserSalt(name);
            var hashPassword = GenerateHash(password, salt);
            var user = model.Users
                .Where(b => b.UserName == name)
                .Where(b => b.UserPassword == hashPassword)
                .FirstOrDefault();
            if(user != null)
            {
                return user.UserId;
            }
            return -1;
        }

        public Model.User GetUser(string name, string password)
        {
            var salt = GetUserSalt(name);
            var hashPassword = GenerateHash(password, salt);
            var user = model.Users
                .Where(b => b.UserName == name)
                .Where(b => b.UserPassword == hashPassword)
                .FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            return null;
        }


        public List<Model.User> GetUserList()
        {
            return model.Users.ToList();
        }

        public Model.User GetUserHash(string name, string password)
        {
            var user = model.Users
                .Where(b => b.UserName == name)
                .Where(b => b.UserPassword == password)
                .FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public void DeleteUser(int userId)
        {
            var tempUser = model.Users.SingleOrDefault(m => m.UserId == userId);
            if (tempUser != null)
            {
                model.Users.Remove(tempUser);
                model.SaveChanges();
                view.ShowMessage("Pomyślnie usunięto użytkownika " + tempUser);
                return;
            }
            view.ShowMessage("Nie udało się usunąć użytkownika.");
        }

        public void SaveUser(Model.User userData, int editUserId)
        {
            if (editUserId == -1)
            {
                model.Users.Add(userData);
                model.SaveChanges();
                view.ShowMessage("Pomyślnie dodano użytkownika " + userData);
                return;
            }
            else
            {
                var tempUser = model.Users.SingleOrDefault(c => c.UserId == editUserId);
                if (tempUser != null)
                {
                    tempUser.UserName = userData.UserName;
                    if(userData.UserPassword != null)
                    {
                        tempUser.UserPassword = userData.UserPassword;
                        tempUser.Salt = userData.Salt;
                    }
                    tempUser.Phone = userData.Phone;
                    tempUser.Adres1 = userData.Adres1;
                    tempUser.Adres2 = userData.Adres2;
                    tempUser.Adres3 = userData.Adres3;
                    tempUser.Rola = userData.Rola;
                    model.SaveChanges();
                    view.ShowMessage("Pomyślnie edytowano dane użytkownika " + userData);
                    return;
                }
                view.ShowMessage("Nie udało się zmienić danych użytkownika");
            }
        }

        public static String GenerateSalt()
        {
            int minSaltSize = 4;
            int maxSaltSize = 6;
            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[saltSize];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }


        public static string GenerateHash(string plainText, string textSalt)
        {
            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] saltBytes = Encoding.UTF8.GetBytes(textSalt);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];


            HashAlgorithm hash = new SHA512Managed();

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }
    }

}
