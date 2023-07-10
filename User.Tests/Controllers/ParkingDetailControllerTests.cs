using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using System.Net;
using User.Controllers;
using User.DTO;
using User.Service.Interface;
using User.Tests.Data;

namespace User.Tests.Controllers
{
    public class ParkingDetailControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private ParkingDetailController _pc;
        private WebApplicationFactory<Program> _program;
        private HttpClient _httpClient;

        public ParkingDetailControllerTests(WebApplicationFactory<Program> program)
        {
            _program = program;
            _httpClient = _program.CreateClient();
        }

        [Fact]
        public async Task ParkingDetailController_GetParkings_ReturnOkAndParkingDetails()
        {
            var response = await _httpClient.GetAsync("/api/ParkingDetail");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [MemberData(nameof(ParkingDetailIds))]
        public async Task ParkingDetailController_GetParking_ReturnOkAndParkingDetail(RequestParkingDetailData data)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"/api/ParkingDetail/{data.Request}", UriKind.Relative)
            };

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var parkingDetail = JsonConvert.DeserializeObject<ParkingDetail>(content);


            response.StatusCode.Should().Be(HttpStatusCode.OK);
            parkingDetail.Should().NotBeNull();
            Assert.Equivalent(parkingDetail!.Id, data.Response);
        }

        public static IEnumerable<object[]> ParkingDetailIds()
        {
            return new List<object[]>
        {
            new object[] { new RequestParkingDetailData ( Guid.Parse("03897ca3-8178-4cf4-9669-244a1b61a7c3"), Guid.Parse("03897ca3-8178-4cf4-9669-244a1b61a7c3"))},
            new object[] { new RequestParkingDetailData (Guid.Parse("041b707d-02b4-4512-989a-d098688d9ee8"), Guid.Parse("041b707d-02b4-4512-989a-d098688d9ee8"))},

        };
        }


        [Fact]
        public async Task ParkingDetailController_GetParkings_ReturnOk()
        {
            var parkingDetails = ParkingDetailData.Instance.GetParkingDetails();

            var parkingMoqService = new Mock<IParkingDetailServices>();

            parkingMoqService.Setup(x => x.GetAll()).ReturnsAsync(parkingDetails);
            _pc = new ParkingDetailController(parkingMoqService.Object);

            parkingMoqService.Setup(s => s.GetAll()).ReturnsAsync(ParkingDetailData.Instance.GetParkingDetails());

            var result = await _pc.GetAll();

            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult)!.StatusCode.Should().Be(200);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equivalent(JsonConvert.SerializeObject(parkingDetails), JsonConvert.SerializeObject(okResult.Value), strict: true);
        }


        [Fact]
        public async Task ParkingDetailController_GetParking_ReturnOkObject()
        {
            string id = "03897ca3-8178-4cf4-9669-244a1b61a7c3";
            var parkingDetail = ParkingDetailData.Instance.GetParkingDetail();
            var parkingMoqService = new Mock<IParkingDetailServices>();
            parkingMoqService.Setup(x => x.GetParkingDetailById(id)).ReturnsAsync(ParkingDetailData.Instance.GetParkingDetail());
            _pc = new ParkingDetailController(parkingMoqService.Object);

            var result = await _pc.GetParkingDetailById(Guid.Parse(id));

            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult)!.StatusCode.Should().Be(200);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equivalent(JsonConvert.SerializeObject(parkingDetail), JsonConvert.SerializeObject(okResult.Value));
        }


    }
}
