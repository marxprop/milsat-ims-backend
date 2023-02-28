namespace MilsatIMS.ViewModels
{
    public class PagingVm
    {
        public int pageNumber { get; }
        public int pageSize { get; }

        public PagingVm(int pageNumber = 1, int pageSize = 15)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize < 20 ? pageSize : 20;
        }
    }
}
