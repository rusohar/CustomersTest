using CustomersCore.Models;
using CustomersDesktop.Annotations;
using CustomersDesktop.Api;
using CustomersDesktop.Commands;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CustomersDesktop.ViewModels.Dialogs
{
    public class EditCustomerDialogViewModel : INotifyPropertyChanged
    {
        private readonly ICustomerApi _api;
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EditCustomerDialogViewModel()
        {
            _api = Refit.RestService.For<ICustomerApi>("https://localhost:7026");

            EditCustomerCommand = new RelayCommand<Window>(EditCustomer, (_) =>
                !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(CompanyName) &&
                !string.IsNullOrEmpty(Phone) &&
                !string.IsNullOrEmpty(Email));
        }

        private async void EditCustomer(Window window)
        {
            try
            {
                await _api.EditCustomer(new CustomerModel
                    { Id=Id,Name = Name, Email = Email, CompanyName = CompanyName, Phone = Phone }, Id);
                window.Close();
            }
            catch (ApiException e)
            {
                MessageBox.Show("Unable to update customer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private long _id;

        public long Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public ICommand EditCustomerCommand { get; set; }
    }
}
