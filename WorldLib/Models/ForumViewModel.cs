using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldLib.Services;

namespace WorldLib.Models
{
    public class ForumViewModel
    {
        public IEnumerable<Discussion> Discussions { get; private set; }
        public IEnumerable<Category> Categories { get; private set; }

        public static ForumViewModel GetModel()
        {
            var discussionsRep = new Repository<Discussion>();
            var categoriesRep = new Repository<Category>();
            return new ForumViewModel
            {
                Discussions = discussionsRep.GetWithInclude(x => x.Id == x.Id, x => x.Category),
                Categories = categoriesRep.Get()
            };
        }
    }
}