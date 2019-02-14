using System;
using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using com.createwebapi.data.Helper;
using com.createwebapi.data.Repositories;
using com.createwebapi.model;
using com.createwebapi.model.Repositores;

namespace com.createwebapi.ioc
{
    public class IOC
    {
        public static ContainerBuilder Builder;
        public static IContainer Container;

        public static IContainer Start()
        {
            Builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));

            var assemblyRepository = Assembly.GetAssembly(typeof(GenericRepository<>));
            Builder.RegisterAssemblyTypes(assemblyRepository).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
            var assemblyServices = Assembly.GetAssembly(typeof(AbstractService));
            Builder.RegisterAssemblyTypes(assemblyServices).Where(t => t.Name.EndsWith("Services")).AsImplementedInterfaces().EnableClassInterceptors().InterceptedBy(typeof(TransactionInterceptor));

            Builder.Register(s => new TransactionInterceptor());
            Container = Builder.Build();
            return Container;
        }
    }
}