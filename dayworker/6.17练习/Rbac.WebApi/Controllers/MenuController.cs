using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Application;
using Rbac.Entity;
using System.Collections.Generic;

//认证
using Microsoft.AspNetCore.Authentication;

//授权
using Microsoft.AspNetCore.Authorization;

namespace Rbac.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        public MenuController(IMenuService menu)
        {
            Menu = menu;
        }

        public IMenuService Menu { get; }

        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<MenuDto> GetAll()
        {
            return Menu.GetAll();
        }

        /// <summary>
        /// 获取菜单下拉框数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<MenuListDto> GetList()
        {
            return Menu.GetList();
        }

        /// <summary>
        /// 添加菜单信息
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddMenu(Menu menu)
        {
            return Menu.AddMenu(menu);
        }

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool DelMenu(int id)
        {
            return Menu.DelMenu(id);
        }

        [HttpGet]
        public Menu GetMenuById(int id)
        {
            return Menu.GetMenuById(id);
        }
    }
}
