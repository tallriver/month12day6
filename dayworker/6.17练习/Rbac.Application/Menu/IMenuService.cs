using Rbac.Entity;
using Rbac.Repository;
using System.Collections.Generic;

namespace Rbac.Application
{
    public interface IMenuService
    {
        List<MenuDto> GetAll();
        List<MenuListDto> GetList();
        bool AddMenu(Menu menu);
        bool DelMenu(int id);
        Menu GetMenuById(int id);


    }
}