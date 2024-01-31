namespace Wam.Core.DataTransferObjects;

public record ExceptionDto(
    string ErrorCode,
    string TranslationKey,
    string Message
    );