using Mints.BLayer.Models.Common;
using Mints.DLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BUser = Mints.BLayer.Models.Identity.User;
using DUser = Mints.DLayer.Models.User;
using BFarmer = Mints.BLayer.Models.Farmer.Farmer;
using DFarmer = Mints.DLayer.Models.Farmer;
using BAnimal = Mints.BLayer.Models.Animal.Animal;
using Mints.BLayer.Helpers;
using Mints.BLayer.Exceptions;

namespace Mints.BLayer.Repositories
{
    public class FarmerRepository : GenericRepository<DFarmer>, IFarmerRepository
    {
        public FarmerRepository(Context dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<BFarmer> FindByNameAsync(string user)
        {
            var farmer = await _dbContext.Farmers
               .Include(u => u.User)
               .SingleOrDefaultAsync(m => m.User.Email.Equals(user, StringComparison.OrdinalIgnoreCase));
            if (farmer == null)
            {
                throw new UserReferenceNotFoundException("The user reference does not exist for a valid user");
            }         
            return farmer.ToFarmer();
        }

        public async Task<ResponseResult> RegisterFarmer(BUser user, BFarmer farmer)
        {
            var response = new ResponseResult();
            if (await _dbContext.Users.AnyAsync(u => u.UserName == user.UserName) || await Query().AnyAsync(m => m.Email == farmer.Email))
            {
                response.Status = Status.Fail;
                response.Data.Add("username", $"username {user.UserName} already exist");
                return response;
            }

            var farmerEntity = farmer.ToDbFarmer();
            farmerEntity.DateCreated = DateTime.Now;
            farmerEntity.DateUpdated = DateTime.Now;
            var userEntity = user.ToDbUser();
            userEntity.DateCreated = DateTime.Now;
            userEntity.DateUpdated = DateTime.Now;
            farmerEntity.User = userEntity;
            await _dbContext.Farmers.AddAsync(farmerEntity);
            await _dbContext.SaveChangesAsync();
            response.Status = Status.Success;
            response.Data.Add("info", "farmer successfully created");
            return response;
        }

        public async Task<ResponseResult> RegisterData(string userName, BFarmer farmer)
        {
            var response = new ResponseResult();

            if (await Query().AnyAsync(m => m.Email.Equals(farmer.Email, StringComparison.OrdinalIgnoreCase)))
            {
                response.Status = Status.Fail;
                response.Data.Add("email", $"email {farmer.Email} already exist");
                return response;
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                throw new UserReferenceNotFoundException("The user reference does not exist");
            }

            var farmerEntity = farmer.ToDbFarmer();
            farmerEntity.DateCreated = DateTime.Now;
            farmerEntity.DateUpdated = DateTime.Now;
            user.Farmer = farmerEntity;
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            farmer.Id = farmerEntity.Id;
            response.Status = Status.Success;
            response.Data.Add("info", "farmer successfully created");
            return response;
        }             
    }
}
