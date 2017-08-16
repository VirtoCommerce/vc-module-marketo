using System;
using VirtoCommerce.Domain.Customer.Events;
using VirtoCommerce.Domain.Customer.Model;
using VirtoCommerce.Marketo.Data.Models;
using VirtoCommerce.Marketo.Data.Services;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Marketo.Data.Observers
{
    public class MemberObserver : IObserver<MemberChangingEvent>
    {
        public MemberObserver(MarketoService service)
        {
            Service = service;
        }
        public MarketoService Service { get; }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(MemberChangingEvent value)
        {
            if (value.ChangeState == EntryState.Added || value.ChangeState == EntryState.Modified)
            {
                var m = value.Member;
                var address = m.Addresses?.Count > 0 ? m.Addresses[0] : null;
                var email = m.Emails?.Count > 0 ? m.Emails[0] : string.Empty;

                if(m is Contact)
                {
                    var contact = m as Contact;

                    var request = new LeadsRequest
                    {
                        lookupField = "email",
                        input = new[] {
                            new Lead {
                                postalCode = address?.PostalCode,
                                email = email,
                                firstName = contact.FirstName,
                                lastName = contact.LastName,
                                leadScore = 10,
                                address = address?.Line1,
                                city = address?.City,
                                country = address?.CountryName,
                                phone = address?.Phone,
                                state = address?.RegionName
                            }
                        }
                    };

                    var result = Service.CreateOrUpdateLeads(request).Result;
                }
            }
        }
    }
}
