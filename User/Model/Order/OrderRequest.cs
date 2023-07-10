namespace User.Model.Order
{
    public class OrderRequest
    {
        public Guid IdUser { get; set; }
        public Guid IdParking { get; set; }
        public Guid IdParkingDetail { get; set; }
        public string LicensePlates { get; set; }
        public double Fee { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public DateTime Time { get; set; }
    
    }
}
