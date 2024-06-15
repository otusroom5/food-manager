﻿using System;

namespace FoodUserAuth.WebApi.Contracts;

internal abstract class ResponseBase
{
    public string Message { get; set; }

    public static ResponseBase CreateFailure()
    {
        return Create("Something wrong. Please contact administrator");
    }
    public static ResponseBase CreateSuccess()
    {
        return Create("Success");
    }


    public static ResponseBase Create(string message)
    {
        return new SimpleResponse()
        {
            Message = message
        };
    }

    public static ResponseBase Create(Exception exception)
    {
        return new SimpleResponse()
        {
            Message = exception.Message
        };
    }

    private sealed class SimpleResponse: ResponseBase
    {
    }
}
