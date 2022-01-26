using System.Runtime.Versioning;
using System.Threading;
namespace ESFS20
{
    public partial class App
    {
        [SupportedOSPlatform("windows")]
        public static void Foo() => Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
    }
}
