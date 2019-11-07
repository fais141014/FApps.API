using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FApps.Core.Domain
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        protected BaseEntity()
        {
            InitializeBaseEntityStates();
        }
        protected void InitializeBaseEntityStates()
        {
            Id = default(Guid?);
            IsActive = true;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }
    }
      
}
