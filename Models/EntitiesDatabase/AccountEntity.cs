using System;
using Models.Bases;

namespace Models.EntitiesDatabase
{
    public class AccountEntity : AccountBase
    {
        public UserEntity User { get; set; }
    }
}