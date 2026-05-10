using System.ComponentModel.DataAnnotations;

namespace bageri_api.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }

}
