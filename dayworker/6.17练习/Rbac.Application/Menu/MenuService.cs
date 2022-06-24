using Rbac.Entity;
using Rbac.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rbac.Application
{
    public class MenuService : IMenuService
    {
        public MenuService(IMenuRepository menuRepository)
        {
            MenuRepository = menuRepository;
        }

        public IMenuRepository MenuRepository { get; }

        public List<MenuDto> GetAll()
        {
            var list=MenuRepository.GetAll();
            List<MenuDto> result=new List<MenuDto>();

            var menudto = list.Where(m => m.ParentId == 0).Select(m => new MenuDto
            {
                MenuId = m.MenuId,
                MenuName = m.MenuName,
                LinkUrl = m.LinkUrl,
            }).ToList();

            GetNodes(menudto);

            return menudto;
        }


        private void GetNodes(List<MenuDto> menus)
        {
            var list = MenuRepository.GetAll();

            foreach (var item in menus)
            {
                var obj = list.Where(m => m.ParentId == item.MenuId).Select(m => new MenuDto
                {
                    MenuId = m.MenuId,
                    MenuName = m.MenuName,
                    LinkUrl = m.LinkUrl,
                }).ToList();

                item.Children.AddRange(obj);

                GetNodes(obj);
            }
        }

        public List<MenuListDto> GetList()
        {
            var list = MenuRepository.GetAll();
            List<MenuListDto> result = new List<MenuListDto>();

            var menudto = list.Where(m => m.ParentId == 0).Select(m => new MenuListDto
            {
                value = m.MenuId,
                label = m.MenuName,
            }).ToList();

            GetNodesList(menudto);

            return menudto;
        }

        private void GetNodesList(List<MenuListDto> menus)
        {
            var list = MenuRepository.GetAll();

            foreach (var item in menus)
            {
                var obj = list.Where(m => m.ParentId == item.value).Select(m => new MenuListDto
                {
                    value = m.MenuId,
                    label = m.MenuName,
                }).ToList();

                item.children.AddRange(obj);

                GetNodesList(obj);
            }
        }

        /// <summary>
        /// 菜单添加
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool AddMenu(Menu menu)
        {
            return MenuRepository.Add(menu);
        }

        /// <summary>
        /// 菜单删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelMenu(int id)
        {
            return MenuRepository.Del(id);
        }


        /// <summary>
        /// 根据id获取菜单等级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Menu GetMenuById(int id)
        {
            var list = MenuRepository.GetAll();
            var obj = list.Where(m => m.ParentId == id).FirstOrDefault();
            return obj;
        }
    }
}
