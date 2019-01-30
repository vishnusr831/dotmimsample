using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Dotmim.Sync;
using Dotmim.Sync.Enumerations;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Sync_OnClick(object sender, RoutedEventArgs e)
        {
            var httpAdress = "http://localhost:5000/api/values";
            WebProxyClientProvider proxyClient = new WebProxyClientProvider(new Uri(httpAdress));
            SqliteSyncProvider sqliteSync = new SqliteSyncProvider("abc8.db");
            SyncAgent agent = new SyncAgent(sqliteSync, proxyClient);
            agent.Parameters.Add("beta", "userid", 1);

            try
            {
                var s = await agent.SynchronizeAsync(SyncType.ReinitializeWithUpload);
                Debug.WriteLine($"Sync finished | Downloaded : {s.TotalChangesDownloaded}");
                Debug.WriteLine($"Sync finished | Time : {s.CompleteTime.Second.ToString()}");

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
