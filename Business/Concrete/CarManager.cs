﻿using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _cars;

        public CarManager(ICarDal carDal)
        {
            _cars = carDal;
        }

        public List<Car> GetByCarID(int carID)
        {
            return _cars.GetAll(c => c.CarID == carID);
        }


        public void Add(Car car)
        {
            using (ReCarContext context = new ReCarContext())
            {
                var addedEntity = context.Entry(car);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(String.Format("{0,-12} | {1,-12} | {2,-13} | {3,-13} | {4,-13} | {5,-13} | {6,-15}", car.CarID,
                            car.CarName, car.BrandID, car.ColorID, car.ModelYear, car.DailyPrice, car.Description));
                Console.ResetColor();
            }
        }

        public List<Car> GetByCars()
        {
            return _cars.GetAll();
        }

        public List<Car> GetCarByBrandID(int brandID)
        {
            return _cars.GetAll(c => c.BrandID == brandID);
        }

        public List<CarDetailsDto> GetCarDetails()
        {
            return _cars.GetCarDetails();
        }

        public List<Car> GetCarByColorID(int colorID)
        {
            return _cars.GetAll(c => c.ColorID == colorID);
        }

        public List<Car> GetCarByModelYear(int date)
        {
            return _cars.GetAll(c => c.ModelYear == date);
        }

        public List<Car> GetCarByDailyPrice(decimal min, decimal max)
        {
            return _cars.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max);
        }

        public List<Car> GetCarByDescription(string carDescription)
        {
            return _cars.GetAll(c=> c.Description== carDescription);
        }

        public void Delete(Car car)
        {
            
            using (ReCarContext context = new ReCarContext())
            {
                var deletedEntity = context.Entry(car);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public void UpDate(Car car)
        {
            using (ReCarContext context = new ReCarContext())
            {
                var updatedEntity = context.Entry(car);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();

                Console.WriteLine(String.Format("{0,-12} | {1,-12} | {2,-13} | {3,-20} | {4,-15} | {5,-20} | {6,-20} ", car.CarID,
                            car.CarName, car.BrandID, car.ColorID, car.ModelYear, car.DailyPrice, car.Description));
            }
        }
    }
}
