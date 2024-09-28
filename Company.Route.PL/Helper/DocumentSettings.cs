namespace Company.Route.PL.Helper
{
    public static class DocumentSettings
    {
        //upload
        public static string Upload(IFormFile file, string folderName)
        {
            //1- get location of folder

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\files\\{folderName}");

            //2- get file name and make it unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            //3- get file path
            string filePath = Path.Combine(folderPath, fileName);

            //4- file stream
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);
            return fileName;
        }

        //Delete

        public static void Delete(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\files\\{folderName}",fileName);
        
            if(File.Exists(filePath))
            {
                File.Delete(filePath); 
            }
        
        }
    }
}
