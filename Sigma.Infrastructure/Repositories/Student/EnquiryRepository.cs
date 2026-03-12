using Dapper;
using Sigma.Application.DTOs.Academics;
using Sigma.Application.Interfaces.Academics;
using Sigma.Domain.Entities.Academics;
using Sigma.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sigma.Infrastructure.Repositories.Academics
{
    public class EnquiryRepository : IEnquiryRepository
    {
        private readonly DapperContext _context;

        public EnquiryRepository(DapperContext context)
        {
            _context = context;
        }

        // Generate Enquiry Number
        private string GenerateEnquiryNo(long enquiryId)
        {
            var year = DateTime.UtcNow.Year;
            return $"ENQ-{year}-{enquiryId.ToString().PadLeft(5, '0')}";
        }

        private const string EnquiryColumns = @"
            enquiry_id            AS EnquiryId,
            enquiry_no            AS EnquiryNo,
            enquiry_date          AS EnquiryDate,

            student_name          AS StudentName,
            student_mobile        AS StudentMobile,
            student_email         AS StudentEmail,

            parent_name           AS ParentName,
            parent_mobile         AS ParentMobile,
            parent_email          AS ParentEmail,

            previous_school       AS PreviousSchool,
            occupation            AS Occupation,

            address               AS Address,
            city                  AS City,
            district_id           AS DistrictId,
            state_id              AS StateId,
            pincode               AS Pincode,

            source                AS Source,
            priority              AS Priority,
            assigned_to           AS AssignedTo,
            followup_date         AS FollowupDate,
            notes                 AS Notes,

            auth_add              AS AuthAdd,
            auth_lst_edt          AS AuthLstEdt,
            auth_del              AS AuthDel,

            add_on_dt             AS AddOnDt,
            edit_on_dt            AS EditOnDt,
            del_on_dt             AS DelOnDt,
            del_status            AS DelStatus
        ";

        public async Task<IEnumerable<Enquiry>> GetAllAsync()
        {
            var query = $@"
                SELECT {EnquiryColumns}
                FROM s_core.enquiry
                WHERE del_status = false
                ORDER BY enquiry_id DESC";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Enquiry>(query);
        }

        public async Task<Enquiry> GetByIdAsync(long id)
        {
            var query = $@"
                SELECT {EnquiryColumns}
                FROM s_core.enquiry
                WHERE enquiry_id = @Id
                AND del_status = false";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Enquiry>(query, new { Id = id });
        }

        public async Task<long> CreateAsync(CreateEnquiryDto dto)
        {
            using var connection = _context.CreateConnection();

            var insertQuery = @"
                INSERT INTO s_core.enquiry
                (
                    student_name, student_mobile, student_email,
                    parent_name, parent_mobile, parent_email,
                    previous_school, occupation,
                    address, city, district_id, state_id, pincode,
                    source, priority, assigned_to, followup_date, notes,
                    auth_add
                )
                VALUES
                (
                    @StudentName,@StudentMobile,@StudentEmail,
                    @ParentName,@ParentMobile,@ParentEmail,
                    @PreviousSchool,@Occupation,
                    @Address,@City,@DistrictId,@StateId,@Pincode,
                    @Source,@Priority,@AssignedTo,@FollowupDate,@Notes,
                    @AuthAdd
                )
                RETURNING enquiry_id";

            var enquiryId = await connection.ExecuteScalarAsync<long>(insertQuery, dto);

            var enquiryNo = GenerateEnquiryNo(enquiryId);

            var updateQuery = @"
                UPDATE s_core.enquiry
                SET enquiry_no = @EnquiryNo
                WHERE enquiry_id = @EnquiryId";

            await connection.ExecuteAsync(updateQuery, new
            {
                EnquiryNo = enquiryNo,
                EnquiryId = enquiryId
            });

            return enquiryId;
        }

        public async Task<bool> UpdateAsync(UpdateEnquiryDto dto)
        {
            var query = @"
                UPDATE s_core.enquiry SET
                    student_name=@StudentName,
                    student_mobile=@StudentMobile,
                    student_email=@StudentEmail,
                    parent_name=@ParentName,
                    parent_mobile=@ParentMobile,
                    parent_email=@ParentEmail,
                    previous_school=@PreviousSchool,
                    occupation=@Occupation,
                    address=@Address,
                    city=@City,
                    district_id=@DistrictId,
                    state_id=@StateId,
                    pincode=@Pincode,
                    source=@Source,
                    priority=@Priority,
                    assigned_to=@AssignedTo,
                    followup_date=@FollowupDate,
                    notes=@Notes,
                    auth_lst_edt=@AuthLstEdt,
                    edit_on_dt=CURRENT_TIMESTAMP
                WHERE enquiry_id=@EnquiryId";

            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, dto);

            return result > 0;
        }

        public async Task<bool> DeleteAsync(long id, string authDel)
        {
            var query = @"
                UPDATE s_core.enquiry
                SET del_status = true,
                    auth_del = @AuthDel,
                    del_on_dt = CURRENT_TIMESTAMP
                WHERE enquiry_id = @Id";

            using var connection = _context.CreateConnection();

            var result = await connection.ExecuteAsync(query, new
            {
                Id = id,
                AuthDel = authDel
            });

            return result > 0;
        }
    }
}