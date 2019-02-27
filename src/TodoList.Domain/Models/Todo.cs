using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Domain.Models
{
    /// <summary>
    /// 任务
    /// </summary>
    public class Todo : Entity
    {
        [Required]
        [Display(Name = "任务")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Display(Name = "任务分类")]
        public TodoType TodoType { get; set; }

        [Display(Name = "标签")]
        [MaxLength(255)]
        public string Label { get; set; }

        [Display(Name = "状态")]
        public TodoStatus Status { get; set; }

        [Display(Name = "截至时间")]
        public DateTime OffTime { get; set; }
    }

    public enum TodoStatus
    {
        代办,
        重要,
        已完成
    }

}
