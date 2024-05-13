using MauiHybridMudblazorCrud.Models;
using Microsoft.AspNetCore.Components;

namespace MauiHybridMudblazorCrud.Components.Pages;

public partial class AddEditStudent
{
    [Parameter] public int StudentId { get; set; }

    private StudentModel AddEditStudentModel { get; set; } = new();
    //private string firstName;
    //private string lastName;
    //private string email;
    //private string gender;
    public bool Validated => !string.IsNullOrEmpty(AddEditStudentModel.FirstName) && !string.IsNullOrEmpty(AddEditStudentModel.LastName) &&
                             !string.IsNullOrEmpty(AddEditStudentModel.Email) && !string.IsNullOrEmpty(AddEditStudentModel.Gender);
    private void setGender(string gender)
    {
        AddEditStudentModel.Gender = gender;
    }

    protected async override Task OnInitializedAsync()
    {
        if (StudentId > 0)
        {
            var response = await StudentService.GetStudentByID(StudentId);
            if (response != null)
            {
                AddEditStudentModel = response;
            }
        }
    }
    private async void AddStudentRecord()
    {
        if (Validation())
        {
            int response = -1;
            if (AddEditStudentModel.Id > 0)
            {
                response = await StudentService.UpdateStudent(AddEditStudentModel);
                //update record
            }
            else
            {
                response = await StudentService.AddStudent(AddEditStudentModel);
                //add record
            }


            if (response > 0)
            {
                //firstName = lastName = gender = email = string.Empty;
                this.StateHasChanged();
                await App.Current.MainPage.DisplayAlert("Record Saved", "Record Saved To Student Table", "OK");
                NavManager.NavigateTo("/students");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Oops",
               "Something went wrong while adding record", "OK");
            }
        }
    }
    private bool Validation()
    {
        if (string.IsNullOrWhiteSpace(AddEditStudentModel.FirstName))
        {
            App.Current.MainPage.DisplayAlert("Validation", "Enter first name", "OK");
            return false;
        }
        else if (string.IsNullOrWhiteSpace(AddEditStudentModel.LastName))
        {
            App.Current.MainPage.DisplayAlert("Validation", "Enter last name", "OK");
            return false;
        }
        else if (string.IsNullOrWhiteSpace(AddEditStudentModel.Email))
        {
            App.Current.MainPage.DisplayAlert("Validation", "Enter email", "OK");
            return false;
        }
        else if (string.IsNullOrWhiteSpace(AddEditStudentModel.Gender))
        {
            App.Current.MainPage.DisplayAlert("Validation", "select gender", "OK");
            return false;
        }
        return true;
    }
}
