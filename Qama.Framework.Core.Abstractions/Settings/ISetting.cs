namespace Qama.Framework.Core.Abstractions.Settings
{
    public interface ISetting<ISettingValue>
    {
        ISettingValue GetValue();
    }
}
