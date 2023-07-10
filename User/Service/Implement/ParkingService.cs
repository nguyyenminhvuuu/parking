using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using User.DTO;
using User.Model.Parking;
using User.Repository;
using User.Repository.Interface;
using User.Service.Interface;

namespace User.Service.Implement
{
    public class ParkingService : IParkingService
    {
        private IUnitOfWork _uow;

        private static string _child = "Parking";
        private static List<Parking> listParking = new List<Parking>();
        public ParkingService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        public async Task<List<Parking>> GetAll()
        {
             var dataSnapshot = await _uow.ParkingRepository.GetAllAsync(_child);
             if (listParking != null)
             {
                listParking.Clear();
             }
             foreach (var item in dataSnapshot)
             {
                listParking.Add(item.Object);
             }
             return listParking;
        }

        public async Task<List<Parking>> GetParkingByIdLocation(Guid idLocation)
        {
            if (listParking!=null)
            {
                listParking.Clear();
            }
            listParking = await GetAll();
            List<Parking> listP = new List<Parking>();
            if (listParking != null)
            {
                foreach (var item in listParking)
                {
                    if (item.IdLocation.Equals(idLocation))
                    {
                        listP.Add(item);
                    }
                }return listP;
            }return null;
        }

        public async Task<List<ParkingView>> GetParkingByKeyLocation(string? key)
        {
            if (listParking != null)
            {
                listParking.Clear();
            }
            List<ParkingView> listPV = new List<ParkingView>();
            List<Location> listLocation = new List<Location>();
            var dataSnapshotLocation = await _uow.LocationRepository.GetAllAsync("Location");
            if (dataSnapshotLocation != null)
            {
                foreach (var item in dataSnapshotLocation)
                {
                    listLocation.Add(item.Object);
                }
            }
            listParking = await GetAll();
            if (key!=null)
            {
                var rs= (from p in listParking join lo in listLocation on p.IdLocation equals lo.Id where lo.Name.ToLower().Contains(key.ToLower()) select p).ToList();
                var rs1 = (from p in listParking join lo in listLocation on p.IdLocation equals lo.Id where lo.Name.ToLower().Contains(key.ToLower()) select lo).ToList();
                for (int i = 0; i < rs.Count(); i++) {
                     listPV.Add(new ParkingView
                     {
                         Id = rs[i].Id,
                         IdLocation = rs[i].IdLocation,
                         IdOwner = rs[i].IdOwner,
                         Quantity = rs[i].Quantity,
                         Name = rs[i].Name,
                         Iframe = rs1[i].Iframe,
                         NameLocation = rs1[i].Name
                     });
                }
                return listPV;
            }
            else
            {
                var rs = (from p in listParking join lo in listLocation on p.IdLocation equals lo.Id  select p).ToList();
                var rs1 = (from p in listParking join lo in listLocation on p.IdLocation equals lo.Id select lo).ToList();
                for (int i = 0; i < rs.Count(); i++)
                {
                    listPV.Add(new ParkingView
                    {
                        Id = rs[i].Id,
                        IdLocation = rs[i].IdLocation,
                        IdOwner = rs[i].IdOwner,
                        Quantity = rs[i].Quantity,
                        Name = rs[i].Name,
                        Iframe = rs1[i].Iframe,
                        NameLocation = rs1[i].Name
                    });
                }
                return listPV;
            }
        }

        public async Task<List<ParkingView>> GetParkingById(Guid idParking)
        {
            if (listParking!=null)
            {
                listParking.Clear();
            }
            listParking = await GetAll();
            if (listParking!=null)
            {
                List<Location> listLocation = new List<Location>();
                List<ParkingView> listPV = new List<ParkingView>();
                var dataSnapshot = await _uow.LocationRepository.GetAllAsync("Location");
                foreach (var item in dataSnapshot)
                {
                    listLocation.Add(item.Object);
                }
                var rs = (from p in listParking join lo in listLocation on p.IdLocation equals lo.Id where p.Id.Equals(idParking) select p).ToList();
                var rs1= (from p in listParking join lo in listLocation on p.IdLocation equals lo.Id where p.Id.Equals(idParking) select lo).ToList();
                for (int i = 0; i < rs.Count(); i++)
                {
                    listPV.Add(new ParkingView
                    {
                        Id = rs[i].Id,
                        IdLocation = rs[i].IdLocation,
                        IdOwner = rs[i].IdOwner,
                        Quantity = rs[i].Quantity,
                        Name = rs[i].Name,
                        Iframe = rs1[i].Iframe,
                        NameLocation = rs1[i].Name
                    });
                }return listPV;
            }
            return null;
        }
    }
}
