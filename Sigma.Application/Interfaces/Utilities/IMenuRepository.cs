using Sigma.Application.DTOs.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces
{
    public interface IMenuRepository
    {
        Task<List<MenuDto>> GetMenuTreeAsync();
    }
}
