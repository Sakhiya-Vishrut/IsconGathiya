
namespace IsconGathiya.Helper
{
    public static class ViewHelper
    {
       
        public static string IsPageActiveClass(PaginationViewModel paginationModel, int currentPage, string cssClass = "active")
        {
            var pageNo = paginationModel.PageIndex;
            if (currentPage == pageNo)
            {
                return cssClass;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
