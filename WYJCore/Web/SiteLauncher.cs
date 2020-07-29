using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace WYJCore.Web
{
    public class SiteLauncher
    {
        public SiteLauncher(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

    }
}
