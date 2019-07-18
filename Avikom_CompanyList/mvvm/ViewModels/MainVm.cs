using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Windows;
using Avikom_CompanyList.Entities;
using Avikom_CompanyList.mvvm.Models;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;

namespace Avikom_CompanyList.mvvm.ViewModels
{
    public class MainVm : BindableBase, IDisposable
    {

        private CompanyContext _db;

        public ObservableCollection<CompanyModel> Companies => _db.Companies.Local;


        #region Constructors

        public MainVm()
        {
            _db = new CompanyContext();
            InitBD();
            LoadFromDb();

            DeleteCompanyCommand = new DelegateCommand<CompanyModel>(DeleteCompany);
            DeleteUserCommand = new DelegateCommand<UserModel>(DeleteUser);

            AddUserCommand = new DelegateCommand<CompanyModel>(AddUser);
            AddCompanyCommand = new DelegateCommand(AddCompany);

            //using (var db = new CompanyContext())
            //{
            //    var t1 = new CompanyModel { Name = "Барселона" };
            //    var t2 = new CompanyModel { Name = "Реал Мадрид" };
            //    db.Companies.Add(t1);
            //    db.Companies.Add(t2);
            //    db.SaveChanges();
            //    var pl1 = new UserModel { Name = "Роналду", CompanyModel = t2 };
            //    var pl2 = new UserModel { Name = "Месси", CompanyModel = t1 };
            //    var pl3 = new UserModel { Name = "Хави", CompanyModel = t1 };
            //    db.Users.AddRange(new List<UserModel> { pl1, pl2, pl3 });
            //    db.SaveChanges();
            //}
        }

        #endregion


        #region Disposable

        public void Dispose()
        {
            _db?.Dispose();
        }

        #endregion


        #region View buttons

        public DelegateCommand<CompanyModel> DeleteCompanyCommand { get; }

        private void DeleteCompany(CompanyModel company)
        {
            var res = MessageBox.Show("Do you really want to delete this item?", "Are you sure?", MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (res == MessageBoxResult.Yes)
            {
                //company.UserModels.Clear();

                foreach (var userModel in company.UserModels)
                {
                    userModel.PropertyChanged -= Update;
                }

                company.PropertyChanged -= Update;

                _db.Companies.Remove(company);
                _db.SaveChanges();

                RaisePropertiesChanged("Companies");
            }

        }


        public DelegateCommand<UserModel> DeleteUserCommand { get; }

        private void DeleteUser(UserModel user)
        {
            var res = MessageBox.Show("Do you really want to delete this item?", "Are you sure?", MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (res == MessageBoxResult.Yes)
            {
                //company.UserModels.Clear();

                user.PropertyChanged -= Update;

                _db.Users.Remove(user);
                _db.SaveChanges();

                RaisePropertiesChanged("Companies");
            }

        }


        public DelegateCommand<CompanyModel> AddUserCommand { get; }

        private void AddUser(CompanyModel company)
        {
            var user = new UserModel(){CompanyModel = company};
            _db.Users.Add(user);
            _db.SaveChanges();
            RaisePropertiesChanged("Companies");
            user.PropertyChanged += Update;

        }


        public DelegateCommand AddCompanyCommand { get; }

        private void AddCompany()
        {
            var company = new CompanyModel();
            _db.Companies.Add(company);
            _db.SaveChanges();
            RaisePropertiesChanged("Companies");
            company.PropertyChanged += Update;

        }

        #endregion


        #region Db Tools

        private void LoadFromDb()
        {
            _db.Companies.Load();
            _db.Users.Load();


            foreach (var company in _db.Companies)
            {
                company.PropertyChanged += Update;
            }
            foreach (var user in _db.Users)
            {
                user.PropertyChanged += Update;
            }

            //  return _db.Companies.ToObservableCollection();
        }

        private void InitBD()
        {
            _db.Database.CreateIfNotExists();
        }


        #endregion


        #region Other private

        private void Update(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            _db.Entry(sender).State = EntityState.Modified;
            _db.SaveChanges();
        }

        #endregion
    }
}
