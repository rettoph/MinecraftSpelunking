﻿namespace MinecraftSpelunking.Presentation.Common
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }

    public class Response<T> : Response
    {
        public T? Model { get; set; }
    }
}
