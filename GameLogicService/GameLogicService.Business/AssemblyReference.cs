using System.Reflection;

namespace GameLogicService.Business
{
    public static class AssemblyReference
    {
        public static Assembly Reference { get; } = typeof(AssemblyReference).Assembly;
    }
}
