using ProjectWithMvvm.Commands;
using ProjectWithMvvm.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectWithMvvm.Domain.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand SelectCustomerCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }
        public RelayCommand OrderCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand DeleteCustomerCommand { get; set; }
        public MainViewModel()
        {

            
            SelectedCustomer = new Customer();
            SelectedOrder = new Order();

            AllCustomers = App.DB.CustomerRepository.GetAllData();
            AllOrders = App.DB.OrderRepository.GetAllData();

            ResetCommand = new RelayCommand((sender) =>
             {
                 SelectedCustomer = new Customer();
             });

            DeleteCommand = new RelayCommand((sender) =>
             {
                 App.DB.OrderRepository.DeleteData(SelectedOrder);
                 AllOrders = App.DB.OrderRepository.GetAllData();
                 MessageBox.Show("Order Successfly Deletes" );
             }, (pred) =>
             {
                 if (SelectedOrder != null && SelectedOrder.Id != 0)
                 {
                     return true;
                 }
                 return false;
             });

            DeleteCustomerCommand = new RelayCommand((sender) =>
             {
                 App.DB.CustomerRepository.DeleteData(SelectedCustomer);
                 AllCustomers = App.DB.CustomerRepository.GetAllData();
                 MessageBox.Show("Customer Successfly");
             }, (pred) =>
             {
                 if (selectedCustomer != null && selectedCustomer.Id != 0)
                 {
                     return true;
                 }
                 return false;
             });

            OrderCommand = new RelayCommand((sender) =>
             {
                 var order = new Order();
                 order.OrderDate = DateTime.Now;
                 order.CustomerId = selectedCustomer.Id;
                 App.DB.OrderRepository.AddData(order);
                 AllOrders= App.DB.OrderRepository.GetAllData();
                 MessageBox.Show("Order Succesfly");
             }, (pred) =>
             {
                 if (selectedCustomer != null && selectedCustomer.Id != 0)
                 {
                     return true;
                 }
                 return false;
             });
            SelectCustomerCommand = new RelayCommand((sender) =>
              {
                  if (SelectedCustomer!= null)
                  {
                      var customer = App.DB.CustomerRepository.GetData(SelectedCustomer.Id);
                      SelectedCustomer = customer;
                      AllOrders = new ObservableCollection<Order>(SelectedCustomer.Orders);
                  }
              });

            AddCommand = new RelayCommand((sender) =>
             {
                 var item = App.DB.CustomerRepository.GetData(SelectedCustomer.Id);
                 if (item == null)
                 {
                     App.DB.CustomerRepository.AddData(selectedCustomer);
                     AllCustomers = App.DB.CustomerRepository.GetAllData();
                     MessageBox.Show("Add Succesfly");
                 }
                 else
                 {
                     MessageBox.Show("This customer is already exist!");
                 }

             });

            UpdateCommand = new RelayCommand((sender) =>
            {
                App.DB.CustomerRepository.UpdateData(selectedCustomer);
                MessageBox.Show("Update Succesfly");
            });
        }

        private ObservableCollection<Customer> allCustomers;

        public ObservableCollection<Customer> AllCustomers
        {
            get { return allCustomers; }
            set { allCustomers = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Order> allOrders;

        public ObservableCollection<Order> AllOrders
        {
            get { return allOrders; }
            set { allOrders = value; OnPropertyChanged(); }
        }

        private Customer selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set { selectedCustomer = value; OnPropertyChanged(); }
        }

        private Order selectedOrder;
        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value; OnPropertyChanged(); }
        }
    }
}
