using Models.Bases;

namespace Models.EntitiesDatabase
{
    public class AccountEntity : AccountBase
    {
        public virtual UserEntity User { get; set; }
    }
}