using MediatR;

namespace Application.Features.Auth.Commands;

public record RegisterUserCommand(
    string PhoneNumber,
    string Email,
    string FirstName,
    string LastName,
    string Password,
    UserRole Role) : IRequest<RegisterUserResponse>;

public record RegisterUserResponse(
    Guid UserId,
    string PhoneNumber,
    string Message);

public record VerifyOtpCommand(
    string PhoneNumber,
    string OtpCode) : IRequest<VerifyOtpResponse>;

public record VerifyOtpResponse(
    Guid UserId,
    bool Verified,
    string Message);

public record LoginUserCommand(
    string PhoneNumber,
    string Password,
    string? DeviceFingerprint,
    string? IpAddress) : IRequest<LoginUserResponse>;

public record LoginUserResponse(
    Guid UserId,
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt);

public record RefreshTokenCommand(
    string RefreshToken) : IRequest<RefreshTokenResponse>;

public record RefreshTokenResponse(
    string AccessToken,
    string NewRefreshToken,
    DateTime AccessTokenExpiresAt);

public record LogoutUserCommand(
    Guid UserId) : IRequest<Unit>;

public record ChangePasswordCommand(
    Guid UserId,
    string CurrentPassword,
    string NewPassword) : IRequest<Unit>;

public record RequestPasswordResetCommand(
    string PhoneNumber) : IRequest<Unit>;

public record ResetPasswordCommand(
    string PhoneNumber,
    string OtpCode,
    string NewPassword) : IRequest<Unit>;

public record RevokeAllTokensCommand(
    Guid UserId) : IRequest<Unit>;

using Domain.Enums;
