using User.DTO;
using User.Repository;
using User.Service.Interface;

namespace User.Service.Implement
{
    public class ParkingDetailServices : IParkingDetailServices
    {
        private IUnitOfWork _context;
        private static string _child = "ParkingDetail";
        private static List<ParkingDetail> listParkingDetail = new List<ParkingDetail>();
        public ParkingDetailServices(IUnitOfWork iuw)
        {
            _context = iuw;
        }
        public async Task<List<ParkingDetail>> GetAll()
        {
            try
            {
                var dataSnapShot = await _context.ParkingDetailRepository.GetAllAsync(_child);
                if (listParkingDetail != null)
                {
                    listParkingDetail.Clear();
                }
                foreach (var item in dataSnapShot)
                {
                    listParkingDetail!.Add(item.Object);
                }
                return listParkingDetail!;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<ParkingDetail>> GetParkingDetailByIdParking(Guid? idParking)
        {
            try
            {
                var rs = await _context.ParkingDetailRepository.GetAllAsync(_child);
                if (listParkingDetail != null)
                {
                    listParkingDetail.Clear();
                }
                foreach (var item in rs)
                {
                    if (item.Object.IdParking.Equals(idParking))
                    {
                        listParkingDetail!.Add(item.Object);
                    }
                }
                 return listParkingDetail!;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }

        public async Task<ParkingDetail> GetParkingDetailById(string? idParkingDetail)
        {
            return await _context.ParkingDetailRepository.GetByPrimaryKeyAsync(idParkingDetail, _child);
        }
    }
}
