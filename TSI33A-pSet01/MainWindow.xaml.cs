using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TSI33A_pSet01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Arquivo { get; set; }
        private List<Regiao> continentesArray;

        public MainWindow()
        {
            InitializeComponent();
            this.continentesArray = new List<Regiao>();
        }
        
        // Ao clicar no botão carregar (btn_carregar)
        private void btn_carregar_Click(object sender, RoutedEventArgs e)
        {
            // Cria um diálogo win32 para escolher o arquivo
            Microsoft.Win32.OpenFileDialog dlgArquivo = new Microsoft.Win32.OpenFileDialog();

            // Seta uma extensão padrão e um filtro pela extensão do arquivo
            dlgArquivo.DefaultExt = ".csv";
            dlgArquivo.Filter = "Documetos CSV (.csv)|*.csv";

            // Exibe o dialogo por meio do método ShowDialog
            Nullable<bool> resultado = dlgArquivo.ShowDialog();

            // Grava o caminho completo do arquivo em uma variavel
            if (resultado == true)
                Arquivo = dlgArquivo.FileName;
            else
                return;


            //aqui pode quebrar em uma outra função
            using (var sr = new StreamReader(Arquivo))
            {
                var reader = new CsvReader(sr);
 
                //CSVReader will now read the whole file into an enumerable
                IEnumerable<DataRecord> records = reader.GetRecords<DataRecord>();

                //verifica se o arquivo csv possui registros. Caso não, será exibido um alerta

                int cont = 0;
                //Todos os registros serão adicionados no array
                foreach (DataRecord record in records)
                {
                    //tenta gravar cada registro lido em um objeto Continete que é armazenado em um Array<>
                    try
                    {
                        this.continentesArray.Add(new Regiao(Convert.ToInt32(record.Id), record.Name));
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("A o arquivo csv esta em um formato inválido. Verifique!");
                        return;
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("Id de um registro possui valor muito grande. Verifique!");
                        return;
                    }
                    cont++;
                }
                //caso arquivo lido não possua registros é exibido um alerta
                if (cont == 0)
                    MessageBox.Show("Atenção! Arquivo não possui registros.");

            }

            this.tbx_carregar.Text = Arquivo;

            //apresenta os regitros lidos no data grid
            this.dtg_regioes.ItemsSource = this.continentesArray;
                //this.dtg_regioes.Items.Add(this.continentesArray.ElementAt(i);

            this.tbx_id.Text = Convert.ToString(Regiao.idGlobal);
            this.btn_salvar.IsEnabled = true;
        }

        // Ao selecionar uma linha do grid (dtg_regioes)
        private void dtg_continentes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentRowIndex = dtg_regioes.Items.IndexOf(dtg_regioes.CurrentItem);

            Console.WriteLine(currentRowIndex);
        }
    }
}
