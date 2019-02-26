using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TodoList.Domain.Models
{
    /// <summary>
    /// 任务步骤
    /// </summary>
    public class TodoItem : Entity
    {
        [Required]
        public Guid TodoId { get; set; }

        [Required]
        [Display(Name = "任务条目")]
        public string Todo { get; set; }
    }
}
