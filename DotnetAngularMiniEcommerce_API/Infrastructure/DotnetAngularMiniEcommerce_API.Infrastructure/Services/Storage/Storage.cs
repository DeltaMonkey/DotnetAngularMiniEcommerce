using DotnetAngularMiniEcommerce_API.Infrastructure.Operations;

namespace DotnetAngularMiniEcommerce_API.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);

        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, bool first = true)
        {
            return await Task.Run<string>(async () =>
            {
                string newFileName = String.Empty;
                string extension = Path.GetExtension(fileName);

                if (first)
                {
                    string fileOldName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameOperation.CharacterRegulatory(fileOldName)}{extension}";
                }
                else
                {
                    newFileName = fileName;
                    var lastIndexOfNumberDash = newFileName.LastIndexOf("-");
                    if (lastIndexOfNumberDash != -1)
                    {
                        var lastIndexOfExtensionDot = newFileName.LastIndexOf(".");
                        var lastPossibleIndex = newFileName.Substring(lastIndexOfNumberDash + 1, lastIndexOfExtensionDot - lastIndexOfNumberDash - 1);
                        if (int.TryParse(lastPossibleIndex, out int lastIndex))
                        {
                            lastIndex++;
                            newFileName = newFileName.Remove(lastIndexOfNumberDash + 1, lastIndexOfExtensionDot - lastIndexOfNumberDash - 1);
                            newFileName = newFileName.Insert(lastIndexOfNumberDash + 1, lastIndex.ToString());
                        }
                        else
                        {
                            string fileOldName = Path.GetFileNameWithoutExtension(fileName);
                            newFileName = $"{NameOperation.CharacterRegulatory(fileOldName)}-2{extension}";
                        }
                    }
                    else
                    {
                        string fileOldName = Path.GetFileNameWithoutExtension(fileName);
                        newFileName = $"{NameOperation.CharacterRegulatory(fileOldName)}-2{extension}";
                    }
                }

                //f (File.Exists($"{path}\\{newFileName}"))
                if (hasFileMethod(pathOrContainerName, newFileName))
                    return await FileRenameAsync(pathOrContainerName, $"{Path.GetFileNameWithoutExtension(newFileName)}{extension}", hasFileMethod, false);
                else
                    return newFileName;
            });
        }
    }
}
