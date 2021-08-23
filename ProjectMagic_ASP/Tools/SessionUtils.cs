using ProjectMagic_ASP.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjectMagic_ASP.Tools
{
    public static class SessionUtils
    {
        static IServiceProvider services = null;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services
        {
            private get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                services = value;
            }
        }

        /// <summary>
        /// Provides static access to the current HttpContext
        /// </summary>
        private static HttpContext Current
        {
            get
            {
                IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return httpContextAccessor?.HttpContext;
            }
        }

        //public static MeteoModel MaMeteo
        //{
        //    get { return Current.Session.Get<MeteoModel>("MaMeteo"); }
        //    set { Current.Session.Set<MeteoModel>("MaMeteo", value); }
        //}

        public static UserModel ConnectedUser
        {
            get { return Current.Session.Get<UserModel>("ConnectedUser"); }
            set { Current.Session.Set<UserModel>("ConnectedUser", value); }
        }

        public static bool IsLogged
        {
            get { return Current.Session.Get<bool>("IsLogged"); }
            set { Current.Session.Set<bool>("IsLogged", true); }
        }

        //public static int User
        //{
        //    get { return Current.Session.Get<int>("User"); }
        //    set { Current.Session.Set<int>("User", value); }
        //}
    }
}
