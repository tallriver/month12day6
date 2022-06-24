using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rbac.Repository
{
    public class MenuRepository : IMenuRepository
    {
        public MenuRepository(MyDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public MyDbContext DbContext { get; }

        //获取菜单信息
        public List<Menu> GetAll()
        {
            return DbContext.Menu.ToList();
        }

        //菜单添加
        public bool Add(Menu menu)
        {
            menu.CreateTime = DateTime.Now;
            DbContext.Menu.Add(menu);
            return DbContext.SaveChanges() > 0;
        }

        //菜单的删除
        public bool Del(int id)
        {
            var list=DbContext.Menu.Find(id);
            DbContext.Menu.Remove(list);
            return DbContext.SaveChanges() > 0;
        }

        



    }
}
