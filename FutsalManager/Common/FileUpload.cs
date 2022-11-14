using System.IO;

namespace FutsalManager.Common
{
    public static class FileUpload
    {
        private static readonly string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","uploads","teams","logos");
        private static string GetTeamLogoName(string teamName)
        {
            if (string.IsNullOrWhiteSpace(teamName)) return Guid.NewGuid().ToString();

            return string.Concat(teamName, Guid.NewGuid().ToString());
        }
        
        public static void UploadTeamLogo(Team team)
        {
            //create folder if not exist
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
            
            if (!string.IsNullOrEmpty(team.LogoPath))
            {
                foreach (var file in Directory.GetFiles(root))
                {
                    if (!team.Name.Contains(file)) continue;
                    
                    var fileToDelete = Path.Combine(root,file);
                    File.Delete(fileToDelete);
                }
            }

            if (team.FileModel == null) return;
            
            //get file extension
            var fileInfo = new FileInfo(team.FileModel.FileName);
            var fileName = GetTeamLogoName(team.Name) + fileInfo.Extension;

            var fileNameWithPath = Path.Combine(root, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                team.FileModel.CopyTo(stream);
            }

            team.LogoPath = fileName;
        }
        
    }
}