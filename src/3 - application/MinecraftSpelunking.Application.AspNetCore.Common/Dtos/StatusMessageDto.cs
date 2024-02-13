using MinecraftSpelunking.Application.AspNetCore.Common.Enums;

namespace MinecraftSpelunking.Application.AspNetCore.Common.Dtos
{
    public class StatusMessageDto
    {
        public required StatusMessageTypeEnum Type { get; init; }
        public required string Value { get; init; }

        public string Class => this.Type switch
        {
            StatusMessageTypeEnum.Secondary => "alert-primary",
            StatusMessageTypeEnum.Success => "alert-success",
            StatusMessageTypeEnum.Error => "alert-danger",
            StatusMessageTypeEnum.Warning => "alert-warning",
            StatusMessageTypeEnum.Information => "alert-info",
            StatusMessageTypeEnum.Light => "alert-light",
            StatusMessageTypeEnum.Dark => "alert-dark",
            _ => "alert-primary",
        };

        public static StatusMessageDto Success(string value) => new StatusMessageDto() { Type = StatusMessageTypeEnum.Success, Value = value };
        public static StatusMessageDto Error(string value) => new StatusMessageDto() { Type = StatusMessageTypeEnum.Error, Value = value };
        public static StatusMessageDto Warning(string value) => new StatusMessageDto() { Type = StatusMessageTypeEnum.Warning, Value = value };
    }
}
