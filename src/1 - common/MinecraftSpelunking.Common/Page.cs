﻿namespace MinecraftSpelunking.Common
{
    public class Page<T>
    {
        public int Number { get; set; }
        public int Total { get; set; }
        public int Size { get; set; }
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    }
}
