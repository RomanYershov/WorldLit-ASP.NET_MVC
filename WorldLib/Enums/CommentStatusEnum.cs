

using System.ComponentModel.DataAnnotations;

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