using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TryAspNetCore.Api.Domain
{
    public class UserToken : IdentityUserToken<Guid>
    {

    }
}