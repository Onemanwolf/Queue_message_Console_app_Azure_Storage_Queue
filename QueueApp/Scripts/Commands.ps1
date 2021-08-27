dotnet new console -n QueueApp
cd QueueApp
dotnet build
dotnet add package Azure.Storage.Queues
setx AZURE_STORAGE_CONNECTION_STRING "DefaultEndpointsProtocol=https;AccountName=jsqueue01js;AccountKey=OxUkv05NPdBwNy3C57jtk9rWFLR4matEW+YRQhGHmZpT675A9NFbp/bwP3TQvhkk/ThB11SMXAzawOsfQkpecg==;EndpointSuffix=core.windows.net"