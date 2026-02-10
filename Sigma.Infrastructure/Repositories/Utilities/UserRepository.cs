using Dapper;
using Sigma.Application.Interfaces;
using Sigma.Domain.Entities.Utilities;
using Sigma.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        // POST
        public async Task<long> CreateAsync(User user)
        {
            var sql = @"
                INSERT INTO auth.tbl_user
                (user_name, user_password, master_id, role_id, is_admin,
                 category, email, mobile_no, full_name, user_type,
                 status, add_on_dt)
                VALUES
                (@UserName, @UserPassword, @MasterId, @RoleId, @IsAdmin,
                 @Category, @Email, @MobileNo, @FullName, @UserType,
                 'ACTIVE', NOW())
                RETURNING user_id;
            ";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<long>(sql, user);
        }

        // GET ALL
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var sql = @"SELECT * FROM auth.tbl_user WHERE del_status = false";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<User>(sql);
        }

        // GET BY ID
        public async Task<User> GetByIdAsync(long userId)
        {
            var sql = @"SELECT * FROM auth.tbl_user
                        WHERE user_id = @UserId AND del_status = false";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                sql, new { UserId = userId });
        }

        // UPDATE
        public async Task<bool> UpdateAsync(User user)
        {
            var sql = @"
                UPDATE auth.tbl_user
                SET user_name = @UserName,
                    master_id = @MasterId,
                    role_id = @RoleId,
                    is_admin = @IsAdmin,
                    category = @Category,
                    mobile_no = @MobileNo,
                    full_name = @FullName,
                    user_type = @UserType,
                    status = @Status,
                    edit_on_dt = NOW()
                WHERE user_id = @UserId;
            ";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(sql, user) > 0;
        }

        // DELETE (SOFT DELETE)
        public async Task<bool> DeleteAsync(long userId)
        {
            var sql = @"
                UPDATE auth.tbl_user
                SET del_status = true,
                    del_on_dt = NOW()
                WHERE user_id = @UserId;
            ";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(sql, new { UserId = userId }) > 0;
        }
    }
}
