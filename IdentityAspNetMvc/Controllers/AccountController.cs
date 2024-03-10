using IdentityAspNetMvc.ViewModels.Account;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAspNetMvc.Controllers;

[Authorize] //Det gör att man måste vara inloggad för att få tillgång till sidan

public class AccountController(UserManager<UserEntity> userManager, AddressServices addressServices) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AddressServices _addressServices = addressServices;


    #region Details
    [HttpGet]
    [Route("/account/details")]
    public async Task<IActionResult> Details()
    {
        var viewModel = new AccountDetailsViewModel
        {
            ProfileInfo = await PopulateProfilInfoAsync()
        };
        viewModel.BasicInfo ??= await PopulateBasicInfoAsync();
        viewModel.AddressModel ??= await PopulateAddressModelAsync();

        return View(viewModel);
    }
    #endregion


    #region [HttpPost] Details
    [HttpPost]
    [Route("/account/details")]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
    {
        if (viewModel.BasicInfo != null)
        {


            if (viewModel.BasicInfo.FirstName != null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.Email != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {

                    user.FirstName = viewModel.BasicInfo.FirstName;
                    user.LastName = viewModel.BasicInfo.LastName;
                    user.Email = viewModel.BasicInfo.Email;
                    user.PhoneNumber = viewModel.BasicInfo.Phone;
                    user.Bio = viewModel.BasicInfo.Bio;

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("IncorrectValues", "Something went wron! Unable to save data");
                        ViewData["ErrorMessage"] = "Something went wron! Unable to update basic information.";
                    }
                }
            }
        }
        if (viewModel.AddressModel != null)
        {


            if (viewModel.AddressModel.Address1 != null && viewModel.AddressModel.PostalCode != null && viewModel.AddressModel.City != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                { 
                    var address = await _addressServices.GetAddressAsync(user.Id);
                    if (address != null)
                    {
                        address.AddressLine_1 = viewModel.AddressModel.Address1;
                        address.AddressLine_2 = viewModel.AddressModel.Address2;
                        address.PostalCode = viewModel.AddressModel.PostalCode;
                        address.City = viewModel.AddressModel.City;

                        var result = await _addressServices.UpdateAddressAsync(address);
                        if (!result)
                        {
                            ModelState.AddModelError("IncorrectValues", "Something went wron! Unable to save data");
                            ViewData["ErrorMessage"] = "Something went wron! Unable to save update address";
                        }
                    }
                    else
                    {
                        address = new AddressEntity
                        {
                            UserId = user.Id,
                            AddressLine_1 = viewModel.AddressModel.Address1,
                            AddressLine_2 = viewModel.AddressModel.Address2,
                            PostalCode = viewModel.AddressModel.PostalCode,
                            City = viewModel.AddressModel.City
                        };
                        var result = await _addressServices.CreateAddressAsync(address);
                        if (!result)
                        {
                            ModelState.AddModelError("IncorrectValues", "Something went wron! Unable to save data");
                            ViewData["ErrorMessage"] = "Something went wron! Unable to save update address";
                        }
                    }
                

            }
        }
    }

    viewModel.ProfileInfo = await PopulateProfilInfoAsync();
    viewModel.BasicInfo ??= await PopulateBasicInfoAsync();
    viewModel.AddressModel ??= await PopulateAddressModelAsync();


        return View(viewModel);
}
#endregion

private async Task<ProfileInfoViewModel> PopulateProfilInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new ProfileInfoViewModel
        {
           
            FirstName = user!.FirstName,
            LastName = user.LastName,
            Email = user.Email!         

        };

    }


    private async Task<BasicInfoViewModel> PopulateBasicInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new BasicInfoViewModel
        {
            UserId = user!.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            Phone = user.PhoneNumber,
            Bio= user.Bio

        };
        
    }
    private async Task<AddressViewModel> PopulateAddressModelAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var address = await _addressServices.GetAddressAsync(user.Id);
            if (address != null)
            {
                return new AddressViewModel
                {
                    Address1 = address.AddressLine_1,
                    Address2 = address.AddressLine_2,
                    PostalCode = address.PostalCode,
                    City = address.City
                };
            }
        }

        // Om användaren är null eller address är null, returnera en tom AddressViewModel
        return new AddressViewModel();
    }


}

