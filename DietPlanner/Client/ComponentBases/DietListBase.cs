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
using Microsoft.AspNetCore.Components.Web;
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
        public HttpInterceptorService HttpInterceptor { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<DietFoodCreateDto> DietFoods { get; set; }
        public List<DietFoodCreateDto> SearchedDietFoods { get; set; }

        public DietCreateDto DietCreateDto { get; set; } = new();
        public FoodCreateDto FoodCreateDto { get; set; } = new();

        public string SearchKey { get; set; } = "";
        public string ErrorMessage { get; set; }
        public string ModalErrorMessage { get; set; }
        public string ListValidMessage { get; set; }
        public bool ModalLoading { get; set; }

        protected override void OnInitialized()
        {
            PageStateService.Title = "Diyet Oluştur";
            PageStateService.NavbarType = NavbarType.Dietician;
            HttpInterceptor.RegisterEvent();
        }

        protected override async Task OnInitializedAsync()
        {
            await InitFoodTableAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
               await  JSRuntime.InvokeVoidAsync("PreventEnterKey", "disableEnterKeyForm");
        }

        protected async Task InitFoodTableAsync()
        {
            Response<List<DietFoodCreateDto>> response = await DieticianHttpService.GetAllFoodAsync();
            if (response.IsSuccessful)
            {
                DietFoods = response.Data;
                SearchedDietFoods = DietFoods;
            }
        }


        public void Search(KeyboardEventArgs e)
        {
            SearchedDietFoods = DietFoods.Where(x=>x.Name.StartsWith(SearchKey,StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        protected async Task DietCreateValidSubmit()
        {
            if (DietFoods.IsNull() || !DietFoods.Any(x => x.IsSelected))
            {
                ListValidMessage = "Yemek Seçmelisiniz";
                return;
            }
            DietCreateDto.SimpleDietFoods = DietFoods.Where(x => x.IsSelected).Select(x => new FoodSimpleCreateDto()
            {
                Id = x.Id
            });
            List<DietFoodCreateDto> temp = DietFoods;
            DietFoods = null;
            Response<NoContent> response = await DieticianHttpService.CreateDietAsync(DietCreateDto);
            if (response.IsSuccessful)
                NavigationManager.NavigateTo(UrlInfo.Dietician);
            else
            {
                DietFoods = temp;
                ErrorMessage = response.ErrorData.GetErrors("<br/>");
            }
        }

        protected async Task ModalValidSubmit()
        {
            ModalLoading = true;
            var response = await DieticianHttpService.CreateFoodAsync(FoodCreateDto);
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

        public void Dispose() => HttpInterceptor.DisposeEvent();
    }
}
