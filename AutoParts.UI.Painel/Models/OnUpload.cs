using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace AutoParts.UI.Painel.Models
{
    public class OnUpload
    {
        public static void Upload(int id, string entidade)
        {
            //Definir a pasta onde será gravado
            var pasta = new DirectoryInfo(HttpContext.Current.Server.MapPath($@"\Uploads\{entidade}\"));

            //Verificar se a pasta existe, caso não, cria-a
            if (!pasta.Exists)
                pasta.Create();

            //Percorrer os arquivos postados
            foreach (string fileName in HttpContext.Current.Request.Files)
            {
                //Obter o arquivo atual
                var file = HttpContext.Current.Request.Files[fileName];

                //Verificar se o arquivo não é nulo
                if (file != null && file.ContentLength > 0)
                {
                    //Verificar se é uma imagem
                    if (file.ContentType.Contains("image/"))
                    {
                        //Salvar a imagem
                        file.SaveAs(pasta + id.ToString() + Path.GetExtension(file.FileName));
                    }
                }
            }
        }

        public static string ObterImagem(int id, string entidade)
        {
            var imagemPadrao = ConfigurationManager.AppSettings["ImagemPadrao"].ToString();
            var caminhoPasta = HttpContext.Current.Server.MapPath($@"~\Uploads\{entidade}\");

            var pasta = new DirectoryInfo(caminhoPasta);

            if (pasta.Exists)
            {
                pasta.Refresh();

                var arquivo = pasta.GetFiles()
                                .OrderByDescending(x => x.LastWriteTimeUtc)
                                .FirstOrDefault(x => x.Name.Split('.')[0] == id.ToString());

                return arquivo == null ? imagemPadrao : $"../Uploads/{entidade}/{arquivo.Name}";
            }

            return imagemPadrao;
        }
    }
}
