namespace Wam.Core.ErrorCodes;

public abstract class WamErrorCode
{

    public abstract string Code { get; }
    public virtual string Namespace { get; } = "Wam.ErrorCodes";
    public virtual string TranslationKey => $"{Namespace}.{Code}";

}