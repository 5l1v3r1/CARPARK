using CARPARK.Data.Model;
using CARPARK.Data.Repository;
using CARPARK.Data.UnitofWork;
using CARPARK.Service.Interfaces;
using CARPARK.Service.Services;
using System;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

namespace CARPARK.IOC
{
    //Unity.MVC5 Dosyasý Nuget'den kurulumu yapýldý.
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            RegisterTypes(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static void RegisterTypes(UnityContainer container)
        {
            //Bu kýsýmda oluþturulan Sýnýf,Service tanýmlanarak birden fazla kez nesne üretmenin önüne geçiþmektedir.Burada her sýnýf ve service tanýmlamasý yapýlarak birden fazla nesne üretmenin önüne geçilmektedir.
            container.BindInRequestScope<IUnitofWork, UnitofWork>();
            container.BindInRequestScope<ILoginService, LoginService>();
            container.BindInRequestScope<IStaffService, StaffService>();
            container.BindInRequestScope<IUserService, UserService>();
            container.BindInRequestScope<IAyarlarService, AyarService>();
            container.BindInRequestScope<ICarService, CarService>();
            container.BindInRequestScope<IMoneyEntryService, MoneyEntryService>();
            container.BindInRequestScope<ICustomerService, CustomerService>();
            container.BindInRequestScope<ISubscriberService, SubscriberService>();
            container.BindInRequestScope<IRepository<Uye>, Repository<Uye>>();
            container.BindInRequestScope<IRepository<Personel>, Repository<Personel>>();
            container.BindInRequestScope<IRepository<Abone>, Repository<Abone>>();
            container.BindInRequestScope<IRepository<AboneGiriCikis>, Repository<AboneGiriCikis>>();
            container.BindInRequestScope<IRepository<Ayarlar>, Repository<Ayarlar>>();
            container.BindInRequestScope<IRepository<Arac>, Repository<Arac>>();
            container.BindInRequestScope<IRepository<Musteri>, Repository<Musteri>>();
            container.BindInRequestScope<IRepository<MusteriPark>, Repository<MusteriPark>>();
            container.BindInRequestScope<IRepository<MusteriYikama>, Repository<MusteriYikama>>();
            container.BindInRequestScope<IRepository<Gelirler>, Repository<Gelirler>>();
        }
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }
    }
}