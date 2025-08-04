namespace IsconGathiya.ViewModels
{
    public class JsonResultData
    {
        /// <summary>
        /// Gets or sets the status code of the JSON result.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the JSON result.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the additional data returned by the JSON result.
        /// </summary>
        public object? ReturnData { get; set; }

        /// <summary>
        /// Sets the properties of the JSON result.
        /// </summary>
        /// <param name="statusCode">The status code of the JSON result.</param>
        /// <param name="message">The message associated with the JSON result.</param>
        /// <param name="returnData">The additional data returned by the JSON result.</param>
        /// <returns>A new instance of <see cref="JsonResultData"/> with the specified properties.</returns>
        public static JsonResultData SetJsonModel(int statusCode, string message, object? returnData = null)
        {
            return new JsonResultData
            {
                StatusCode = statusCode,
                Message = message,
                ReturnData = returnData
            };
        }
    }
}
