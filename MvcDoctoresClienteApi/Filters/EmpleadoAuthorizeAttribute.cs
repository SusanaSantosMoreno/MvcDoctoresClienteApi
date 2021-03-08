﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDoctoresClienteApi.Filters {
    public class EmpleadoAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter {
        public void OnAuthorization (AuthorizationFilterContext context) {
            //si no se ha validado, lo llevamos al login
            var user = context.HttpContext.User;
            if(user.Identity.IsAuthenticated == false) {
                RouteValueDictionary rutaLogin = new RouteValueDictionary(new {
                    controller = "Identity",
                    action = "Login"
                });
                context.Result = new RedirectToRouteResult(rutaLogin);
            }
        }
    }
}
