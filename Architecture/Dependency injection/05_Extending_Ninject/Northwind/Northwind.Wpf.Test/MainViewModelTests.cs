using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using NUnit.Framework;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.MockingKernel;
using Ninject.MockingKernel.Moq;
using Northwind.Domain;
using Northwind.Wpf.Infrastructure;
using Northwind.Wpf.ViewModels;

namespace Northwind.Wpf.Test
{
    [TestFixture]
    class MainViewModelTests
    {
        private readonly MoqMockingKernel kernel;

        public MainViewModelTests()
        {
            this.kernel = new MoqMockingKernel();
            kernel.Bind<ICommandFactory>().ToFactory();
            kernel.Bind<IViewFactory>().ToFactory();
            kernel.Bind<ICommand>().To<ActionCommand>();
        }

        [TearDown]
        public void TearDown()
        {
            kernel.Reset();
        }


        [Test]
        public void GettingCustomersCallsRepositoryGetAll()
        {
            var repositoryMock = kernel.GetMock<ICustomerRepository>();
            repositoryMock.Setup(r => r.GetAll());

            var sut = kernel.Get<MainViewModel>();

            var customers = sut.Customers;

            repositoryMock.VerifyAll();
        }

        [Test]
        public void ExecutingCreateCustomerCommandShowsCustometView()
        {
            var customerViewMock = kernel.GetMock<ICustomerView>();
            customerViewMock.Setup(v => v.ShowDialog());

            var sut = kernel.Get<MainViewModel>();

            sut.CreateCustomerCommand.Execute(null);

            customerViewMock.VerifyAll();
        }

    }
}
