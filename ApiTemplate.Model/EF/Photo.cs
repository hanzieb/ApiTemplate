using ApiTemplate.Model.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.Model.EF
{
    public class Photo : TableEntity
    {
        [Key()]
        public int Id { get; set; }
        public byte[] FileContents { get; set; }
        public int Index { get; set; }
        public int Width {  get; set; }
        public int Height { get; set; }

        public virtual int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }

    }
}
