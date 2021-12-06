using System;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using TableAttribute = System.ComponentModel.DataAnnotations.Schema.TableAttribute;

namespace My_DB
{
    public partial class MyContext : DbContext
    {
        public MyContext()
                : base("DefaultConnection")
        {
            Database.SetInitializer<MyContext>(new CreateDatabaseIfNotExists<MyContext>());
        }
        public virtual DbSet<Achievement> Achievement { get; set; }
        public virtual DbSet<prize> prize { get; set; }
        public virtual DbSet<WantList> WantList { get; set; }
    }

    [Table("Achievement")]
    public class Achievement
    {
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 3)]
        [MaxLength(50, ErrorMessage = "Character length exceeds the requirement")]
        public string UserID { get; set; }//用户名
        [Key]
        public int AchievementID { get; set; }//成就编号
        [MaxLength(50, ErrorMessage = "Character length exceeds the requirement")]
        [Required]
        public string AchievementName { get; set; }//成就名称
        public float score { get; set; }// 得分
        [MaxLength(50, ErrorMessage = "Character length exceeds the requirement")]
        public string AchieveTime { get; set; } //时间
        [MaxLength(50, ErrorMessage = "Character length exceeds the requirement")]
        public string Description { get; set; }//对于该成就的描述
        [DefaultValue(false)]
        public bool flag { get; set; } //兑换标志
    }

    [Table("prize")]
    public class prize
    {
        [Key]
        public int ProductID { get; set; }//产品编号
        [MaxLength(50, ErrorMessage = "Character length exceeds the requirement")]
        [Required]
        public string ProductName { get; set; }//产品名称
        public float Price { get; set; }//价格
        public string Pic { get; set; } //产品图片
        public float Integral { get; set; } //所需积分
    }
    [Table("WantList")]
    public class WantList
    {
        [MaxLength(50, ErrorMessage = "Character length exceeds the requirement")]
        [Key]
        public string UserID { get; set; } //用户名
        public int ProductID { get; set; } //产品编号
        [MaxLength(50, ErrorMessage = "Character length exceeds the requirement")]
        public string ProductName { get; set; } //产品名称
    }

}