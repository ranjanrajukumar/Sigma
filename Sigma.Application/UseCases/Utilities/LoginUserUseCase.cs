using Sigma.Application.DTOs;
using Sigma.Application.Interfaces.Services;
using Sigma.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Sigma.Shared.Helpers;

namespace Sigma.Application.UseCases.Utilities
{
    public class LoginUserUseCase
    {
        private readonly IAuthUserRepository _repository;
        private readonly IJwtTokenService _jwtService;

        public LoginUserUseCase(
            IAuthUserRepository repository,
            IJwtTokenService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto?> ExecuteAsync(
            LoginRequestDto request,
            string ipAddress)
        {
            var user = await _repository.GetByEmailAsync(request.Email);

            if (user == null || user.Status != "ACTIVE")
                return null;

            if (!PasswordHelper.Verify(request.Password, user.UserPassword))
            {
                await _repository.UpdateLoginFailAsync(user.UserId);
                return null;
            }

            await _repository.UpdateLoginSuccessAsync(user.UserId, ipAddress);

            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Token = token
            };
        }
    }
}
