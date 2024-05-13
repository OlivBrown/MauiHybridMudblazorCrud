using SQLite;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiHybridMudblazorCrud.Models;

public class StudentModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    public string Gender { get; set; }
}
