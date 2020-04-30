namespace SnowmanLabsChallenge.Infra.CrossCutting.Templates
{
    using System;
    using System.IO;
    using System.Text;

    internal class Program
    {
        private static string entityName = string.Empty;
        private static string entityNamePtBr = string.Empty;
        private static bool onlyModel = false;
        private static string currentPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), @".."));
        private static string lineSeparator = "--------------------------------------";

        private static void Main(string[] args)
        {
            ReadInputs();

            Console.WriteLine();
            Console.WriteLine(lineSeparator);
            Console.WriteLine(string.Format("Gerando código para a entitidade: {0}", entityName));
            Console.WriteLine(lineSeparator);
            Console.WriteLine();

            if (!onlyModel)
            {
                CreateWebApiTemplates();
                CreateApplicationTemplates();
            }

            CreateDomainTemplates();
            CreateInfraDataTemplates();
            CreateInfraCrossCuttingTemplates();

            Console.WriteLine();
            Console.WriteLine(lineSeparator);
            Console.WriteLine(string.Format("Execute o comando 'Add-Migrations AddTable-{0}' para gerar o script de atualização do banco de dados.", entityName));
            Console.WriteLine(lineSeparator);
            Console.WriteLine();

            Console.ReadKey();
        }

        /// <summary>
        /// Lê os parâmetros de entrada
        /// </summary>
        private static void ReadInputs()
        {
            do
            {
                do
                {
                    entityName = string.Empty;

                    Console.Write("Digite o nome da entidade. O padrão do Projeto é entidades em português e no singular: ");

                    entityName = Console.ReadLine();
                }
                while (string.IsNullOrEmpty(entityName));

                entityName = entityName.ToCamelCase();
            }
            while (ExistEntityNameOnProject());

            entityNamePtBr = !string.IsNullOrEmpty(entityNamePtBr) ? entityNamePtBr : entityName;
        }

        #region WebApi

        private static void CreateWebApiTemplates()
        {
            Console.WriteLine();
            Console.WriteLine(lineSeparator);
            Console.WriteLine(string.Format("Gerando código para a camada de WebApi para a entitidade: {0}", entityName));
            Console.WriteLine(lineSeparator);
            Console.WriteLine();

            var webApiTemplatePath = Path.GetFullPath(Path.Combine(currentPath, @"1 - Services"));

            CreateWebApiController(webApiTemplatePath);
        }

        private static void CreateWebApiController(string templatePath)
        {
            var destinationPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.WebApi\Controllers"));
            var fileTemplateName = "ControllerTemplate.txt";
            var newfilePath = CreateTemplateFile(templatePath, fileTemplateName);
            var newfileName = string.Format("{0}Controller.cs", entityName);
            CopyFileTemplateTo(newfilePath, destinationPath, newfileName);
            DeleteFileTemplate(newfilePath);
        }

        #endregion WebApi

        #region Application

        private static void CreateApplicationTemplates()
        {
            Console.WriteLine();
            Console.WriteLine(lineSeparator);
            Console.WriteLine(string.Format("Gerando código para a camada Application para a entitidade: {0}", entityName));
            Console.WriteLine(lineSeparator);
            Console.WriteLine();

            var applicationTemplatePath = Path.GetFullPath(Path.Combine(currentPath, @"2 - Application"));
            CreateApplicationInterface(applicationTemplatePath);
            CreateApplicationService(applicationTemplatePath);
            CreateApplicationViewModel(applicationTemplatePath);
            CreateApplicationFilter(applicationTemplatePath);
            AddAutoMapperConfig();
        }

        private static void CreateApplicationInterface(string applicationTemplatePath)
        {
            var destinationPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Application\Interfaces"));
            var fileTemplateName = "IAppServiceTemplate.txt";
            var newfilePath = CreateTemplateFile(applicationTemplatePath, fileTemplateName);
            var newfileName = string.Format("I{0}AppService.cs", entityName);
            CopyFileTemplateTo(newfilePath, destinationPath, newfileName);
            DeleteFileTemplate(newfilePath);
        }

        private static void CreateApplicationService(string applicationTemplatePath)
        {
            var destinationPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Application\Services"));
            var fileTemplateName = "AppServiceTemplate.txt";
            var newfilePath = CreateTemplateFile(applicationTemplatePath, fileTemplateName);
            var newfileName = string.Format("{0}AppService.cs", entityName);
            CopyFileTemplateTo(newfilePath, destinationPath, newfileName);
            DeleteFileTemplate(newfilePath);
        }

        private static void CreateApplicationViewModel(string applicationTemplatePath)
        {
            var destinationPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Application\ViewModels"));
            var fileTemplateName = "ViewModelTemplate.txt";
            var newfilePath = CreateTemplateFile(applicationTemplatePath, fileTemplateName);
            var newfileName = string.Format("{0}ViewModel.cs", entityName);
            CopyFileTemplateTo(newfilePath, destinationPath, newfileName);
            DeleteFileTemplate(newfilePath);
        }

        private static void CreateApplicationFilter(string applicationTemplatePath)
        {
            var destinationPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Application\Filters"));
            var fileTemplateName = "FilterTemplate.txt";
            var newfilePath = CreateTemplateFile(applicationTemplatePath, fileTemplateName);
            var newfileName = string.Format("{0}Filter.cs", entityName);
            CopyFileTemplateTo(newfilePath, destinationPath, newfileName);
            DeleteFileTemplate(newfilePath);
        }

        private static void AddAutoMapperConfig()
        {
            var autoMapperConfigPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Application\AutoMapper"));
            var autoMapperDomainToViewConfigFilePath = string.Format(@"{0}\DomainToViewModelMappingProfile.cs", autoMapperConfigPath);

            string[] lines = File.ReadAllLines(autoMapperDomainToViewConfigFilePath);
            using (var fs = File.Create(autoMapperDomainToViewConfigFilePath))
            {
                var newLine = Encoding.UTF8.GetBytes(Environment.NewLine);

                foreach (var line in lines)
                {
                    var info = new UTF8Encoding(true).GetBytes(line);
                    fs.Write(info, 0, info.Length);
                    fs.Write(newLine, 0, newLine.Length);

                    var addConf = line.Contains("AddNewConf");
                    if (addConf)
                    {
                        var newConf = new UTF8Encoding(true).GetBytes(string.Format("            this.CreateMap<{0}, {0}ViewModel>().MaxDepth(1);", entityName));
                        fs.Write(newConf, 0, newConf.Length);
                        fs.Write(newLine, 0, newLine.Length);
                        fs.Write(newLine, 0, newLine.Length);
                    }
                }
            }

            var autoMapperViewToDomainConfigFilePath = string.Format(@"{0}\ViewModelToDomainMappingProfile.cs", autoMapperConfigPath);

            lines = File.ReadAllLines(autoMapperViewToDomainConfigFilePath);
            using (var fs = File.Create(autoMapperViewToDomainConfigFilePath))
            {
                var newLine = Encoding.UTF8.GetBytes(Environment.NewLine);

                foreach (var line in lines)
                {
                    var info = new UTF8Encoding(true).GetBytes(line);
                    fs.Write(info, 0, info.Length);
                    fs.Write(newLine, 0, newLine.Length);

                    var addConf = line.Contains("AddNewConf");
                    if (addConf)
                    {
                        var newConf = new UTF8Encoding(true).GetBytes(string.Format("            this.CreateMap<{0}ViewModel, {0}>();", entityName));
                        fs.Write(newConf, 0, newConf.Length);
                        fs.Write(newLine, 0, newLine.Length);

                        fs.Write(newLine, 0, newLine.Length);
                    }
                }
            }
        }

        #endregion Application

        #region Domain

        private static void CreateDomainTemplates()
        {
            Console.WriteLine();
            Console.WriteLine(lineSeparator);
            Console.WriteLine(string.Format("Gerando código para a camada Domain para a entitidade: {0}", entityName));
            Console.WriteLine(lineSeparator);
            Console.WriteLine();

            var domainTemplatePath = Path.GetFullPath(Path.Combine(currentPath, @"3 - Domain"));
            CreateDomainInterface(domainTemplatePath);
            CreateDomainModel(domainTemplatePath);
        }

        private static void CreateDomainInterface(string templatePath)
        {
            var destinationPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Domain\Interfaces"));
            var fileTemplateName = "IRepositoryTemplate.txt";
            var newfilePath = CreateTemplateFile(templatePath, fileTemplateName);
            var newfileName = string.Format("I{0}Repository.cs", entityName);
            CopyFileTemplateTo(newfilePath, destinationPath, newfileName);
            DeleteFileTemplate(newfilePath);
        }

        private static void CreateDomainModel(string templatePath)
        {
            var destinationPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Domain\Models"));
            var fileTemplateName = "ModelTemplate.txt";
            var newfilePath = CreateTemplateFile(templatePath, fileTemplateName);
            var newfileName = string.Format("{0}.cs", entityName);
            CopyFileTemplateTo(newfilePath, destinationPath, newfileName);
            DeleteFileTemplate(newfilePath);
        }

        #endregion Domain

        #region Infra.Data

        private static void CreateInfraDataTemplates()
        {
            Console.WriteLine();
            Console.WriteLine(lineSeparator);
            Console.WriteLine(string.Format("Gerando código para a camada Infra.Data para a entitidade: {0}", entityName));
            Console.WriteLine(lineSeparator);
            Console.WriteLine();

            var infraDataTemplatePath = Path.GetFullPath(Path.Combine(currentPath, @"4.1 - Infra.Data"));
            AddDataContextConfig();
            CreateInfraDataMapping(infraDataTemplatePath);
            CreateInfraDataRepository(infraDataTemplatePath);
            // CreateInfraDataSeed(infraDataTemplatePath);
        }

        private static void AddDataContextConfig()
        {
            var contextConfigPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Infra.Data\Context"));
            var contextConfigFilePath = string.Format(@"{0}\MainContext.cs", contextConfigPath);

            string[] lines = File.ReadAllLines(contextConfigFilePath);
            using (var fs = File.Create(contextConfigFilePath))
            {
                var newLine = Encoding.UTF8.GetBytes(Environment.NewLine);

                foreach (var line in lines)
                {
                    var info = new UTF8Encoding(true).GetBytes(line);
                    fs.Write(info, 0, info.Length);
                    fs.Write(newLine, 0, newLine.Length);

                    var addDbSet = line.Contains("AddNewDbSet");
                    if (addDbSet)
                    {
                        var newDbSet = new UTF8Encoding(true).GetBytes(string.Format("        public DbSet<{0}> {0}s ", entityName) + "{ get; set; }");
                        fs.Write(newDbSet, 0, newDbSet.Length);
                        fs.Write(newLine, 0, newLine.Length);
                        fs.Write(newLine, 0, newLine.Length);
                    }

                    var addNewMapping = line.Contains("AddNewMapping");
                    if (addNewMapping)
                    {
                        var newMapping = new UTF8Encoding(true).GetBytes(string.Format("            modelBuilder.ApplyConfiguration(new {0}Map());", entityName));
                        fs.Write(newMapping, 0, newMapping.Length);
                        fs.Write(newLine, 0, newLine.Length);
                    }
                }
            }
        }

        private static void CreateInfraDataMapping(string infraDataTemplatePath)
        {
            var destinationPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Infra.Data\Mappings"));
            var fileTemplateName = "MappingTemplate.txt";
            var newfilePath = CreateTemplateFile(infraDataTemplatePath, fileTemplateName);
            var newfileName = string.Format("{0}Map.cs", entityName);
            CopyFileTemplateTo(newfilePath, destinationPath, newfileName);
            DeleteFileTemplate(newfilePath);
        }

        private static void CreateInfraDataRepository(string infraDataTemplatePath)
        {
            var destinationPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Infra.Data\Repository"));
            var fileTemplateName = "RepositoryTemplate.txt";
            var newfilePath = CreateTemplateFile(infraDataTemplatePath, fileTemplateName);
            var newfileName = string.Format("{0}Repository.cs", entityName);
            CopyFileTemplateTo(newfilePath, destinationPath, newfileName);
            DeleteFileTemplate(newfilePath);
        }

        private static void CreateInfraDataSeed(string infraDataTemplatePath)
        {
            var destinationPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Infra.Data\Seeds"));
            var fileTemplateName = "SeedInitializerTemplate.txt";
            var newfilePath = CreateTemplateFile(infraDataTemplatePath, fileTemplateName);
            var newfileName = string.Format("{0}Initializer.cs", entityName);
            CopyFileTemplateTo(newfilePath, destinationPath, newfileName);
            DeleteFileTemplate(newfilePath);

            AddSeedInitializerConfig();
            AddSeedInitializerCall();
        }

        private static void AddSeedInitializerConfig()
        {
            var startUpConfigPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Web.Api"));
            var startUpConfigFilePath = string.Format(@"{0}\Startup.cs", startUpConfigPath);

            string[] lines = File.ReadAllLines(startUpConfigFilePath);
            using (var fs = File.Create(startUpConfigFilePath))
            {
                var newLine = Encoding.UTF8.GetBytes(Environment.NewLine);

                foreach (var line in lines)
                {
                    var info = new UTF8Encoding(true).GetBytes(line);
                    fs.Write(info, 0, info.Length);
                    fs.Write(newLine, 0, newLine.Length);

                    var addSeedInitializer = line.Contains("AddNewSeedInitializer");
                    if (addSeedInitializer)
                    {
                        var newSeedInitializer = new UTF8Encoding(true).GetBytes(string.Format("            services.AddTransient<{0}Initializer>();", entityName));
                        fs.Write(newSeedInitializer, 0, newSeedInitializer.Length);
                        fs.Write(newLine, 0, newLine.Length);
                    }
                }
            }
        }

        private static void AddSeedInitializerCall()
        {
            var seedInitializerConfigPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Infra.Data\Seeds"));
            var seedInitializerConfigFilePath = string.Format(@"{0}\SeedInitializer.cs", seedInitializerConfigPath);

            string[] lines = File.ReadAllLines(seedInitializerConfigFilePath);
            using (var fs = File.Create(seedInitializerConfigFilePath))
            {
                var newLine = Encoding.UTF8.GetBytes(Environment.NewLine);

                foreach (var line in lines)
                {
                    var info = new UTF8Encoding(true).GetBytes(line);
                    fs.Write(info, 0, info.Length);
                    fs.Write(newLine, 0, newLine.Length);

                    var addInitializerVariable = line.Contains("AddNewInitializerVariable");
                    if (addInitializerVariable)
                    {
                        var newSeedInitializer = new UTF8Encoding(true).GetBytes(string.Format("        private {0}Initializer {1}Initializer;", entityName, entityName.ToLower()));
                        fs.Write(newSeedInitializer, 0, newSeedInitializer.Length);
                        fs.Write(newLine, 0, newLine.Length);
                    }

                    var addInitializerIoc = line.Contains("AddNewInitializerIoC");
                    if (addInitializerIoc)
                    {
                        var newInitializerIoc = new UTF8Encoding(true).GetBytes(string.Format("            {0}Initializer {1}Initializer,", entityName, entityName.ToLower()));
                        fs.Write(newInitializerIoc, 0, newInitializerIoc.Length);
                        fs.Write(newLine, 0, newLine.Length);
                    }

                    var addInitializerSetter = line.Contains("AddNewInitializerSetter");
                    if (addInitializerSetter)
                    {
                        var newInitializerSetter = new UTF8Encoding(true).GetBytes(string.Format("            this.{0}Initializer = {0}Initializer;", entityName.ToLower()));
                        fs.Write(newInitializerSetter, 0, newInitializerSetter.Length);
                        fs.Write(newLine, 0, newLine.Length);
                    }

                    var addNewInitializerSeedWait = line.Contains("AddNewInitializerSeedWait");
                    if (addNewInitializerSeedWait)
                    {
                        var newInitializerSeedWait = new UTF8Encoding(true).GetBytes(string.Format("            this.{0}Initializer.Seed().Wait();", entityName.ToLower()));
                        fs.Write(newInitializerSeedWait, 0, newInitializerSeedWait.Length);
                        fs.Write(newLine, 0, newLine.Length);
                    }
                }
            }
        }

        #endregion Infra.Data

        #region Infra.CrossCutting

        private static void CreateInfraCrossCuttingTemplates()
        {
            Console.WriteLine();
            Console.WriteLine(lineSeparator);
            Console.WriteLine(string.Format("Gerando código para a camada Infra.CrossCutting para a entitidade: {0}", entityName));
            Console.WriteLine(lineSeparator);
            Console.WriteLine();

            AddIoCConfig();
        }

        private static void AddIoCConfig()
        {
            var iocConfigPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Infra.CrossCutting.IoC"));
            var contextConfigFilePath = string.Format(@"{0}\NativeInjectorBootStrapper.cs", iocConfigPath);

            string[] lines = File.ReadAllLines(contextConfigFilePath);
            using (var fs = File.Create(contextConfigFilePath))
            {
                var newLine = Encoding.UTF8.GetBytes(Environment.NewLine);

                foreach (var line in lines)
                {
                    var info = new UTF8Encoding(true).GetBytes(line);
                    fs.Write(info, 0, info.Length);
                    fs.Write(newLine, 0, newLine.Length);

                    var addAppService = line.Contains("AddAppService");
                    if (addAppService && !onlyModel)
                    {
                        var newAppService = new UTF8Encoding(true).GetBytes(string.Format("            services.AddScoped<I{0}AppService, {0}AppService>();", entityName));
                        fs.Write(newAppService, 0, newAppService.Length);
                        fs.Write(newLine, 0, newLine.Length);
                    }

                    var addNewRepository = line.Contains("AddNewRepository");
                    if (addNewRepository)
                    {
                        var newRepository = new UTF8Encoding(true).GetBytes(string.Format("            services.AddScoped<I{0}Repository, {0}Repository>();", entityName));
                        fs.Write(newRepository, 0, newRepository.Length);
                        fs.Write(newLine, 0, newLine.Length);
                    }
                }
            }
        }

        #endregion Infra.CrossCutting

        #region Utils

        private static string CreateTemplateFile(string templatePath, string fileTemplateName)
        {
            var newFileName = Guid.NewGuid();
            var newFilePath = string.Format(@"{0}\{1}.cs", templatePath, newFileName);

            using (var fs = File.Create(newFilePath))
            {
                var fileTemplatePath = string.Format(@"{0}\{1}", templatePath, fileTemplateName);
                string[] lines = File.ReadAllLines(fileTemplatePath);
                foreach (var line in lines)
                {
                    var lineContent = line.Replace("**EntityName**", entityName)
                                          .Replace("**EntityNameLowerCase**", entityName.ToLower())
                                          .Replace("**EntityNamePtBr**", entityNamePtBr);

                    var info = new UTF8Encoding(true).GetBytes(lineContent);
                    fs.Write(info, 0, info.Length);

                    var newline = Encoding.UTF8.GetBytes(Environment.NewLine);
                    fs.Write(newline, 0, newline.Length);
                }
            }

            return newFilePath;
        }

        private static void CopyFileTemplateTo(string sourcePath, string destinationPath, string fileName)
        {
            var fileDestinationPath = string.Format(@"{0}\{1}", destinationPath, fileName);
            if (File.Exists(fileDestinationPath))
            {
                File.Delete(fileDestinationPath);
            }

            File.Copy(sourcePath, fileDestinationPath);
        }

        private static void DeleteFileTemplate(string filePath)
        {
            File.Delete(filePath);
        }

        private static bool ExistEntityNameOnProject()
        {
            var fileExists = false;

            var destinationPath = Path.GetFullPath(Path.Combine(currentPath, @"..\SnowmanLabsChallenge.Domain\Models"));
            var filePath = string.Format("{0}\\{1}.cs", destinationPath, entityName);
            if (File.Exists(filePath))
            {
                Console.Write(string.Format("A entidade {0} já está adicionada no projeto. Deseja sobrescrevê-la? Sim (S) ou Não(N): ", entityName));

                var key = Console.ReadLine();
                key = key.ToUpper();
                if (key != "S" && key != "Y" &&
                    key != "SIM" && key != "YES")
                {
                    fileExists = true;
                }
            }

            return fileExists;
        }

        #endregion Utils
    }
}
