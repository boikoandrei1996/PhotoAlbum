using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IPhotoService : IService<BLLPhoto>
    {
        ICollection<BLLPhoto> GetAllByDateCreation(DateTime dateCreation);
        ICollection<BLLPhoto> GetUserPhotos(int userId);
        ICollection<BLLPhoto> GetPhotosByDescription(string pattern, TimeSpan? timeOutPerMatch = null);
    }
}
