using Qama.Framework.Core.Abstractions.Settings;

namespace Qama.Framework.Core.Abstractions.Persistence
{
    public class DbConnectionString : ISettingValue
    {
        public string ConnectionString { get; set; }
    }
}
