using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
   public class ProjectDetail
   {
      [Key]
      public int ProjectId { get; set; }

      public Project Project { get; set; }

      public decimal Budget { get; set; }

      public bool Critical { get; set; }
   }
}