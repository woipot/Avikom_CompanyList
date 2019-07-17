using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avikom_CompanyList.other.Enums;
using DevExpress.Mvvm;

namespace Avikom_CompanyList.mvvm.Models
{
    public class CompanyModel : BindableBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ContractStatus ContractStatus { get; set; }

        public virtual ICollection<UserModel> UserModels { get; set; }

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
