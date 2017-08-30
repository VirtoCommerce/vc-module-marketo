using Microsoft.Practices.Unity;
using System;
using VirtoCommerce.Domain.Customer.Events;
using VirtoCommerce.Marketo.Data.Observers;
using VirtoCommerce.Marketo.Data.Services;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.Marketo
{
    public class Module : ModuleBase
    {
        private readonly IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public override void Initialize()
        {
            base.Initialize();

            var settingsManager = _container.Resolve<ISettingsManager>();
       
            var connection = new MarketoConnectionInfo() {
                RestApiUrl = settingsManager.GetValue("VirtoCommerce.Marketo.General.APIUrl", String.Empty),
                ClientId = settingsManager.GetValue("VirtoCommerce.Marketo.General.ClientId", String.Empty),
                ClientSecret = settingsManager.GetValue("VirtoCommerce.Marketo.General.ClientSecret", String.Empty)
            };

            _container.RegisterType<MarketoService>(new InjectionConstructor(connection));

            // This method is called for each installed module on the first stage of initialization.
            _container.RegisterType<IObserver<MemberChangingEvent>, MemberObserver>("MemberObserver");
        }
    }
}