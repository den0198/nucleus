using System;
using Models.Bases;

namespace Models.EntitiesDatabase
{
    public class AccountEntity : AccountBase
    {
        public virtual UserDetailsEntity UserDetails { get; set; }
    }
}