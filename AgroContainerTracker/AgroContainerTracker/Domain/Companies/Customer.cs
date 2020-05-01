
namespace AgroContainerTracker.Domain.Companies
{
    public class Customer : CompanyBase
    {
        public Rate Rate { get; set; }

        public string BankAccount { get; set; }

    }

    public class AddCustomerRequest : AddCompanyBaseRequest
    {
        public int? RateId { get; set; }

        public string BankAccount { get; set; }
    }
}
