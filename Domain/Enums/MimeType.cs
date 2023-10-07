using System.ComponentModel;

namespace Trainify.Me_Api.Domain.Enums
{
    public enum MimeType
    {
        [Description("image/jpeg")]
        ImageJpeg = 0,

        [Description("video/mp4")]
        VideoMp4 = 1,

        [Description("application/pdf")]
        ApplicationPdf = 2,

        [Description("video/x-msvideo")]
        VideoXmsvideo = 3,

        [Description("video/mpeg")]
        VideoMpeg = 4,

        [Description("video/webm")]
        VideoWebm = 5,

        [Description("video/x-matroska")]
        VideoXmatroska = 6,

        [Description("image/png")]
        ImagePng = 7
    }
}
