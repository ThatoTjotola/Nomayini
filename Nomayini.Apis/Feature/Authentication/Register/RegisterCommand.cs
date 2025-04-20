using MediatR;

namespace Nomayini.Apis.Feature.Auth.Register;
public sealed record RegisterCommand(string Email, string Password) : IRequest<AuthResponse>;
public sealed record AuthResponse(string Token);


