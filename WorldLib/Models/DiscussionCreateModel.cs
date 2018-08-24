using System;
using WorldLib.Services;

namespace WorldLib.Models
{
    public class DiscussionCreateModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public  Discussion Create()
        {
            var categryRep = new Repository<Category>();
            return new Discussion
            {
                Name = Name,
                CategoryId = CategoryId,
                Status = 0,
                DateTime = DateTime.Now,
                Description = Description
            };
        }
    }
}