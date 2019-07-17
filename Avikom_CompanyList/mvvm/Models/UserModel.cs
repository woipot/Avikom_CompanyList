using DevExpress.Mvvm;

namespace Avikom_CompanyList.mvvm.Models
{
    public class UserModel : BindableBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }


        public int CompanyModelId { get; set; }
        public virtual CompanyModel CompanyModel { get; set; }

    }

}
