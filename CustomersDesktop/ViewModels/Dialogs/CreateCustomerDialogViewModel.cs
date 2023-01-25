using CustomersCore.Models;
using CustomersDesktop.Annotations;
using CustomersDesktop.Api;
using CustomersDesktop.Commands;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CustomersDesktop.ViewModels.Dialogs
{
    public class CreateCustomerDialogViewModel : INotifyPropertyChanged
    {
        private readonly ICustomerApi _api;
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateCustomerDialogViewModel()
        {
            _api = Refit.RestService.For<ICustomerApi>("https://localhost:7026");

            CreateCustomerCommand = new RelayCommand<Window>(CreateCustomer, (_) =>
                !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(CompanyName) &&
                !string.IsNullOrEmpty(Phone) &&
                !string.IsNullOrEmpty(Email));
        }

        private async void CreateCustomer(Window window)
        {
            try
            {
                await _api.CreateCustomer(new CustomerModel
                    { Name = Name, Email = Email, CompanyName = CompanyName, Phone = Phone });
                window.Close();
            }
            catch (ApiException e)
            {
                MessageBox.Show("Unable to create customer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private IEnumerable<CustomerModel> _customers;
        public IEnumerable<CustomerModel> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public ICommand CreateCustomerCommand { get; set; }
    }
}
