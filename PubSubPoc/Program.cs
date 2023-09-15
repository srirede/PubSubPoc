using Google.Cloud.PubSub.V1;
using log4net.Config;
using QueuingService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Configure log4net
builder.Logging.AddLog4Net();
XmlConfigurator.Configure(new FileInfo("log4net.config"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Invoke pubsub
TopicName topicName = TopicName.FromProjectTopic("iconic-hue-399103", "DeliverySystem");
builder.Services.AddPublisherClient(builder =>
{
    builder.TopicName = topicName;
    builder.CredentialsPath = "C:\\Google\\iconic-hue-399103-8c1f32521df9.json";
});

builder.Services.AddSingleton<IPubSubQueuing, PubSubQueuing>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
