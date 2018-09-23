using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UploadFile.Models
{
    public class UploadFileResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDArquivo { get; set; }
        public string Nome { get; set; }
        public int Tamanho { get; set; }
        public string Tipo { get; set; }
        public string Caminho { get; set; }

        public string Hash { get; set; }


        public void CalculaHash(string arquivo)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(arquivo))
                {
                    this.Hash = BitConverter.ToString(md5.ComputeHash(stream));
                }
            }
        }

        public bool ComparaHash(string arquivo)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(arquivo))
                {
                    string SegundoHash = BitConverter.ToString(md5.ComputeHash(stream));
                    int result = (SegundoHash.CompareTo(Hash));
                    if (result != 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }
}