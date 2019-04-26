using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Ecommerce.Views
{
    public partial class ClienteListView : Window
    {
        EcommerceEntities bancoDados = new EcommerceEntities();
        public ClienteListView()
        {
            InitializeComponent();
        }
        private void pesquisar_Click(object sender, RoutedEventArgs e)
        {
            pesquisar();
        }
        public void pesquisar()
        {
            var sql = from x in bancoDados.Cliente
                        select x;
            if (!string.IsNullOrEmpty(nomeTextBox.Text))
                sql = sql.Where(x => x.nome.Contains(nomeTextBox.Text));
            if (!cpfTextBox.Text.Equals(""))
                sql = sql.Where(x => x.cpf.Contains(cpfTextBox.Text));
            clienteDataGrid.ItemsSource = sql.OrderBy(x => x.nome).ToList();
        }
        private void todos_Click(object sender, RoutedEventArgs e)
        {
            clienteDataGrid.ItemsSource = bancoDados.Cliente.OrderBy(x => x.nome).ToList();
        }
        private void inserir_Click(object sender, RoutedEventArgs e)
        {
            ClienteView clienteView = new ClienteNew(this, bancoDados);
            clienteView.ShowDialog();
        }
        private void editar_Click(object sender, MouseButtonEventArgs e)
        {
            ClienteView clienteView = new ClienteEdit(clienteDataGrid.SelectedItem as Cliente, this, bancoDados);
            clienteView.ShowDialog();
        }
        private void limpar_Click(object sender, RoutedEventArgs e)
        {
            nomeTextBox.Text = "";
            cpfTextBox.Text = "";
            clienteDataGrid.ItemsSource = null;
        }
    }
}
