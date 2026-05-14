namespace Interview.API.Controllers
{
    [ApiController]
    [Route("api/myofficeacpd")]
    public class MyOfficeAcpdController(BackendExamHub_Context _dbContext) : GenericController
    {
        private readonly BackendExamHub_Context dbContext = _dbContext;
        private readonly DbSet<MyOffice_ACPD> acpdSet = _dbContext.Set<MyOffice_ACPD>();
        private readonly DbSet<MyOffice_ExcuteionLog> executionLogSet = _dbContext.Set<MyOffice_ExcuteionLog>();

        [HttpGet("")]
        [ProducesResponseType(typeof(List<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseFailure), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await acpdSet.ToListAsync();
                var response = result.Select(UserResponse.FromEntity).ToList();
                return GenericContent((null, response));
            }
            catch (Exception ex)
            {
                await LogErrorAsync(nameof(GetAll), ex);
                return GenericContent((new BaseFailure
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = "系統發生錯誤，請聯繫系統管理員",
                    Exception = ex,
                }, (object?)null));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseFailure), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseFailure), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseFailure), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                if (id.Length > 20)
                {
                    return GenericContent((new BaseFailure
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = "id 長度不可超過 20 碼",
                    }, (object?)null));
                }

                var result = await acpdSet.SingleOrDefaultAsync(user => user.ACPD_SID == id);
                if (result is null)
                {
                    return GenericContent((new BaseFailure
                    {
                        Code = HttpStatusCode.NotFound,
                        Message = "查無此筆資料紀錄",
                    }, (object?)null));
                }
                var response = UserResponse.FromEntity(result);
                return GenericContent((null, response));
            }
            catch (InvalidOperationException ex)
            {
                await LogErrorAsync(nameof(GetById), ex);
                return GenericContent((new BaseFailure
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = "系統發生錯誤，請聯繫系統管理員",
                }, (object?)null));
            }
            catch (Exception ex)
            {
                await LogErrorAsync(nameof(GetById), ex);
                return GenericContent((new BaseFailure
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = "系統發生錯誤，請聯繫系統管理員",
                    Exception = ex,
                }, (object?)null));
            }
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseFailure), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseFailure), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add([FromBody] AddUserRequest request)
        {
            try
            {
                var returnSid = new OutputParameter<string>();
                await dbContext.Procedures.NEWSIDAsync("MyOffice_ACPD", returnSid);

                var entity = new MyOffice_ACPD
                {
                    ACPD_SID = returnSid.Value,
                    ACPD_Cname = request.ACPD_Cname,
                    ACPD_Ename = request.ACPD_Ename,
                    ACPD_Sname = request.ACPD_Sname,
                    ACPD_Email = request.ACPD_Email,
                    ACPD_Status = request.ACPD_Status,
                    ACPD_Stop = request.ACPD_Stop,
                    ACPD_StopMemo = request.ACPD_StopMemo,
                    ACPD_LoginID = request.ACPD_LoginID,
                    ACPD_LoginPWD = request.ACPD_LoginPWD,
                    ACPD_Memo = request.ACPD_Memo,
                };

                acpdSet.Add(entity);
                await dbContext.SaveChangesAsync();

                var response = UserResponse.FromEntity(entity);
                return GenericContent((null, response));
            }
            catch (Exception ex)
            {
                await LogErrorAsync(nameof(Add), ex);
                return GenericContent((new BaseFailure
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = "系統發生錯誤，請聯繫系統管理員",
                    Exception = ex,
                }, (object?)null));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseFailure), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseFailure), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseFailure), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Edit(string id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                if (id.Length > 20)
                {
                    return GenericContent((new BaseFailure
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = "id 長度不可超過 20 碼",
                    }, (object?)null));
                }

                var existing = await acpdSet.SingleOrDefaultAsync(x => x.ACPD_SID == id);
                if (existing is null)
                {
                    return GenericContent((new BaseFailure
                    {
                        Code = HttpStatusCode.NotFound,
                        Message = "查無此筆資料紀錄",
                    }, (object?)null));
                }

                existing.ACPD_Cname = request.ACPD_Cname;
                existing.ACPD_Ename = request.ACPD_Ename;
                existing.ACPD_Sname = request.ACPD_Sname;
                existing.ACPD_Email = request.ACPD_Email;
                existing.ACPD_Status = request.ACPD_Status;
                existing.ACPD_Stop = request.ACPD_Stop;
                existing.ACPD_StopMemo = request.ACPD_StopMemo;
                existing.ACPD_LoginID = request.ACPD_LoginID;
                existing.ACPD_LoginPWD = request.ACPD_LoginPWD;
                existing.ACPD_Memo = request.ACPD_Memo;
                existing.ACPD_UPDDateTime = request.ACPD_UPDDateTime;
                existing.ACPD_UPDID = request.ACPD_UPDID;

                await dbContext.SaveChangesAsync();

                var response = UserResponse.FromEntity(existing);
                return GenericContent((null, response));
            }
            catch (Exception ex)
            {
                await LogErrorAsync(nameof(Edit), ex);
                return GenericContent((new BaseFailure
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = "系統發生錯誤，請聯繫系統管理員",
                    Exception = ex,
                }, (object?)null));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(BaseFailure), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseFailure), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseFailure), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Remove(string id)
        {
            try
            {
                if (id.Length > 20)
                {
                    return GenericContent((new BaseFailure
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = "id 長度不可超過 20 碼",
                    }, (object?)null));
                }

                var existing = await acpdSet.SingleOrDefaultAsync(x => x.ACPD_SID == id);
                if (existing is null)
                {
                    return GenericContent((new BaseFailure
                    {
                        Code = HttpStatusCode.NotFound,
                        Message = "查無此筆資料紀錄",
                    }, (object?)null));
                }

                acpdSet.Remove(existing);
                await dbContext.SaveChangesAsync();

                return GenericContent((null, (object?)null), _ => NoContent());
            }
            catch (Exception ex)
            {
                await LogErrorAsync(nameof(Remove), ex);
                return GenericContent((new BaseFailure
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = "系統發生錯誤，請聯繫系統管理員",
                    Exception = ex,
                }, (object?)null));
            }
        }

        private async Task LogErrorAsync(string actionName, Exception ex)
        {
            try
            {
                var outputParam = new OutputParameter<string>();
                await dbContext.Procedures.usp_AddLogAsync(
                    0,
                    nameof(MyOfficeAcpdController),
                    Guid.NewGuid(),
                    actionName,
                    ex.ToString(),
                    outputParam
                );
            }
            catch
            {
                // Logging failure should not mask the original error
            }
        }
    }
}
