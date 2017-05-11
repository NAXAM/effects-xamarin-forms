using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Naxam.Effects.Demo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var items = new List<string>(100);
            for (int i = 0; i < 100; i++)
            {
                items.Add("Item - 000" + i);
            }

            //lstItems.ItemsSource = items;
            _SnapshotBtn.Command = new Command((obj) =>
            {
                var func = SnapshotEffect.GetTakeSnapshotFunc(_CV);
                if (func != null)
                {
                    var output = func.Invoke();
                    var img = new Image();
                    img.Source = ImageSource.FromStream( () => 
                                                        output
                     );
                    _SnapshotHolder.Content = img;
                }
            });
        }
    }
}
