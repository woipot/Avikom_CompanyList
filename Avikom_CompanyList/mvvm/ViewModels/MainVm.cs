using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using Avikom_CompanyList.Entities;
using Avikom_CompanyList.mvvm.Models;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;

namespace Avikom_CompanyList.mvvm.ViewModels
{
    public class MainVm : BindableBase
    {
        public ObservableCollection<CompanyModel> Companies { get; private set; }

        public MainVm()
        {
            InitBD();
            Companies = LoadFromDb();
           // var user = Companies.First().Users.First();

            //using (var db = new CompanyContext())
            //{
            //    var t1 = new CompanyModel {Name = "Барселона"};
            //    var t2 = new CompanyModel {Name = "Реал Мадрид"};
            //    db.Companies.Add(t1);
            //    db.Companies.Add(t2);
            //    db.SaveChanges();
            //    var pl1 = new UserModel {Name = "Роналду", CompanyModel = t2};
            //    var pl2 = new UserModel {Name = "Месси", CompanyModel = t1};
            //    var pl3 = new UserModel {Name = "Хави", CompanyModel = t1 };
            //    db.Users.AddRange(new List<UserModel> {pl1, pl2, pl3});
            //    db.SaveChanges();

            //    // вывод 
            //    foreach (var pl in db.Users)
            //        Console.WriteLine("{0} - {1}", pl.Name, pl.CompanyModel != null ? pl.CompanyModel.Name : "");
            //    Console.WriteLine();
            //    foreach (var t in db.Companies)
            //    {
            //        Console.WriteLine("Команда: {0}", t.Name);
            //        foreach (UserModel pl in t.Users)
            //        {
            //            Console.WriteLine("{0}", pl.Name);
            //        }

            //        Console.WriteLine();
            //    }
            //}
        }


        private ObservableCollection<CompanyModel> LoadFromDb()
        {

            using (var db = new CompanyContext())
            {
                db.Companies.Load();

                db.Users.Load();
                return db.Companies.Local.ToObservableCollection();
            }
        }

        private void InitBD()
        {
            using (var db = new CompanyContext())
            {
                db.Database.CreateIfNotExists();
            }
        }
    }
}
