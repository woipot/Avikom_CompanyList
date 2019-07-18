using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using Avikom_CompanyList.Entities;
using Avikom_CompanyList.other.Enums;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;

namespace Avikom_CompanyList.mvvm.Models
{
    public class CompanyModel : BindableBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ContractStatus ContractStatus { get; set; }

        public virtual ObservableCollection<UserModel> UserModels { get; set; }

        public CompanyModel()
        {
            ContractStatus = ContractStatus.NotConcluded;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
