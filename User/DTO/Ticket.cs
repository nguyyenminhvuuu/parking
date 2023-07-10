namespace User.DTO
{
    public class Ticket
    {
        public Guid IdOrder { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdParking { get; set; }
        public Guid IdParkingDetail { get; set; }
        public string LicensePlates { get; set; }
        public DateTime DateCreate { get; set; }
        public string Status { get; set; }
    }
}
