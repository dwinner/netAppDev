using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.MockingKernel.Moq;
using Northwind.Core;
using Northwind.Wpf.Infrastructure;
using Northwind.Wpf.ViewModels;
using NUnit.Framework;

namespace Northwind.Wpf.Test
{
   [TestFixture]
   internal class MainViewModelTests
   {
      [TearDown]
      public void TearDown()
      {
         _kernel.Reset();
      }

      private readonly MoqMockingKernel _kernel;

      public MainViewModelTests()
      {
         _kernel = new MoqMockingKernel();

         _kernel.Bind(x => x.FromAssembliesMatching("Northwind.*")
            .SelectAllClasses()
            .BindDefaultInterfaces());

         _kernel.Bind(x => x.FromAssembliesMatching("Northwind.*")
            .SelectAllInterfaces()
            .EndingWith("Factory")
            .BindToFactory());
      }

      [Test]
      public void ExecutingCreateCustomerCommandShowsCustomerView()
      {
         var customerViewMock = _kernel.GetMock<ICustomerView>();
         customerViewMock.Setup(v => v.ShowDialog());
         var sut = _kernel.Get<MainViewModel>();
         sut.CreateCustomerCommand.Execute(null);
         customerViewMock.VerifyAll();
      }


      [Test]
      public void GettingCustomersCallsRepositoryGetAll()
      {
         var repositoryMock = _kernel.GetMock<ICustomerRepository>();
         repositoryMock.Setup(r => r.GetAll());
         var sut = _kernel.Get<MainViewModel>();
         var customers = sut.Customers;
         repositoryMock.VerifyAll();
      }
   }
}