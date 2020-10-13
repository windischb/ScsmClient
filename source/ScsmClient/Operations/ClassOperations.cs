﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EnterpriseManagement.Configuration;
using Reflectensions.ExtensionMethods;
using ScsmClient.Caches;

namespace ScsmClient.Operations
{
    public class ClassOperations: BaseOperation
    {
        private static readonly ManagementPackElementCache<ManagementPackClass> CachedClasses = new ManagementPackElementCache<ManagementPackClass>();
        
        public ClassOperations(SCSMClient client) : base(client)
        {
        }

        public ManagementPackClass GetClassById(string id)
        {
            return GetClassById(id.ToGuid());
        }
        public ManagementPackClass GetClassById(Guid id)
        {
            return _client.ManagementGroup.EntityTypes.GetClass(id);
        }

        public ManagementPackClass GetClassByName(string className)
        {
            return CachedClasses.GetOrAddByName(className, s =>
            {
                var parsed = ParseName(className);

                if (String.IsNullOrEmpty(parsed.ManagementPackName))
                {
                    var crit = new ManagementPackClassCriteria($"Name='{className}'");
                    return GetClassesByCriteria(crit).FirstOrDefault();
                }
                else
                {
                    var mp = _client.ManagementPack().GetManagementPackByName(parsed.ManagementPackName);
                    return GetClassByName(parsed.ClassName, mp);
                }
            });

        }

        public ManagementPackClass GetClassByName(string className, ManagementPack managementPack)
        {
            return CachedClasses.GetOrAddByName(className, s =>
            {
                var parsed = ParseName(className);
                return _client.ManagementGroup.EntityTypes.GetClass(parsed.ClassName, managementPack);
            });

        }


        public IList<ManagementPackClass> GetClassesByCriteria(ManagementPackClassCriteria criteria)
        {
            return _client.ManagementGroup.EntityTypes.GetClasses(criteria);
        }

        public ManagementPackRelationship GetRelationshipClassByName(string name)
        {
            var crit = new ManagementPackRelationshipCriteria($"Name='{name}'");
            return _client.ManagementGroup.EntityTypes.GetRelationshipClasses(crit).FirstOrDefault();
        }

        private (string ClassName, string ManagementPackName) ParseName(string name)
        {

            var regMatch = new Regex(@"(\[(?<managementPack>.+)\])?(?<className>.+)").Match(name);

            var managementPack = regMatch.Groups["managementPack"]?.Value;
            var className = regMatch.Groups["className"]?.Value;

            return (className, managementPack);
        }

    }
}
