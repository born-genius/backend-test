using System.ComponentModel.DataAnnotations;

namespace TechnicalTest.Infrastructure.Entities
{
    public class CustomerState
    {
        public int Id { get; set; }


        [StringLength(60)]
        public string StateName { get; set; }

        [StringLength(2550)]
        public string StateDescription { get; set; }
    }
}
