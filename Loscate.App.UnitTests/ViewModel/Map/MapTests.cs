using System.Threading.Tasks;
using Loscate.App.Map;
using Loscate.App.Services;
using Loscate.App.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace Loscate.App.UnitTests.ViewModel.Map
{
    [TestClass]
    public class MapTests
    {
        public MapTests()
        {
            DependencyService.Register<IFirebaseAuthenticator, FakeAuthenticator>();
            DependencyService.Register<IMapService, FakeMap>();
            MockForms.Init();
            Application.Current = new App();
        }

        [TestMethod]
        public async Task MapLoadTest()
        {
            var map = new CustomMap();
            var viewModel = new MapViewModel(map);
            await viewModel.LoadPins();
        }
        
        [TestMethod]
        public void OpenSearchTest()
        {
            var map = new CustomMap();
            var viewModel = new MapViewModel(map);
            viewModel.SearchCommand.Execute(null);
        }
        
        [TestMethod]
        public void AddPinTest()
        {
            var map = new CustomMap();
            var viewModel = new MapViewModel(map);
            viewModel.AddPinCommand.Execute(null);
        }
    }
}