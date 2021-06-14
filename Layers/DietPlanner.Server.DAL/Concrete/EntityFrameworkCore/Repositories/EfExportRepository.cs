using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.ComplexTypes;
using DietPlanner.Server.Entities.Concrete;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using static DietPlanner.Server.Entities.ComplexTypes.ExportInfo;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfExportRepository : IExportRepository
    {
        private readonly DietPlannerDbContext dbContext;

        public EfExportRepository(DietPlannerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ExportInfo> GetExportInfoByReportIdAsync(Guid reportId)
        {
            var export = await dbContext.Reports.Join(dbContext.Diets.Include(x => x.DietFoods).ThenInclude(x => x.Food),
                report => report.DietId, diet => diet.Id, (report, diet) => new
                {
                    Report = report,
                    Diet = new
                    {
                        diet.Name,
                        diet.Description,
                        Foods = diet.DietFoods.Select(x => new FoodInfo(x.Food.Name, x.Food.Description)).ToList()
                    }
                }).Join(dbContext.Diseases, CT => CT.Report.DiseaseId, disease => disease.Id, (CT, disease) => new
                {
                    CT.Report,
                    CT.Diet,
                    Disease = disease
                }).Join(dbContext.Dieticians, CT => CT.Report.DieticianId, dietician => dietician.Id, (CT, dietician) => new
                {
                    CT.Report,
                    CT.Diet,
                    CT.Disease,
                    Dietician = dietician,
                }).Join(dbContext.Patients, CT => CT.Report.PatientId, patient => patient.Id, (CT, patient) => new
                {
                    CT.Report,
                    CT.Diet,
                    CT.Disease,
                    CT.Dietician,
                    Patient = patient
                }).FirstOrDefaultAsync(x => x.Report.Id == reportId);

            if (export is null) return null;

            return new(
                User: new(
                    Patient: new(export.Patient.FirstName, export.Patient.LastName, export.Patient.Email, export.Patient.Address, export.Patient.IdentityNumber, export.Patient.PhoneNumber),
                    Dietician: new(export.Dietician.FirstName, export.Dietician.LastName, export.Dietician.Email, export.Dietician.PhoneNumber),
                    Disease: new(export.Disease.Name)
                    ),
                Diet: new(export.Diet.Name, export.Diet.Description, export.Report.CreatedTime, export.Report.StartTime, export.Report.EndTime, export.Diet.Foods)
               );
        }


        public async Task<List<ReportInfo>> GetExportInfosAsync(Guid userId)
        {
            var exports = await dbContext.Reports.Join(dbContext.Diets.Include(x => x.DietFoods).ThenInclude(x => x.Food),
                 report => report.DietId, diet => diet.Id, (report, diet) => new
                 {
                     Report = report,
                     Diet = new
                     {
                         diet.Name,
                         diet.Description,
                         Foods = diet.DietFoods.Select(x => new FoodInfo(x.Food.Name, x.Food.Description)).ToList()
                     }
                 }).Join(dbContext.Diseases, CT => CT.Report.DiseaseId, disease => disease.Id, (CT, disease) => new
                 {
                     CT.Report,
                     CT.Diet,
                     Disease = disease
                 }).Join(dbContext.Dieticians, CT => CT.Report.DieticianId, dietician => dietician.Id, (CT, dietician) => new
                 {
                     CT.Report,
                     CT.Diet,
                     CT.Disease,
                     Dietician = dietician,
                 }).Join(dbContext.Patients, CT => CT.Report.PatientId, patient => patient.Id, (CT, patient) => new
                 {
                     CT.Report,
                     CT.Diet,
                     CT.Disease,
                     CT.Dietician,
                     Patient = patient
                 }).Where(x => x.Patient.Id == userId).ToListAsync();

            if (exports is null) return null;

            return exports.Select(export => new ReportInfo(
              ReportId: export.Report.Id,
              DietName: export.Diet.Name,
              DiseaseName: export.Disease.Name,
              CreateTime: export.Report.CreatedTime,
              StartTime: export.Report.StartTime,
              EndTime: export.Report.EndTime)).ToList();
        }
    }
}
