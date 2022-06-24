using Rbac.Entity;
using System.Collections.Generic;

namespace Rbac.Repository
{
    public interface IMenuRepository
    {
        List<Menu> GetAll();
        bool Add(Menu menu);
        bool Del(int id);
    }
}