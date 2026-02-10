using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Utilities
{
    public class MenuDto
    {
        public long MenuId { get; set; }
        public long? ParentMenuId { get; set; }
        public string MenuKey { get; set; }
        public string DisplayName { get; set; }
        public string RoutePath { get; set; }
        public string MenuType { get; set; }
        public string IconName { get; set; }
        public int DisplayOrder { get; set; }

        public List<MenuActionDto> Actions { get; set; } = new();
        public List<MenuDto> Children { get; set; } = new();
    }
}
