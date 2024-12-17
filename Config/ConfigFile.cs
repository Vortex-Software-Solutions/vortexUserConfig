namespace vortexUserConfig.Config;

public enum ConfigPermission
{
    Roles,
    Permission
}

public class ConfigUserInit
{
    public ConfigPermission configPermission { get; init; }
}