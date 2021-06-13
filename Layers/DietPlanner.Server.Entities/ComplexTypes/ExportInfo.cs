using System.Collections.Generic;

using static DietPlanner.Server.Entities.ComplexTypes.ExportInfo;

namespace DietPlanner.Server.Entities.ComplexTypes
{
    public partial record ExportInfo(UserSection User, DietInfo Diet);

    public partial record ExportInfo
    {
        public record UserSection(PatientInfo Patient, DieticianInfo Dietician, DiseaseInfo Disease);

        public record PatientInfo(string FirstName, string LastName, string Email, string Address, string IdentityNumber, string PhoneNumber);

        public record DieticianInfo(string FirstName, string LastName, string Email, string PhoneNumber);

        public record DiseaseInfo(string Name);

        public record DietInfo(string Name, string Description, IEnumerable<FoodInfo> Foods);

        public record FoodInfo(string Name, string Description);
    }
}
