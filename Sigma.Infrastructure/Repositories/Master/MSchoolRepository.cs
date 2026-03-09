using Dapper;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;
using Sigma.Infrastructure.Persistence;
using System.Data;

namespace Sigma.Infrastructure.Repositories.Master
{
    public class MSchoolRepository : IMSchoolRepository
    {
        private readonly DapperContext _context;

        public MSchoolRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<MSchool>> GetAllAsync()
        {
            var sql = @"SELECT 
                        school_id AS SchoolId,
                        school_code AS SchoolCode,
                        school_name AS SchoolName,
                        principal_name AS PrincipalName,
                        email AS Email,
                        phone_number AS PhoneNumber,
                        logo AS Logo,
                        logo_name AS LogoName,
                        logo_type AS LogoType,
                        address_line1 AS AddressLine1,
                        address_line2 AS AddressLine2,
                        city AS City,
                        state AS State,
                        country AS Country,
                        postal_code AS PostalCode,
                        is_active AS IsActive,
                        auth_add AS AuthAdd,
                        auth_lst_edit AS AuthLstEdt,
                        auth_del AS AuthDel,
                        add_on_dt AS AddOnDt,
                        edit_on_dt AS EditOnDt,
                        del_on_dt AS DelOnDt,
                        del_status AS DelStatus
                    FROM s_master.m_school
                    WHERE del_status = false
                    ORDER BY school_id";

            using var conn = _context.CreateConnection();
            var result = await conn.QueryAsync<MSchool>(sql);

            return result.ToList();
        }

        public async Task<MSchool?> GetByIdAsync(long id)
        {
            var sql = @"SELECT 
                        school_id AS SchoolId,
                        school_code AS SchoolCode,
                        school_name AS SchoolName,
                        principal_name AS PrincipalName,
                        email AS Email,
                        phone_number AS PhoneNumber,
                        logo AS Logo,
                        logo_name AS LogoName,
                        logo_type AS LogoType,
                        address_line1 AS AddressLine1,
                        address_line2 AS AddressLine2,
                        city AS City,
                        state AS State,
                        country AS Country,
                        postal_code AS PostalCode,
                        is_active AS IsActive,
                        auth_add AS AuthAdd,
                        auth_lst_edit AS AuthLstEdt,
                        auth_del AS AuthDel,
                        add_on_dt AS AddOnDt,
                        edit_on_dt AS EditOnDt,
                        del_on_dt AS DelOnDt,
                        del_status AS DelStatus
                    FROM s_master.m_school
                    WHERE school_id = @Id
                    AND del_status = false";

            using var conn = _context.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<MSchool>(sql, new { Id = id });
        }

        public async Task<long> CreateAsync(MSchool school)
        {
            var sql = @"INSERT INTO s_master.m_school
                        (
                            school_code,
                            school_name,
                            principal_name,
                            email,
                            phone_number,
                            logo,
                            logo_name,
                            logo_type,
                            address_line1,
                            address_line2,
                            city,
                            state,
                            country,
                            postal_code,
                            is_active,
                            auth_add,
                            add_on_dt
                        )
                        VALUES
                        (
                            @SchoolCode,
                            @SchoolName,
                            @PrincipalName,
                            @Email,
                            @PhoneNumber,
                            @Logo,
                            @LogoName,
                            @LogoType,
                            @AddressLine1,
                            @AddressLine2,
                            @City,
                            @State,
                            @Country,
                            @PostalCode,
                            true,
                            @AuthAdd,
                            NOW()
                        )
                        RETURNING school_id";

            using var conn = _context.CreateConnection();
            return await conn.ExecuteScalarAsync<long>(sql, school);
        }

        public async Task<bool> UpdateAsync(long id, MSchool school)
        {
            var sql = @"UPDATE s_master.m_school
                        SET
                            school_code = @SchoolCode,
                            school_name = @SchoolName,
                            principal_name = @PrincipalName,
                            email = @Email,
                            phone_number = @PhoneNumber,
                            logo = @Logo,
                            logo_name = @LogoName,
                            logo_type = @LogoType,
                            address_line1 = @AddressLine1,
                            address_line2 = @AddressLine2,
                            city = @City,
                            state = @State,
                            country = @Country,
                            postal_code = @PostalCode,
                            auth_lst_edit = @AuthLstEdt,
                            edit_on_dt = NOW()
                        WHERE school_id = @Id
                        AND del_status = false";

            using var conn = _context.CreateConnection();

            var rows = await conn.ExecuteAsync(sql, new
            {
                Id = id,
                school.SchoolCode,
                school.SchoolName,
                school.PrincipalName,
                school.Email,
                school.PhoneNumber,
                school.Logo,
                school.LogoName,
                school.LogoType,
                school.AddressLine1,
                school.AddressLine2,
                school.City,
                school.State,
                school.Country,
                school.PostalCode,
                school.AuthLstEdt
            });

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(long id, string authDel)
        {
            var sql = @"UPDATE s_master.m_school
                        SET
                            del_status = true,
                            auth_del = @AuthDel,
                            del_on_dt = NOW()
                        WHERE school_id = @Id
                        AND del_status = false";

            using var conn = _context.CreateConnection();

            var rows = await conn.ExecuteAsync(sql, new
            {
                Id = id,
                AuthDel = authDel
            });

            return rows > 0;
        }


        public async Task<bool> UpdateLogoAsync(long id, byte[] logo, string logoName, string logoType, string authLstEdt)
        {
            var sql = @"UPDATE s_master.m_school
                SET
                    logo = @Logo,
                    logo_name = @LogoName,
                    logo_type = @LogoType,
                    auth_lst_edit = @AuthLstEdt,
                    edit_on_dt = NOW()
                WHERE school_id = @Id
                AND del_status = false";

            using var conn = _context.CreateConnection();

            var rows = await conn.ExecuteAsync(sql, new
            {
                Id = id,
                Logo = logo,
                LogoName = logoName,
                LogoType = logoType,
                AuthLstEdt = authLstEdt
            });

            return rows > 0;
        }
    }
}