using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral_exchange.Models
{
   interface IDB
    {
        Achievement GetAchievement(int? id);
        prize GetPrize(int? id);
        WantList GetWant(string id);
        IQueryable<prize> Pri_FindAll();
        IQueryable<Achievement> Ach_FindAllAch();
        IQueryable<WantList> Want_FindAll();
        IQueryable<Achievement> Ach_FindBykey(string UserID, string AchievementID, string AchievementName, string AchieveTime);
        void Ach_Add(Achievement Ach);
        void Ach_Del(Achievement Ach);
        void Pri_Add(prize Pri);
        void Pri_Del(prize Pri);
        void Want_Add(WantList Want);
        void Want_Del(WantList Want);
        void Save();
        int pageNum {get; set;}
        int ApageNum{ get; set; }
        IEnumerable<Achievement> ToTB(string UserName, int F = 0);
    }
}
