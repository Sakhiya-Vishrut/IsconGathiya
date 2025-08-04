using IsconGathiya;

namespace IsconGathiya
{
    public class BaseModelViewModel : PaginationViewModel
    {
        /// <summary>
        /// Gets or sets the description of the page.
        /// </summary>
        public string? BreadcrumbTitle { get; set; }
        public string? BreadcrumbParent { get; set; }
        public string? BreadcrumbChild { get; set; }

        /// <summary>
        /// Gets or sets the title of the page.
        /// </summary>
        public string? PageTitle { get; set; }

        /// <summary>
        /// Gets or sets the description of the page.
        /// </summary>
        public string? PageDescription { get; set; }

        /// <summary>
        /// Gets or sets the date when the model was created.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the model.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date when the model was last updated.
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last updated the model.
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the ID of the selected menu.
        /// </summary>
        public int SelectedMenuId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the selected role.
        /// </summary>
        public int SelectedRoleId { get; set; }

        
    }
}
