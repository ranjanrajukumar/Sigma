using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;

namespace Sigma.API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class MSchoolController : ControllerBase
    {
        private readonly IMSchoolRepository _repository;

        public MSchoolController(IMSchoolRepository repository)
        {
            _repository = repository;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetSchools()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchool(long id)
        {
            var result = await _repository.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MSchoolDto dto)
        {
            byte[]? logoBytes = null;

            if (dto.Logo != null)
            {
                using var ms = new MemoryStream();
                await dto.Logo.CopyToAsync(ms);
                logoBytes = ms.ToArray();
            }

            var school = new MSchool
            {
                SchoolCode = dto.SchoolCode,
                SchoolName = dto.SchoolName,
                PrincipalName = dto.PrincipalName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Logo = logoBytes,
                LogoName = dto.Logo?.FileName,
                LogoType = dto.Logo?.ContentType,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                PostalCode = dto.PostalCode,
                AuthAdd = "ADMIN"
            };

            var id = await _repository.CreateAsync(school);

            return Ok(new { SchoolId = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromForm] UpdateSchoolDto dto)
        {
            byte[]? logoBytes = null;

            if (dto.Logo != null)
            {
                using var ms = new MemoryStream();
                await dto.Logo.CopyToAsync(ms);
                logoBytes = ms.ToArray();
            }

            var school = new MSchool
            {
                SchoolCode = dto.SchoolCode,
                SchoolName = dto.SchoolName,
                PrincipalName = dto.PrincipalName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Logo = logoBytes,
                LogoName = dto.Logo?.FileName,
                LogoType = dto.Logo?.ContentType,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                PostalCode = dto.PostalCode,
                AuthLstEdt = "ADMIN"
            };

            var result = await _repository.UpdateAsync(id, school);

            if (!result)
                return NotFound();

            return Ok("School Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _repository.DeleteAsync(id, "ADMIN");

            if (!result)
                return NotFound();

            return Ok("School Deleted Successfully");
        }

        [HttpPut("{id}/logo")]
        public async Task<IActionResult> UpdateLogo(long id, [FromForm] UpdateSchoolLogoDto dto)
        {
            if (dto.Logo == null || dto.Logo.Length == 0)
                return BadRequest("Logo file is required");

            using var memoryStream = new MemoryStream();
            await dto.Logo.CopyToAsync(memoryStream);

            var logoBytes = memoryStream.ToArray();
            var logoName = dto.Logo.FileName;
            var logoType = dto.Logo.ContentType;

            var result = await _repository.UpdateLogoAsync(
                id,
                logoBytes,
                logoName,
                logoType,
                dto.AuthLstEdt
            );

            if (!result)
                return NotFound();

            return Ok("Logo updated successfully");
        }
    }
}
