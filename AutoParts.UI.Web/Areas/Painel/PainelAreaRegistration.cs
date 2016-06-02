﻿using System.Web.Mvc;

namespace AutoParts.UI.Web.Areas.Painel
{
    public class PainelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Painel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Painel_default",
                "Painel/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "AutoParts.UI.Web.Areas.Painel.Controllers" }
            );
        }
    }
}