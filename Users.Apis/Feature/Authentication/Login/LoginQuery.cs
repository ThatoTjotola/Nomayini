﻿using MediatR;

namespace Users.Apis.Feature.Auth.Login;
public sealed record LoginQuery(string Email, string Password) : IRequest<LoginResponse>;

public sealed record LoginResponse(string Token);