using System;
using System.Collections.Generic;

using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IPhotoRepository : IRepository<DALPhoto>
    {
        IEnumerable<DALPhoto> GetAllByDateCreation(DateTime dateCreation);
        IEnumerable<DALPhoto> GetAllByUser(int userId);
    }
}
