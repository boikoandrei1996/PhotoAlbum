using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using BLL.Entities;

namespace BLL.Interfaces.Async
{
    public interface IPhotoServiceAsync : IServiceAsync<BLLPhoto>
    {
        Task<ICollection<BLLPhoto>> GetAllByDateCreationAsync(DateTime dateCreation, CancellationToken? token = null);
        Task<ICollection<BLLPhoto>> GetUserPhotosAsync(int userId, CancellationToken? token = null);
        Task<ICollection<BLLPhoto>> GetPhotosByDescriptionAsync(string pattern, TimeSpan? timeOutPerMatch = null, CancellationToken? token = null);
    }
}
