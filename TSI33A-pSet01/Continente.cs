using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSI33A_pSet01
{
    class Regiao
    {
        public static int idGlobal = 1;

        private int id;
        
        //propriedades
        
        public int Id
        {
            get { return this.id; } //readonly
        }

        public string Nome { get; set; } //automaticas
        //fim propriedades

        //caso seja criado um novo registro
        public Regiao (string nome)
        {
            this.id = Regiao.idGlobal;
            Regiao.idGlobal++;

            Nome = nome;
        }

        //caso seja lido um registro do arquivo csv
        public Regiao (int id, string nome)
        {
            this.id = id;
            Nome = nome;

            Regiao.idGlobal = ++id;
        }
    }
}
