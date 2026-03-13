using Dapper;
using Sigma.Application.Common.Constants;
using Sigma.Application.Common.Responses;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;
using Sigma.Infrastructure.Persistence;

namespace Sigma.Infrastructure.Repositories.Master
{
    public class StudentDocumentRepository : IStudentDocumentRepository
    {
        private readonly DapperContext _context;

        public StudentDocumentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<long>> CreateStudentDocument(StudentDocument entity)
        {
            try
            {
                using var connection = _context.CreateConnection();

                string checkQuery = @"SELECT COUNT(1)
                                      FROM s_master.m_student_document
                                      WHERE (LOWER(document_code) = LOWER(@DocumentCode)
                                      OR LOWER(document_name) = LOWER(@DocumentName))
                                      AND del_status = FALSE";

                var exists = await connection.ExecuteScalarAsync<int>(checkQuery, new
                {
                    entity.DocumentCode,
                    entity.DocumentName
                });

                if (exists > 0)
                {
                    return new ApiResponse<long>
                    {
                        Status = false,
                        Message = ApiMessages.DuplicateRecord,
                        Data = 0
                    };
                }

                string query = @"INSERT INTO s_master.m_student_document
                                (document_name, document_code, description,
                                 is_mandatory, is_active, auth_add)
                                VALUES
                                (@DocumentName, @DocumentCode, @Description,
                                 @IsMandatory, @IsActive, @AuthAdd)
                                RETURNING student_document_id";

                var id = await connection.ExecuteScalarAsync<long>(query, entity);

                return new ApiResponse<long>
                {
                    Status = true,
                    Message = ApiMessages.Created,
                    Data = id
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<long>
                {
                    Status = false,
                    Message = ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<IEnumerable<StudentDocument>> GetAllStudentDocuments()
        {
            using var connection = _context.CreateConnection();

            string query = @"SELECT 
                                student_document_id AS StudentDocumentId,
                                document_name AS DocumentName,
                                document_code AS DocumentCode,
                                description AS Description,
                                is_mandatory AS IsMandatory,
                                is_active AS IsActive,
                                auth_add AS AuthAdd,
                                auth_lst_edt AS AuthLstEdt,
                                auth_del AS AuthDel,
                                add_on_dt AS AddOnDt,
                                edit_on_dt AS EditOnDt,
                                del_on_dt AS DelOnDt,
                                del_status AS DelStatus
                             FROM s_master.m_student_document
                             WHERE del_status = FALSE
                             ORDER BY document_name";

            return await connection.QueryAsync<StudentDocument>(query);
        }

        public async Task<StudentDocument?> GetStudentDocumentById(long id)
        {
            using var connection = _context.CreateConnection();

            string query = @"SELECT 
                                student_document_id AS StudentDocumentId,
                                document_name AS DocumentName,
                                document_code AS DocumentCode,
                                description AS Description,
                                is_mandatory AS IsMandatory,
                                is_active AS IsActive,
                                auth_add AS AuthAdd,
                                auth_lst_edt AS AuthLstEdt,
                                auth_del AS AuthDel,
                                add_on_dt AS AddOnDt,
                                edit_on_dt AS EditOnDt,
                                del_on_dt AS DelOnDt,
                                del_status AS DelStatus
                             FROM s_master.m_student_document
                             WHERE student_document_id = @Id
                             AND del_status = FALSE";

            return await connection.QueryFirstOrDefaultAsync<StudentDocument>(query, new { Id = id });
        }

        public async Task<bool> UpdateStudentDocument(StudentDocument entity)
        {
            using var connection = _context.CreateConnection();

            string query = @"UPDATE s_master.m_student_document
                             SET document_name = @DocumentName,
                                 document_code = @DocumentCode,
                                 description = @Description,
                                 is_mandatory = @IsMandatory,
                                 is_active = @IsActive,
                                 auth_lst_edt = @AuthLstEdt,
                                 edit_on_dt = NOW()
                             WHERE student_document_id = @StudentDocumentId
                             AND del_status = FALSE";

            var result = await connection.ExecuteAsync(query, entity);

            return result > 0;
        }

        public async Task<bool> DeleteStudentDocument(long id)
        {
            using var connection = _context.CreateConnection();

            string query = @"UPDATE s_master.m_student_document
                             SET del_status = TRUE,
                                 del_on_dt = NOW()
                             WHERE student_document_id = @Id";

            var result = await connection.ExecuteAsync(query, new { Id = id });

            return result > 0;
        }
    }
}