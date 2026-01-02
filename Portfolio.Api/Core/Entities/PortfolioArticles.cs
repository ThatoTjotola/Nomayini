using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Core.Entities
{
    public class PortfolioArticles
    {
       public int Id { get; set; }

        [Required]
        public string ?PortfolioContent { get; set; }
    }
}
