namespace Application.Auth.Dtos;

public record RegisterRequest(string Name, string Email, string Password, int Role);
public record AuthResponse(Guid UserId, string Name, string Email, string AccessToken, string RefreshToken);
public record LoginRequest(string Email, string Password);
public record RefreshRequest(string RefreshToken);
public record ChangePasswordRequest(string OldPassword, string NewPassword);