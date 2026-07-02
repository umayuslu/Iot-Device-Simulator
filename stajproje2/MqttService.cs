using MQTTnet;

namespace stajproje2.Services
{
    public class MqttService
    {
        private IMqttClient? _client;

        public async Task ConnectAsync(string host, int port, string username, string password)
        {
            var factory = new MqttClientFactory();
            _client = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(host, port)
                .WithCredentials(username, password)
                .Build();

            await _client.ConnectAsync(options);
        }

        public async Task PublishAsync(string topic, string payload)
        {
            if (_client == null || !_client.IsConnected)
                throw new InvalidOperationException("MQTT istemcisi bağlı değil.");

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .Build();

            await _client.PublishAsync(message);
        }

        public async Task DisconnectAsync()
        {
            if (_client != null && _client.IsConnected)
                await _client.DisconnectAsync();
        }

        public bool IsConnected => _client?.IsConnected ?? false;
    }
}