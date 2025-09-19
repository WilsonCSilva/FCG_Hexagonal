namespace FCG.Middlewares
{
    public class BaseLogger<T>
    {
        protected readonly ILogger<T> _logger;

        public BaseLogger(ILogger<T> logger)
        {
            _logger = logger;
        }

        public virtual void LogInformation(string message)
        {
            _logger?.LogInformation($"{DateTime.Now}: {message}");
        }

        public virtual void LogWarning(string message)
        {
            _logger?.LogWarning($"{DateTime.Now}: {message}");
        }

        public virtual void LogError(string message)
        {
            _logger?.LogError($"{DateTime.Now}: {message}");
        } 

        public virtual void LogDebug(string message) 
        {
            _logger?.LogDebug($"{DateTime.Now}: {message}");
        }
    }
}
