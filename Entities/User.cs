using Microsoft.AspNetCore.Identity;

namespace bageri_api.Entities;

public class User : IdentityUser
{
    public string StoreName { get; set; }
    public string ContactPerson { get; set; }
}
