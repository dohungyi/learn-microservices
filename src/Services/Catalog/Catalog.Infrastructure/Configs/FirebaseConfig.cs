using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

namespace Catalog.Infrastructure.Configs;

public class FirebaseConfig
{
    public static string ApiKey { get; private set; }
    public static string AuthDomain { get; private set; }
    public static string ProjectId { get; private set; }
    public static string StorageBucket { get; private set; }
    public static string MessagingSenderId { get; private set; }
    public static string AppId { get; private set; }
    public static string MeasurementId { get; private set; }
    public static string Root { get; private set; }
    
    public static void SetConfig(IConfiguration configuration)
    {
        ApiKey = configuration.GetRequiredSection("FirebaseConfig:ApiKey").Value;
        AuthDomain = configuration.GetRequiredSection("FirebaseConfig:AuthDomain").Value;
        ProjectId = configuration.GetRequiredSection("FirebaseConfig:ProjectId").Value;
        StorageBucket = configuration.GetRequiredSection("FirebaseConfig:StorageBucket").Value;
        MessagingSenderId = configuration.GetRequiredSection("FirebaseConfig:MessagingSenderId").Value;
        AppId = configuration.GetRequiredSection("FirebaseConfig:AppId").Value;
        MeasurementId = configuration.GetRequiredSection("FirebaseConfig:MeasurementId").Value;
        Root = configuration.GetRequiredSection("FirebaseConfig:Root").Value;
    }
    
    public static string FirebaseConfigToJson() =>  Newtonsoft.Json.JsonConvert.SerializeObject(new FirebaseConfig());
}