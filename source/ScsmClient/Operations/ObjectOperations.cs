﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EnterpriseManagement.Common;
using Microsoft.EnterpriseManagement.Configuration;
using ScsmClient.ExtensionMethods;
using ScsmClient.Model;

namespace ScsmClient.Operations
{
    public class ObjectOperations: BaseOperation
    {
        public ObjectOperations(SCSMClient client) : base(client)
        {
        }

        public IEnumerable<EnterpriseManagementObjectDto> GetObject(string className, string criteria, int? maxResult = null)
        {

            var objectClass = _client.Class().GetClassByName(className);
            var crit = _client.Criteria().BuildObjectCriteria(criteria, objectClass);


           

            var critOptions = new ObjectQueryOptions();
            critOptions.DefaultPropertyRetrievalBehavior = ObjectPropertyRetrievalBehavior.All;
            critOptions.ObjectRetrievalMode = ObjectRetrievalOptions.NonBuffered;
            critOptions.MaxResultCount = maxResult ?? Int32.MaxValue;


            var reader = _client.ManagementGroup.EntityObjects.GetObjectReader<EnterpriseManagementObject>(crit, critOptions);
            
            var count = 0;
            
            foreach (EnterpriseManagementObject enterpriseManagementObject in reader)
            {
                if (count == critOptions.MaxResultCount)
                    break;
                yield return enterpriseManagementObject.ToObjectDto();
            }

        }

        public ManagementPackClass GetClass(ManagementPackClassCriteria criteria)
        {
            return _client.ManagementGroup.EntityTypes.GetClasses(criteria).FirstOrDefault();
        }

    }
}
