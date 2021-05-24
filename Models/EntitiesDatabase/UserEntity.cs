using System;
using Models.Bases;

namespace Models.EntitiesDatabase
{
    public class UserEntity : UserBase
    {
        public Guid Id { get; set; }
    }
}