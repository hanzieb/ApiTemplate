namespace ApiTemplate.Business.ViewModels.Generic
{
    public class ResultViewModel<T>
    {
        public int? Limit { get; set; }
        public int? NextStart { get; set; }
        public int? Length { get; set; }
        public int? Count { get; set; }
        public IEnumerable<T> Value { get; set; }

        public ResultViewModel(IEnumerable<T> value, int? limit, int? nextStart, int? length, int? count)
        {
            Value = value;
            Limit = limit;
            NextStart = nextStart;
            Length = length;
            Count = count;
        }

        public ResultViewModel(IEnumerable<T> value)
        {
            Value = value;
        }
    }
}
