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
using Microsoft.JSInterop;

namespace DietPlanner.Client.ComponentBases
{
    public class DietListBase : ComponentBase, IDisposable
    {
        [Inject]
        public IPageStateService PageStateService { get; set; }

        [Inject]
        public IDieticianHttpService DieticianHttpService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IValidator<DietCreateDto> Validator { get; set; }
        [Inject]
        public IValidator<FoodCreateDto> ModalValidator { get; set; }

        [Inject]
        public HttpInterceptorService httpInterceptor { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public DietCreateDto DietCreateDto { get; set; } = new();
        public FoodCreateDto FoodCreateDto { get; set; } = new();

        public string ErrorMessage { get; set; }
        public string ModalErrorMessage { get; set; }
        public string ListValidMessage { get; set; }
        public bool ModalLoading { get; set; }

        protected override void OnInitialized()
        {
            PageStateService.Title = "Diyet Oluştur";
            PageStateService.NavbarType = NavbarType.Dietician;
            httpInterceptor.RegisterEvent();
        }

        protected override async Task OnInitializedAsync()
        {
            await InitFoodTableAsync();
        }

        protected async Task InitFoodTableAsync()
        {
            Response<IEnumerable<DietFoodCreateDto>> response = await DieticianHttpService.GetAllFood();
            if (response.IsSuccessful)
                DietCreateDto.TransferDietFoods = response.Data;
        }

        protected async Task DietCreateValidSubmit()
        {
            if (DietCreateDto.TransferDietFoods.IsNull() || !DietCreateDto.TransferDietFoods.Any(x => x.IsSelected))
            {
                ListValidMessage = "Yemek Seçmelisiniz";
                return;
            }
            DietCreateDto.SimpleDietFoods = DietCreateDto.TransferDietFoods.Where(x => x.IsSelected).Select(x => new FoodSimpleCreateDto()
            {
                Id = x.Id
            });
            IEnumerable<DietFoodCreateDto> temp = DietCreateDto.TransferDietFoods;
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

        protected async Task ModalValidSubmit()
        {
            ModalLoading = true;
            var response = await DieticianHttpService.CreateFood(FoodCreateDto);
            if (!response.IsSuccessful)
                ModalErrorMessage = response.ErrorData.GetErrors("<br/>");
            else
            {
                ModalErrorMessage = "";
                FoodCreateDto = new();
            }
            await InitFoodTableAsync();
            ModalLoading = false;
            await JSRuntime.InvokeVoidAsync("closeModal", "modal");
        }

        public void Dispose() => httpInterceptor.DisposeEvent();
    }
}
