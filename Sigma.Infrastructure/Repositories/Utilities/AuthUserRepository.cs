using Dapper;
using Sigma.Application.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Infrastructure.Repositories
{
    public class AuthUserRepository : IAuthUserRepository
    {
        private readonly DapperContext _context;

        public AuthUserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<AuthUser?> GetByEmailAsync(string email)
        {
            var sql = @"
        SELECT
            user_id AS UserId,
            user_name AS UserName,
            user_password AS UserPassword,
            role_id AS RoleId,
            is_admin AS IsAdmin,
            email,
            mobile_no AS MobileNo,
            full_name AS FullName,
            last_login AS LastLogin,
            login_attempt AS LoginAttempt,
            is_logged AS IsLogged,
            status
        FROM auth.tbl_user
        WHERE email = @Email
          AND del_status = false";

            using var conn = _context.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<AuthUser>(
                sql, new { Email = email });
        }

        public async Task UpdateLoginSuccessAsync(long userId, string ipAddress)
        {
            var sql = @"
        UPDATE auth.tbl_user
        SET last_login = NOW(),
            login_attempt = 0,
            is_logged = true,
            ip_address = @Ip
        WHERE user_id = @UserId";

            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync(sql, new { UserId = userId, Ip = ipAddress });
        }

        public async Task UpdateLoginFailAsync(long userId)
        {
            var sql = @"
        UPDATE auth.tbl_user
        SET login_attempt = login_attempt + 1
        WHERE user_id = @UserId";

            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync(sql, new { UserId = userId });
        }
    }
}
