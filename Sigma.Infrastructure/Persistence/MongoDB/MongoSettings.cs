using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Infrastructure.Persistence
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
