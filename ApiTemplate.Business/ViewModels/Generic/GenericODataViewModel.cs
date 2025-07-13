namespace API.Business.ViewModels
{
    /// <summary>
    /// The generic view model for Odata
    /// Pass in a string for the OData params you would like to use into the body
    /// </summary>
    public class GenericODataViewModel
    {
        /// <summary>
        /// Skips the first n results
        /// </summary>
        public string Skip { get; set; }
        /// <summary>
        /// Returns the first n results
        /// </summary>
        public string Top { get; set; }
        /// <summary>
        /// Filters the results using oData Notation
        /// </summary>
        public string? Filter { get; set; }
        /// <summary>
        /// Orders the results by the fields listed
        /// </summary>
        public string OrderBy { get; set; }
    }
}