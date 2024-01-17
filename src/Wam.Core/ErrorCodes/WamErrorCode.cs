namespace Wam.Core.ErrorCodes;

public abstract class WamErrorCode
{
    public abstract string Code { get; }
    public virtual string RootNamespace { get; } = "Wam.ErrorCodes";
    public virtual string TranslationKey => $"{RootNamespace}.{Code}";

    public static WamErrorCode Unknown() => new Unknown();
}

public class Unknown : WamErrorCode
{
    public override string Code => "Unknown";
}