using Models.Bases;
using Models.EntitiesDatabase;

namespace BusinessLogic.Handlers
{
    public class UserHandler
    {

        #region RegisterUser

        public AccountEntity GetAccount(RegisterUserBase accountBase) =>
            new()
            {
                UserName = accountBase.Login,
                Email = accountBase.Email,
                PhoneNumber = accountBase.PhoneNumber,
            };

        public UserEntity GetUser(UserBase userBase) =>
            new()
            {
                FirstName = userBase.FirstName,
                LastName = userBase.LastName,
                MiddleName = userBase.MiddleName,
                Age = userBase.Age
            };

        #endregion

    }
}