using MauiHybridMudblazorCrud.Models;
using MauiHybridMudblazorCrud.Services;
using Microsoft.AspNetCore.Components;

namespace MauiHybridMudblazorCrud.Components.Pages;
public partial class Students
{
    [Inject] IStudentService StudentService { get; set; }
    private List<StudentModel> students;

    private string _searchString = "";
    protected override async Task OnInitializedAsync()
    {
        await LoadStudends();
    }
    private async Task LoadStudends()
    {
        students = await StudentService.GetAllStudent();
    }
    private void EditStudent(int studentId)
    {
        NavManager.NavigateTo($"update_student/{studentId}");
    }

    private async void DeleteStudent(StudentModel student)
    {
        if(await App.Current.MainPage.DisplayAlert("Delete!!!", $"Are you sure to delete {student.FullName}", "Yes", "Cancel"))
        {
            var response = await StudentService.DeleteStudent(student);
            if (response > 0)
            {
                await OnInitializedAsync();
                this.StateHasChanged();
            }
        }
    }
}
