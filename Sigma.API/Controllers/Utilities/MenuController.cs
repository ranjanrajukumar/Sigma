using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sigma.Application.Interfaces;

namespace Sigma.API.Controllers.Utilities
{
    [ApiController]
    [Route("api/master/menus")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _repository;

        public MenuController(IMenuRepository repository)
        {
            _repository = repository;
        }

        // GET MENU LIST
        [HttpGet]
        public async Task<IActionResult> GetMenuList()
        {
            var menus = await _repository.GetMenuTreeAsync();
            return Ok(menus);
        }
    }
}
