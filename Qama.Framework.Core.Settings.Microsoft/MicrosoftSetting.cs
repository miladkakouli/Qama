using System;
using Microsoft.Extensions.Options;
using Qama.Framework.Core.Abstractions.Settings;

namespace Qama.Framework.Core.Settings.Microsoft
{
    public class MicrosoftSetting<T> : ISetting<T>
        where T : class, ISettingValue
    {
        public MicrosoftSetting(IOptions<T> value)
        {
            Value = value;
        }

        private IOptions<T> Value { get; set; }

        public T GetValue() => this.Value.Value;
    }
}

