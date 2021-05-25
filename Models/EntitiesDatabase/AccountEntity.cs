using System;
using Models.Bases;

namespace Models.EntitiesDatabase
{
    public class AccountEntity : AccountBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}