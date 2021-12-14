using DomainModel.Posts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contract.repos.Posts
{
    public interface IPostTypeRepo
    {
        Task<PostType> GetById(Guid postTypeId);
    }
}
