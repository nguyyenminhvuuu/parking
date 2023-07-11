using FakeItEasy;
using Firebase.Database;
using User.DTO;
using User.Repository;
using User.Repository.Interface;
using User.Service.Implement;
using User.Service.Interface;

namespace User.Tests.Service
{
    public class ParkingDetailServiceTest
    {
        private static string _child = "ParkingDetail";
        private IParkingDetailServices _parkingDetailServices;
        private IParkingDetailRepository _parkingDetailRepository;
        private IUnitOfWork _unitOfWork;

        public ParkingDetailServiceTest()
        {
            _parkingDetailRepository = A.Fake<IParkingDetailRepository>();
            _unitOfWork= A.Fake<IUnitOfWork>();
        }

        [Fact]
        public async Task ParkingDetailService_GetAll_ReturnNotNull()
        {
            var parkingDetails = A.Fake<List<FirebaseObject<ParkingDetail>>>();

            //  var parkingMoqRepository = A.Fake<List<IUnitOfWork>>();
            //  parkingMoqRepository.Setup(x => x.ParkingDetailRepository.GetAllAsync(_child)).ReturnsAsync(parkingDetails);

            A.CallTo(() => _parkingDetailRepository.GetAllAsync(_child)).Returns(parkingDetails);

            //  _parkingDetailServices = new ParkingDetailServices(parkingMoqRepository.Object);
            _parkingDetailServices = new ParkingDetailServices(_unitOfWork);
            var result = await _parkingDetailServices.GetAll();
            Assert.NotNull(result);
        }
    }
}
