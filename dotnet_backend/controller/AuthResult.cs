using EnumStatus;

namespace controller
{
    public class AuthResult {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
}