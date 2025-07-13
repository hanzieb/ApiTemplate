using ApiTemplate.Model.EF;

namespace ApiTemplate.Business.ViewModels
{
    public class PhotoViewModel
    {
        public PhotoViewModel(Photo photo) 
        {
            Id = photo.Id;
            FileContents = photo.FileContents;
            Index = photo.Index;
            Width = photo.Width;
            Height = photo.Height;
        }
        public int Id { get; set; }
        public byte[] FileContents { get; set; }
        public int Index { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
