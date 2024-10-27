using System.Net;

namespace CollegeApp.DTO
{
    public class ApiResponse
    {
        public bool Status { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public dynamic data { get; set; }

        public List<string> Errors { get; set; }
    }
}
