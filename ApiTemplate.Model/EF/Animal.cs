using ApiTemplate.Model.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.Model.EF
{
    public class Animal : TableEntity
    {
        [Key()]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AnimalTypes AnimalType { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
