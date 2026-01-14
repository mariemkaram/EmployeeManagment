namespace EmployeeManagment.Services
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audince { get; set; }
        public int LifeTime { get; set; }
        public string SiginingKey { get; set; }
    }
}
