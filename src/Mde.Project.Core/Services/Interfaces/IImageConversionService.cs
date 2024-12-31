namespace Mde.Project.Core.Services.Interfaces
{
    public interface IImageConversionService
    {
        byte[] ConvertBase64ToByteArray(string base64String);
        Stream ConvertBase64ToStream(string base64String);
        string ConvertStreamToBase64(Stream imageStream);
    }
}
