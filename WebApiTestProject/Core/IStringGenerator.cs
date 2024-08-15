
namespace WebApiTestProject.Core
{
    public interface IStringGenerator
    {
        Task<List<string>> GetStringCombination(int stringMaxLength, int count);
    }
}