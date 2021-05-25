using System;
using Models.Bases;

namespace Models.Responses
{
    public class SignInResponse : TokenBase
    {
        public Guid UserId { get; set; }
    }
}