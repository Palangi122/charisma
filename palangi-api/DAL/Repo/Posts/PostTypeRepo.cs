using Contract.repos.Posts;
using DAL.Context;
using DomainModel.Posts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo.Posts
{
    public class PostTypeRepo : IPostTypeRepo
    {
        private readonly OrderContext context;

        public PostTypeRepo(OrderContext context)
        {
            this.context = context;
        }
        public async Task<PostType> GetById(Guid postTypeId)
        {
            return await context.postTypes.FirstOrDefaultAsync(pt => pt.PostTypeId == postTypeId);
        }
    }
}
