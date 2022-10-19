using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminImagensController(IWebHostEnvironment hostingEnvironment,
            IOptions<ConfigurationImagens> myConfiguration)
        {
            _hostingEnvironment = hostingEnvironment;
            _myConfig =     myConfiguration.Value;
        }
        public IActionResult Index()
        {
            return View();
        }
        //enviar arquivos
        //metjod asyncrono                 recebendo lista de arquivos(files) do list Iformfile
     public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            //se quantidade é null
            if(files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }
            //se quantidade é maior que 10
            if (files.Count > 10)
            {
                ViewData["Erro"] = "Error: Quantidade de arquivos excedeu o limite";
                return View(ViewData);
            }
            //soma quantidade de bytes
            long size = files.Sum(f => f.Length);

            var filePathsName = new List<string>();

            //montando o caminho onde vai salvar
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                _myConfig.NomePastaImagensProdutos);

            // percorre e verifica se os arquivos são arquivos de imagem
            foreach(var formFile in files)
            {
                //tipo do arquivo
                if (   formFile.FileName.Contains(".jpg") 
                    || formFile.FileName.Contains(".gif") 
                    || formFile.FileName.Contains(".png")
                   )
                {
                    //                       cocnatenar nome completo do local+\\+nome do arquivo
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);
                    
                    //armazenar na variavel criada que é uma lista de stream
                    filePathsName.Add(fileNameWithPath);
                    //se não existir filemode ele cria, caso exista ele sobrescreve
                    //     stream   tipo  Filestream    nome do arquivo     model create 
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    
                }
            }
            ViewData["Resultado"]=  $"{files.Count} arquivos foram enviados ao servidor, " + 
                                    $"com tamanho total de: {size} bytes";

            //transportar dados entre view ou controllers para view
            ViewBag.Arquivos = filePathsName;

            return View(ViewData);
        }
        public IActionResult GetImagens()
        {
            //declarando a instancia da classe FileManagerModel
            FileManagerModel model = new FileManagerModel();

            //variavel para armazenar caminho das imagens
            var userImagesPath = Path.Combine(_hostingEnvironment.WebRootPath, 
                _myConfig.NomePastaImagensProdutos);

            //criando uma instancia e inicializando um diretório            neste local
            DirectoryInfo dir = new DirectoryInfo                       (userImagesPath);
            //utilizar um methodo da classe directoryinfo pegar imagens     neste local
            FileInfo[] files = dir.GetFiles();
            //atribuir a propriedade do model o local que é la na pasta appsettings.json
            model.PathImagesProduto = _myConfig.NomePastaImagensProdutos;
            //verificar se possui arquivos na pasta,
            if (files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
            }

            model.Files = files;

            return View(model);
        
        }

        public IActionResult Deletefile(string fname)
        {
            string _imagemDeleta = Path.Combine(_hostingEnvironment.WebRootPath,
                _myConfig.NomePastaImagensProdutos + "\\", fname);

            if ((System.IO.File.Exists(_imagemDeleta)))
            {
                System.IO.File.Delete(_imagemDeleta);

                ViewData["Deletado"] = $"Arquivo(s) {_imagemDeleta} deletado com sucesso";
            }
            return View("index");
        }
    }
}
