namespace IWantApp.Services;

public class UserInfoService(IHttpContextAccessor http)
{
    private readonly IHttpContextAccessor _http = http;

    public string Id => _http.HttpContext!.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value ?? string.Empty;

    public string Name => _http.HttpContext!.User.Claims.FirstOrDefault(c => c.Type.Equals(nameof(ClaimsUser.Name)))!.Value ?? string.Empty;

    public string Cpf => _http.HttpContext!.User.Claims.FirstOrDefault(c => c.Type.Equals(nameof(ClaimsUser.Cpf)))!.Value ?? string.Empty;

    public string EmployeeCode => _http.HttpContext!.User.Claims.FirstOrDefault(c => c.Type.Equals(nameof(ClaimsUser.EmployeeCode)))!.Value ?? string.Empty;

    public bool IsClient => _http.HttpContext!.User.Claims.Any(c => c.Type.Equals(nameof(ClaimsUser.Cpf)));

    public bool IsEmployee => _http.HttpContext!.User.Claims.Any(c => c.Type.Equals(nameof(ClaimsUser.EmployeeCode)));
}
