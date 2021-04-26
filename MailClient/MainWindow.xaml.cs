using MailClient.Model.Enum;
using MailClient.Common.Helpers;
using System;
using System.Collections.Generic;
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
using MailClient.Model;
using MailClient.Core.Interface;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //@TODO : USE MVVM
        //USE MODEL BINDING!
        private readonly IMailClientFactory _mailFactory;
        public MainWindow(IMailClientFactory mailFactory)
        {
            _mailFactory = mailFactory;
            InitializeComponent();
            InitProperties();
        }

        private void InitProperties()
        {
            MailRequestModel mailRequestModel = new MailRequestModel();
            ServerTypeComboBox.ItemsSource = Enum.GetValues(typeof(MailServerType));
            EncryptionTypeComboBox.ItemsSource = Enum.GetValues(typeof(EncryptionType));

            ServerTextBox.Text = mailRequestModel.Server;
            UsernameText.Text = mailRequestModel.Username;
            PasswordText.Text = mailRequestModel.Password;

        }

        public void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            MailRequestModel mailRequestModel = new MailRequestModel();
            mailRequestModel.ServerType = EnumHelper.GetEnumValue<MailServerType>(ServerTypeComboBox.SelectedValue.ToString());
            mailRequestModel.EncryptionType = EnumHelper.GetEnumValue<EncryptionType>(EncryptionTypeComboBox.SelectedValue.ToString());
            mailRequestModel.Server = ServerTextBox.Text;
            mailRequestModel.Username = UsernameText.Text;
            mailRequestModel.Password = PasswordText.Text;
            mailRequestModel.Port = int.Parse(PortTextBox.Text);

            var service = _mailFactory.GetMailService(mailRequestModel.ServerType);

            service.SetProperties(mailRequestModel);
            service.Connect();
        }
    }
}
