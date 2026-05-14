namespace Interview.API.Models.Responses
{
    public class UserResponse
    {
        public string ACPD_SID { get; set; } = null!;
        public string? ACPD_Cname { get; set; }
        public string? ACPD_Ename { get; set; }
        public string? ACPD_Sname { get; set; }
        public string? ACPD_Email { get; set; }
        public byte? ACPD_Status { get; set; }
        public bool? ACPD_Stop { get; set; }
        public string? ACPD_StopMemo { get; set; }
        public string? ACPD_LoginID { get; set; }
        public string? ACPD_LoginPWD { get; set; }
        public string? ACPD_Memo { get; set; }
        public DateTime? ACPD_NowDateTime { get; set; }
        public string? ACPD_NowID { get; set; }
        public DateTime? ACPD_UPDDateTime { get; set; }
        public string? ACPD_UPDID { get; set; }

        public static UserResponse FromEntity(MyOffice_ACPD entity)
        {
            return new UserResponse
            {
                ACPD_SID = entity.ACPD_SID,
                ACPD_Cname = entity.ACPD_Cname,
                ACPD_Ename = entity.ACPD_Ename,
                ACPD_Sname = entity.ACPD_Sname,
                ACPD_Email = entity.ACPD_Email,
                ACPD_Status = entity.ACPD_Status,
                ACPD_Stop = entity.ACPD_Stop,
                ACPD_StopMemo = entity.ACPD_StopMemo,
                ACPD_LoginID = entity.ACPD_LoginID,
                ACPD_LoginPWD = entity.ACPD_LoginPWD,
                ACPD_Memo = entity.ACPD_Memo,
                ACPD_NowDateTime = entity.ACPD_NowDateTime,
                ACPD_NowID = entity.ACPD_NowID,
                ACPD_UPDDateTime = entity.ACPD_UPDDateTime,
                ACPD_UPDID = entity.ACPD_UPDID,
            };
        }
    }
}
