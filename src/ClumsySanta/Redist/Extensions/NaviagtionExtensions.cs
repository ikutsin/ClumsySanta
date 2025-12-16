using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ClumsySanta.Redist.Extensions
{
    public static class NavigationExtensions
    {
        private static object _navigationData = null;

        public static void Navigate(this NavigationService service, string page, object data)
        {
            _navigationData = data;
            service.Navigate(new Uri(page, UriKind.Relative));
        }

        public static object GetLastNavigationData(this NavigationService service)
        {
            object data = _navigationData;
            _navigationData = null;
            return data;
        }
    }
}
