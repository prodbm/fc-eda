using System.ComponentModel.DataAnnotations.Schema;

namespace BalanceApi.Models;

[Table("accounts")]
public class Account
{

    [Column("id")]
    public string Id { get; set; }
    
    [Column("balance")]
    public int Balance { get; set; }
    
    [Column("updated_At")]
    public DateTime Updated_At { get; set; }
}
