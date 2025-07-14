using ApiTemplate.Model.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.Model.EF.Entities
{
    public class Animal : TableEntity
    {
        public Animal() { }

        public Animal(string name, string desc, string breed, AnimalTypes type, List<Photo> photos)
        {
            Name = name;
            Description = desc;
            Breed = breed;
            AnimalType = type;
            Photos = photos;
        }

        [Key()]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Breed { get; set; }
        public AnimalTypes AnimalType { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
