using System.Reflection;

namespace GameStatsService.Business
{
    public static class AssemblyReference
    {
        public static Assembly Reference { get; } = typeof(AssemblyReference).Assembly;
    }
}
