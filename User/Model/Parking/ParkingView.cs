namespace User.Model.Parking
{
    public class ParkingView
    {
        public Guid Id { get; set; }
        public Guid IdOwner { get; set; }
        public string Name { get; set; }
        public Guid IdLocation { get; set; }
        public int Quantity { get; set; }
        public string NameLocation { get; set; }
        public string Iframe { get; set; }
    }
}
