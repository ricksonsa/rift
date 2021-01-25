using System.Collections.Generic;
namespace rift.Dto
{
    public class ProfileInfoDto
    {
        public List<string> ActiveProfiles { get; set; }

        public ProfileInfoDto(List<string> activeProfiles)
        {
            ActiveProfiles = activeProfiles;

        }
    }
}
