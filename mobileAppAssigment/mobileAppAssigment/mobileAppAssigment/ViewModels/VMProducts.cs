using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using mobileAppAssigment.Models;

namespace mobileAppAssigment.ViewModels
{
    public class VMProducts : INotifyPropertyChanged
    {
        FirebaseClient fClient;

        private Products _product { get; set; }

        public Products product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

        private bool _showButton { get; set; }
        public bool showButton
        {
            get { return _showButton; }
            set
            {
                _showButton = value;
                OnPropertyChanged();
            }
        }
        private bool _isBusy { get; set; }
        public bool isBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                showButton = !value;
            }
        }

        private ICommand _btnSaveProduct { get; set; }
        public ICommand btnSaveProduct
        {
            get { return _btnSaveProduct; }
            set
            {
                _btnSaveProduct = value;
                OnPropertyChanged();
            }
        }

        private string _lblMessage { get; set; }
        public string lblMessage
        {
            get { return _lblMessage; }
            set
            {
                _lblMessage = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Products> _lstProducts { get; set; }

        public ObservableCollection<Products> lstProducts
        {
            get { return _lstProducts; }
            set
            {
                _lstProducts = value;
                OnPropertyChanged();
            }
        }

        private string _btnSaveText { get; set; }
        public string btnSaveText
        {
            get { return _btnSaveText; }
            set
            {
                _btnSaveText = value;
                OnPropertyChanged();
            }
        }

        readonly string productResource = "Products";
        public VMProducts()
        {
            try
            {
                lstProducts = new ObservableCollection<Products>();
                btnSaveProduct = new Command(async () =>
                {
                    isBusy = true;
                    await trnProducts("ADD");
                });
                var callList = new Command(async () => await GetAllProducts());
                callList.Execute(null);

            }
            catch (Exception ex)
            {
                lblMessage = "Error occurred " + ex.Message.ToString();
            }
        }

        public bool connectFirebase()
        {
            try
            {
                if (fClient == null)
                    fClient = new FirebaseClient("https://assignmentu17-default-rtdb.firebaseio.com/");
                return true;
            }
            catch (Exception ex)
            {
                lblMessage = "Error occurred in connecting firebase. Error:" + ex.Message.ToString();
                return false;
            }

        }

        public async Task trnProducts(string action)
        {
            try
            {
                if (product == null || String.IsNullOrWhiteSpace(product.productName) || product.productPrice == null)
                {
                    lblMessage = "Please enter product details to save product";
                    isBusy = false;
                    return;
                }

                if (connectFirebase())
                {
                    Products products = new Products();
                    products.productName = product.productName;
                    products.productPrice = product.productPrice;
                    if (btnSaveText == "SAVE" && action.Equals("ADD"))
                    {
                        products.productId = Guid.NewGuid();

                        await fClient.Child(productResource).PostAsync(JsonConvert.SerializeObject(products));
                        await GetAllProducts();
                        lblMessage = "Product saved successfully";
                    }
                    else if (btnSaveText == "UPDATE" && action.Equals("ADD"))
                    {
                        products.productId = product.productId;

                        var updateProduct = (await fClient.Child(productResource).OnceAsync<Products>()).FirstOrDefault(x => x.Object.productId == products.productId);

                        if (updateProduct == null)
                        {
                            lblMessage = "Cannot find selected Product";
                            isBusy = false;
                            return;
                        }
                        await fClient
                        .Child(productResource + "/" + updateProduct.Key).PatchAsync(JsonConvert.SerializeObject(products));
                        await GetAllProducts();
                        lblMessage = "Product updated successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage = "Error occurred. Cannot save Product. Error:" + ex.Message.ToString();

            }
            isBusy = false;
        }

        public async Task GetAllProducts()
        {
            Clear();
            isBusy = true;
            try
            {
                lstProducts = new ObservableCollection<Products>();
                if (connectFirebase())
                {
                    var lst = (await fClient.Child(productResource).OnceAsync<Products>()).Select(x =>
                    new Products
                    {
                        productId = x.Object.productId,
                        productName = x.Object.productName,
                        productPrice = x.Object.productPrice,
                    }).ToList();

                    lstProducts = new ObservableCollection<Products>(lst);
                }
            }
            catch (Exception ex)
            {
                lblMessage = "Error occurred in getting products. Error:" + ex.Message.ToString();
            }
            isBusy = false;
        }

        public void setProduct(Products edt)
        {
            product = new Products();
            product.productName = edt.productName;
            product.productPrice = edt.productPrice;
            btnSaveText = "UPDATE";
            product.productId = edt.productId;
        }

        public void Clear()
        {
            product = new Products();
            product.productName = "";
            product.productPrice = null;
            isBusy = false;
            product.productId = Guid.Empty;
            btnSaveText = "SAVE";
            lblMessage = "";
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
