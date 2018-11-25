using Mints.BLayer.Models.Identity;
using Mints.DLayer.Models;
using BUser = Mints.BLayer.Models.Identity.User;
using DUser = Mints.DLayer.Models.User;

namespace Mints.BLayer.Helpers
{
    public static class UserHelper
    {
        public static BUser ToUser (this DUser dataModel) => new BUser
        {
            Id = dataModel.Id,
            Email = dataModel.Email,
            UserName = dataModel.UserName,
            PhoneNumber = dataModel.Phone,
            PasswordHash = dataModel.PasswordHash,
            HashSalt = dataModel.HashSalt,
            DateCreated = dataModel.DateCreated
        };



        public static DUser ToDbUser (this BUser user) => new DUser
        {
            Id = user.Id,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            HashSalt = user.HashSalt,
            Phone = user.PhoneNumber,
            UserName = user.UserName
        };
    }
}
