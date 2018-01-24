namespace Library.Web
{
    public static class Settings
    {
        public static string GetConnectionString()
        {
            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            System.Configuration.ConnectionStringSettings connString;
            if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
            {
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings["PublicationsContext"];
                if (null != connString)
                    return connString.ConnectionString;
            }
            return "";
        }
    }
}