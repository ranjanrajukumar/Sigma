using Dapper;
using Sigma.Application.DTOs;
using Sigma.Application.DTOs.Academics;
using Sigma.Application.Interfaces.Academics;
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

        // ✅ Generate Enquiry Number
        private string GenerateEnquiryNo(long enquiryId)
        {
            var year = DateTime.UtcNow.Year;
            return $"ENQ-{year}-{enquiryId.ToString().PadLeft(5, '0')}";
        }

        // ✅ Use readonly (better than const)
        private readonly string EnquiryColumns = @"
            e.enquiry_id            AS EnquiryId,
            e.enquiry_no            AS EnquiryNo,
            e.enquiry_date          AS EnquiryDate,

            e.student_name          AS StudentName,
            e.student_mobile        AS StudentMobile,
            e.student_email         AS StudentEmail,

            e.parent_name           AS ParentName,
            e.parent_mobile         AS ParentMobile,
            e.parent_email          AS ParentEmail,

            e.previous_school       AS PreviousSchool,
            e.occupation            AS Occupation,

            e.address               AS Address,
            e.city                  AS City,

            e.district_id           AS DistrictId,
            d.district_name         AS DistrictName,

            e.state_id              AS StateId,
            s.state_name            AS StateName,

            e.pincode               AS Pincode,

            e.source                AS Source,
            e.priority              AS Priority,
            e.assigned_to           AS AssignedTo,
            e.followup_date         AS FollowupDate,
            e.notes                 AS Notes,

            e.auth_add              AS AuthAdd,
            e.auth_lst_edt          AS AuthLstEdt,
            e.auth_del              AS AuthDel,

            e.add_on_dt             AS AddOnDt,
            e.edit_on_dt            AS EditOnDt,
            e.del_on_dt             AS DelOnDt,
            e.del_status            AS DelStatus
        ";

        // ✅ GET ALL
        public async Task<IEnumerable<EnquiryResponseDto>> GetAllAsync()
        {
            var query = $@"
                SELECT {EnquiryColumns}
                FROM s_core.enquiry e
                LEFT JOIN s_master.m_district d ON e.district_id = d.district_id
                LEFT JOIN s_master.m_state s ON e.state_id = s.state_id
                WHERE e.del_status = false
                ORDER BY e.enquiry_id DESC";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<EnquiryResponseDto>(query);
        }

        // ✅ GET BY ID (nullable)
        public async Task<EnquiryResponseDto?> GetByIdAsync(long id)
        {
            var query = $@"
                SELECT {EnquiryColumns}
                FROM s_core.enquiry e
                LEFT JOIN s_master.m_district d ON e.district_id = d.district_id
                LEFT JOIN s_master.m_state s ON e.state_id = s.state_id
                WHERE e.enquiry_id = @Id
                AND e.del_status = false";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<EnquiryResponseDto>(query, new { Id = id });
        }

        // ✅ CREATE
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

            await connection.ExecuteAsync(
                @"UPDATE s_core.enquiry 
                  SET enquiry_no = @EnquiryNo 
                  WHERE enquiry_id = @EnquiryId",
                new { EnquiryNo = enquiryNo, EnquiryId = enquiryId });

            return enquiryId;
        }

        // ✅ UPDATE
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
            return await connection.ExecuteAsync(query, dto) > 0;
        }

        // ✅ DELETE (Soft)
        public async Task<bool> DeleteAsync(long id, string authDel)
        {
            var query = @"
                UPDATE s_core.enquiry
                SET del_status = true,
                    auth_del = @AuthDel,
                    del_on_dt = CURRENT_TIMESTAMP
                WHERE enquiry_id = @Id";

            using var connection = _context.CreateConnection();

            return await connection.ExecuteAsync(query, new
            {
                Id = id,
                AuthDel = authDel
            }) > 0;
        }
    }
}