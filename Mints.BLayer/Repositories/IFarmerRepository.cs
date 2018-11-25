using Mints.DLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BUser = Mints.BLayer.Models.Identity.User;
using DUser = Mints.DLayer.Models.User;
using BFarmer = Mints.BLayer.Models.Farmer.Farmer;
using DFarmer = Mints.DLayer.Models.Farmer;
using BAnimal = Mints.BLayer.Models.Animal.Animal;
using System.Threading.Tasks;
using Mints.BLayer.Models.Common;

namespace Mints.BLayer.Repositories
{
    public interface IFarmerRepository: IGenericRepository<DFarmer>
    {
        Task<ResponseResult> RegisterFarmer(BUser user, BFarmer farmer);

        Task<ResponseResult> RegisterData(string userName, BFarmer farmer);        

        Task<BFarmer> FindByNameAsync(string user);
    }
}
