using ApiTemplate.Model.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.Model.EF.Entities
{
    public class Photo : TableEntity
    {
        public Photo() { }
        public Photo (string filePath, int index, int width, int height)
        {
            FilePath = filePath;
            Height = height;
            Width = width;
            Index = index;
        }

        [Key()]
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int Index { get; set; }
        public int Width {  get; set; }
        public int Height { get; set; }

        public virtual int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }

    }
}
