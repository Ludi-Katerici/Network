﻿namespace FrontEnd.Infrastructure;

internal sealed class ErrorResponse
{
    public int StatusCode { get; set; }
    
    public string Message { get; set; } = string.Empty;
    
    public Dictionary<string, List<string>> Errors { get; set; } = new();
}