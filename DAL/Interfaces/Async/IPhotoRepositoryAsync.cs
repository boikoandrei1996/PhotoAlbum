using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using DAL.Entities;

namespace DAL.Interfaces.Async
{
    public interface IPhotoRepositoryAsync : IRepositoryAsync<DALPhoto>
    {
        Task<IEnumerable<DALPhoto>> GetAllByDateCreationAsync(DateTime dateCreation, CancellationToken? token = null);
        Task<IEnumerable<DALPhoto>> GetAllByUserAsync(int userId, CancellationToken? token = null);
    }
}
