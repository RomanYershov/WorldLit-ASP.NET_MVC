using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorldLib.Enums
{
    public enum AdminFunkcionalEnum
    {
        [Display(Name = "Пользователи", Description = "Редактирование пользователей")]
        Пользователи,
        [Display(Name = "Афоризмы", Description = "Редактирование ,добавление (афоризмы)")]
        Афоризмы,
        [Display(Name = "Форум", Description = "Редактирование ,добавление (форум)")]
        Форум,
        [Display(Name = "Комментарии", Description = "Редактирование ,добавление (комментарии)")]
        Комментарии
    }
}