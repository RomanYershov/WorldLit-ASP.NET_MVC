using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorldLib.Enums
{
    public enum CommentStatusEnum
    {
        [Display(Name = "На проверке")]
        Moderation,
        [Display(Name = "Опубликован")]
        Published,
        [Display(Name = "Удален")]
        Deleted,
    }
}