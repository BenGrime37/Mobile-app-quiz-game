using mobileAppAssigment.Models;
using mobileAppAssigment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobileAppAssigment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProducts : ContentPage
    {
        VMProducts vmProduct;
        public AddProducts()
        {
            InitializeComponent();
            vmProduct = new VMProducts();
            this.BindingContext = vmProduct;
        }

        private void lstProducts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (lstProducts.SelectedItem != null)
            {
                Products product = (Products)e.SelectedItem;
                if (product != null)
                {
                    {
                        vmProduct.setProduct(product);
                    }

                    {
                        vmProduct.setProduct(product);
                    }
                }
            }


            lstProducts.SelectedItem = null;
        }
    }
}