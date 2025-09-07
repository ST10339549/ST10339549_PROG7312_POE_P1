using System;
using System.Windows.Forms;
using MunicipalServiceApp.Application.Services;
using MunicipalServiceApp.Infrastructure.Repositories;
using WinForms = System.Windows.Forms;

namespace MunicipalServiceApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
#if NET6_0_OR_GREATER
            ApplicationConfiguration.Initialize();
#else
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#endif
            var repo = new InMemoryIssueRepository();
            var svc  = new IssueService(repo);

            WinForms.Application.Run(new Presentation.MainMenuForm(svc));
        }
    }
}