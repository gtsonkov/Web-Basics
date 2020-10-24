using SUS.MvcFramework;
using System.Threading.Tasks;

namespace SULS.App
{
    public static class Program
    {
        public static async Task Main()
        {
            await Host.CreateHostAsync(new StartUp());
        }
    }
}