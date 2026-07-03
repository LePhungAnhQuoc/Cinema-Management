using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Public static property holding the single instance
        public static UnitOfWork UnitOfWork { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize your database context and Unit of Work once
            UnitOfWork = new UnitOfWork();
        }
    }
}
