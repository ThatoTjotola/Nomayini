﻿namespace Users.Apis;
public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message) { }
}
