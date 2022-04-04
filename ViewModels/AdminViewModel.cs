using System.ComponentModel.DataAnnotations;
using marketplace.Datas.Entities;

namespace marketplace.ViewModels;

public class AdminViewModel
{
    private Admin result;

    public AdminViewModel()
    {
        Nama = string.Empty;
        NoHp = string.Empty;
        Username = string.Empty;
        Password = string.Empty;
        Email = string.Empty;
    }

    public AdminViewModel(int IdAdmin, string nama, string noHp, string username, string password , string email)
    {
        IdAdmin = IdAdmin;
        Nama = nama;
        NoHp = noHp;
        Username = username;
        Password = password;
        Email = email;
    }
    public AdminViewModel(Admin item){
            IdAdmin = item.IdAdmin;
            Nama = item.Nama;
            NoHp = item.NoHp;
            Username = item.Username;
            Password = item.Password;
            Email = item.Email;
        }
    public int IdAdmin { get; set; }
    public string Nama { get; set; }
    public string NoHp { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }

    public Admin ConvertToDbModel(){
            return new Admin {
                IdAdmin = this.IdAdmin,
                Nama = this.Nama,
                NoHp = this.NoHp,
                Username = this.Username,
                // Password = this.Password,
                Email = this.Email,
            };
        }
} 