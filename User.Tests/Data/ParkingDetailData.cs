using Firebase.Database;
using System.Collections.Generic;
using User.DTO;

namespace User.Tests.Data
{
    public class ParkingDetailData
    {
        private static ParkingDetailData instance;

        public static ParkingDetailData Instance { get { if (instance == null) { instance = new ParkingDetailData(); } return instance; } private set => instance = value; }

        public List<ParkingDetail> GetParkingDetails()
        {

            return new List<ParkingDetail>() {
                    new ParkingDetail{
                Id=Guid.Parse("03897ca3-8178-4cf4-9669-244a1b61a7c3"),
                IdParking=Guid.Parse("0821c17f-b67c-4105-8b1a-7ecf754fd0fe"),
                lastUpdate=DateTime.Parse("2023-05-23T20:04:27.9149794+07:00"),
                Price=123,
                Status="None",
                Type=Guid.Parse("aad45a8e-ba94-4956-8829-ce248498037d")
        },
                    new ParkingDetail{
                Id=Guid.Parse("041b707d-02b4-4512-989a-d098688d9ee8"),
                IdParking=Guid.Parse("7e720a24-8953-4358-a1a9-5f7f5f82ed8d"),
                 lastUpdate=DateTime.Parse("2023-05-23T20:12:22.6094593+07:00"),
                Price=5,
                Status="None",
                Type=Guid.Parse("d96acc68-874b-4a5b-b364-d0472181e28c")
            },
            };
        }
        public ParkingDetail GetParkingDetail()
        {
            return new ParkingDetail
            {
                Id = Guid.Parse("03897ca3-8178-4cf4-9669-244a1b61a7c3"),
                IdParking = Guid.Parse("0821c17f-b67c-4105-8b1a-7ecf754fd0fe"),
                lastUpdate = DateTime.Parse("2023-05-23T20:04:27.9149794+07:00"),
                Price = 123,
                Status = "None",
                Type = Guid.Parse("aad45a8e-ba94-4956-8829-ce248498037d")
            };
        }
    }
}
