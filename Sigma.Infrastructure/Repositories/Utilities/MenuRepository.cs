using Dapper;
using Sigma.Application.DTOs.Utilities;
using Sigma.Application.Interfaces;
using Sigma.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Infrastructure.Repositories.Utilities
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DapperContext _context;

        public MenuRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<MenuDto>> GetMenuTreeAsync()
        {
            using var connection = _context.CreateConnection();

            // 1️⃣ Menus
            var menus = (await connection.QueryAsync<MenuDto>(
                @"SELECT 
                    menu_id        AS MenuId,
                    parent_menu_id AS ParentMenuId,
                    menu_key       AS MenuKey,
                    display_name   AS DisplayName,
                    route_path     AS RoutePath,
                    menu_type      AS MenuType,
                    icon_name      AS IconName,
                    display_order  AS DisplayOrder
                  FROM s_master.m_menu
                  WHERE del_status = false
                    AND is_visible = true
                  ORDER BY display_order"
            )).ToList();

            // 2️⃣ Actions
            var actions = (await connection.QueryAsync<dynamic>(
                @"SELECT 
                    menu_action_id,
                    menu_id,
                    action_key,
                    action_name
                  FROM s_master.m_menu_action
                  WHERE del_status = false"
            )).ToList();

            // 3️⃣ Attach actions
            foreach (var menu in menus)
            {
                menu.Actions = actions
                    .Where(a => a.menu_id == menu.MenuId)
                    .Select(a => new MenuActionDto
                    {
                        MenuActionId = a.menu_action_id,
                        ActionKey = a.action_key,
                        ActionName = a.action_name
                    }).ToList();
            }

            // 4️⃣ Build hierarchy
            var menuLookup = menus.ToDictionary(m => m.MenuId);

            foreach (var menu in menus)
            {
                if (menu.ParentMenuId.HasValue &&
                    menuLookup.ContainsKey(menu.ParentMenuId.Value))
                {
                    menuLookup[menu.ParentMenuId.Value]
                        .Children.Add(menu);
                }
            }

            // 5️⃣ Return root menus
            return menus
                .Where(m => m.ParentMenuId == null)
                .OrderBy(m => m.DisplayOrder)
                .ToList();
        }
    }
}
