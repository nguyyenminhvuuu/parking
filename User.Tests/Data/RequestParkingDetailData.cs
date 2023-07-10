namespace User.Tests.Data
{
    public class RequestParkingDetailData
    {
        public Guid Request { get; set; }
        public Guid Response { get; set; }
        public RequestParkingDetailData(Guid Request, Guid Response)
        {
            this.Request = Request;
            this.Response = Response;
        }
    }
}
