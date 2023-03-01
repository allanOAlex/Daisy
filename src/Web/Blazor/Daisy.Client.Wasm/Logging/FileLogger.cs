using System;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;


namespace Daisy.Client.Wasm.Logging
{
    public class FileLogger : ILogger
    {
        private readonly string? path;
        private readonly IFileProvider fileProvider;

        public FileLogger(string? _Path)
        {
            path = _Path;
            fileProvider = new PhysicalFileProvider(Path.GetDirectoryName(path));
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            try
            {
                if (!IsEnabled(logLevel))
                {
                    return;
                }

                using (var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(formatter(state, exception));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
