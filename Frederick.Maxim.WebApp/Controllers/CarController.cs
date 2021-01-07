using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Frederick.Maxim.Data;

namespace Frederick.Maxim.WebApp.Controller
{
    [RoutePrefix("api/car")]
    public class CarController : ApiController
    {
        private readonly IRepository<Car> _carRepository;

        // using dependency injection to inject the repo into controller
        public CarController (IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        [Route("NewestVehicles")]
        public IEnumerable<Car> GetNewestVehicles()
        {
            int newestYear = _carRepository.GetAll().Distinct<Car>().Max(c => c.Year);
            return _carRepository.GetByCondition(c => c.Year == newestYear);
        }

        [Route("AlphabetizedList")]
        public IEnumerable<Car> GetAlphabetizedList()
        {
            return _carRepository.GetAll().OrderBy(c => c.Model);
        }

        [Route("OrderByPrice")]
        public IEnumerable<Car> GetCarOrderedByPrice()
        {
            return _carRepository.GetAll().OrderBy(c => c.Price);
        }

        // I'm assuming TCC rating stands for how good the value is for a car
        [Route("BestValue")]
        public IEnumerable<Car> GetCarByBestValue()
        {
            double rating = (double)_carRepository.GetAll().Distinct<Car>().Max(c => c.TCC_Rating);
            return _carRepository.GetByCondition(c => c.TCC_Rating == rating);
        }

        [Route("FuelConsumption/{id}/{miles}")]
        public double GetFuelConsumptionOfCar(int id, double miles)
        {
            double mpg = (double)_carRepository.GetByCondition(c => c.Id == id).FirstOrDefault().Hwy_MPG;
            return miles / mpg;
        }

        [Route("Random")]
        public Car GetRandomCar()
        {
            int count = _carRepository.Count();
            Random rnd = new Random();
            return _carRepository.GetByCondition(c => c.Id == rnd.Next(1, count + 1)).FirstOrDefault();
        }

        [Route("AvgMpg/{year}")]
        public double GetAverageMpgByYear(int year)
        {
            return (double)_carRepository.GetByCondition(c => c.Year == year).Select(c => c.Hwy_MPG).Average();
        }
    }
}