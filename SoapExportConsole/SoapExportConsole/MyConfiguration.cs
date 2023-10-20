using System;
using System.Configuration;
using System.IO;

public class MyConfiguration
{
    public string LogPath { get; private set; }
    public string ApiUrl { get; private set; }
    public string FilePath { get; private set; }

    public MyConfiguration()
    {
        LogPath = ConfigurationManager.AppSettings["LogFilePath"];
        FilePath = ConfigurationManager.AppSettings["CsvFilePath"];
        ApiUrl = ConfigurationManager.AppSettings["ApiUrl"];
    }

    public string GetCsvFilePath()
    {
        return FilePath;
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