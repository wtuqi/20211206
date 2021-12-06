using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integral_exchange.Models;
using System.Data;

public class DB:My_DB.DB //继承接口
    {
    //接口功能实现
    MyContext db = new MyContext();
    public static int _PageNum=-1;
    public static int _APageNum = 0;
    public static int inte = 0;
    public static string Username;
    public Achievement GetAchievement(int? id)
    {
        return db.Achievement.SingleOrDefault(s => s.ID == id);
    }

    public prize GetPrize(int? id)
    {
        return db.prize.SingleOrDefault(s => s.ProductID == id);
    }

    public WantList GetWant(string id)
    {
        return db.WantList.SingleOrDefault(s => s.UserID == id);
    }

    public IQueryable<prize> Pri_FindAll()
    {
        return db.prize;
    }

    public IQueryable<Achievement> Ach_FindAllAch()
    {
        return db.Achievement;
    }

    public IQueryable<WantList> Want_FindAll()
    {
        return db.WantList;
    }

    public IQueryable<Achievement> Ach_FindBykey(string UserID, string AchievementID, string AchievementName, string AchieveTime)
    {
        return null;
    }

    public void Ach_Add(Achievement Ach)
    {
        db.Achievement.Add(Ach);
    }
    public void Ach_Del(Achievement Ach)
    {
        db.Achievement.Remove(Ach);
    }
    public void Save()
    {
        db.SaveChanges();
    }

    public void Pri_Add(prize Pri)
    {
        db.prize.Add(Pri);
    }

    public void Pri_Del(prize Pri)
    {
        db.prize.Remove(Pri);
    }

    public void Want_Add(WantList Want)
    {
        db.WantList.Add(Want);
    }

   public void Want_Del(WantList Want) 
    {
        db.WantList.Remove(Want);
    }

    //页码
    public int PpageNum 
    {
        get 
        {
            return _PageNum;
        }
        set
        {
            if (value < 0) { value = 0; } 
            if (value >db.prize.Count()-1) 
            {
                value = db.prize.Count()-1; 
            }
            _PageNum = value;
        }
    }

    //分组页码
    public int ApageNum
    {
        get
        {
            return _APageNum;
        }
        set
        {
            if (value < 0) { value = 0; }
            if (value >= db.Achievement.Select(m => m.UserID).Distinct().ToList().Count)
            {
                value = db.Achievement.Select(m => m.UserID).Distinct().ToList().Count-1;
            }
            _APageNum = value;
        }
    }

    public List<SelectListItem> listItem = new List<SelectListItem>
        {
            new SelectListItem{Text="Getting Started",Value="Getting Started"},
            new SelectListItem{Text="Presenter",Value="Presenter"},
            new SelectListItem{ Text= "Leader", Value="Leader"},
            new SelectListItem{ Text= "Loyalty", Value="Loyalty"},
            new SelectListItem{ Text= "Business Star", Value="Business Star"},
            new SelectListItem{ Text= "Good Practitioner", Value="Good Practitioner"},
            new SelectListItem{ Text= "Good Lecture", Value="Good Lecture"},
            new SelectListItem{ Text= "Champion", Value="Champion"},
        };

    public IEnumerable<Achievement> ToTB(string UserName, int F = 0)
    {
        var TB = db.Achievement.ToList();
        List<string> L = db.Achievement.Select(m => m.UserID).Distinct().ToList();
        int count = L.Count() - 1;
        for (int i = 0; i <= count; i++)
        {
            System.Diagnostics.Debug.Print(L[i]);
        }
        if (Username == "admin")
        {
            if (UserName != null & Username == "admin")
            {
                TB = (from m in db.Achievement.ToList()
                      where m.UserID == UserName
                      select m).ToList();
                return TB.ToList();
            }
            else
            {
                switch (F)
                {
                    case 0:
                        ApageNum = 0;
                        break;
                    case 1:
                        ApageNum -= 1;
                        break;
                    case 2:
                        ApageNum += 1;
                        break;
                }
            }
            return TB.Where(m => m.UserID == L[ApageNum]);
        }
        return TB.Where(m => m.UserID == Username);
    }

   public bool calc(int point,string Username)
    {
        bool flag = false;
        try
        {
           int integral = Convert.ToInt32(db.Achievement.Where(m => m.UserID == Username).Select(u => u.score).Sum());
           inte= integral / 5;
        }
        catch { }
        if (inte >= point) { flag = true; }
        return flag;
    }

    public string PrizeSel(int? id,int Sel=0,string Username=null)
    {
       string msg = null;
       prize prize = db.prize.Find(id);
        int MaxNum = db.prize.Count();
        switch (Sel)
        {
            case 0:
                PpageNum = 0;
                break;
            case 1:
                PpageNum -= 1;
                break;

            case 2:
                PpageNum += 1;
                break;
            default:
                if (prize != null)
                {
                    try
                    {
                        WantList want = new WantList();
                        want.UserID = Username;
                        want.ProductName = prize.ProductName;
                        want.ProductID = prize.ProductID;
                        Want_Add(want);
                        Save();
                        msg = "Congratulations on your successful exchange!!!";
                    }
                    catch(Exception ex)
                    {
                        msg = ex.ToString();
                    }
                }
                break;
        }
        return msg;
    }


}
