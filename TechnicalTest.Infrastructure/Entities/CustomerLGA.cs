using System.ComponentModel.DataAnnotations;

namespace TechnicalTest.Infrastructure.Entities
{
    public class CustomerLGA
    {
        public int Id { get; set; }

        [StringLength(60)]
        public string LGAName { get; set; }
        [StringLength(2055)]
        public string? LGADescription { get; set; }

        public int CustomerStateId { get; set; }

        //Navigation property
        public CustomerState CustomerState { get; set; }
    }
}
