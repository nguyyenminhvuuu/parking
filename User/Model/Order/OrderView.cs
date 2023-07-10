namespace User.Model.Order
{
    public class OrderView
    {
    
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdParking { get; set; }
        public Guid IdParkingDetail { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public double Fee { get; set; }
        public double TotalMoney { get; set; }
        public string LicensePlates { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime Time { get; set; }
        public DateTime DateCreate { get; set; }
        public string Status { get; set; }
    }
}
