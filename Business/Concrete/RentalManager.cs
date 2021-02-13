﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService

    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {

            var result = _rentalDal.GetAll(r=> r.CarID== rental.CarID && r.ReturnDate<rental.ReturnDate);
            if (result.Count>0)
            {
                return new ErrorResult(Massages.ReturnDate);
            }
            _rentalDal.Add(rental);

            return new SuccessResult(Massages.RentalAdded);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);

            return new SuccessResult(Massages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetByRental()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetByRentID(int rentID)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.ID == rentID));
        }

        public IDataResult<List<Rental>> GetRentByCarID(int carID)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarID == carID));
        }

        public IDataResult<List<Rental>> GetRentByCustomerID(int customerID)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CustomerID == customerID));
        }

        public IDataResult<List<Rental>> GetRentDate(DateTime date)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.RentDate == date));
        }

        public IDataResult<List<Rental>> GetReturnDate(DateTime returnDate)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.RentDate == returnDate));
        }

        public IResult UpDate(Rental rental)
        {
            _rentalDal.Update(rental);

            return new SuccessResult(Massages.RentalUpdated);
        }
        
    }
}