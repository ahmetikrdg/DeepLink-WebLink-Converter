using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using LinkConverter.Business.Abstract;
using LinkConverter.Business.Concrate;
using LinkConverter.Business.ConvertingMethods.DeepUrlConvertToWeburl;
using LinkConverter.Business.ConvertingMethods.WeburlConvertToDeeplink;
using LinkConverter.Core.Utilities.Interceptors;
using LinkConverter.Data.Abstract;
using LinkConverter.Data.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Business.DependencyResolvers
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConvertDeepManager>().As<IConverterDeepServices>().SingleInstance();
            builder.RegisterType<ConverterDeepRepository>().As<IConverterDeepRepository>().SingleInstance();

            builder.RegisterType<ConvertWebUrlManager>().As<IConverterWebUrlServices>().SingleInstance();
            builder.RegisterType<ConverterWebUrlRepository>().As<IConverterWebUrlRepository>().SingleInstance();

            builder.RegisterType<WebUrlToDeeplinkConverter>().As<IWebUrlToDeeplinkConvert>().SingleInstance();
            builder.RegisterType<DeeplinkConvertToWebUrl>().As<IDeeplinkConvertToWebUrl>().SingleInstance();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();       

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()           
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
