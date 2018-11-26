using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreWebApp.Model
{
    public class Entity
    {
        [Key] //主键 
        [MaxLength(100)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public string Id { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
