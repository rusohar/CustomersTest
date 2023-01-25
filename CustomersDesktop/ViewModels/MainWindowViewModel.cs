using CustomersCore.Models;
using CustomersDesktop.Api;
using CustomersDesktop.Commands;
using CustomersDesktop.Enums;
using CustomersDesktop.ViewModels.Dialogs;
using CustomersDesktop.Views.Dialogs;
using Refit;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CustomersDesktop.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly ICustomerApi _api;

        #endregion Fields

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INotifyPropertyChanged

        public MainWindowViewModel(ICustomerApi api)
        {
            _api = api;

            Task.Run(GetCustomers);

            SortingOrder = "asc";
            SortingColumn = SortingColumnEnum.Name;
            
            AddCustomerCommand = new RelayCommand(OpenAddDialog);
            DeleteCustomerCommand = new RelayCommand<long>(DeleteCustomer);
            FilterCommand = new RelayCommand(FilterCustomers);
            ResetFiltersCommand = new RelayCommand(ResetFilters);
            LastPageCommand = new RelayCommand(OnLastPage);
            NextPageCommand = new RelayCommand(OnNextPage);
            PreviousPageCommand = new RelayCommand(OnPrevPage);
            FirstPageCommand = new RelayCommand(OnFirstPage);
            EditCustomerCommand = new RelayCommand<CustomerModel>(EditCustomer); 
        }

        #region Methods
        private async void DeleteCustomer(long id)
        {
            try
            {
                await _api.DeleteCustomer(id);
                await GetCustomers();
            }
            catch (ApiException e)
            {
                MessageBox.Show("Unable to get customers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditCustomer(CustomerModel obj)
        {
            var vm = new EditCustomerDialogViewModel
            {
                Name = obj.Name,
                CompanyName = obj.CompanyName,
                Id = obj.Id,
                Phone = obj.Phone,
                Email = obj.Email
            };
            var editDialog = new EditCustomerDialog();
            editDialog.DataContext = vm;

            editDialog.ShowDialog();
        }
        private void OpenAddDialog(object obj)
        {
            var createDialog = new CreateCustomerDialog();
            createDialog.ShowDialog();
        }

        private async Task GetCustomers()
        {
            try
            {
                var response = await _api.GetCustomers(Name, CompanyName, Phone, Email, PageNum, SortingOrder,
                    (int)SortingColumn);
                Customers = new ObservableCollection<CustomerModel>(response.Customers);
                PageNum = response.Paging.PageNum;
                TotalPages = response.Paging.TotalPages;
            }
            catch (ApiException e)
            {
                MessageBox.Show("Unable to get customers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void FilterCustomers(object obj)
        {
            Console.WriteLine("FilterCustomers");
            await GetCustomers();
        }

        private async void ResetFilters(object obj)
        {
            Console.WriteLine("FilterCustomers");
            Name = "";
            CompanyName = "";
            Phone = "";
            Email = "";
            SortingColumn = SortingColumnEnum.Name;
            SortingOrder = "asc";

            await GetCustomers();
        }

        private async void OnLastPage(object obj)
        {
            Console.WriteLine("OnLastPage");
            PageNum = TotalPages;
            await GetCustomers();
        }

        private async void OnNextPage(object obj)
        {
            Console.WriteLine("OnNextPage");
            PageNum++;
            await GetCustomers();
        }

        private async void OnPrevPage(object obj)
        {
            Console.WriteLine("OnPrevPage");
            PageNum--;
            await GetCustomers();
        }

        private async void OnFirstPage(object obj)
        {
            Console.WriteLine("OnFirstPage");
            PageNum = 1;
            await GetCustomers();
        }

        #endregion Methods

        #region Properties

        private ObservableCollection<CustomerModel> _customers;

        public ObservableCollection<CustomerModel> Customers
        {
            get => _customers;
            set
            {
                if (_customers != value)
                {
                    _customers = value;
                    OnPropertyChanged(nameof(Customers));
                }
            }
        }

        private int _pageNum;

        public int PageNum
        {
            get => _pageNum;
            set
            {
                if (_pageNum != value)
                {
                    _pageNum = value;
                    IsEnabledNextButton = PageNum != TotalPages && TotalPages>0;
                    OnPropertyChanged(nameof(PageNum));
                }
            }
        }

        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        private int _totalPages;
        public int TotalPages
        {
            get => _totalPages;
            set
            {
                if (_totalPages != value)
                {
                    _totalPages = value;
                    IsEnabledNextButton = PageNum != TotalPages && TotalPages > 0;
                    OnPropertyChanged(nameof(TotalPages));
                }
            }
        }

        private string _sortingOrder;

        public string SortingOrder
        {
            get => _sortingOrder;
            set
            {
                if (_sortingOrder != value)
                {
                    _sortingOrder = value;
                    OnPropertyChanged(nameof(SortingOrder));
                }
            }
        }

        private SortingColumnEnum _sortingColumn;

        public SortingColumnEnum SortingColumn
        {
            get => _sortingColumn;
            set
            {
                if (_sortingColumn != value)
                {
                    _sortingColumn = value;
                    OnPropertyChanged(nameof(SortingColumn));
                }
            }
        }

        private bool _isEnabledNextPageButton;

        public bool IsEnabledNextButton
        {
            get => _isEnabledNextPageButton;
            set
            {
                if (_isEnabledNextPageButton != value)
                {
                    _isEnabledNextPageButton = value;
                    OnPropertyChanged(nameof(IsEnabledNextButton));
                }
            }
        }

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _companyName;

        public string CompanyName
        {
            get => _companyName;
            set
            {
                if (_companyName != value)
                {
                    _companyName = value;
                    OnPropertyChanged(nameof(CompanyName));
                }
            }
        }


        private string _phone;

        public string Phone
        {
            get => _phone;
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged(nameof(Phone));
                }
            }
        }

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        #endregion Properties

        #region Commands

        public ICommand FirstPageCommand { get; set; }

        public ICommand PreviousPageCommand { get; set; }

        public ICommand NextPageCommand { get; set; }

        public ICommand LastPageCommand { get; set; }

        public ICommand FilterCommand { get; set; }

        public ICommand ResetFiltersCommand { get; set; }

        public ICommand EditCustomerCommand { get; set; }

        public ICommand AddCustomerCommand { get; set; }
        public ICommand DeleteCustomerCommand { get; set; }

        #endregion Commands
    }
}