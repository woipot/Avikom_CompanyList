using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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

        public MainVm()
        {
            _db = new CompanyContext();
            InitBD();
            LoadFromDb();

            DeleteCompanyCommand = new DelegateCommand<CompanyModel>(DeleteCompany);

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

        public void Dispose()
        {
            _db?.Dispose();
        }


        public DelegateCommand<CompanyModel> DeleteCompanyCommand { get; }

        private void DeleteCompany(CompanyModel company)
        {
            _db.Companies.Remove(company);
            RaisePropertiesChanged("Companies");
            _db.SaveChanges();
        }

        private ObservableCollection<CompanyModel> LoadFromDb()
        {
            _db.Companies.Load();
            foreach (var company in _db.Companies)
            {
                company.PropertyChanged += Update;
            }
         //   _db.Users.Load();
            
            return _db.Companies.Local.ToObservableCollection();
        }

        private void InitBD()
        {
            _db.Database.CreateIfNotExists();
        }

        private void Update(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            _db.Entry(sender).State = EntityState.Modified;
            _db.SaveChanges();
        }

    }
}
