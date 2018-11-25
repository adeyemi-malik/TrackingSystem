using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BAnimal = Mints.BLayer.Models.Animal.Animal;
using DAnimal = Mints.DLayer.Models.Animal;

namespace Mints.BLayer.Repositories
{
    public interface IAnimalRepository: IGenericRepository<DAnimal>
    {
        Task<bool> AddAnimals(BAnimal animal);

        Task<int> TotalAnimals();

        Task<IEnumerable<BAnimal>> GetAnimals();

        Task<BAnimal> GetAnimal(string id);

        Task<int> TotalAnimals(string user);

        Task<IEnumerable<BAnimal>> GetAnimals(string user);

        Task<BAnimal> GetAnimal(string user, string id);
    }
}
