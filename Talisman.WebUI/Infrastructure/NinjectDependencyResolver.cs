//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using System.Web;
////using Moq;
////using Ninject;
//using Talisman.Domain.Abstract;
//using Talisman.Domain.Entities;
//using Talisman.Domain.Concrete;
//using Talisman.WebUI.Infrastructure.Abstract;
//using Talisman.WebUI.Infrastructure.Concreet;

//namespace Talisman.WebUI.Infrastructure
//{
//    public class NinjectDependencyResolver : IDependencyResolver
//    {
//        private IKernel kernel;
//        public NinjectDependencyResolver(IKernel kernelParam)
//        {
//            kernel = kernelParam;
//            AddBindings();
//        }
//        public object GetService(Type serviceType)
//        {
//            return kernel.TryGet(serviceType);
//        }
//        public IEnumerable<object> GetServices(Type serviceType)
//        {
//            return kernel.GetAll(serviceType);
//        }
//        private void AddBindings()
//        {
//            //Mock<IRepository> mock = new Mock<IRepository>();
//            //mock.Setup(m => m.Categories).Returns(new List<Categorie> {
//            //new Categorie { CategoryName="Шкафы-купе" },
//            //new Categorie { CategoryName="Кухни" },
//            //new Categorie { CategoryName="Прихожие" }
//            //});
//            //kernel.Bind<IRepository>().ToConstant(mock.Object);
//            //kernel.Bind<IRepository>().To<Talisman.Domain.Concrete.Repository2>();
//            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
//            //kernel.Bind<IGlobalData>().To<GData>();
//        }
//    }
//}