using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qal.workflow.domain.Data.DTO.ScoreCard
{
    public class ScoreDto
    {
        public string Name { get; set; }
        public string Score { get; set; }
        public string ErrorMessage { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public dynamic Details { get; set; }
    }

    public class ScoreCardRuleRequest : Property
    {
        //EloanDb
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? Education { get; set; }
        public string? MartialStatus { get; set; }
        public string? NoOfDependents { get; set; }
        public string? NoOfWorking { get; set; }
        public string? ResidenceType { get; set; }
        public string? ResidenceOwnership { get; set; }
        public string? YearAtCurrentAddress { get; set; }
        public string? OtherProperty { get; set; }
        public string? YearsInBusiness { get; set; }
        public string? NoOfEmployees { get; set; }
        public decimal? ShopArea { get; set; }
        public string? BuildingType { get; set; }
        public string? RentalFee { get; set; }
        public string? CommercialVehicle { get; set; }
        public string? VehicleCondition { get; set; }
        public string? BorrowedFromFamilyNFriend { get; set; }
        public string? BorrowedFromPawnShop { get; set; }
        public string? BorrowedFromMFIs { get; set; }
        public string? BorrowedFromOtherBanks { get; set; }

        //AcoE Data
        public decimal? AverageAccountBalance { get; set; }
        public int? AverageTranxPerMonth { get; set; }
        public int? AvgBlueTranxVol { get; set; }
        public decimal? AvgBlueTranxValue { get; set; }
        public int? AvgRedTranxVol { get; set; }
        public decimal? AvgRedTranxValue { get; set; }
        public decimal? AvgRedRevenue { get; set; }
        public string? ExistingCreditCardCustomer { get; set; }
        public string? ExistingMortageLoanCustomer { get; set; }
        public string? ExistingKBZPaySLPLCustomer { get; set; }
        public string? CreditCardMortgageKBZPaySLLast12Months { get; set; }
        public string? BusinessSinceWithKpay { get; set; }
        public string? AvgCommissionForKBZPayLast6Month { get; set; }
    }

    public class ScoreCardResponse : Property
    {
        public double TotalScore { get; set; }
        public string ScoreGrade { get; set; }
        public List<ScoreDto> ScoreDetail { get; set; } = new List<ScoreDto>();
    }
}
