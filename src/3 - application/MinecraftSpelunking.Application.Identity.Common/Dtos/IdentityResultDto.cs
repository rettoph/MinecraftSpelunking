using Microsoft.AspNetCore.Identity;

namespace MinecraftSpelunking.Application.Identity.Common.Dtos
{
    public class IdentityResultDto
    {
        public bool Succeeded { get; set; }
        public List<IdentityError> Errors { get; set; } = new List<IdentityError>();

        public bool Merge(IdentityResult identityResult)
        {
            this.Succeeded |= identityResult.Succeeded;
            this.Errors.AddRange(identityResult.Errors);

            return this.Succeeded;
        }
    }
}
