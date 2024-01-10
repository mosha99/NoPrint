using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NoPrint.Framework.Identity;

namespace NoPrint.Framework;

public abstract class Aggregate<T> where T : IdentityBase, new()
{
    protected Aggregate()
    {
        Id = new T() { Id = 0 };
    }
    [NotMapped]
    public T Id { get; set; }

    [Key]
    public long _Id
    {
        get => Id.Id;
        private set => Id = new T() { Id = value };
    }
}