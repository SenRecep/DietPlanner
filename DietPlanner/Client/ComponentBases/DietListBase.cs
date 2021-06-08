using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Interceptors;
using DietPlanner.ClientShared.Models;
using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.ClientShared.StringInfo;
using DietPlanner.DTO.Diet;
using DietPlanner.DTO.ExtensionMethods;
using DietPlanner.DTO.Food;
using DietPlanner.DTO.Response;
using DietPlanner.Shared.ExtensionMethods;

using FluentValidation;

using Microsoft.AspNetCore.Components;

namespace DietPlanner.Client.ComponentBases
{
    public class DietListBase : ComponentBase,IDisposable
    {
        [Inject]
        public IPageStateService PageStateService { get; set; }

        [Inject]
        public IDieticianHttpService DieticianHttpService { get; set; }

        [Inject]
        public IValidator<DietCreateDto> Validator { get; set; }

        [Inject]
        public HttpInterceptorService httpInterceptor { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public DietCreateDto DietCreateDto { get; set; } = new();

        public string ErrorMessage { get; set; }
        public string ListValidMessage { get; set; }

        protected override void OnInitialized()
        {
            PageStateService.Title = "Diyet Oluştur";
            PageStateService.NavbarType = NavbarType.Dietician;
            httpInterceptor.RegisterEvent();
            DietCreateDto.TransferDietFoods = new List<FoodCreateDto>()
            {
                new() { Name="Ekmek",Description="Kepek ekmeği",Id=Guid.NewGuid(),IsSelected=false},
                new() { Name="Ekmek 2",Description="Kepek ekmeği yemek",Id=Guid.NewGuid(),IsSelected=true},
                new() { Name="Ekmek 3",Description="Kepek ekmeği yarismasi",Id=Guid.NewGuid(),IsSelected=false},
                new() { Name="Ekmek 4",Description="Kepek ekmeği",Id=Guid.NewGuid(),IsSelected=true}
            };
        }

        protected async Task DietCreateValidSubmit()
        {
            if (DietCreateDto.TransferDietFoods.IsNull() || (DietCreateDto.TransferDietFoods.IsNotNull() && !DietCreateDto.TransferDietFoods.Any(x=>x.IsSelected)))
            {
                ListValidMessage = "Yemek Seçmelisiniz";
                return;
            }
            DietCreateDto.DietFoods = DietCreateDto.TransferDietFoods.Where(x => x.IsSelected).Select(x => new FoodSimpleCreateDto()
            {
                Id = x.Id
            });
            var temp = DietCreateDto.TransferDietFoods;
            DietCreateDto.TransferDietFoods = null;
            Response<NoContent> response = await DieticianHttpService.CreateDietAsync(DietCreateDto);
            if (response.IsSuccessful)
                NavigationManager.NavigateTo(UrlInfo.Dietician);
            else
            {
                DietCreateDto.TransferDietFoods = temp;
                ErrorMessage = response.ErrorData.GetErrors("<br/>");
            }
        }

        public void Dispose()
        {
            httpInterceptor.DisposeEvent();
        }
    }
}
