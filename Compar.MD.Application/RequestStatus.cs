namespace Compar.MD.Application
{
    public class RequestStatus
    {
        public bool IsSuccessful { get; set; } = true;
        public string Error { get; set; }

        public static RequestStatus Create(string error)
        {
            return new RequestStatus()
            {
                IsSuccessful = false,
                Error = error
            };
        }
    }
}