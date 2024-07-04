using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.CrossCuttingCorcerns.Logging.SeriLog;

using Core.CrossCuttingCorcerns.Logging;

//using Core.Utilities.Helper.FileHelper;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.JWT;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookManager>().As<IBookService>().SingleInstance();//=/builder.Services.AddSingleton<IBookService,BookManager>();
            builder.RegisterType<EfBookDal>().As<IBookDal>().SingleInstance();

            builder.RegisterType<ShareManager>().As<IShareService>().SingleInstance();
            builder.RegisterType<EfShareDal>().As<IShareDal>().SingleInstance();

         
            builder.RegisterType<NoteManager>().As<INoteService>().SingleInstance();
            builder.RegisterType<EfNoteDal>().As<INoteDal>().SingleInstance();

           

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<CustomerManager>().As<ICustomerService>();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();

            builder.RegisterType<ShelfManager>().As<IShelfService>();
            builder.RegisterType<EfShelfDal>().As<IShelfDal>();

            builder.RegisterType<FriendShipManager>().As<IFriendShipService>();
            builder.RegisterType<EfFriendShipDal>().As<IFriendShipDal>();


            builder.RegisterType<SerilogLoggerService>().As<ILoggerService>().SingleInstance();

            // builder.RegisterType<ShareManager>().As<DataContext>();


            // builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
