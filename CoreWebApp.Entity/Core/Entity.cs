using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreWebApp.Model
{
    public class Entity
    {
        [Key] //主键 
        [MaxLength(100)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public string Id { get; set; }
        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    public class Entity_Enabled:Entity
    {
        public bool Enabled { get; set; }
    }

    public class Entity_Create_Update_Enabled : Entity
    {
        [Required]
        public string CreateBy { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool Enabled { get; set; }

        public Entity_Create_Update_Enabled()
        {
            CreateTime = DateTime.Now;
            Enabled = true;
        }
    }
}
