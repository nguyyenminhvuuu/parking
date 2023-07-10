using FakeItEasy;
using Firebase.Database;
using Moq;
using User.DTO;
using User.Repository;
using User.Service.Implement;
using User.Service.Interface;

namespace User.Tests.Service
{
    public class ParkingDetailServiceTest
    {
        private static string _child = "ParkingDetail";
        private IParkingDetailServices _parkingDetailServices;

        [Fact]
        public async Task ParkingDetailService_GetAll_ReturnNotNull()
        {
            var parkingDetails = A.Fake<List<FirebaseObject<ParkingDetail>>>();

            var parkingMoqRepository = new Mock<IUnitOfWork>();

            parkingMoqRepository.Setup(x => x.ParkingDetailRepository.GetAllAsync(_child)).ReturnsAsync(parkingDetails);

            _parkingDetailServices = new ParkingDetailServices(parkingMoqRepository.Object);

            var result = await _parkingDetailServices.GetAll();
            Assert.NotNull(result);
        }
    }
}
