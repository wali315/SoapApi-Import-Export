using Microsoft.IdentityModel.Protocols;
using System;
using System.Configuration;
using System.IO;

public class MyConfiguration
{
    public string LogPath { get; private set; }
    public string ApiUrl { get; private set; }

    public MyConfiguration()
    {
        LogPath = ConfigurationManager.AppSettings["LogFilePath"];
        ApiUrl = ConfigurationManager.AppSettings["ApiUrl"];
    }

    public string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
    }
    public string GetLogPath()
    {
        return LogPath;
    }
    public string GetApiUrl()
    {
        return ApiUrl;
    }
}