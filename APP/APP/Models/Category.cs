using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        [NotMapped]
        public IFormFile clientFile { get; set; }

        public byte[]? dbImg { get; set; }
        [NotMapped]
        public string? imgSrc
        {
            get
            {
                if (dbImg != null)
                {
                    string base64String = Convert.ToBase64String(dbImg,0,dbImg.Length);
                    return "data:image/jpg;base64,"+ base64String;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public ICollection<Item>? Items { get; set; }
    }
}
