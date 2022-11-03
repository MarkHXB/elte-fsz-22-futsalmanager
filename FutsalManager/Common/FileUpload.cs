using System.IO;

namespace FutsalManager.Common
{
    public static class FileUpload
    {
        private static string GetTeamLogoName(string teamName)
        {
            if (string.IsNullOrEmpty(teamName)) return Guid.NewGuid().ToString();

            return teamName + Guid.NewGuid().ToString();
        }

        public static void UploadTeamLogo(Team team)
        {
            // if(team == null)
            //     throw new NullReferenceException($"The object {nameof(team)} is null.");
            //
            // if(team.FileModel == null)
            //     throw new NullReferenceException($"The object {nameof(team.FileModel)} is null.");

            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploads\teams\");

            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //get file extension
            var fileInfo = new FileInfo(team.FileModel.FileName);
            var fileName = FileUpload.GetTeamLogoName(team.Name) + fileInfo.Extension;

            var fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                 //team.FileModel.CopyToAsync(stream);
                team.FileModel.CopyTo(stream);
            }

            team.LogoPath = fileName;
        }
    }
}
