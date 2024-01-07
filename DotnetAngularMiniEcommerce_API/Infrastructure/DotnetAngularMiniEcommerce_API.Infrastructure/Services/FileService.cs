using DotnetAngularMiniEcommerce_API.Application.Services;
using DotnetAngularMiniEcommerce_API.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DotnetAngularMiniEcommerce_API.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(
            IWebHostEnvironment webHostEnvironment
            )
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new List<(string fileName, string path)>();
            List<bool> results = new List<bool>();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);

                var finalUploadPath = $"{uploadPath}\\{fileNewName}";
                bool result = await CopyFileAsync(finalUploadPath, file);
                datas.Add((fileNewName, finalUploadPath));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true)))
                return datas;

            return null;
            //TODO: Eğer ki yukarıdaki if geçerli değilse burada dosyaların sunucuda yüklenirken
            //hata alındığına dair uyarıcı bir exception oluşturulup fırlatılması gerekiyor
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fs = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fs);
                await fs.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //TODO: log!
                throw ex;
            }
        }

        private async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
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
                            newFileName = newFileName.Remove(lastIndexOfNumberDash + 1 , lastIndexOfExtensionDot - lastIndexOfNumberDash - 1);
                            newFileName = newFileName.Insert(lastIndexOfNumberDash + 1, lastIndex.ToString());
                        }
                        else
                        {
                            string fileOldName = Path.GetFileNameWithoutExtension(fileName);
                            newFileName = $"{NameOperation.CharacterRegulatory(fileOldName)}-2{extension}";
                        }
                    }
                    else {
                        string fileOldName = Path.GetFileNameWithoutExtension(fileName);
                        newFileName = $"{NameOperation.CharacterRegulatory(fileOldName)}-2{extension}";
                    }
                }

                if (File.Exists($"{path}\\{newFileName}"))
                    return await FileRenameAsync(path, $"{Path.GetFileNameWithoutExtension(newFileName)}{extension}", false);
                else
                    return newFileName;
            });
        }
    }
}
