using ApiTemplate.Model.EF.Entities;

namespace ApiTemplate.Business.ViewModels
{
    public class PhotoViewModel
    {
        public PhotoViewModel(Photo photo) 
        {
            Id = photo.Id;
            FilePath = photo.FilePath;
            Index = photo.Index;
            Width = photo.Width;
            Height = photo.Height;
        }
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int Index { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
