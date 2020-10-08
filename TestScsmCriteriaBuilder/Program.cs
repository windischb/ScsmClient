﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EnterpriseManagement.Common;
using Microsoft.EnterpriseManagement.Configuration;
using ScsmClient;
using ScsmClient.CriteriaParser;
using ScsmClient.CriteriaParser.Syntax;

namespace TestScsmCriteriaBuilder
{
    class Program
    {

        static void Main(string[] args)
        {
            //Main1(args);

            var creds = new NetworkCredential("LANFL\\administrator", "ABC12abc");
            var scsmClient = new SCSMClient("192.168.75.20", creds);

            //var filterobj = "zOKZ != null && zName like '%Polizei%'";

            //var okzs = scsmClient.Object().GetObjects("zOrganisationseinheit", filterobj).ToList();

            //var filter1 = "zFirstname == Bernhard && zLastname == Windisch";


            //var objs = scsmClient.TypeProjection().GetObjectProjectionObjects("zTP_zBenutzer_zAccount", filter1).ToList();


            var filter2 = "G:Id == '3cd8c933-995e-2e21-f145-43a3c06fd585'";
            var filter3 = "G:LastModified > '19.03.2020 9:46' && G:LastModified < '19.03.2020 10:00'";
            var filter4 = "G:LastModified == '19.03.2020 9:46:14.133'";
            var filter5 = "G:LastModified -gt '19.03.2020 9:46:14'";

            var objs2 = scsmClient.TypeProjection().GetObjectProjectionObjects("zTP_zBenutzer_zAccount", filter3).ToList();

        }



        static void Main1(string[] args)
        {

            var creds = new NetworkCredential("LANFL\\administrator", "ABC12abc");
            var scsmClient = new SCSMClient("192.168.75.20", creds);

            //var incidentClass = scsmClient.Class().GetClassByName("zOrganisationseinheit");

            var filter1 = "G:Id == 'd8e70ac7-3a63-8e80-5fca-4ebf6ab682de'";
            var filter2 = "Id == 658";
            var filter3 = "Workitem.Id == '658'";
            var filter4 = "System.WorkItem.TroubleTicket.Id == '658'";
            var filter5 = "Id == '658'";
            var filterByGenericId = "Id == 651";
            //var filter2 = "zOKZ like '%BWI%'";



            //var syntaxTree = SyntaxTree.Parse(filter);
            //var compilation = new Compilation(syntaxTree, scsmClient);
            //var result = compilation.Evaluate(incidentClass);
            //var resultValue =  result.Value;


            //var crit = resultValue.ToString();


            //ManagementPackTypeProjectionCriteria projectionSelectionCriteria = new ManagementPackTypeProjectionCriteria("Name = 'System.WorkItem.Incident.View.ProjectionType'");
            //var tp = scsmClient.TypeProjection().GetTypeProjection(WellKnown.Incident.ProjectionType);
            //ObjectProjectionCriteria objProjectionCriteria = new ObjectProjectionCriteria(crit, tp, scsmClient.ManagementGroup);


            var searchbyidcrit = @"<Criteria xmlns=""http://Microsoft.EnterpriseManagement.Core.Criteria/"">
                <Expression>
    <SimpleExpression>
      <ValueExpressionLeft>
        <GenericProperty>Id</GenericProperty>
      </ValueExpressionLeft>
      <Operator>Equal</Operator>
      <ValueExpressionRight>
        <Value>d8e70ac7-3a63-8e80-5fca-4ebf6ab682de</Value>
      </ValueExpressionRight>
    </SimpleExpression>
  </Expression>
</Criteria>";


            var obj = scsmClient.Object().GetObjectById(Guid.Parse("d8e70ac7-3a63-8e80-5fca-4ebf6ab682de"));
            var inc = scsmClient.Incident().GetByCriteria(filter4).ToList().FirstOrDefault();

            //var found = scsmClient.TypeProjection().GetObjectProjectionReader(objProjectionCriteria, ObjectQueryOptions.Default);

            var incdisp = inc.DisplayName;

        }
    }
}