using Mints.DLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BUser = Mints.BLayer.Models.Identity.User;
using DUser = Mints.DLayer.Models.User;
using BFarmer = Mints.BLayer.Models.Farmer.Farmer;
using DFarmer = Mints.DLayer.Models.Farmer;
using BAnimal = Mints.BLayer.Models.Animal.Animal;
using DAnimal = Mints.DLayer.Models.Animal;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mints.BLayer.Exceptions;
using System.Linq;
using Mints.BLayer.Helpers;

namespace Mints.BLayer.Repositories
{
    public class AnimalRepository: GenericRepository<DAnimal>, IAnimalRepository
    {
        public AnimalRepository(Context dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task<int> TotalAnimals()
        {           
            var count = await Query().CountAsync();
            return count;
        }

        public async Task<bool> AddAnimals(BAnimal animal)
        {
            var dAnimal = animal.ToDbAnimal();
            await InsertAsync(dAnimal);
            animal.Id = dAnimal.Id;
            return true;
        }

        public async Task<int> TotalAnimals(string user)
        {
            var farmer = await _dbContext.Farmers
               .Include(u => u.User)
               .Include(f => f.FarmerAnimals)
               .SingleOrDefaultAsync(m => m.User.Email.Equals(user, StringComparison.OrdinalIgnoreCase));
            if (farmer == null)
            {
                throw new UserReferenceNotFoundException("The user reference does not exist for a valid user");
            }

            var count = farmer.FarmerAnimals.Count();
            return count;
        }

        public async Task<IEnumerable<BAnimal>> GetAnimals()
        {
            var animals =  await Task.Run(() =>
                Query().Select(a => a.ToAnimal()
                ));
            return animals;
        }

        public async Task<IEnumerable<BAnimal>> GetAnimals(string user)
        {
            var farmer = await _dbContext.Farmers
               .Include(u => u.User)
               .Include(f => f.FarmerAnimals)
               .ThenInclude(f => f.Animal)
               .SingleOrDefaultAsync(m => m.User.Email.Equals(user, StringComparison.OrdinalIgnoreCase));
            if (farmer == null)
            {
                throw new UserReferenceNotFoundException("The user reference does not exist for a valid user");
            }

            var animals = farmer.FarmerAnimals.Select(f => f.Animal.ToAnimal());
            return animals;
        }

        public async Task<BAnimal> GetAnimal(string id)
        {
            
            var animal = await Query().SingleOrDefaultAsync(a => a.Tag.Equals(id, StringComparison.OrdinalIgnoreCase));
            if (animal == null)
            {
                throw new AnimalReferenceNotFoundException("The animal reference does not exist for a valid animal");
            }
            return animal.ToAnimal();
        }

        public async Task<BAnimal> GetAnimal(string user, string id)
        {
            var farmer = await _dbContext.Farmers
               .Include(u => u.User)
               .Include(f => f.FarmerAnimals)
               .ThenInclude(f => f.Animal)
               .SingleOrDefaultAsync(m => m.User.Email.Equals(user, StringComparison.OrdinalIgnoreCase));
            if (farmer == null)
            {
                throw new UserReferenceNotFoundException("The user reference does not exist for a valid user");
            }

            var animal = farmer.FarmerAnimals.SingleOrDefault(f => f.Animal.Tag.Equals(id, StringComparison.OrdinalIgnoreCase));
            if (animal == null)
            {
                throw new AnimalReferenceNotFoundException("The animal reference does not exist for a valid animal");
            }
            return animal.Animal.ToAnimal();
        }
    }

}
