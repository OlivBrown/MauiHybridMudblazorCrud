using MauiHybridMudblazorCrud.AppConstants;
using MauiHybridMudblazorCrud.Models;
using SQLite;

namespace MauiHybridMudblazorCrud.Services;

public class StudentService : IStudentService
{
    private SQLiteAsyncConnection _dbConnection;

    public StudentService()
    {
        SetUpDb();
    }

    private async Task SetUpDb()
    {
        try
        {
            if (_dbConnection == null)
            {
                _dbConnection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
                await _dbConnection.CreateTableAsync<StudentModel>();
            }
        }
        catch (Exception e)
        {

        }

    }

    public async Task<int> AddStudent(StudentModel studentModel)
    {
        await SetUpDb();
        return await _dbConnection.InsertAsync(studentModel);
    }

    public async Task<int> DeleteStudent(StudentModel studentModel)
    {
        await SetUpDb();
        return await _dbConnection.DeleteAsync(studentModel);
    }
    public async Task<int> UpdateStudent(StudentModel studentModel)
    {
        await SetUpDb();
        return await _dbConnection.UpdateAsync(studentModel);
    }
    public async Task<List<StudentModel>> GetAllStudent()
    {
        await SetUpDb();
        return await _dbConnection.Table<StudentModel>().ToListAsync();
    }

    public async Task<StudentModel> GetStudentByID(int StudentID)
    {
        await SetUpDb();
        var student = await _dbConnection.QueryAsync<StudentModel>($"Select * From {nameof(StudentModel)} where Id={StudentID} ");
        return student.FirstOrDefault();
    }
}
