using System.Windows;
using System.ComponentModel;

namespace Ecommerce.Views
{
    public partial class ClienteView : Window, INotifyPropertyChanged
    {
        protected EcommerceEntities bancoDados;
        protected ClienteListView clienteList;
        private Cliente _cliente;
       
        public ClienteView(ClienteListView clienteList, EcommerceEntities bancoDados)
        {
            InitializeComponent();
            DataContext = this;
            this.clienteList = clienteList;
            this.bancoDados = bancoDados;
            cliente = new Cliente();
        }
        public ClienteView(Cliente cliente, ClienteListView clienteList, EcommerceEntities bancoDados)
        {
            this.cliente = cliente;
            InitializeComponent();
            DataContext = this;
            this.clienteList = clienteList;
            this.bancoDados = bancoDados;
        }
        public Cliente cliente
        {
            get { return _cliente; }
            set { _cliente = value; OnPropertyChanged("cliente"); }
        }
        
        private void salvar_Click(object sender, RoutedEventArgs e)
        {
                salvar();
                bancoDados.SaveChanges();
                clienteList.pesquisar();
                this.Close();
        }
        private void excluir_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Confirma a exclusão deste cliente?", "FinSys", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                    bancoDados.Cliente.Remove(cliente);
                    bancoDados.SaveChanges();
                    clienteList.pesquisar();
                    MessageBox.Show("Cliente excluído com sucesso.", "FinSys");
                    this.Close();
            }
        }
        public virtual void salvar() { }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
    public class ClienteNew : ClienteView
    {
        public ClienteNew(ClienteListView clienteList, EcommerceEntities bancoDados) : base(clienteList, bancoDados)
        {
            excluirButton.IsEnabled = false;
        }
        public override void salvar()
        {
            bancoDados.Cliente.Add(cliente);
        }
    }
    public class ClienteEdit : ClienteView
    {
        public ClienteEdit(Cliente cliente, ClienteListView clienteList, EcommerceEntities bancoDados) : base(cliente, clienteList, bancoDados)
        {
        }
        public override void salvar()
        {
           
        }
    }
}
