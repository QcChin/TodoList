using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Domain.Models
{
    /// <summary>
    /// 任务分类
    /// </summary>
    public class TodoType : Entity
    {
        [Required]
        [Display(Name = "任务分类")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }
    }
}
