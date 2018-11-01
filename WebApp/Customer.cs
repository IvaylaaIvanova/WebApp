using System.ComponentModel.DataAnnotations;

namespace WebApp
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }
    }
}
