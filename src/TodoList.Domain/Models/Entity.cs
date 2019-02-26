using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TodoList.Domain.Models
{
    public abstract class Entity
    {
        [Key]
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 是否有效（表示是否被删除）
        /// </summary>
        public virtual bool IsActive { get; set; }

        public virtual Guid Creator { get; set; }

        [DataType(DataType.DateTime)]
        public virtual DateTime CreateDate { get; set; }

        public virtual Guid Updater { get; set; }

        [DataType(DataType.DateTime)]
        public virtual DateTime UpdateDate { get; set; }

        public void AddModel(Guid userId)
        {
            Id = Guid.NewGuid();
            Creator = userId;
            Updater = userId;
            CreateDate = DateTime.Now;
            UpdateDate = CreateDate;
            IsActive = true;
        }

        public void UpdateModel(Guid userId)
        {
            Updater = userId;
            UpdateDate = DateTime.Now;
        }
    }
}
